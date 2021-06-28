using System;

using PDP.DREAM.NpdsDataLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public abstract class NexusViewModelBase : INexusViewModelBase
  {
    // ItemXnam as string cannot be nullable if/when used as a key in a dictionary
    public string ItemXnam { get { return itemXnam; } }
    protected string itemXnam = string.Empty;

    // for PRC (PDP REST Context)
    public Guid? AgentGuid { get; set; } = Guid.Empty;
    public string? PdpStatusName { get; set; } = string.Empty;
    public string? PdpStatusMessage { get; set; } = string.Empty;

    // independent parent table
    public Guid? RRRecordGuid { get; set; } = Guid.Empty; // ResRep Record guid
    public Guid? RRInfosetGuid { get; set; } = Guid.Empty; // ResRep Infoset guid
    // dependent child tables
    public Guid? RRFgroupGuid { get; set; } = Guid.Empty; // ResRep Fgroup guid


    // resrep record attributes
    public byte HasIndex { get; set; } = 0;
    public byte HasPriority { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
    public bool IsLimited { get; set; } = false;
    public bool IsMarked { get; set; } = false;
    public bool IsPrincipal { get; set; } = false;
    public bool IsPrivate { get; set; } = false;
    public bool IsReleased { get; set; } = false;
    public bool IsShared { get; set; } = false;

    public Guid? ManagedByAgentGuid { get; set; } = Guid.Empty;
    public string? ManagedByAgentName { get; set; } = string.Empty;

    public DateTime? CreatedOn { get; set; } = null;
    public Guid? CreatedByAgentGuid { get; set; } = Guid.Empty;
    public string? CreatedByAgentName { get; set; } = string.Empty;


    public DateTime? UpdatedOn { get; set; } = null;
    public Guid? UpdatedByAgentGuid { get; set; } = Guid.Empty;
    public string? UpdatedByAgentName { get; set; } = string.Empty;

    public DateTime? DeletedOn { get; set; } = null;
    public Guid? DeletedByAgentGuid { get; set; } = Guid.Empty;
    public string? DeletedByAgentName { get; set; } = string.Empty;

    public int CountEntityLabels { get; set; } = 0;
    public int CountSupportingTags { get; set; } = 0;
    public int CountSupportingLabels { get; set; } = 0;
    public int CountCrossReferences { get; set; } = 0;
    public int CountOtherTexts { get; set; } = 0;
    public int CountLocations { get; set; } = 0;
    public int CountDescriptions { get; set; } = 0;
    public int CountProvenances { get; set; } = 0;
    public int CountDistributions { get; set; } = 0;
    public int CountFairMetrics { get; set; } = 0;
    public int CountSnapshots { get; set; } = 0;

    public virtual string? NexusStatusSummary { get; set; } = string.Empty;

    public string? HtmlEscapeHashLiteral(string? withHash)
    {
      var withoutHash = withHash.Replace("#", "\\#");
      return withoutHash;
    }

  } // class

} // namespace
