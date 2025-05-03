// ServiceRestrictionOrEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

using static PDP.DREAM.CoreDataLib.Types.PdpGuid;

namespace PDP.DREAM.ScribeDataLib.Models;

public class ServiceRestrictionOrEditModel : ServiceRestrictionOrViewModel
{
  public ServiceRestrictionOrEditModel()
  {
    itemXnam = PdpAppConst.ServiceRestrictionOrItemXnam;
  }
  //public Guid? RestrictionAndGuid { get; set; } = Guid.Empty;
  //public string? RestrictionAndGuidStr { get { return RestrictionAndGuid.ToPdpGuidString(); } }
  //public short RestrictionAndHasIndex { get; set; } = 0;
  //public short RestrictionAndHasPriority { get; set; } = 0;
  //public string? RestrictionName { get; set; } = string.Empty;

  //public Guid? RestrictionOrGuid { get; set; } = Guid.Empty;
  //public string? RestrictionOrGuidStr { get { return RestrictionOrGuid.ToPdpGuidString(); } }
  //public short RestrictionOrHasIndex { get; set; } = 0;
  //public short RestrictionOrHasPriority { get; set; } = 0;
  //public string? RestrictionValue { get; set; } = string.Empty;

  //public bool IsWordPhrase { get; set; } = false;
  //public bool IsConceptLabel { get; set; } = false;

} // end class

// end file