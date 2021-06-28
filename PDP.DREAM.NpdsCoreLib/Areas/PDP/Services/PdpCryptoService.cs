using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;

namespace PDP.DREAM.NpdsCoreLib.Services
{
  public static class PdpCryptoService
  {
    public enum TokenVerifyResult
    {
      Failed = 0,
      Succeeded = 1,
      RehashNeeded = 2
    }
    public static string HashToken(string token)
    {
      byte[] salt;
      byte[] buffer2;
      if (token == null)
      {
        throw new ArgumentNullException("token");
      }
      using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(token, 0x10, 0x3e8))
      {
        salt = bytes.Salt;
        buffer2 = bytes.GetBytes(0x20);
      }
      byte[] dst = new byte[0x31];
      Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
      Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
      return Convert.ToBase64String(dst);
    }
    public static bool VerifyHashedToken(string hashedToken, string token)
    {
      byte[] buffer4;
      if (hashedToken == null)
      {
        return false;
      }
      if (token == null)
      {
        throw new ArgumentNullException("password");
      }
      byte[] src = Convert.FromBase64String(hashedToken);
      if ((src.Length != 0x31) || (src[0] != 0))
      {
        return false;
      }
      byte[] dst = new byte[0x10];
      Buffer.BlockCopy(src, 1, dst, 0, 0x10);
      byte[] buffer3 = new byte[0x20];
      Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
      using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(token, dst, 0x3e8))
      {
        buffer4 = bytes.GetBytes(0x20);
      }
      return ByteArrayCompare(buffer3, buffer4);
    }

    private static bool ByteArrayCompare(byte[] a1, byte[] a2)
    {
      return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
    }

    public static bool TokenEqualsHash(string token, string hash)
    {
      bool valid;
      valid = string.Equals(HashToken(token), hash, StringComparison.Ordinal);
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
      byte[] tokenBytes = new byte[TokenSizeInBytes];
      generator.GetBytes(tokenBytes);
      token = string.Concat(Array.ConvertAll(tokenBytes, x => x.ToString("X2")));
      token = WebUtility.UrlEncode(token);
      return token;
    }
    private const int TokenSizeInBytes = 16;

  } // class

  public class PdpIdentityResult
  {
    public PdpIdentityResult() { }

    public List<PdpIdentityError> Errors { get; set; }
    public bool Failed { get; set; }
    public bool LockedOut { get; set; }
    public bool NotAllowed { get; set; }
    public bool TwoFactorRequired { get; set; }
    public bool Succeeded { get; set; }

  } // class

  public class PdpIdentityError
  {
    public PdpIdentityError() { }
    public PdpIdentityError(Exception exc)
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

  }

}