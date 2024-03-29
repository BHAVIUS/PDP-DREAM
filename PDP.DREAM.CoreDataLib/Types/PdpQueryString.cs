﻿// PdpQueryString.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpQueryString
{
  // TODO: reconsider b/o Microsoft's QueryString that only
  //       parses key-value pairs rather than just keys without any values
  //
  // TODO: create ParseGuid for parsing guid keys
  // TODO: reconcile this ParseGuid with PdpGuid
  // TODO: reconcile all repos with Guids vs Guid?s vs PdpGuids

  public static bool ParseFlag(string key, string value)
  {
    bool flag = false;
    // if key present even without value then should be true
    if (!string.IsNullOrEmpty(key)) { flag = true; }
    // if key present and value present then
    // flag should be true if value non-zero
    //  => flag turned off only for "0" and "false"
    if (flag)
    {
      flag = ParseFlag(value);
    }
    return flag;
  }
  public static bool ParseFlag(string value)
  {
    // assume key present so initialize flag true for "on"
    bool flag = true;
    // test value only for "0" and "false" to set flag false for "off"
    if (!string.IsNullOrEmpty(value))
    {
      switch (value.ToLower())
      {
        case "true":
          break;
        case "false":
          flag = false;
          break;
        default:
          try
          {
            int val = Convert.ToInt32(value);
            if (val == 0) { flag = false; }
          }
          catch { };
          break;
      }
    }
    return flag;
  }

  public static int ParsePosInt(string value, int deflt)
  {
    int posInt;
    try
    {
      posInt = Convert.ToInt32(value);
      if (posInt < 1) { posInt = deflt; }
    }
    catch { posInt = deflt; }
    return posInt;
  }

} // end class

// end file