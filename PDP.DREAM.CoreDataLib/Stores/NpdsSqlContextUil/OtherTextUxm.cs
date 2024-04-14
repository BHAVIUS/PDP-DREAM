// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class OtherTextUxm : ResrepRootModelBase
{
  public OtherTextUxm()
  {
    itemXnam = PdpAppConst.OtherTextItemXnam;
  }

  public string? OtherText { get; set; } = ESS;

  private string ssOtxt128 = ESS;
  public string OtherText128
  {
    get {
      if (string.IsNullOrEmpty(OtherText)) { ssOtxt128 = ESS; }
      else { ssOtxt128 = OtherText.TruncateForTkgr(128).StringEscapeHashLiteral(); }
      return ssOtxt128;
    }
  }

  private string ssOtxtHtml = ESS;
  public string OtherTextHtml
  {
    get {
      if (string.IsNullOrEmpty(OtherText)) { ssOtxtHtml = ESS; }
      else { ssOtxtHtml = OtherText.StringEscapeHashLiteral(); }
      return ssOtxtHtml;
    }
  }

} // end class

// end file