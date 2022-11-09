// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class QebHtmlHelperExtensions
{
  public static string Truncate(this HtmlHelper helper, string input, int length)
  {
    if (input.Length <= length) { return input; }
    else { return input.Substring(0, length) + "..."; }
  }

}
