using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class HtmlHelperExtensions
  {
    // example: @Html.Truncate(ViewBag.Message as string, 8)
    public static string Truncate(this HtmlHelper helper, string input, int length)
    {
      if (input.Length <= length) { return input; }
      else { return input.Substring(0, length) + "..."; }
    }

  }

}