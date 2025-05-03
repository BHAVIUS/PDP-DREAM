// ProvenanceEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class ProvenanceEditModel : ProvenanceViewModel
{
  public ProvenanceEditModel()
  {
    itemXnam = PdpAppConst.ProvenanceItemXnam;
  }

  //public string? Provenance { get; set; } = string.Empty;

  //public string? ProvenanceHtml { get { return Provenance.StringEscapeHashLiteral(); } }

  //private string? ssprov128;
  //public string? Provenance128
  //{
  //  get {
  //    if (string.IsNullOrEmpty(Provenance)) { ssprov128 = string.Empty; }
  //    else { ssprov128 = Provenance.ToPartialPhrase(128).StringEscapeHashLiteral(); }
  //    return ssprov128;
  //  }
  //}

} // end class

// end file