// NexusViewModelBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text;

using PDP.DREAM.CoreDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Types.PdpGuid;
using static PDP.DREAM.CoreDataLib.Utilities.PdpStringPhraseFormFile;

namespace PDP.DREAM.NexusDataLib.Models;

public abstract class NexusViewModelBase : CoreResrepModelBase, ICoreResrepViewModel
{
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

  // enum codes may be byte in UIL and short/smallint in DAL
  public short InfosetPortalStatusCode { get; set; } = (short)default(PdpAppConst.NpdsInfosetStatus);
  public string? InfosetPortalStatusName { get; set; } = string.Empty;

  // enum codes may be byte in UIL and short/smallint in DAL
  public short InfosetDoorsStatusCode { get; set; } = (short)default(PdpAppConst.NpdsInfosetStatus);
  public string? InfosetDoorsStatusName { get; set; } = string.Empty;


  // Nexus DiristryGuid & DiristryName
  private Guid? diristryGuid = NPDSSD.NpdsDefaultDiristryGuid;
  public Guid? RecordDiristryGuid
  { get { return diristryGuid; } set { diristryGuid = value; } }
  public string? RecordDiristryTag { get; set; } = string.Empty;
  public string? RecordDiristryName { get; set; } = string.Empty;

  // PORTAL RegistryGuid & RegistryName
  private Guid? registryGuid = NPDSSD.NpdsDefaultRegistryGuid;
  public Guid? RecordRegistryGuid
  { get { return registryGuid; } set { registryGuid = value; } }
  public string? RecordRegistryTag { get; set; } = string.Empty;
  public string? RecordRegistryName { get; set; } = string.Empty;

  // DOORS DirectoryGuid & DirectoryName
  private Guid? directoryGuid = NPDSSD.NpdsDefaultDirectoryGuid;
  public Guid? RecordDirectoryGuid
  { get { return directoryGuid; } set { directoryGuid = value; } }
  public string? RecordDirectoryTag { get; set; } = string.Empty;
  public string? RecordDirectoryName { get; set; } = string.Empty;

  // Scribe RegistrarGuid & RegistrarName
  private Guid? registrarGuid = NPDSSD.NpdsDefaultRegistrarGuid;
  public Guid? RecordRegistrarGuid
  { get { return registrarGuid; } set { registrarGuid = value; } }
  public string? RecordRegistrarTag { get; set; } = string.Empty;
  public string? RecordRegistrarName { get; set; } = string.Empty;

  //  Infoset Status Names, Codes (short), and Counts (int)

  public short ResrepEntityStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? ResrepEntityStatusName { get; set; } = string.Empty;

  public short ResrepRecordStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? ResrepRecordStatusName { get; set; } = string.Empty;

  public short ResrepInfosetStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? ResrepInfosetStatusName { get; set; } = string.Empty;


  public int EntityLabelsCount { get; set; } = 0;
  public short EntityLabelsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? EntityLabelsStatusName { get; set; } = string.Empty;

  public int SupportingTagsCount { get; set; } = 0;
  public short SupportingTagsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? SupportingTagsStatusName { get; set; } = string.Empty;
  public int SupportingLabelsCount { get; set; } = 0;
  public short SupportingLabelsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? SupportingLabelsStatusName { get; set; } = string.Empty;
  public int CrossReferencesCount { get; set; } = 0;
  public short CrossReferencesStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? CrossReferencesStatusName { get; set; } = string.Empty;
  public int OtherTextsCount { get; set; } = 0;
  public short OtherTextsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? OtherTextsStatusName { get; set; } = string.Empty;

  // TODO: clone the FormatCode/FormatName to Locations, Descriptions, Provenances, Distributions
  public short OtherTextsFormatCode { get; set; } = 0; // short/smallint in DAL
  public string? OtherTextsFormatName { get; set; } = string.Empty;

  public int LocationsCount { get; set; } = 0;
  public short LocationsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? LocationsStatusName { get; set; } = string.Empty;
  public int DescriptionsCount { get; set; } = 0;
  public short DescriptionsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? DescriptionsStatusName { get; set; } = string.Empty;
  public int ProvenancesCount { get; set; } = 0;
  public short ProvenancesStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? ProvenancesStatusName { get; set; } = string.Empty;
  public int DistributionsCount { get; set; } = 0;
  public short DistributionsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? DistributionsStatusName { get; set; } = string.Empty;
  public int FairMetricsCount { get; set; } = 0;
  public short FairMetricsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? FairMetricsStatusName { get; set; } = string.Empty;
  public int NexusSnapshotsCount { get; set; } = 0;
  public short NexusSnapshotsStatusCode { get; set; } = 0; // short/smallint in DAL
  public string? NexusSnapshotsStatusName { get; set; } = string.Empty;


  public string? NexusStatusSummary
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
      statSumm.AppendLine($"<div class='pdpStatusValid'><label>RecordManagedByAgent:</label>&nbsp;{ManagedByAgentName}&nbsp;</div>");
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

} // end class

// end file