// SqldbcUilResrepRootView.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static NexusResrepViewModel ToViewable(this NexusResrepRoot r, Guid agentGuidRef = default)
  {
    var nre = new NexusResrepViewModel()
    {
      AgentGuid = agentGuidRef,
      RRRecordGuid = r.RecordGuidKey,
      RRInfosetGuid = r.InfosetGuidKey,
      RecordHandle = r.RecordHandle,
      RecordIsDeleted = r.RecordIsDeleted,
      ManagedByAgentGuid = r.RecordManagedByAgentGuidRef,
      CreatedOn = r.RecordCreatedOn,
      CreatedByAgentGuid = r.RecordCreatedByAgentGuidRef,
      UpdatedOn = r.RecordUpdatedOn,
      UpdatedByAgentGuid = r.RecordUpdatedByAgentGuidRef,
      DeletedOn = r.RecordDeletedOn,
      DeletedByAgentGuid = r.RecordDeletedByAgentGuidRef,
      //
      EntityTypeCode = r.EntityTypeCodeRef,
      EntityTypeName = r.EntityTypeName,
      EntityName = r.EntityName,
      EntityNature = r.EntityNature,
      InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
      InfosetIsAgentShared = r.InfosetIsAgentShared,
      InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
      InfosetIsManagerReleased = r.InfosetIsManagerReleased,
      RecordDiristryGuid = r.RecordDiristryGuidRef,
      RecordDiristryName = r.RecordDiristryTag,
      RecordRegistryGuid = r.RecordRegistryGuidRef,
      RecordRegistryName = r.RecordRegistryTag,
      RecordDirectoryGuid = r.RecordDirectoryGuidRef,
      RecordDirectoryName = r.RecordDirectoryTag,
      RecordRegistrarGuid = r.RecordRegistrarGuidRef,
      RecordRegistrarName = r.RecordRegistrarTag
    };
    return nre;
  }

  public static IQueryable<NexusResrepViewModel> ToViewable(this IQueryable<NexusResrepRoot> qry, Guid agentGuidRef = default)
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
        CreatedOn = r.RecordCreatedOn,
        CreatedByAgentGuid = r.RecordCreatedByAgentGuidRef,
        UpdatedOn = r.RecordUpdatedOn,
        UpdatedByAgentGuid = r.RecordUpdatedByAgentGuidRef,
        DeletedOn = r.RecordDeletedOn,
        DeletedByAgentGuid = r.RecordDeletedByAgentGuidRef,
          //
          EntityTypeCode = r.EntityTypeCodeRef,
        EntityTypeName = r.EntityTypeName,
        EntityName = r.EntityName,
        EntityNature = r.EntityNature,
        InfosetIsAuthorPrivate = r.InfosetIsAuthorPrivate,
        InfosetIsAgentShared = r.InfosetIsAgentShared,
        InfosetIsUpdaterLimited = r.InfosetIsUpdaterLimited,
        InfosetIsManagerReleased = r.InfosetIsManagerReleased,
        RecordDiristryGuid = r.RecordDiristryGuidRef,
        RecordDiristryName = r.RecordDiristryTag,
        RecordRegistryGuid = r.RecordRegistryGuidRef,
        RecordRegistryName = r.RecordRegistryTag,
        RecordDirectoryGuid = r.RecordDirectoryGuidRef,
        RecordDirectoryName = r.RecordDirectoryTag,
        RecordRegistrarGuid = r.RecordRegistrarGuidRef,
        RecordRegistrarName = r.RecordRegistrarTag
      };
    return rows;
  }

}

public partial class NexusDbsqlContext
{
  public IQueryable<NexusResrepRoot> QueryStorableResrepRootByRKey(Guid guidKey)
  {
    IQueryable<NexusResrepRoot> qry = this.NexusResrepRoots;
    qry = qry.Where(r => (r.RecordGuidKey == guidKey));
    return qry;
  }
  public IQueryable<NexusResrepRoot> QueryStorableResrepRootByIKey(Guid guidKey)
  {
    IQueryable<NexusResrepRoot> qry = this.NexusResrepRoots;
    qry = qry.Where(r => (r.InfosetGuidKey == guidKey));
    return qry;
  }

  public NexusResrepRoot GetStorableResrepRootByRKey(Guid guidKey)
  { return QueryStorableResrepRootByRKey(guidKey).SingleOrDefault(); }
  public NexusResrepRoot GetStorableResrepRootByIKey(Guid guidKey)
  { return QueryStorableResrepRootByIKey(guidKey).SingleOrDefault(); }

  public IEnumerable<NexusResrepRoot> ListStorableResrepRoots()
  {
    IQueryable<NexusResrepRoot> qry = InitQueryStorableResrepRoot();
    IEnumerable<NexusResrepRoot> lst = qry.ToList();
    return lst;
  }
  public IEnumerable<NexusResrepRoot> ListStorableResrepRootsWithFacets(IQueryable<NexusResrepRoot>? query = null)
  {
    var listCount = PRC.ListCount;
    if (query == null) { query = InitQueryStorableResrepRoot(); }
    query = query.Select((NexusResrepRoot nr) => nr)
      .OrderBy((NexusResrepRoot nr) => nr.EntityName).Take(listCount)
      .Include((NexusResrepRoot nr) => nr.NexusEntityLabels)
      .Include((NexusResrepRoot nr) => nr.NexusSupportingTags)
      .Include((NexusResrepRoot nr) => nr.NexusSupportingLabels)
      .Include((NexusResrepRoot nr) => nr.NexusCrossReferences)
      .Include((NexusResrepRoot nr) => nr.NexusOtherTexts)
      .Include((NexusResrepRoot nr) => nr.NexusLocations)
      .Include((NexusResrepRoot nr) => nr.NexusDescriptions)
      .Include((NexusResrepRoot nr) => nr.NexusProvenances)
      .Include((NexusResrepRoot nr) => nr.NexusDistributions)
      .Include((NexusResrepRoot nr) => nr.NexusFairMetrics);
    IEnumerable<NexusResrepRoot> list = query.ToList();
    return list;
  }

  public NexusResrepViewModel GetViewableResrepRootByRKey(Guid guidKey)
  { return QueryStorableResrepRootByRKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepRootByRKeyAsync(Guid guidKey)
  { return QueryStorableResrepRootByRKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel GetViewableResrepRootByIKey(Guid guidKey)
  { return QueryStorableResrepRootByIKey(guidKey).ToViewable().SingleOrDefault(); }
  public Task<NexusResrepViewModel?> GetViewableResrepRootByIKeyAsync(Guid guidKey)
  { return QueryStorableResrepRootByIKey(guidKey).ToViewable().SingleOrDefaultAsync(); }
  public NexusResrepViewModel GetViewableResrepRootByKey(Guid guidKey, bool isInfosetKey = false)
  {
    IQueryable<NexusResrepRoot> qry;
    if (isInfosetKey) // InfosetGuidKey
    {
      qry = QueryStorableResrepRootByIKey(guidKey);
    }
    else // ResrepRGuid
    {
      qry = QueryStorableResrepRootByRKey(guidKey);
    }
    NexusResrepViewModel row = qry.ToViewable().SingleOrDefault();
    return row;
  }

  public IEnumerable<NexusResrepViewModel> ListViewableResrepRoots(DataSourceRequest dsRequest, out int listCount)
  {
    var pageSize = dsRequest.PageSize;
    var pageNumber = dsRequest.Page;
    IQueryable<NexusResrepRoot> query = InitQueryStorableResrepRoot();
    foreach (FilterDescriptor filterDescriptor in dsRequest.Filters)
    {
      var filterMember = filterDescriptor.Member;
      var filterOperator = filterDescriptor.Operator;
      var filterValue = filterDescriptor.Value;
      var filterValueConverted = filterDescriptor.ConvertedValue;
      switch (filterMember)
      {
        case "RecordHandle":
          if (filterOperator == FilterOperator.IsEqualTo)
          { query = query.Where(rr => rr.RecordHandle.Equals((string)filterValue)); }
          else
          { query = query.Where(rr => rr.RecordHandle.Contains((string)filterValue)); }
          break;
        case "EntityTypeCode":
          if (filterOperator == FilterOperator.IsEqualTo)
          { query = query.Where(rr => rr.EntityTypeName.Equals((string)filterValue)); }
          else
          { query = query.Where(rr => rr.EntityTypeName.Contains((string)filterValue)); }
          break;
        case "EntityName":
          if (filterOperator == FilterOperator.IsEqualTo)
          { query = query.Where(rr => rr.EntityName.Equals((string)filterValue)); }
          else
          { query = query.Where(rr => rr.EntityName.Contains((string)filterValue)); }
          break;
        case "EntityNature":
          if (filterOperator == FilterOperator.IsEqualTo)
          { query = query.Where(rr => rr.EntityNature.Equals((string)filterValue)); }
          else
          { query = query.Where(rr => rr.EntityNature.Contains((string)filterValue)); }
          break;
        case "UpdatedOn":
          if (filterOperator == FilterOperator.IsEqualTo)
          { query = query.Where(rr => (rr.RecordUpdatedOn.Value == (DateTime)filterValue)); }
          else if (filterOperator == FilterOperator.IsGreaterThanOrEqualTo)
          { query = query.Where(rr => (rr.RecordUpdatedOn.Value >= (DateTime)filterValue)); }
          else if (filterOperator == FilterOperator.IsLessThanOrEqualTo)
          { query = query.Where(rr => (rr.RecordUpdatedOn.Value <= (DateTime)filterValue)); }
          break;
        default:
          break;
      }
    }
    listCount = (from NexusResrepRoot rr in query select rr).Count();
    if (listCount > 0)
    {
      if (dsRequest.Sorts.Count > 0)
      {
        var sortMember = dsRequest.Sorts[0].Member;
        var sortDirection = dsRequest.Sorts[0].SortDirection;
        switch (sortMember)
        {
          case "RecordHandle":
            if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
            { query = query.OrderBy(rr => rr.RecordHandle); }
            else
            { query = query.OrderByDescending(rr => rr.RecordHandle); }
            break;
          case "EntityTypeCode":
            if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
            { query = query.OrderBy(rr => rr.EntityTypeName); }
            else
            { query = query.OrderByDescending(rr => rr.EntityTypeName); }
            break;
          case "EntityName":
            if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
            { query = query.OrderBy(rr => rr.EntityName); }
            else
            { query = query.OrderByDescending(rr => rr.EntityName); }
            break;
          case "EntityNature":
            if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
            { query = query.OrderBy(rr => rr.EntityNature); }
            else
            { query = query.OrderByDescending(rr => rr.EntityNature); }
            break;
          case "UpdatedOn":
            if (sortDirection == Kendo.Mvc.ListSortDirection.Ascending)
            { query = query.OrderBy(rr => rr.RecordUpdatedOn); }
            else
            { query = query.OrderByDescending(rr => rr.RecordUpdatedOn); }
            break;
          default:
            break;
        }
      }
      if (pageSize > 0)
      {
        if (pageNumber > 1)
        {
          query = query.Skip(pageSize * (pageNumber - 1));
        }
        query = query.Take(pageSize);
      }
    }
    var agentGuid = PRC.AgentGuid;
    IEnumerable<NexusResrepViewModel> list = query.ToViewable(agentGuid).ToList();
    return list;
  }

}
