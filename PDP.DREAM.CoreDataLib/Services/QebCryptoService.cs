// QebCryptoService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;

namespace PDP.DREAM.CoreDataLib.Services;

public static class QebCryptoService
{
  public const int QebSaltOffset = 1;
  public const int QebSaltSize = 16;
  public const int QebTokenOffset = QebSaltOffset + QebSaltSize;
  public const int QebTokenSize = 16;
  public const int QebRfcBufferSize = QebTokenOffset + QebTokenSize;
  public const int QebHashIter = 1024;
  public static HashAlgorithmName QebHashName = HashAlgorithmName.SHA512;
  public enum TokenVerifyResult
  {
    Failed = 0,
    Succeeded = 1,
    RehashNeeded = 2
  }
  public static string HashToken(string token)
  {
    if (string.IsNullOrEmpty(token)) { throw new ArgumentNullException(nameof(token)); }
    byte[] salt;
    byte[] hash;
    using (var rfcBytes = new Rfc2898DeriveBytes(token, QebSaltSize, QebHashIter, QebHashName))
    {
      salt = rfcBytes.Salt;
      hash = rfcBytes.GetBytes(QebTokenSize);
    }
    byte[] dstArray = new byte[QebRfcBufferSize];
    Buffer.BlockCopy(salt, 0, dstArray, QebSaltOffset, QebSaltSize);
    Buffer.BlockCopy(hash, 0, dstArray, QebTokenOffset, QebTokenSize);
    return Convert.ToBase64String(dstArray);
  }
  public static bool VerifyHashedToken(string hashedToken, string plainToken)
  {
    if (string.IsNullOrEmpty(plainToken)) { throw new ArgumentNullException(nameof(plainToken)); }
    if (hashedToken == null) { return false; }
    byte[] srcArray = Convert.FromBase64String(hashedToken);
    if ((srcArray.Length != QebRfcBufferSize) || (srcArray[0] != 0)) { return false; }
    byte[] salt1 = new byte[QebSaltSize];
    byte[] hash1 = new byte[QebTokenSize];
    Buffer.BlockCopy(srcArray, QebSaltOffset, salt1, 0, QebSaltSize);
    Buffer.BlockCopy(srcArray, QebTokenOffset, hash1, 0, QebTokenSize);
    byte[] salt2;
    byte[] hash2;
    using (var rfcBytes = new Rfc2898DeriveBytes(plainToken, salt1, QebHashIter, QebHashName))
    {
      salt2 = rfcBytes.Salt;
      hash2 = rfcBytes.GetBytes(QebTokenSize);
    }
    var sameSalt = ByteArrayCompare(salt1, salt2);
    var sameHash = ByteArrayCompare(hash1, hash2);
    return (sameSalt && sameHash);
  }

  private static bool ByteArrayCompare(byte[] array1, byte[] array2)
  {
    return StructuralComparisons.StructuralEqualityComparer.Equals(array1, array2);
  }

  public static bool TokenEqualsHash(string plainToken, string hashedToken)
  {
    bool valid;
    // valid = string.Equals(HashToken(token), hash, StringComparison.Ordinal);
    valid = VerifyHashedToken(hashedToken, plainToken);
    return valid;
  }
  public static bool TokenEqualsToken(string tokenA, string tokenB)
  {
    bool valid;
    valid = string.Equals(tokenA, tokenB, StringComparison.Ordinal);
    return valid;
  }

  public static string GenerateToken()
  {
    var prng = RandomNumberGenerator.Create();
    var token = GenerateToken(prng);
    return token;
  }
  internal static string GenerateToken(RandomNumberGenerator generator)
  {
    string token;
    byte[] tokenBytes = new byte[QebTokenSize];
    generator.GetBytes(tokenBytes);
    token = string.Concat(Array.ConvertAll(tokenBytes, x => x.ToString("X2")));
    token = WebUtility.UrlEncode(token);
    return token;
  }

} // end class

public class QebIdentityResult
{
  public QebIdentityResult() { }

  public List<QebIdentityError> Errors { get; set; }
  public bool Failed { get; set; }
  public bool LockedOut { get; set; }
  public bool NotAllowed { get; set; }
  public bool TwoFactorRequired { get; set; }
  public bool Succeeded { get; set; }

} // end class

public class QebIdentityError
{
  public QebIdentityError() { }
  public QebIdentityError(Exception exc)
  {
    Data = exc.Data;
    Message = exc.Message;
    Source = exc.Source;
    StackTrace = exc.StackTrace;
  }
  public IDictionary Data { get; set; }
  public string Message { get; set; }
  public string Source { get; set; }
  public string StackTrace { get; set; }

} // end class

// end file