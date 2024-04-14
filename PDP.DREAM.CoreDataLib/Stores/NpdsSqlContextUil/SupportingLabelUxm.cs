// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class SupportingLabelUxm : ResrepRootModelBase
{
  public SupportingLabelUxm()
  {
    itemXnam = PdpAppConst.SupportingLabelItemXnam;
  }

  public string? SupportingLabel { get; set; } = ESS;

  private string ssSlabHtml = ESS;
  public string SupportingLabelHtml
  {
    get {
      if (string.IsNullOrEmpty(SupportingLabel)) { ssSlabHtml = ESS; }
      else { ssSlabHtml = SupportingLabel.StringEscapeHashLiteral(); }
      return ssSlabHtml;
    }
  }

  private string ssSlab128 = ESS;
  public string SupportingLabel128
  {
    get {
      if (string.IsNullOrEmpty(SupportingLabel)) { ssSlab128 = ESS; }
      else { ssSlab128 = SupportingLabel.TruncateForTkgr(128).StringEscapeHashLiteral(); }
      return ssSlab128;
    }
  }

} // end class

// end file