// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// use of interface instead of abstract class
// required for compatibility with Devart Entity Developer ORM

public interface ICoreResrepLeaf
{
  public string? EntityCanonicalLabel { get; set; }
  public string? EntityName { get; set; }
  public string? EntityNature { get; set; }
  public string? EntityPrincipalTag { get; set; }
  public string? EntityInitialPrincipalTag { get; set; }
  public string? EntityInitialSupportingTag { get; set; }

  public short EntityTypeCode { get; set; }
  public string? EntityTypeName { get; set; }
  public bool EntityTypeIsComponent { get; set; }
  public bool EntityTypeIsConstituent { get; set; }

  // TODO: deprecate roles ??? to just Admin vs Agent ???
  //  or alternatively maintain the complexity
  public bool EntityTypeEditedByAdmin { get; set; }
  public bool EntityTypeEditedByAgent { get; set; }
  public bool EntityTypeEditedByAuthor { get; set; }
  public bool EntityTypeEditedByEditor { get; set; }
  public bool EntityTypeEditedByReviewer { get; set; }

  public Guid InfosetGuid { get; set; }
  public bool InfosetIsAgentShared { get; set; }
  public bool InfosetIsAuthorPrivate { get; set; }
  public bool InfosetIsManagerReleased { get; set; }
  public bool InfosetIsUpdaterLimited { get; set; }

  public short InfosetDoorsStatusCode { get; set; }
  public short InfosetPortalStatusCode { get; set; }

  public Guid RecordGuid { get; set; }
  public string? RecordHandle { get; set; }

  public bool RecordIsCached { get; set; }
  public DateTime? RecordCachedOn { get; set; }
  public Guid? RecordCachedByAgentGuid { get; set; }
  public string? RecordCachedByAgentAlias { get; set; }
  public DateTime? RecordCreatedOn { get; set; }
  public Guid? RecordCreatedByAgentGuid { get; set; }
  public string? RecordCreatedByAgentAlias { get; set; }
  public bool RecordIsDeleted { get; set; }
  public DateTime? RecordDeletedOn { get; set; }
  public Guid? RecordDeletedByAgentGuid { get; set; }
  public string? RecordDeletedByAgentAlias { get; set; }
  public DateTime? RecordUpdatedOn { get; set; }
  public Guid? RecordUpdatedByAgentGuid { get; set; }
  public string? RecordUpdatedByAgentAlias { get; set; }

  public Guid? RecordAuthoredByAgentGuid { get; set; }
  public Guid? RecordReviewedByAgentGuid { get; set; }
  public Guid? RecordEditedByAgentGuid { get; set; }
  public Guid? RecordManagedByAgentGuid { get; set; }

  public Guid RecordDirectoryGuid { get; set; }
  public string? RecordDirectoryTag { get; set; }
  public Guid RecordDiristryGuid { get; set; }
  public string? RecordDiristryTag { get; set; }
  public Guid RecordRegistrarGuid { get; set; }
  public string? RecordRegistrarTag { get; set; }
  public Guid RecordRegistryGuid { get; set; }
  public string? RecordRegistryTag { get; set; }

  public IList<CoreEntityLabel>? CoreEntityLabels { get; set; }
  public IList<CoreEntityCanonicalLabel>? CoreEntityCanonicalLabels { get; set; }
  public IList<CoreEntityAliasLabel>? CoreEntityAliasLabels { get; set; }
  public IList<PortalSupportingTag>? PortalSupportingTags { get; set; }
  public IList<PortalSupportingLabel>? PortalSupportingLabels { get; set; }
  public IList<PortalOtherText>? PortalOtherTexts { get; set; }
  public IList<PortalCrossReference>? PortalCrossReferences { get; set; }
  public IList<DoorsLocation>? DoorsLocations { get; set; }
  public IList<DoorsDescription>? DoorsDescriptions { get; set; }
  public IList<DoorsProvenance>? DoorsProvenances { get; set; }
  public IList<DoorsDistribution>? DoorsDistributions { get; set; }
  public IList<DoorsFairMetric>? DoorsFairMetrics { get; set; }

  public Guid? EntityContactGuid { get; set; }
  public string? EntityContactLabel { get; set; }
  public string? EntityContactName { get; set; }
  public string? EntityContactTag { get; set; }
  public Guid? EntityOtherGuid { get; set; }
  public string? EntityOtherLabel { get; set; }
  public string? EntityOtherName { get; set; }
  public string? EntityOtherTag { get; set; }
  public Guid? EntityOwnerGuid { get; set; }
  public string? EntityOwnerLabel { get; set; }
  public string? EntityOwnerName { get; set; }
  public string? EntityOwnerTag { get; set; }
  public string? InfosetDoorsStatusName { get; set; }
  public DateTime? InfosetDoorsStatusTestedOn { get; set; }
  public string? InfosetEntailment { get; set; }
  public string? InfosetPortalStatusName { get; set; }
  public DateTime? InfosetPortalStatusTestedOn { get; set; }
  public IList<CoreServiceRestrictionAnd>? CoreServiceRestrictionAnds { get; set; }
  public IList<CoreResrepService>? CoreResrepServices { get; set; }
  public IList<CoreResrepSnapshot>? CoreResrepSnapshots { get; set; }
  public string? RecordDirectoryLabel { get; set; }
  public string? RecordDirectoryName { get; set; }
  public string? RecordDiristryLabel { get; set; }
  public string? RecordDiristryName { get; set; }
  public string? RecordManagedByAgentAlias { get; set; }
  public Guid? RecordRegistrantGuid { get; set; }
  public string? RecordRegistrantLabel { get; set; }
  public string? RecordRegistrantName { get; set; }
  public string? RecordRegistrantTag { get; set; }
  public string? RecordRegistrarLabel { get; set; }
  public string? RecordRegistrarName { get; set; }
  public string? RecordRegistryLabel { get; set; }
  public string? RecordRegistryName { get; set; }
  public string? RecordSignature { get; set; }

  // infosubset format codes, names go in each of the infosubsets
  // not here for the RR root, stem or leaf
  // short InfosetFormatCodeRef { get; set; } // InfssetFormatCodeRef
  // string? InfosetFormatName { get; set; } // InfssetFormatName
  // short InfosetDescriptionFormatCode { get; set; }
  // string? InfosetDescriptionFormatName { get; set; }

  public int InfosetCrossReferencesCount { get; set; }
  public string? InfosetCrossReferencesStatusName { get; set; }
  public short InfosetCrossReferencesStatusCode { get; set; }

  public int InfosetDescriptionsCount { get; set; }
  public string? InfosetDescriptionsStatusName { get; set; }
  public short InfosetDescriptionsStatusCode { get; set; }

  public int InfosetDistributionsCount { get; set; }
  public string? InfosetDistributionsStatusName { get; set; }
  public short InfosetDistributionsStatusCode { get; set; }

  public int InfosetEntityLabelsCount { get; set; }
  public string? InfosetEntityLabelsStatusName { get; set; }
  public short InfosetEntityLabelsStatusCode { get; set; }

  public int InfosetFairMetricsCount { get; set; }
  public string? InfosetFairMetricsStatusName { get; set; }
  public short InfosetFairMetricsStatusCode { get; set; }

  public int InfosetLocationsCount { get; set; }
  public string? InfosetLocationsStatusName { get; set; }
  public short InfosetLocationsStatusCode { get; set; }

  public int InfosetNexusSnapshotsCount { get; set; }
  public string? InfosetNexusSnapshotsStatusName { get; set; }
  public short InfosetNexusSnapshotsStatusCode { get; set; }

  public int InfosetOtherTextsCount { get; set; }
  public string? InfosetOtherTextsStatusName { get; set; }
  public short InfosetOtherTextsStatusCode { get; set; }

  public int InfosetProvenancesCount { get; set; }
  public string? InfosetProvenancesStatusName { get; set; }
  public short InfosetProvenancesStatusCode { get; set; }

  public int InfosetSupportingLabelsCount { get; set; }
  public string? InfosetSupportingLabelsStatusName { get; set; }
  public short InfosetSupportingLabelsStatusCode { get; set; }

  public int InfosetSupportingTagsCount { get; set; }
  public string? InfosetSupportingTagsStatusName { get; set; }
  public short InfosetSupportingTagsStatusCode { get; set; }

  // infoset status code, name, when tested

  public DateTime? InfosetResrepEntityTestedOn { get; set; }
  public string? InfosetResrepEntityStatusName { get; set; }
  public short InfosetResrepEntityStatusCode { get; set; }

  public DateTime? InfosetResrepInfosetTestedOn { get; set; }
  public string? InfosetResrepInfosetStatusName { get; set; }
  public short InfosetResrepInfosetStatusCode { get; set; }

  public DateTime? InfosetResrepRecordTestedOn { get; set; }
  public string? InfosetResrepRecordStatusName { get; set; }
  public short InfosetResrepRecordStatusCode { get; set; }

} // end class

// end file