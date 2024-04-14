// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class FairMetricUxm : ResrepRootModelBase
{
  public FairMetricUxm()
  {
    itemXnam = PdpAppConst.FairMetricItemXnam;
  }

  public short MInvalidOldClaim { get; set; }
  public short QValidOldClaim { get; set; }
  public short PInvalidNewClaim { get; set; }
  public short NValidNewClaim { get; set; }

  public float FAIR1Q { get; set; }
  public float FAIR2M { get; set; }
  public float FAIR3P { get; set; }
  public float FAIR4N { get; set; }

} // end class

// end file