// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

// use of interface instead of abstract class
// required for compatibility with Devart Entity Developer ORM

// TODO: simplify to just Guid/Name instead of GuidRef or GuidKey and Name
// keep the distinction between GuidRef and GuidKey only in the database layer

public interface ICoreResrepRoot
{
  public string? EntityName { get; set; }
  public string? EntityNature { get; set; }
  public string? EntityCanonicalLabel { get; set; }
  public string? EntityPrincipalTag { get; set; }
  public string? EntityInitialPrincipalTag { get; set; }
  public string? EntityInitialSupportingTag { get; set; }

  public byte EntityTypeCode { get; set; }
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

  public byte InfosetDoorsStatusCode { get; set; }
  public byte InfosetPortalStatusCode { get; set; }

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
  public IList<PortalCrossReference>? PortalCrossReferences { get; set; }
  public IList<PortalOtherText>? PortalOtherTexts { get; set; }
  public IList<DoorsLocation>? DoorsLocations { get; set; }
  public IList<DoorsDescription>? DoorsDescriptions { get; set; }
  public IList<DoorsProvenance>? DoorsProvenances { get; set; }
  public IList<DoorsDistribution>? DoorsDistributions { get; set; }
  public IList<DoorsFairMetric>? DoorsFairMetrics { get; set; }

} // end class

// end file