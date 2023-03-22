// SupportingLabelViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class SupportingLabelViewModel : CoreResrepModelBase
{
  public SupportingLabelViewModel()
  {
    itemXnam = PdpAppConst.SupportingLabelItemXnam;
  }

  public string? SupportingLabel { get; set; } = string.Empty;

  private string ssSlabHtml = string.Empty;
  public string SupportingLabelHtml
  {
    get {
      if (string.IsNullOrEmpty(SupportingLabel)) { ssSlabHtml = string.Empty; }
      else { ssSlabHtml = SupportingLabel.StringEscapeHashLiteral(); }
      return ssSlabHtml;
    }
  }

  private string ssSlab128 = string.Empty;
  public string SupportingLabel128
  {
    get {
      if (string.IsNullOrEmpty(SupportingLabel)) { ssSlab128 = string.Empty; }
      else { ssSlab128 = SupportingLabel.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssSlab128;
    }
  }

} // end class

// end file