// ServiceRestrictionAndViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class ServiceRestrictionAndViewModel : CoreResrepModelBase
{
  public ServiceRestrictionAndViewModel()
  {
    itemXnam = PdpAppConst.ServiceRestrictionAndItemXnam;
  }

  public Guid? RestrictionAndGuid { get; set; } = Guid.Empty;
  public short RestrictionAndHasIndex { get; set; } = 0;
  public short RestrictionAndHasPriority { get; set; } = 0;
  public string? RestrictionName { get; set; } = string.Empty;

  // do not prefix with Restriction // available in CoreResrepModelBase
  // public bool IsExcluding { get; set; } = false;
  // public bool IsSufficient { get; set; } = false;

} // end class

// end file