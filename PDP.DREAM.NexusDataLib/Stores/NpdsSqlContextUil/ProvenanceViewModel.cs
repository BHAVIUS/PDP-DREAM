// ProvenanceViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Utilities.PdpStringPhraseFormFile;

namespace PDP.DREAM.NexusDataLib.Models;

public class ProvenanceViewModel : CoreResrepModelBase
{
  public ProvenanceViewModel()
  {
    itemXnam = PdpAppConst.ProvenanceItemXnam;
  }

  public string? Provenance { get; set; } = string.Empty;

  private string ssprov = string.Empty;
  public string ProvenanceHtml
  {
    get {
      if (string.IsNullOrEmpty(Provenance)) { ssprov = string.Empty; }
      else { ssprov = Provenance.StringEscapeHashLiteral(); }
      return ssprov;
    }
  }

  private string ssprov128 = string.Empty;
  public string Provenance128
  {
    get {
      if (string.IsNullOrEmpty(Provenance)) { ssprov128 = string.Empty; }
      else { ssprov128 = Provenance.ToTruncatedPhrase(128).StringEscapeHashLiteral(); }
      return ssprov128;
    }
  }

} // end class

// end file