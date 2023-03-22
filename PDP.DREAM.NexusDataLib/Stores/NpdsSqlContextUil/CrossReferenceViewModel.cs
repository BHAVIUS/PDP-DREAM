// CrossReferenceViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class CrossReferenceViewModel : CoreResrepModelBase
{
  public CrossReferenceViewModel()
  {
    itemXnam = PdpAppConst.CrossReferenceItemXnam;
  }

  public string? CrossReference { get; set; } = string.Empty;

  private string ssCrefHtml = string.Empty;
  public string CrossReferenceHtml
  {
    get {
      if (string.IsNullOrEmpty(CrossReference)) { ssCrefHtml = string.Empty; }
      else { ssCrefHtml = CrossReference.StringEscapeHashLiteral(); }
      return ssCrefHtml;
    }
  }

  private string ssCref128 = string.Empty;
  public string CrossReference128
  {
    get {
      if (string.IsNullOrEmpty(CrossReference)) { ssCref128 = string.Empty; }
      else { ssCref128 = CrossReference.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssCref128;
    }
  }

} // end class

// end file