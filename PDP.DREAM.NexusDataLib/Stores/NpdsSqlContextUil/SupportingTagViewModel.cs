// SupportingTagViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Utilities.PdpStringFrasFormFile;

namespace PDP.DREAM.NexusDataLib.Models;

public class SupportingTagViewModel : CoreResrepModelBase
{
  public SupportingTagViewModel()
  {
    itemXnam = PdpAppConst.SupportingTagItemXnam;
  }

  public string? SupportingTag { get; set; } = string.Empty;

  private string ssStagHtml = string.Empty;
  public string SupportingTagHtml
  {
    get {
      if (string.IsNullOrEmpty(SupportingTag)) { ssStagHtml = string.Empty; }
      else { ssStagHtml = SupportingTag.StringEscapeHashLiteral(); }
      return ssStagHtml;
    }
  }

  private string ssStag128 = string.Empty;
  public string SupportingTag128
  {
    get {
      if (string.IsNullOrEmpty(SupportingTag)) { ssStag128 = string.Empty; }
      else { ssStag128 = SupportingTag.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssStag128;
    }
  }

} // end class

// end file