// SqldbcUilResrepLeafView.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kendo.Mvc.Extensions;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores
{
  public static partial class NpdsLinqSqlOperators
  {
    public static NexusResrepViewModel ToViewable(this NexusResrepLeaf r, Guid agentGuidRef = default)
    {
      var nre = new NexusResrepViewModel()
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
        // EntityInitialTag = r.EntityInitialTag,
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        //EntityOwnerGuid = r.EntityOwnerGuidRef,
        //EntityOwnerLabel = r.EntityOwnerLabel,
        //EntityContactGuid = r.EntityOwnerGuidRef,
        //EntityContactLabel = r.EntityOwnerLabel,
        //EntityOtherGuid = r.EntityOwnerGuidRef,
        //EntityOtherLabel = r.EntityOwnerLabel,
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        InfosetPortalStatusCode = r.InfosetPortalStatusCode,
        InfosetPortalStatusName = r.InfosetPortalStatusName,
        InfosetDoorsStatusCode = r.InfosetDoorsStatusCode,
        InfosetDoorsStatusName = r.InfosetDoorsStatusName,
        RecordDiristryGuid = r.RecordDiristryGuidRef,
        //    RecordDiristryName = r.RecordDiristryName,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        //   RecordRegistryName = r.RecordRegistryName,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        //   RecordDirectoryName = r.RecordDirectoryName,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        //    RecordRegistrarName = r.RecordRegistrarName,
        //ResrepEntityStatusCode = r.InfosetResrepEntityStatusCode,
        //ResrepEntityStatusName = r.InfosetResrepEntityStatusName,
        //ResrepRecordStatusCode = r.InfosetResrepRecordStatusCode,
        //ResrepRecordStatusName = r.InfosetResrepRecordStatusName,
        //ResrepInfosetStatusCode = r.InfosetResrepInfosetStatusCode,
        //ResrepInfosetStatusName = r.InfosetResrepInfosetStatusName,
        //EntityLabelsCount = r.InfosetEntityLabelsCount,
        //EntityLabelsStatusCode = r.InfosetEntityLabelsStatusCode,
        //EntityLabelsStatusName = r.InfosetEntityLabelsStatusName,
        //SupportingTagsCount = r.InfosetSupportingTagsCount,
        //SupportingTagsStatusCode = r.InfosetSupportingTagsStatusCode,
        //SupportingTagsStatusName = r.InfosetSupportingTagsStatusName,
        //SupportingLabelsCount = r.InfosetSupportingLabelsCount,
        //SupportingLabelsStatusCode = r.InfosetSupportingLabelsStatusCode,
        //SupportingLabelsStatusName = r.InfosetSupportingLabelsStatusName,
        //CrossReferencesCount = r.InfosetCrossReferencesCount,
        //CrossReferencesStatusCode = r.InfosetCrossReferencesStatusCode,
        //CrossReferencesStatusName = r.InfosetCrossReferencesStatusName,
        //OtherTextsCount = r.InfosetOtherTextsCount,
        //OtherTextsStatusCode = r.InfosetOtherTextsStatusCode,
        //OtherTextsStatusName = r.InfosetOtherTextsStatusName,
        //LocationsCount = r.InfosetLocationsCount,
        //LocationsStatusCode = r.InfosetLocationsStatusCode,
        //LocationsStatusName = r.InfosetLocationsStatusName,
        //DescriptionsCount = r.InfosetDescriptionsCount,
        //DescriptionsStatusCode = r.InfosetDescriptionsStatusCode,
        //DescriptionsStatusName = r.InfosetDescriptionsStatusName,
        //ProvenancesCount = r.InfosetProvenancesCount,
        //ProvenancesStatusCode = r.InfosetProvenancesStatusCode,
        //ProvenancesStatusName = r.InfosetProvenancesStatusName,
        //DistributionsCount = r.InfosetDistributionsCount,
        //DistributionsStatusCode = r.InfosetDistributionsStatusCode,
        //DistributionsStatusName = r.InfosetDistributionsStatusName,
        //FairMetricsCount = r.InfosetFairMetricsCount,
        //FairMetricsStatusCode = r.InfosetFairMetricsStatusCode,
        //FairMetricsStatusName = r.InfosetFairMetricsStatusName,
        //NexusSnapshotsCount = r.InfosetNexusSnapshotsCount,
        //NexusSnapshotsStatusCode = r.InfosetNexusSnapshotsStatusCode,
        //NexusSnapshotsStatusName = r.InfosetNexusSnapshotsStatusName
      };
      return nre;
    }

    public static IQueryable<NexusResrepViewModel> ToViewable(this IQueryable<NexusResrepLeaf> qry, Guid agentGuidRef = default)
    {
      IQueryable<NexusResrepViewModel> rows =
        from r in qry
        select new NexusResrepViewModel
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
          // EntityInitialTag = r.EntityInitialTag,
          EntityName = r.EntityName,
          EntityNature = r.EntityNature,
          //EntityOwnerGuid = r.EntityOwnerGuidRef,
          //EntityOwnerLabel = r.EntityOwnerLabel,
          //EntityContactGuid = r.EntityOwnerGuidRef,
          //EntityContactLabel = r.EntityOwnerLabel,
          //EntityOtherGuid = r.EntityOwnerGuidRef,
          //EntityOtherLabel = r.EntityOwnerLabel,
          InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
          InfosetIsAgentShared = r.InfosetIsAgentShared,
          InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
          InfosetIsManagerReleased = r.InfosetIsManagerReleased,
          InfosetPortalStatusCode = r.InfosetPortalStatusCode,
          InfosetPortalStatusName = r.InfosetPortalStatusName,
          InfosetDoorsStatusCode = r.InfosetDoorsStatusCode,
          InfosetDoorsStatusName = r.InfosetDoorsStatusName,
          RecordDiristryGuid = r.RecordDiristryGuidRef,
          //    RecordDiristryName = r.RecordDiristryName,
          RecordRegistryGuid = r.RecordRegistryGuidRef,
          //    RecordRegistryName = r.RecordRegistryName,
          RecordDirectoryGuid = r.RecordDirectoryGuidRef,
          //   RecordDirectoryName = r.RecordDirectoryName,
          RecordRegistrarGuid = r.RecordRegistrarGuidRef,
          //    RecordRegistrarName = r.RecordRegistrarName,
          //ResrepEntityStatusCode = r.InfosetResrepEntityStatusCode,
          //ResrepEntityStatusName = r.InfosetResrepEntityStatusName,
          //ResrepRecordStatusCode = r.InfosetResrepRecordStatusCode,
          //ResrepRecordStatusName = r.InfosetResrepRecordStatusName,
          //ResrepInfosetStatusCode = r.InfosetResrepInfosetStatusCode,
          //ResrepInfosetStatusName = r.InfosetResrepInfosetStatusName,
          //EntityLabelsCount = r.InfosetEntityLabelsCount,
          //EntityLabelsStatusCode = r.InfosetEntityLabelsStatusCode,
          //EntityLabelsStatusName = r.InfosetEntityLabelsStatusName,
          //SupportingTagsCount = r.InfosetSupportingTagsCount,
          //SupportingTagsStatusCode = r.InfosetSupportingTagsStatusCode,
          //SupportingTagsStatusName = r.InfosetSupportingTagsStatusName,
          //SupportingLabelsCount = r.InfosetSupportingLabelsCount,
          //SupportingLabelsStatusCode = r.InfosetSupportingLabelsStatusCode,
          //SupportingLabelsStatusName = r.InfosetSupportingLabelsStatusName,
          //CrossReferencesCount = r.InfosetCrossReferencesCount,
          //CrossReferencesStatusCode = r.InfosetCrossReferencesStatusCode,
          //CrossReferencesStatusName = r.InfosetCrossReferencesStatusName,
          //OtherTextsCount = r.InfosetOtherTextsCount,
          //OtherTextsStatusCode = r.InfosetOtherTextsStatusCode,
          //OtherTextsStatusName = r.InfosetOtherTextsStatusName,
          //LocationsCount = r.InfosetLocationsCount,
          //LocationsStatusCode = r.InfosetLocationsStatusCode,
          //LocationsStatusName = r.InfosetLocationsStatusName,
          //DescriptionsCount = r.InfosetDescriptionsCount,
          //DescriptionsStatusCode = r.InfosetDescriptionsStatusCode,
          //DescriptionsStatusName = r.InfosetDescriptionsStatusName,
          //ProvenancesCount = r.InfosetProvenancesCount,
          //ProvenancesStatusCode = r.InfosetProvenancesStatusCode,
          //ProvenancesStatusName = r.InfosetProvenancesStatusName,
          //DistributionsCount = r.InfosetDistributionsCount,
          //DistributionsStatusCode = r.InfosetDistributionsStatusCode,
          //DistributionsStatusName = r.InfosetDistributionsStatusName,
          //FairMetricsCount = r.InfosetFairMetricsCount,
          //FairMetricsStatusCode = r.InfosetFairMetricsStatusCode,
          //FairMetricsStatusName = r.InfosetFairMetricsStatusName,
          //NexusSnapshotsCount = r.InfosetNexusSnapshotsCount,
          //NexusSnapshotsStatusCode = r.InfosetNexusSnapshotsStatusCode,
          //NexusSnapshotsStatusName = r.InfosetNexusSnapshotsStatusName
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IQueryable<NexusResrepLeaf> QueryStorableResrepLeafByRKey(Guid guidKey)
    {
      IQueryable<NexusResrepLeaf> qry = this.NexusResrepLeafs;
      qry = qry.Where(r => (r.RecordGuidKey == guidKey));
      return qry;
    }
    public IQueryable<NexusResrepLeaf> QueryStorableResrepLeafByIKey(Guid guidKey)
    {
      IQueryable<NexusResrepLeaf> qry = this.NexusResrepLeafs;
      qry = qry.Where(r => (r.InfosetGuidKey == guidKey));
      return qry;
    }

    public NexusResrepLeaf GetStorableResrepLeafByRKey(Guid guidKey)
    { return QueryStorableResrepLeafByRKey(guidKey).SingleOrDefault(); }
    public NexusResrepLeaf GetStorableResrepLeafByIKey(Guid guidKey)
    { return QueryStorableResrepLeafByIKey(guidKey).SingleOrDefault(); }

    public IEnumerable<NexusResrepLeaf> ListStorableResrepLeafs()
    {
      IQueryable<NexusResrepLeaf> query = InitQueryStorableResrepLeaf();
      IEnumerable<NexusResrepLeaf> list = query.ToList();
      return list;
    }
    public IEnumerable<NexusResrepLeaf> ListStorableResrepLeafsWithFacets(IQueryable<NexusResrepLeaf>? query = null)
    {
      var listCount = PRC.ListCount;
      if (query == null) { query = InitQueryStorableResrepLeaf(); }
      query = query.Select((NexusResrepLeaf nr) => nr)
        .OrderBy((NexusResrepLeaf nr) => nr.EntityName).Take(listCount)
        .Include((NexusResrepLeaf nr) => nr.NexusEntityLabels)
        .Include((NexusResrepLeaf nr) => nr.NexusSupportingTags)
        .Include((NexusResrepLeaf nr) => nr.NexusSupportingLabels)
        .Include((NexusResrepLeaf nr) => nr.NexusCrossReferences)
        .Include((NexusResrepLeaf nr) => nr.NexusOtherTexts)
        .Include((NexusResrepLeaf nr) => nr.NexusLocations)
        .Include((NexusResrepLeaf nr) => nr.NexusDescriptions)
        .Include((NexusResrepLeaf nr) => nr.NexusProvenances)
        .Include((NexusResrepLeaf nr) => nr.NexusDistributions)
        .Include((NexusResrepLeaf nr) => nr.NexusFairMetrics);
      IEnumerable<NexusResrepLeaf> list = query.ToList();
      return list;
    }

    public NexusResrepViewModel GetViewableResrepLeafByRKey(Guid guidKey)
    { return QueryStorableResrepLeafByRKey(guidKey).ToViewable().SingleOrDefault(); }
    public Task<NexusResrepViewModel?> GetViewableResrepLeafByRKeyAsync(Guid guidKey)
    { return QueryStorableResrepLeafByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
    public NexusResrepViewModel GetViewableResrepLeafByIKey(Guid guidKey)
    { return QueryStorableResrepLeafByIKey(guidKey).ToViewable().SingleOrDefault(); }
    public Task<NexusResrepViewModel?> GetViewableResrepLeafByIKeyAsync(Guid guidKey)
    { return QueryStorableResrepLeafByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
    public NexusResrepViewModel GetViewableResrepLeafByKey(Guid guidKey, bool isInfosetKey = false)
    {
      IQueryable<NexusResrepLeaf> qry;
      if (isInfosetKey) // InfosetGuidKey
      {
        qry = QueryStorableResrepLeafByIKey(guidKey);
      }
      else // ResrepRGuid
      {
        qry = QueryStorableResrepLeafByRKey(guidKey);
      }
      NexusResrepViewModel row = qry.ToViewable().SingleOrDefault();
      return row;
    }

  } // class

} // namespace
