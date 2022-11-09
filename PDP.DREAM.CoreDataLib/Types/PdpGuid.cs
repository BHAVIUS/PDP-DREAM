// PdpGuid.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpGuid
{
  public static string ToPdpGuidString(this Guid? theGuid)
  {
    string theString;
    if (theGuid.IsInvalid()) { theString = Guid.Empty.ToString(); }
    else { theString = theGuid.ToString(); }
    return theString;
  }

  public static bool IsEmpty(this Guid theGuid)
  {
    var isEmptyGuid = (theGuid == Guid.Empty);
    return isEmptyGuid;
  }
  public static bool IsInvalid(this Guid theGuid)
  {
    var isInvalidGuid = IsInvalidGuid(theGuid);
    return isInvalidGuid;
  }
  public static bool IsNullOrEmpty(this Guid? theGuid)
  {
    var isNullOrEmptyGuid = (!theGuid.HasValue || theGuid.Value == Guid.Empty);
    return isNullOrEmptyGuid;
  }
  public static bool IsInvalid(this Guid? theGuid)
  {
    return (!theGuid.HasValue || theGuid.Value == Guid.Empty || IsInvalidGuid(theGuid));
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
      theGuid = new Guid(GuidNullString); // invalid so theGuid set to null guid
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
  public static Guid? ParseToNullable(string strGuid, Guid defaultValue)
  {
    Guid? theGuid = ParseToNullable(new Guid(strGuid.Trim()), defaultValue);
    return theGuid;
  }
  public static Guid? ParseToNullable(Guid? nullableValue, Guid defaultValue)
  {
    var theGuid = (Guid?)(((nullableValue == null) || (nullableValue == Guid.Empty)) ? defaultValue : nullableValue);
    return theGuid;
  }

  // parse to non-nullable guid
  public static Guid ParseToNonNullable(string strGuid)
  {
    Guid theGuid = ParseToNonNullable(new Guid(strGuid.Trim()), new Guid(PdpGuid.GuidNullString));
    return theGuid;
  }
  public static Guid ParseToNonNullable(string strGuid, Guid defaultValue)
  {
    Guid theGuid = ParseToNonNullable(new Guid(strGuid.Trim()), defaultValue);
    return theGuid;
  }
  public static Guid ParseToNonNullable(Guid? nullableValue, Guid defaultValue)
  {
    var theGuid = (Guid)(((nullableValue == null) || (nullableValue == Guid.Empty)) ? defaultValue : nullableValue);
    return theGuid;
  }

  // PdpGuid wrapper for Microsoft NewGuid
  public static Guid NewGuid()
  {
    Guid theGuid;
    theGuid = Guid.NewGuid();
    return theGuid;
  }

} // end class

// end file