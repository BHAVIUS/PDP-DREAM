// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Data.SqlTypes;
using System.Net.NetworkInformation;

namespace PDP.DREAM.CoreDataLib.Types;

// ATTN: NpdsTagGuid is instance while PdpGuid is static

public class NpdsTagGuid
{
  private Guid? npdsGuid;
  private string? npdsTag;
  private string? npdsTagGuidStr;
  private string[]? splitTGS = null;

  // ATTN: setting TagGuidStr resets both tag and guid
  public string? NpdsTagGuidStr
  {
    get { return npdsTagGuidStr; }
    set {
      npdsTagGuidStr = value; splitTGS = npdsTagGuidStr.Split(EqualSeparator);
      npdsTag = splitTGS[0]; npdsGuid = PdpGuid.ParseToNullable(splitTGS[1]);
    }
  }

  // ATTN: setting tag does not reset guid!!!
  public string? NpdsSrvcTag
  {
    get { return npdsTag; }
    set { npdsTag = value; }
  }

  // ATTN: setting guid does not reset tag!!! 
  public Guid? NpdsSrvcGuid
  {
    get { return npdsGuid; }
    set { npdsGuid = value; }
  }

}

public static class PdpGuid
{
  public static string ToString(this Guid? theGuid)
  {
    string theString;
    if (theGuid.IsInvalid()) { theString = EGS.ToString(); }
    else { theString = theGuid.ToString(); }
    return theString;
  }

  public static bool IsEmpty(this Guid theGuid)
  {
    var isEmptyGuid = (theGuid == EGS);
    return isEmptyGuid;
  }
  public static bool IsInvalid(this Guid theGuid)
  {
    var isInvalidGuid = IsInvalidGuid(theGuid);
    return isInvalidGuid;
  }
  public static bool IsNullOrEmpty(this Guid? theGuid)
  {
    var isNullOrEmptyGuid = (!theGuid.HasValue || theGuid.Value == EGS);
    return isNullOrEmptyGuid;
  }
  public static bool IsInvalid(this Guid? theGuid)
  {
    return (!theGuid.HasValue || theGuid.Value == EGS || IsInvalidGuid(theGuid));
  }

  // ATTN: a Const cannot be guid so must be string here
  public const string GuidNullString = "00000000-0000-0000-0000-000000000000";

  public static bool IsInvalidGuid(object? obj)
  {
    if (obj == null) { return true; }
    string? str = obj.ToString();
    if (string.IsNullOrEmpty(str)) { return true; }
    Guid theGuid;
    try
    {
      theGuid = new Guid(str); // valid guid in str but could be a null guid, ie, one with all zeros
    }
    catch
    {
      theGuid = new Guid(GuidNullString); // invalid so npdsGuid set to null guid
    }
    if (theGuid.ToString() == GuidNullString)
    {
      return true; // for any null or invalid guid
    }
    else
    {
      return false; // for all valid guids
    }
  }

  // parse to nullable guid
  public static Guid? ParseToNullable(string strGuid)
  {
    Guid? theGuid = null;
    theGuid = ParseToNullable(strGuid, EGS);
    return theGuid;
  }
  public static Guid? ParseToNullable(string strGuid, Guid defaultValue)
  {
    Guid? theGuid = null;
    if (string.IsNullOrEmpty(strGuid)) { theGuid = defaultValue; }
    else { theGuid = ParseToNullable(new Guid(strGuid.Trim()), defaultValue); }
    return theGuid;
  }
  public static Guid? ParseToNullable(Guid? nullableValue)
  {
    return ParseToNullable(nullableValue, EGS);
  }
  public static Guid? ParseToNullable(Guid? nullableValue, Guid defaultValue)
  {
    var theGuid = (Guid?)(((nullableValue == null) || (nullableValue == EGS)) ? defaultValue : nullableValue);
    return theGuid;
  }

  // parse to non-nullable guid
  public static Guid ParseToNonNullable(string strGuid)
  {
    Guid theGuid = ParseToNonNullable(new Guid(strGuid.Trim()), new Guid(PdpGuid.GuidNullString));
    return theGuid;
  }
  public static Guid ParseToNonNullable(string strGuid, Guid? defaultValue)
  {
    if (defaultValue == null) { defaultValue = EGS; }
    Guid theGuid = ParseToNonNullable(new Guid(strGuid.Trim()), defaultValue);
    return theGuid;
  }
  public static Guid ParseToNonNullable(Guid? nullableValue, Guid? defaultValue)
  {
    if (defaultValue == null) { defaultValue = EGS; }
    var theGuid = (Guid)(((nullableValue == null) || (nullableValue == EGS)) ? defaultValue : nullableValue);
    return theGuid;
  }

  // PdpNewGuid wrapper for Microsoft NewGuid
  public static Guid PdpNewGuid()
  {
    Guid theGuid;
    theGuid = Guid.NewGuid();
    return theGuid;
  }

} // end class

// end file