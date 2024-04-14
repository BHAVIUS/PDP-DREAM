// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebString
{
  public static bool ParseToBool(string? str)
  {
    bool val = false;
    if (!String.IsNullOrEmpty(str)) { val = Boolean.Parse(str); };
    return val;
  }

  public static byte ParseToByte(string? str)
  {
    byte val = 0;
    if (!String.IsNullOrEmpty(str)) { val = Byte.Parse(str); };
    return val;
  }

  public static int ParseToInt(string? str)
  {
    int val = 0;
    if (!String.IsNullOrEmpty(str)) { val = Int32.Parse(str); };
    return val;
  }

  public static string IsRegexMatch(string? str, string? rgx)
  {
    var val = ESS;
    if ((str != null) && (rgx != null))
    {
      var mtch = Regex.Match(str, rgx);
      if (mtch.Success) { val = mtch.Value; }
    }
    return val;
  }

} // end class

// end file