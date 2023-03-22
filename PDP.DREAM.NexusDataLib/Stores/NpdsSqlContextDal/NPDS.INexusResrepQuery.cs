// INexusResrepQuery.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

// TODO: simplify to just Guid/Name instead of GuidRef or GuidKey and Name
// keep the distinction between GuidRef and GuidKey only in the database layer

public interface INexusResrepQuery
{
  string? EntityCanonicalLabel { get; set; }
  string? EntityInitialTag { get; set; }
  string? EntityName { get; set; }
  string? EntityNature { get; set; }
  string? EntityPrincipalTag { get; set; }

  short EntityTypeCodeRef { get; set; }
  bool EntityTypeEditedByAdmin { get; set; }
  bool EntityTypeEditedByAgent { get; set; }
  bool EntityTypeEditedByAuthor { get; set; }
  bool EntityTypeEditedByEditor { get; set; }
  bool EntityTypeIsComponent { get; set; }
  bool EntityTypeIsConstituent { get; set; }
  string? EntityTypeName { get; set; }

   short InfosetDoorsStatusCode { get; set; }

  Guid InfosetGuidKey { get; set; }
  bool InfosetIsAgentShared { get; set; }
  bool InfosetIsAuthorPrivate { get; set; }
  bool InfosetIsManagerReleased { get; set; }
  bool InfosetIsUpdaterLimited { get; set; }

  short InfosetPortalStatusCode { get; set; }

  IList<NexusCrossReference>? NexusCrossReferences { get; set; }
  IList<NexusDescription>? NexusDescriptions { get; set; }
  IList<NexusDistribution>? NexusDistributions { get; set; }
  IList<NexusEntityAliasLabel>? NexusEntityAliasLabels { get; set; }
  IList<NexusEntityCanonicalLabel>? NexusEntityCanonicalLabels { get; set; }
  IList<NexusEntityLabel>? NexusEntityLabels { get; set; }
  IList<NexusFairMetric>? NexusFairMetrics { get; set; }
  IList<NexusLocation>? NexusLocations { get; set; }
  IList<NexusOtherText>? NexusOtherTexts { get; set; }
  IList<NexusProvenance>? NexusProvenances { get; set; }
  IList<NexusSupportingLabel>? NexusSupportingLabels { get; set; }
  IList<NexusSupportingTag>? NexusSupportingTags { get; set; }
  Guid? RecordCreatedByAgentGuidRef { get; set; }
  DateTime? RecordCreatedOn { get; set; }
  Guid? RecordDeletedByAgentGuidRef { get; set; }
  DateTime? RecordDeletedOn { get; set; }
  Guid RecordDirectoryGuidRef { get; set; }
  string? RecordDirectoryTag { get; set; }
  Guid RecordDiristryGuidRef { get; set; }
  string? RecordDiristryTag { get; set; }
  Guid RecordGuidKey { get; set; }
  string? RecordHandle { get; set; }
  bool RecordIsCached { get; set; }
  bool RecordIsDeleted { get; set; }
  Guid? RecordManagedByAgentGuidRef { get; set; }
  Guid RecordRegistrarGuidRef { get; set; }
  string? RecordRegistrarTag { get; set; }
  Guid RecordRegistryGuidRef { get; set; }
  string? RecordRegistryTag { get; set; }
  Guid? RecordUpdatedByAgentGuidRef { get; set; }
  DateTime? RecordUpdatedOn { get; set; }
}
