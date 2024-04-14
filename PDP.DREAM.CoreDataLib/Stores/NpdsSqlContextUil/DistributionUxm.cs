// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class DistributionUxm : ResrepRootModelBase
{
  public DistributionUxm()
  {
    itemXnam = PdpAppConst.DistributionItemXnam;
  }

  private string? ssDist = ESS;
  public string? Distribution
  {
    set {
      ssDist = value;
      if (string.IsNullOrEmpty(ssDist))
      {
        ssDistTkgr = ESS;
        ssDistJsam = ESS;
      }
      else
      {
        ssDistTkgr = ssDist.TruncateForTkgr(MCGenLong);
        ssDistJsam = ssDist.TruncateToJsam();
      }
    }
    get { return ssDist; }
  }

  private string ssDistTkgr = ESS;
  public string DistributionTkgr
  {
    get { return ssDistTkgr; }
  }

  private string ssDistJsam = ESS;
  public string DistributionJsam
  {
    get { return ssDistJsam; }
  }

} // end class

// end file