// DescriptionViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Utilities.PdpStringPhraseFormFile;

namespace PDP.DREAM.NexusDataLib.Models;

public class DescriptionViewModel : CoreResrepModelBase
{
  public DescriptionViewModel()
  {
    itemXnam = PdpAppConst.DescriptionItemXnam;
  }

  public string? Description { get; set; } = string.Empty;

  private string ssDescHtml = string.Empty;
  public string DescriptionHtml
  {
    get {
      if (string.IsNullOrEmpty(Description)) { ssDescHtml = string.Empty; }
      else { ssDescHtml = Description.StringEscapeHashLiteral(); }
      return ssDescHtml;
    }
  }

  private string ssDesc128 = string.Empty;
  public string Description128
  {
    get {
      if (string.IsNullOrEmpty(Description)) { ssDesc128 = string.Empty; }
      else { ssDesc128 = Description.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssDesc128;
    }
  }

} // end class

// end file