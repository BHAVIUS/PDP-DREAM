// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class DescriptionUxm : ResrepRootModelBase
{
  public DescriptionUxm()
  {
    itemXnam = PdpAppConst.DescriptionItemXnam;
  }

  private string? ssDesc = ESS;
  public string? Description
  {
    set {
      ssDesc = value;
      if (string.IsNullOrEmpty(ssDesc))
      {
        ssDescTkgr = ESS;
        ssDescJsam = ESS;
      }
      else
      {
        ssDescTkgr = ssDesc.TruncateForTkgr(MCGenLong);
        ssDescJsam = ssDesc.TruncateToJsam();
      }
    }
    get { return ssDesc; }
  }

  private string ssDescTkgr = ESS;
  public string DescriptionTkgr
  {
    get { return ssDescTkgr; }
  }

  private string ssDescJsam = ESS;
  public string DescriptionJsam
  {
    get { return ssDescJsam; }
  }

} // end class

// end file