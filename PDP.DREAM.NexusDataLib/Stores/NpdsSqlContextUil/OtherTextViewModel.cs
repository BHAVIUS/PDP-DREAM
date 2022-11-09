// OtherTextViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Utilities.PdpStringPhraseFormFile;

namespace PDP.DREAM.NexusDataLib.Models;

public class OtherTextViewModel : CoreResrepModelBase
{
  public OtherTextViewModel()
  {
    itemXnam = PdpAppConst.OtherTextItemXnam;
  }

  public string? OtherText { get; set; } = string.Empty;

  private string ssOtxtHtml = string.Empty;
  public string OtherTextHtml
  {
    get {
      if (string.IsNullOrEmpty(OtherText)) { ssOtxtHtml = string.Empty; }
      else { ssOtxtHtml = OtherText.StringEscapeHashLiteral(); }
      return ssOtxtHtml;
    }
  }

  private string ssOtxt128 = string.Empty;
  public string OtherText128
  {
    get {
      if (string.IsNullOrEmpty(OtherText)) { ssOtxt128 = string.Empty; }
      else { ssOtxt128 = OtherText.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssOtxt128;
    }
  }

} // end class

// end file