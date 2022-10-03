﻿// ServiceRestrictionOrViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models;

public class ServiceRestrictionOrViewModel : CoreResrepModelBase
{
  public ServiceRestrictionOrViewModel()
  {
    itemXnam = PdpAppConst.ServiceRestrictionOrItemXnam;
  }

  public Guid? RestrictionAndGuid { get; set; } = Guid.Empty;
  // public string? RestrictionAndGuidStr { get { return RestrictionAndGuid.ToPdpGuidString(); }  }
  public short RestrictionAndHasIndex { get; set; } = 0;
  public short RestrictionAndHasPriority { get; set; } = 0;
  public string? RestrictionName { get; set; } = string.Empty;

  public Guid? RestrictionOrGuid { get; set; } = Guid.Empty;
  //  public string? RestrictionOrGuidStr { get { return RestrictionOrGuid.ToPdpGuidString(); } }
  public short RestrictionOrHasIndex { get; set; } = 0;
  public short RestrictionOrHasPriority { get; set; } = 0;
  public string? RestrictionValue { get; set; } = string.Empty;

  // do not prefix with Restriction // available in CoreResrepModelBase
  //public bool IsWordPhrase { get; set; } = false;
  //public bool IsConceptLabel { get; set; } = false;

} // end class

// end file