// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ServiceRestrictionAndUxm : ResrepRootModelBase
{
  public ServiceRestrictionAndUxm()
  {
    itemXnam = PdpAppConst.ServiceRestrictionAndItemXnam;
  }

  public Guid? RestrictionAndGuid { get; set; } = EGS;
  public byte RestrictionAndHasIndex { get; set; } = 0;
  public byte RestrictionAndHasPriority { get; set; } = 0;
  public string? RestrictionName { get; set; } = ESS;

} // end class

// end file