// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class ResrepLeafModelBase : ResrepRootModelBase // : ICoreResrepModelBase
{
  public ResrepLeafModelBase() { }
  public ResrepLeafModelBase(NpdsRecordAccess nra) : base(nra) { }

  // TODO: reconcile/deconflict with properties in NpdsClient
  // ATTN: for use with merge, split, moves of records
  // DstnSrvc prefix for Destination Service

  [DisplayName("Diristry"), Required]
  public string? DstnSrvcGuidStr
  {
    set {
      if (!string.IsNullOrEmpty(value)) { drstryGuidStr = value; }
      else { drstryGuidStr = NPDSCD.DiristryGuidDefault.ToString(); }
      drstryGuid = PdpGuid.ParseToNonNullable(drstryGuidStr, NPDSCD.DiristryGuidDefault);
      drstryTag = NPDSCD.NpdsServiceCache.GetByNullableGuid(drstryGuid);
    }
    get {
      // if get called before set, then trigger set defaults
      if (string.IsNullOrEmpty(drstryGuidStr)) { DstnSrvcGuidStr = ""; }
      return drstryGuidStr;
    }
  }
  private string? drstryGuidStr;

  public Guid? DstnSrvcGuid { get { return drstryGuid; } }
  private Guid? drstryGuid;

  public string? DstnSrvcTag { get { return drstryTag; } }
  private string? drstryTag;

  // Counts for Nexus may be int in both UIL and DAL
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


  // advanced properties

  public string? EntityOwnerLabel { get; set; } = ESS;
  public string? EntityOwnerHandle { get; set; } = ESS;
  public Guid? EntityOwnerGuid { get; set; } = EGS;

  public string? EntityContactLabel { get; set; } = ESS;
  public string? EntityContactHandle { get; set; } = ESS;
  public Guid? EntityContactGuid { get; set; } = EGS;

  public string? EntityOtherLabel { get; set; } = ESS;
  public string? EntityOtherHandle { get; set; } = ESS;
  public Guid? EntityOtherGuid { get; set; } = EGS;

  //  Infoset Status Names, Codes (byte), and Counts (int)

  public byte ResrepEntityStatusCode { get; set; } = 0;
  public string? ResrepEntityStatusName { get; set; } = ESS;

  public byte ResrepRecordStatusCode { get; set; } = 0;
  public string? ResrepRecordStatusName { get; set; } = ESS;

  public byte ResrepInfosetStatusCode { get; set; } = 0;
  public string? ResrepInfosetStatusName { get; set; } = ESS;


  // TODO: re-eval vocab terminology "ServiceDefaults" vs "CoreDefaults" vs alternative
  public int ServiceDefaultsCount { get; set; } = 0;
  public byte ServiceDefaultsStatusCode { get; set; } = 0;
  public string? ServiceDefaultsStatusName { get; set; } = ESS;


  public int EntityLabelsCount { get; set; } = 0;
  public byte EntityLabelsStatusCode { get; set; } = 0;
  public string? EntityLabelsStatusName { get; set; } = ESS;

  public int SupportingTagsCount { get; set; } = 0;
  public byte SupportingTagsStatusCode { get; set; } = 0;
  public string? SupportingTagsStatusName { get; set; } = ESS;
  public int SupportingLabelsCount { get; set; } = 0;
  public byte SupportingLabelsStatusCode { get; set; } = 0;
  public string? SupportingLabelsStatusName { get; set; } = ESS;
  public int CrossReferencesCount { get; set; } = 0;
  public byte CrossReferencesStatusCode { get; set; } = 0;
  public string? CrossReferencesStatusName { get; set; } = ESS;
  public int OtherTextsCount { get; set; } = 0;
  public byte OtherTextsStatusCode { get; set; } = 0;
  public string? OtherTextsStatusName { get; set; } = ESS;

  public byte OtherTextsFormatCode { get; set; } = 0;
  public string? OtherTextsFormatName { get; set; } = ESS;

  public int LocationsCount { get; set; } = 0;
  public byte LocationsStatusCode { get; set; } = 0;
  public string? LocationsStatusName { get; set; } = ESS;
  public int DescriptionsCount { get; set; } = 0;
  public byte DescriptionsStatusCode { get; set; } = 0;
  public string? DescriptionsStatusName { get; set; } = ESS;
  public int ProvenancesCount { get; set; } = 0;
  public byte ProvenancesStatusCode { get; set; } = 0;
  public string? ProvenancesStatusName { get; set; } = ESS;
  public int DistributionsCount { get; set; } = 0;
  public byte DistributionsStatusCode { get; set; } = 0;
  public string? DistributionsStatusName { get; set; } = ESS;
  public int FairMetricsCount { get; set; } = 0;
  public byte FairMetricsStatusCode { get; set; } = 0;
  public string? FairMetricsStatusName { get; set; } = ESS;

  public int CoreResrepSnapshotsCount { get; set; } = 0;
  public byte CoreResrepSnapshotsStatusCode { get; set; } = 0;
  public string? CoreResrepSnapshotsStatusName { get; set; } = ESS;

  public int NexusSnapshotsCount { get; set; } = 0;
  public byte NexusSnapshotsStatusCode { get; set; } = 0;
  public string? NexusSnapshotsStatusName { get; set; } = ESS;


  public string? ResrepStatusSummary
  {
    get {
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
      statSumm.AppendLine($"<div class='pdpStatusValid'><label>RecordManagedByAgent:</label>&nbsp;{AuthoredByAgentAlias}&nbsp;</div>");
      statSumm.AppendLine($"<div><label>RecordCreatedOn:</label>&nbsp;{CreatedOn}&nbsp;</div>");
      statSumm.AppendLine($"<div><label>RecordCreatedByAgent:</label>&nbsp;{CreatedByAgentAlias}&nbsp;</div>");
      statSumm.AppendLine($"<div><label>RecordUpdatedOn:</label>&nbsp;{UpdatedOn}&nbsp;</div>");
      statSumm.AppendLine($"<div><label>RecordUpdatedByAgent:</label>&nbsp;{UpdatedByAgentAlias}&nbsp;</div>");
      if ((DeletedOn != null) && (!string.IsNullOrEmpty(DeletedByAgentAlias)))
      {
        statSumm.AppendLine($"<div><label>RecordDeletedOn:</label>&nbsp;{DeletedOn}&nbsp;</div>");
        statSumm.AppendLine($"<div><label>RecordDeletedByAgent:</label>&nbsp;{DeletedByAgentAlias}&nbsp;</div>");
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

} // end class

// end file