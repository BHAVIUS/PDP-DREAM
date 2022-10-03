﻿// SqldbcUilResrepLeafViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.EntityFrameworkCore;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // ListViewables //
  public IList<NexusResrepViewModel?> ListViewableResrepLeafs()
  {
    IQueryable<INexusResrepLeaf> query = InitQueryStorableNexusLeaf();
    IList<NexusResrepViewModel?> list = query.ToViewable().ToList();
    return list;
  }
  public IList<NexusResrepViewModel?> ListViewableResrepLeafs(int pageSize, int pageNumber, out int listCount)
  {
    IQueryable<INexusResrepLeaf> query = InitQueryStorableNexusLeaf();
    listCount = (from INexusResrepLeaf rr in query select rr).Count();
    if (pageSize > 0)
    {
      if (pageNumber > 1)
      {
        query = query.Skip(pageSize * (pageNumber - 1));
      }
      query = query.Take(pageSize);
    }
    IList<NexusResrepViewModel?> list = query.ToViewable().ToList();
    return list;
  }
  public IList<NexusResrepViewModel?> ListViewableResrepLeafs(DataSourceRequest dsRequest, out int listCount)
  {
    IList<NexusResrepViewModel?> resreps;
    var pageSize = dsRequest.PageSize;
    var pageNumber = dsRequest.Page;
    IQueryable<INexusResrepLeaf> query = InitQueryStorableNexusLeaf();
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
    listCount = (from INexusResrepLeaf rr in query select rr).Count();
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
            query = query.OrderByDescending(rr => rr.RecordUpdatedOn);
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
    var agentGuid = QURC.QebAgentGuid;
    resreps = query.ToViewable(agentGuid).ToList();
    return resreps;
  }

  // ListStorables //
  public IList<INexusResrepLeaf> ListStorableNexusLeafs(IQueryable<INexusResrepLeaf>? query = null, int listCount = 0)
  {
    IList<INexusResrepLeaf> resreps;
    if (listCount == 0) { listCount = QURC.ListCount; }
    if (query == null) { query = InitQueryStorableNexusLeaf(); }
    query = query.Select((INexusResrepLeaf rr) => rr)
      .OrderByDescending((INexusResrepLeaf rr) => rr.RecordUpdatedOn)
      .Take(listCount);
    resreps = query.ToList();
    return resreps;
  }
  public IList<INexusResrepLeaf> ListStorableNexusLeafsWithFacets(IQueryable<INexusResrepLeaf>? query = null, int listCount = 0)
  {
    IList<INexusResrepLeaf> resreps;
    if (listCount == 0) { listCount = QURC.ListCount; }
    if (query == null) { query = InitQueryStorableNexusLeaf(); }
    query = query.Select((INexusResrepLeaf rr) => rr)
      .OrderByDescending((INexusResrepLeaf rr) => rr.RecordUpdatedOn)
      .Take(listCount)
      .Include((INexusResrepLeaf rr) => rr.NexusEntityLabels)
      .Include((INexusResrepLeaf rr) => rr.NexusSupportingTags)
      .Include((INexusResrepLeaf rr) => rr.NexusSupportingLabels)
      .Include((INexusResrepLeaf rr) => rr.NexusCrossReferences)
      .Include((INexusResrepLeaf rr) => rr.NexusOtherTexts)
      .Include((INexusResrepLeaf rr) => rr.NexusLocations)
      .Include((INexusResrepLeaf rr) => rr.NexusDescriptions)
      .Include((INexusResrepLeaf rr) => rr.NexusProvenances)
      .Include((INexusResrepLeaf rr) => rr.NexusDistributions)
      .Include((INexusResrepLeaf rr) => rr.NexusFairMetrics);
    resreps = query.ToList();
    return resreps;
  }

} // end class

// end file