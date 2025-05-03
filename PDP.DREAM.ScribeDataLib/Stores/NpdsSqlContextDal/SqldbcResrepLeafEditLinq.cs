// SqldbcUilResrepLeafEditLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

using Kendo.Mvc.Extensions;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<NexusResrepEditModel?> ToEditable(this IQueryable<INexusResrepLeaf> qry, Guid agentGuidRef = default)
  {
    IQueryable<NexusResrepEditModel?> rows =
      from r in qry
      select new NexusResrepEditModel
      {
        AgentGuid = agentGuidRef,
        RRRecordGuid = r.RecordGuidKey,
        RRInfosetGuid = r.InfosetGuidKey,
        RecordHandle = r.RecordHandle,
        RecordIsDeleted = r.RecordIsDeleted,
        ManagedByAgentGuid = r.RecordManagedByAgentGuidRef,
        ManagedByAgentName = r.RecordManagedByUserName,
        CreatedOn = r.RecordCreatedOn,
        CreatedByAgentGuid = r.RecordCreatedByAgentGuidRef,
        CreatedByAgentName = r.RecordCreatedByUserName,
        UpdatedOn = r.RecordUpdatedOn,
        UpdatedByAgentGuid = r.RecordUpdatedByAgentGuidRef,
        UpdatedByAgentName = r.RecordUpdatedByUserName,
        DeletedOn = r.RecordDeletedOn,
        DeletedByAgentGuid = r.RecordDeletedByAgentGuidRef,
        DeletedByAgentName = r.RecordDeletedByUserName,
        //
        EntityTypeCode = r.EntityTypeCodeRef,
        EntityTypeName = r.EntityTypeName,
        EntityInitialTag = r.EntityInitialTag,
        EntityPrincipalTag = r.EntityPrincipalTag,
        EntityCanonicalLabel = r.EntityCanonicalLabel,
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        EntityOwnerGuid = r.EntityOwnerGuidRef,
        EntityOwnerLabel = r.EntityOwnerLabel,
        EntityContactGuid = r.EntityOwnerGuidRef,
        EntityContactLabel = r.EntityOwnerLabel,
        EntityOtherGuid = r.EntityOwnerGuidRef,
        EntityOtherLabel = r.EntityOwnerLabel,
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        InfosetPortalStatusCode = r.InfosetPortalStatusCode,
        InfosetPortalStatusName = r.InfosetPortalStatusName,
        InfosetDoorsStatusCode = r.InfosetDoorsStatusCode,
        InfosetDoorsStatusName = r.InfosetDoorsStatusName,
        RecordDiristryGuid = r.RecordDiristryGuidRef,
        RecordDiristryTag = r.RecordDiristryTag,
        RecordDiristryName = r.RecordDiristryName,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        RecordRegistryTag = r.RecordRegistryTag,
        RecordRegistryName = r.RecordRegistryName,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        RecordDirectoryTag = r.RecordDirectoryTag,
        RecordDirectoryName = r.RecordDirectoryName,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        RecordRegistrarTag = r.RecordRegistrarTag,
        RecordRegistrarName = r.RecordRegistrarName,
        ResrepEntityStatusCode = r.InfosetResrepEntityStatusCode,
        ResrepEntityStatusName = r.InfosetResrepEntityStatusName,
        ResrepRecordStatusCode = r.InfosetResrepRecordStatusCode,
        ResrepRecordStatusName = r.InfosetResrepRecordStatusName,
        ResrepInfosetStatusCode = r.InfosetResrepInfosetStatusCode,
        ResrepInfosetStatusName = r.InfosetResrepInfosetStatusName,
        EntityLabelsCount = r.InfosetEntityLabelsCount,
        EntityLabelsStatusCode = r.InfosetEntityLabelsStatusCode,
        EntityLabelsStatusName = r.InfosetEntityLabelsStatusName,
        SupportingTagsCount = r.InfosetSupportingTagsCount,
        SupportingTagsStatusCode = r.InfosetSupportingTagsStatusCode,
        SupportingTagsStatusName = r.InfosetSupportingTagsStatusName,
        SupportingLabelsCount = r.InfosetSupportingLabelsCount,
        SupportingLabelsStatusCode = r.InfosetSupportingLabelsStatusCode,
        SupportingLabelsStatusName = r.InfosetSupportingLabelsStatusName,
        CrossReferencesCount = r.InfosetCrossReferencesCount,
        CrossReferencesStatusCode = r.InfosetCrossReferencesStatusCode,
        CrossReferencesStatusName = r.InfosetCrossReferencesStatusName,
        OtherTextsCount = r.InfosetOtherTextsCount,
        OtherTextsStatusCode = r.InfosetOtherTextsStatusCode,
        OtherTextsStatusName = r.InfosetOtherTextsStatusName,
        LocationsCount = r.InfosetLocationsCount,
        LocationsStatusCode = r.InfosetLocationsStatusCode,
        LocationsStatusName = r.InfosetLocationsStatusName,
        DescriptionsCount = r.InfosetDescriptionsCount,
        DescriptionsStatusCode = r.InfosetDescriptionsStatusCode,
        DescriptionsStatusName = r.InfosetDescriptionsStatusName,
        ProvenancesCount = r.InfosetProvenancesCount,
        ProvenancesStatusCode = r.InfosetProvenancesStatusCode,
        ProvenancesStatusName = r.InfosetProvenancesStatusName,
        DistributionsCount = r.InfosetDistributionsCount,
        DistributionsStatusCode = r.InfosetDistributionsStatusCode,
        DistributionsStatusName = r.InfosetDistributionsStatusName,
        FairMetricsCount = r.InfosetFairMetricsCount,
        FairMetricsStatusCode = r.InfosetFairMetricsStatusCode,
        FairMetricsStatusName = r.InfosetFairMetricsStatusName,
        NexusSnapshotsCount = r.InfosetNexusSnapshotsCount,
        NexusSnapshotsStatusCode = r.InfosetNexusSnapshotsStatusCode,
        NexusSnapshotsStatusName = r.InfosetNexusSnapshotsStatusName
      };
    return rows;
  }

} // end class

// end file