// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<CoreResrepLeafUxm?> ToEditable(this IQueryable<ICoreResrepLeaf> qry, Guid agentGuidRef = default)
  {
    IQueryable<CoreResrepLeafUxm?> rows =
      from r in qry
      select new CoreResrepLeafUxm
      {
        AgentGuid = agentGuidRef,
        RRRecordGuid = r.RecordGuid,
        RRInfosetGuid = r.InfosetGuid,
        RecordHandle = r.RecordHandle,
        RecordIsDeleted = r.RecordIsDeleted,
        CreatedOn = r.RecordCreatedOn,
        CreatedByAgentGuid = r.RecordCreatedByAgentGuid,
        CreatedByAgentAlias = r.RecordCreatedByAgentAlias,
        UpdatedOn = r.RecordUpdatedOn,
        UpdatedByAgentGuid = r.RecordUpdatedByAgentGuid,
        UpdatedByAgentAlias = r.RecordUpdatedByAgentAlias,
        DeletedOn = r.RecordDeletedOn,
        DeletedByAgentGuid = r.RecordDeletedByAgentGuid,
        DeletedByAgentAlias = r.RecordDeletedByAgentAlias,
        //
        ManagedByAgentGuid = r.RecordManagedByAgentGuid,
        ManagedByAgentAlias = r.RecordManagedByAgentAlias,
        AuthoredByAgentGuid = r.RecordAuthoredByAgentGuid,
        // AuthoredByAgentAlias = r.RecordAuthoredByAgentAlias,
        ReviewedByAgentGuid = r.RecordReviewedByAgentGuid,
        // ReviewedByAgentAlias = r.RecordReviewedByAgentAlias,
        EditedByAgentGuid = r.RecordEditedByAgentGuid,
        // EditedByAgentAlias = r.RecordEditedByAgentAlias,
        //
        EntityTypeCode = (byte)r.EntityTypeCode,
        EntityTypeName = r.EntityTypeName,
        EntityInitPrinTag = r.EntityInitialPrincipalTag,
        EntityPrincipalTag = r.EntityPrincipalTag,
        EntityCanonicalLabel = r.EntityCanonicalLabel,
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        EntityOwnerGuid = r.EntityOwnerGuid,
        EntityOwnerLabel = r.EntityOwnerLabel,
        EntityContactGuid = r.EntityOwnerGuid,  // TODO: fix this placeholder
        EntityContactLabel = r.EntityOwnerLabel,// TODO: fix this placeholder
        EntityOtherGuid = r.EntityOwnerGuid,// TODO: fix this placeholder
        EntityOtherLabel = r.EntityOwnerLabel,// TODO: fix this placeholder
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        InfosetPortalStatusCode = (byte)r.InfosetPortalStatusCode,
        InfosetPortalStatusName = r.InfosetPortalStatusName,
        InfosetDoorsStatusCode = (byte)r.InfosetDoorsStatusCode,
        InfosetDoorsStatusName = r.InfosetDoorsStatusName,
        RecordDiristryGuid = r.RecordDiristryGuid,
        RecordDiristryTag = r.RecordDiristryTag,
        RecordDiristryName = r.RecordDiristryName,
        RecordRegistryGuid = r.RecordRegistryGuid,
        RecordRegistryTag = r.RecordRegistryTag,
        RecordRegistryName = r.RecordRegistryName,
        RecordDirectoryGuid = r.RecordDirectoryGuid,
        RecordDirectoryTag = r.RecordDirectoryTag,
        RecordDirectoryName = r.RecordDirectoryName,
        RecordRegistrarGuid = r.RecordRegistrarGuid,
        RecordRegistrarTag = r.RecordRegistrarTag,
        RecordRegistrarName = r.RecordRegistrarName,
        ResrepEntityStatusCode = (byte)r.InfosetResrepEntityStatusCode,
        ResrepEntityStatusName = r.InfosetResrepEntityStatusName,
        ResrepRecordStatusCode = (byte)r.InfosetResrepRecordStatusCode,
        ResrepRecordStatusName = r.InfosetResrepRecordStatusName,
        ResrepInfosetStatusCode = (byte)r.InfosetResrepInfosetStatusCode,
        ResrepInfosetStatusName = r.InfosetResrepInfosetStatusName,
        EntityLabelsCount = r.InfosetEntityLabelsCount,
        EntityLabelsStatusCode = (byte)r.InfosetEntityLabelsStatusCode,
        EntityLabelsStatusName = r.InfosetEntityLabelsStatusName,
        SupportingTagsCount = r.InfosetSupportingTagsCount,
        SupportingTagsStatusCode = (byte)r.InfosetSupportingTagsStatusCode,
        SupportingTagsStatusName = r.InfosetSupportingTagsStatusName,
        SupportingLabelsCount = r.InfosetSupportingLabelsCount,
        SupportingLabelsStatusCode = (byte)r.InfosetSupportingLabelsStatusCode,
        SupportingLabelsStatusName = r.InfosetSupportingLabelsStatusName,
        CrossReferencesCount = r.InfosetCrossReferencesCount,
        CrossReferencesStatusCode = (byte)r.InfosetCrossReferencesStatusCode,
        CrossReferencesStatusName = r.InfosetCrossReferencesStatusName,
        OtherTextsCount = r.InfosetOtherTextsCount,
        OtherTextsStatusCode = (byte)r.InfosetOtherTextsStatusCode,
        OtherTextsStatusName = r.InfosetOtherTextsStatusName,
        LocationsCount = r.InfosetLocationsCount,
        LocationsStatusCode = (byte)r.InfosetLocationsStatusCode,
        LocationsStatusName = r.InfosetLocationsStatusName,
        DescriptionsCount = r.InfosetDescriptionsCount,
        DescriptionsStatusCode = (byte)r.InfosetDescriptionsStatusCode,
        DescriptionsStatusName = r.InfosetDescriptionsStatusName,
        ProvenancesCount = r.InfosetProvenancesCount,
        ProvenancesStatusCode = (byte)r.InfosetProvenancesStatusCode,
        ProvenancesStatusName = r.InfosetProvenancesStatusName,
        DistributionsCount = r.InfosetDistributionsCount,
        DistributionsStatusCode = (byte)r.InfosetDistributionsStatusCode,
        DistributionsStatusName = r.InfosetDistributionsStatusName,
        FairMetricsCount = r.InfosetFairMetricsCount,
        FairMetricsStatusCode = (byte)r.InfosetFairMetricsStatusCode,
        FairMetricsStatusName = r.InfosetFairMetricsStatusName,
        NexusSnapshotsCount = r.InfosetNexusSnapshotsCount,
        NexusSnapshotsStatusCode = (byte)r.InfosetNexusSnapshotsStatusCode,
        NexusSnapshotsStatusName = r.InfosetNexusSnapshotsStatusName
      };
    return rows;
  }

} // end class

// end file