using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static NexusResrepViewModel ToViewable(this NexusResrepStem r, Guid agentGuidRef = default)
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
        EntityInitialTag = r.EntityInitialTag,
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
        RecordDiristryName = r.RecordDiristryName,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        RecordRegistryName = r.RecordRegistryName,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        RecordDirectoryName = r.RecordDirectoryName,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        RecordRegistrarName = r.RecordRegistrarName,
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

    public static IQueryable<NexusResrepViewModel> ToViewable(this IQueryable<NexusResrepStem> qry, Guid agentGuidRef = default)
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
          EntityInitialTag = r.EntityInitialTag,
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
          RecordDiristryName = r.RecordDiristryName,
          RecordRegistryGuid = r.RecordRegistryGuidRef,
          RecordRegistryName = r.RecordRegistryName,
          RecordDirectoryGuid = r.RecordDirectoryGuidRef,
          RecordDirectoryName = r.RecordDirectoryName,
          RecordRegistrarGuid = r.RecordRegistrarGuidRef,
          RecordRegistrarName = r.RecordRegistrarName,
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
    public IQueryable<NexusResrepStem> QueryStorableResrepStemByRKey(Guid guidKey)
    {
      IQueryable<NexusResrepStem> qry = this.NexusResrepStems;
      qry = qry.Where(r => (r.RecordGuidKey == guidKey));
      return qry;
    }
    public IQueryable<NexusResrepStem> QueryStorableResrepStemByIKey(Guid guidKey)
    {
      IQueryable<NexusResrepStem> qry = this.NexusResrepStems;
      qry = qry.Where(r => (r.InfosetGuidKey == guidKey));
      return qry;
    }

    public NexusResrepStem GetStorableResrepStemByRKey(Guid guidKey)
    { return QueryStorableResrepStemByRKey(guidKey).SingleOrDefault(); }
    public NexusResrepStem GetStorableResrepStemByIKey(Guid guidKey)
    { return QueryStorableResrepStemByIKey(guidKey).SingleOrDefault(); }

    public IEnumerable<NexusResrepStem> ListStorableResrepStems(IQueryable<NexusResrepStem>? query = null)
    {
      IEnumerable<NexusResrepStem> theList;
      var listCount = PRC.ListCount;
      if (query == null) { query = InitQueryStorableResrepStem(); }
      query = query.Select((NexusResrepStem nr) => nr)
        .OrderBy((NexusResrepStem nr) => nr.EntityName).Take(listCount);
      theList = query.ToList();
      return theList;
    }
    public IEnumerable<NexusResrepStem> ListStorableResrepStemsWithFacets(IQueryable<NexusResrepStem>? query = null)
    {
      IEnumerable<NexusResrepStem> theList;
      var listCount = PRC.ListCount;
      if (query == null) { query = InitQueryStorableResrepStem(); }
      query = query.Select((NexusResrepStem nr) => nr)
        .OrderBy((NexusResrepStem nr) => nr.EntityName).Take(listCount)
        .Include((NexusResrepStem nr) => nr.NexusEntityLabels)
        .Include((NexusResrepStem nr) => nr.NexusSupportingTags)
        .Include((NexusResrepStem nr) => nr.NexusSupportingLabels)
        .Include((NexusResrepStem nr) => nr.NexusCrossReferences)
        .Include((NexusResrepStem nr) => nr.NexusOtherTexts)
        .Include((NexusResrepStem nr) => nr.NexusLocations)
        .Include((NexusResrepStem nr) => nr.NexusDescriptions)
        .Include((NexusResrepStem nr) => nr.NexusProvenances)
        .Include((NexusResrepStem nr) => nr.NexusDistributions)
        .Include((NexusResrepStem nr) => nr.NexusFairMetrics);
      theList = query.ToList();
      return theList;
    }

    public NexusResrepViewModel GetViewableResrepStemByRKey(Guid guidKey)
    { return QueryStorableResrepStemByRKey(guidKey).ToViewable().SingleOrDefault(); }
    public Task<NexusResrepViewModel?> GetViewableResrepStemByRKeyAsync(Guid guidKey)
    { return QueryStorableResrepStemByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
    public NexusResrepViewModel GetViewableResrepStemByIKey(Guid guidKey)
    { return QueryStorableResrepStemByIKey(guidKey).ToViewable().SingleOrDefault(); }
    public Task<NexusResrepViewModel?> GetViewableResrepStemByIKeyAsync(Guid guidKey)
    { return QueryStorableResrepStemByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
    public NexusResrepViewModel GetViewableResrepStemByKey(Guid guidKey, bool isInfosetKey = false)
    {
      IQueryable<NexusResrepStem> qry;
      if (isInfosetKey) // InfosetGuidKey
      {
        qry = QueryStorableResrepStemByIKey(guidKey);
      }
      else // ResrepRGuid
      {
        qry = QueryStorableResrepStemByRKey(guidKey);
      }
      NexusResrepViewModel row = qry.ToViewable().SingleOrDefault();
      return row;
    }

  } // class

} // namespace
