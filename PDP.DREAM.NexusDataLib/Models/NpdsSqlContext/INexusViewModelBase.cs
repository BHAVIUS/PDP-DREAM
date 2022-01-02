// INexusViewModelBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.NexusDataLib.Models
{
  public interface INexusViewModelBase
  {
    // for PRC (PDP REST Context)
    Guid? AgentGuid { get; set; }

    string? PdpStatusName { get; set; }
    string? PdpStatusMessage { get; set; }

    // independent parent table
    Guid? RRRecordGuid { get; set; }

    Guid? RRInfosetGuid { get; set; }

    // dependent child tables
    Guid? RRFgroupGuid { get; set; }

    byte HasIndex { get; set; }
    byte HasPriority { get; set; }
  }

}