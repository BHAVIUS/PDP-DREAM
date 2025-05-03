// INexusViewModelBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.NexusDataLib.Models;

public interface ICoreResrepViewModel
{
  // for PRC (PDP REST Context)
  Guid? AgentGuid { get; set; }

  string? PdpStatusElement { get; set; }
  string? PdpStatusMessage { get; set; }

  // independent parent table
  Guid? RRRecordGuid { get; set; }

  Guid? RRInfosetGuid { get; set; }

  // dependent child tables
  Guid? RRFgroupGuid { get; set; }

  short HasIndex { get; set; }
  short HasPriority { get; set; }
}

