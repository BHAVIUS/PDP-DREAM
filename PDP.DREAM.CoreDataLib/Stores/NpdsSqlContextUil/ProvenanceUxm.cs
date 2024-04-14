// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ProvenanceUxm : ResrepRootModelBase
{
  public ProvenanceUxm()
  {
    itemXnam = PdpAppConst.ProvenanceItemXnam;
  }

  private string? sProv = ESS;
  public string? Provenance
  {
    set {
      sProv = value;
      if (string.IsNullOrEmpty(sProv))
      {
        ssProvTkgr = ESS;
        ssProvJsam = ESS;
      }
      else
      {
        ssProvTkgr = sProv.TruncateForTkgr(MCGenLong);
        ssProvJsam = sProv.TruncateToJsam();
      }
    }
    get { return sProv; }
  }

  private string ssProvTkgr = ESS;
  public string ProvenanceTkgr
  {
    get { return ssProvTkgr; }
  }

  private string ssProvJsam = ESS;
  public string ProvenanceJsam
  {
    get { return ssProvJsam; }
  }

} // end class

// end file