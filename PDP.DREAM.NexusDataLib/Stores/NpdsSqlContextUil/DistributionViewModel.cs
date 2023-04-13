// DistributionViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class DistributionViewModel : CoreResrepModelBase
{
  public DistributionViewModel()
  {
    itemXnam = PdpAppConst.DistributionItemXnam;
  }

  public string? Distribution { get; set; } = string.Empty;

  private string ssDist128 = string.Empty;
  public string Distribution128
  {
    get {
      if (string.IsNullOrEmpty(Distribution)) { ssDist128 = string.Empty; }
      else { ssDist128 = Distribution.ToHoverHideHtml(128).StringEscapeHashLiteral(); }
      return ssDist128;
    }
  }

  private string ssDistHtml = string.Empty;
  public string DistributionHtml
  {
    get {
      if (string.IsNullOrEmpty(Distribution)) { ssDistHtml = string.Empty; }
      else { ssDistHtml = Distribution.StringEscapeHashLiteral(); }
      return ssDistHtml;
    }
  }

} // end class

// end file