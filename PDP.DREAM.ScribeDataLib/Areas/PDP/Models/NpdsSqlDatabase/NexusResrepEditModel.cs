using System;
using System.Text;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class NexusResrepEditModel : NexusEditModelBase
  {
    public NexusResrepEditModel()
    {
      itemXnam = NpdsConst.NexusResrepItemXnam;
    }

    // Display and UIHint necessary for Telerik Grid EditForm and PopUp modes but not for InLine mode

    public bool AgentIsCreator { get { return (CreatedByAgentGuid == AgentGuid); } }
    public bool AgentIsUpdater { get { return (UpdatedByAgentGuid == AgentGuid); } }
    public bool AgentIsManager { get { return (ManagedByAgentGuid == AgentGuid); } }

    public string? RequestOrRelease
    {
      get { return ((AgentIsManager) ? "Release" : "Request"); }
    }
    public string? ReqRelButtonHtml
    {
      get { return $"<a href='#' id='{RRRecordGuid}' class='k-button' onclick='OnRequestReleaseRecord(this)'>{RequestOrRelease}</a>"; }
    }
    public string? AgentRequestHtml
    {
      get { return ((AgentIsManager) ? RequestOrRelease : ReqRelButtonHtml); }
    }
    public string? AuthorReleaseHtml
    {
      get { return ((AgentIsManager) ? ReqRelButtonHtml : RequestOrRelease); }
    }

    public string? RecordHandle { get; set; } = string.Empty;
    public bool RecordIsDeleted { get; set; } = false;

    public short EntityTypeCode { get; set; } = (short)default(NpdsConst.EntityType);
    public string? EntityTypeName { get; set; } = string.Empty;

    public string? EntityInitialTag { get; set; } = string.Empty;

    public string? EntityName { get; set; } = string.Empty;
    public string? EntityName64
    {
      get
      {
        if (string.IsNullOrEmpty(EntityName)) { entNam64 = string.Empty; }
        else
        {
          entNam64 = ((EntityName.Length > 64) ?
            EntityName.Substring(0, 64).ToColorSpan("partial") : EntityName);
        }
        return entNam64;
      }
    }
    private string? entNam64;

    public string? EntityNature { get; set; } = string.Empty;
    public string? EntityNature128
    {
      get
      {
        if (string.IsNullOrEmpty(EntityNature)) { entNat128 = string.Empty; }
        else
        {
          entNat128 = ((EntityNature.Length > 128) ?
           EntityNature.Substring(0, 128).ToColorSpan("partial") : EntityNature);
        }
        return entNat128;
      }
    }
    private string? entNat128;

    // advanced properties

    public string? EntityOwnerLabel { get; set; } = string.Empty;
    public string? EntityOwnerHandle { get; set; } = string.Empty;
    public Guid? EntityOwnerGuid { get; set; } = Guid.Empty;

    public string? EntityContactLabel { get; set; } = string.Empty;
    public string? EntityContactHandle { get; set; } = string.Empty;
    public Guid? EntityContactGuid { get; set; } = Guid.Empty;

    public string? EntityOtherLabel { get; set; } = string.Empty;
    public string? EntityOtherHandle { get; set; } = string.Empty;
    public Guid? EntityOtherGuid { get; set; } = Guid.Empty;

    public bool InfosetIsAuthorPrivate { get; set; } = false;
    public bool InfosetIsAgentShared { get; set; } = false;
    public bool InfosetIsUpdaterLimited { get; set; } = false;
    public bool InfosetIsManagerReleased { get; set; } = false;

    public short InfosetPortalStatusCode { get; set; } = (short)default(NpdsConst.InfosetStatus);
    public string? InfosetPortalStatusName { get; set; } = string.Empty;

    public short InfosetDoorsStatusCode { get; set; } = (short)default(NpdsConst.InfosetStatus);
    public string? InfosetDoorsStatusName { get; set; } = string.Empty;


    // Nexus DiristryGuid & DiristryName
    private Guid? diristryGuid = NpdsServiceDefaults.GetValues.NpdsDefaultDiristryGuid;
    public Guid? RecordDiristryGuid
    { get { return diristryGuid; } set { diristryGuid = value; } }
    public string? RecordDiristryName { get; set; } = string.Empty;

    // PORTAL RegistryGuid & RegistryName
    private Guid? registryGuid = NpdsServiceDefaults.GetValues.NpdsDefaultRegistryGuid;
    public Guid? RecordRegistryGuid
    { get { return registryGuid; } set { registryGuid = value; } }
    public string? RecordRegistryName { get; set; } = string.Empty;

    // DOORS DirectoryGuid & DirectoryName
    private Guid? directoryGuid = NpdsServiceDefaults.GetValues.NpdsDefaultDirectoryGuid;
    public Guid? RecordDirectoryGuid
    { get { return directoryGuid; } set { directoryGuid = value; } }
    public string? RecordDirectoryName { get; set; } = string.Empty;

    // Scribe RegistrarGuid & RegistrarName
    private Guid? registrarGuid = NpdsServiceDefaults.GetValues.NpdsDefaultRegistrarGuid;
    public Guid? RecordRegistrarGuid
    { get { return registrarGuid; } set { registrarGuid = value; } }
    public string? RecordRegistrarName { get; set; } = string.Empty;

    //  Infoset Status Codes, Names, and Counts

    public short ResrepEntityStatusCode { get; set; } = 0;
    public string? ResrepEntityStatusName { get; set; } = string.Empty;

    public short ResrepRecordStatusCode { get; set; } = 0;
    public string? ResrepRecordStatusName { get; set; } = string.Empty;

    public short ResrepInfosetStatusCode { get; set; } = 0;
    public string? ResrepInfosetStatusName { get; set; } = string.Empty;


    public int EntityLabelsCount { get; set; } = 0;
    public short EntityLabelsStatusCode { get; set; } = 0;
    public string? EntityLabelsStatusName { get; set; } = string.Empty;

    public int SupportingTagsCount { get; set; } = 0;
    public short SupportingTagsStatusCode { get; set; } = 0;
    public string? SupportingTagsStatusName { get; set; } = string.Empty;
    public int SupportingLabelsCount { get; set; } = 0;
    public short SupportingLabelsStatusCode { get; set; } = 0;
    public string? SupportingLabelsStatusName { get; set; } = string.Empty;
    public int CrossReferencesCount { get; set; } = 0;
    public short CrossReferencesStatusCode { get; set; } = 0;
    public string? CrossReferencesStatusName { get; set; } = string.Empty;
    public int OtherTextsCount { get; set; } = 0;
    public short OtherTextsStatusCode { get; set; } = 0;
    public string? OtherTextsStatusName { get; set; } = string.Empty;

    public int LocationsCount { get; set; } = 0;
    public short LocationsStatusCode { get; set; } = 0;
    public string? LocationsStatusName { get; set; } = string.Empty;
    public int DescriptionsCount { get; set; } = 0;
    public short DescriptionsStatusCode { get; set; } = 0;
    public string? DescriptionsStatusName { get; set; } = string.Empty;
    public int ProvenancesCount { get; set; } = 0;
    public short ProvenancesStatusCode { get; set; } = 0;
    public string? ProvenancesStatusName { get; set; } = string.Empty;
    public int DistributionsCount { get; set; } = 0;
    public short DistributionsStatusCode { get; set; } = 0;
    public string? DistributionsStatusName { get; set; } = string.Empty;
    public int FairMetricsCount { get; set; } = 0;
    public short FairMetricsStatusCode { get; set; } = 0;
    public string? FairMetricsStatusName { get; set; } = string.Empty;
    public int NexusSnapshotsCount { get; set; } = 0;
    public short NexusSnapshotsStatusCode { get; set; } = 0;
    public string? NexusSnapshotsStatusName { get; set; } = string.Empty;

    public override string? NexusStatusSummary
    {
      get
      {
        statSumm = new StringBuilder("<div class='pdpGrid1x3'>");

        statSumm.AppendLine("<div class='pdpGridCell1'>");
        statSumm.AppendLine($"<div><label><b>EntityLabels</b> Count:</label>&nbsp;{EntityLabelsCount}&nbsp;<label>Status:</label>&nbsp;{EntityLabelsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>SupportingTags</b> Count:</label>&nbsp;{SupportingTagsCount}&nbsp;<label>Status:</label>&nbsp;{SupportingTagsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>SupportingLabels</b> Count:</label>&nbsp;{SupportingLabelsCount}&nbsp;<label>Status:</label>&nbsp;{SupportingLabelsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>CrossReferences</b> Count:</label>&nbsp;{CrossReferencesCount}&nbsp;<label>Status:</label>&nbsp;{CrossReferencesStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>OtherTexts</b> Count:</label>&nbsp;{OtherTextsCount}&nbsp;<label>Status:</label>&nbsp;{OtherTextsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>Locations</b> Count:</label>&nbsp;{LocationsCount}&nbsp;<label>Status:</label>&nbsp;{LocationsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>Descriptions</b> Count:</label>&nbsp;{DescriptionsCount}&nbsp;<label>Status:</label>&nbsp;{DescriptionsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>Provenances</b> Count:</label>&nbsp;{ProvenancesCount}&nbsp;<label>Status:</label>&nbsp;{ProvenancesStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>Distributions</b> Count:</label>&nbsp;{DistributionsCount}&nbsp;<label>Status:</label>&nbsp;{DistributionsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>FairMetrics</b> Count:</label>&nbsp;{FairMetricsCount}&nbsp;<label>Status:</label>&nbsp;{FairMetricsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label><b>NexusSnapshots</b> Count:</label>&nbsp;{NexusSnapshotsCount}&nbsp;<label>Status:</label>&nbsp;{NexusSnapshotsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine("</div>");

        statSumm.AppendLine("<div class='pdpGridCell2'>");
        statSumm.AppendLine($"<div><label>Infoset PORTAL Status:</label>&nbsp;{InfosetPortalStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>Infoset DOORS Status:</label>&nbsp;{InfosetDoorsStatusName.ToColorSpan()}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>InfosetIsAuthorPrivate:</label>&nbsp;{InfosetIsAuthorPrivate}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>InfosetIsAgentShared:</label>&nbsp;{InfosetIsAgentShared}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>InfosetIsUpdaterLimited:</label>&nbsp;{InfosetIsUpdaterLimited}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>InfosetIsManagerReleased:</label>&nbsp;{InfosetIsManagerReleased}&nbsp;</div>");
        statSumm.AppendLine("</div>");

        statSumm.AppendLine("<div class='pdpGridCell3'>");
        statSumm.AppendLine($"<div><label>RecordHandle:</label>&nbsp;{RecordHandle}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>RecordManagedByAgent:</label>&nbsp;{ManagedByAgentName}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>RecordCreatedOn:</label>&nbsp;{CreatedOn}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>RecordCreatedByAgent:</label>&nbsp;{CreatedByAgentName}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>RecordUpdatedOn:</label>&nbsp;{UpdatedOn}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>RecordUpdatedByAgent:</label>&nbsp;{UpdatedByAgentName}&nbsp;</div>");
        if ((DeletedOn != null) && (!string.IsNullOrEmpty(DeletedByAgentName)))
        {
          statSumm.AppendLine($"<div><label>RecordDeletedOn:</label>&nbsp;{DeletedOn}&nbsp;</div>");
          statSumm.AppendLine($"<div><label>RecordDeletedByAgent:</label>&nbsp;{DeletedByAgentName}&nbsp;</div>");
        }
        statSumm.AppendLine("</div>");

        statSumm.AppendLine("<div class='pdpGridCell4'>");
        statSumm.AppendLine($"<div><label>EntityTypeCode:</label>&nbsp;{EntityTypeCode}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>EntityTypeName:</label>&nbsp;{EntityTypeName}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>EntityName:</label>&nbsp;{EntityName}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>EntityNature:</label>&nbsp;{EntityNature}&nbsp;</div>");
        statSumm.AppendLine("</div>");

        statSumm.AppendLine("</div>");
        return statSumm.ToString();
      }
    }
    private StringBuilder? statSumm;

  }

}
