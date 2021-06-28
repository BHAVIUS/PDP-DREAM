using System;

namespace PDP.DREAM.NpdsDataLib.Models
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