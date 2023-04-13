// OtherTextViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class OtherTextViewModel : CoreResrepModelBase
{
  public OtherTextViewModel()
  {
    itemXnam = PdpAppConst.OtherTextItemXnam;
  }

  public string? OtherText { get; set; } = string.Empty;



  private string ssOtxt128 = string.Empty;
  public string OtherText128
  {
    get {
      if (string.IsNullOrEmpty(OtherText)) { ssOtxt128 = string.Empty; }
      else { ssOtxt128 = OtherText.ToHoverHideHtml(128).StringEscapeHashLiteral(); }
      return ssOtxt128;
    }
  }

  private string ssOtxtHtml = string.Empty;
  public string OtherTextHtml
  {
    get {
      if (string.IsNullOrEmpty(OtherText)) { ssOtxtHtml = string.Empty; }
      else { ssOtxtHtml = OtherText.StringEscapeHashLiteral(); }
      return ssOtxtHtml;
    }
  }

} // end class

// end file