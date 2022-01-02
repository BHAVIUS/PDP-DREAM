// PdpGuid.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Types
{
  public static class PdpGuid
  {
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

    public static Guid Parse(string strGuid)
    {
      Guid theGuid;
      if (string.IsNullOrEmpty(strGuid))
      {
        theGuid = new Guid(PdpGuid.GuidNullString);
      }
      else
      {
        try
        {
          theGuid = new Guid(strGuid.Trim());
        }
        catch
        {
          theGuid = new Guid(PdpGuid.GuidNullString);
        }
      }
      return theGuid;
    }

    public static Guid? Parse(string strGuid, Guid defaultValue)
    {
      Guid? theGuid;
      try
      {
        theGuid = new Guid(strGuid.Trim());
      }
      catch
      {
        theGuid = defaultValue;
      }
      return theGuid;
    }

    public static Guid? ParseToNullable(Guid? nullableValue, Guid defaultValue)
    {
      var theGuid = (Guid?)(((nullableValue == null) || (nullableValue == Guid.Empty)) ? defaultValue : nullableValue);
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

  }

}
