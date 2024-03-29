﻿// SqldbcUilResrepRootEditList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.UI; // do not use global because of conflict on System.Data.CommandType

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // ListEditables //
  public IList<NexusResrepEditModel?> ListEditableResrepRoots()
  {
    IQueryable<INexusResrepRoot> query = InitDataQueryStorableNexusRoot();
    IList<NexusResrepEditModel?> list = query.ToEditable().ToList();
    return list;
  }
  public IList<NexusResrepEditModel?> ListEditableResrepRoots(int pageSize, int pageNumber, out int listCount)
  {
    IQueryable<INexusResrepRoot> query = InitDataQueryStorableNexusRoot();
    listCount = (from INexusResrepRoot rr in query select rr).Count();
    if (pageSize > 0)
    {
      if (pageNumber > 1)
      {
        query = query.Skip(pageSize * (pageNumber - 1));
      }
      query = query.Take(pageSize);
    }
    IList<NexusResrepEditModel?> list = query.ToEditable().ToList();
    return list;
  }
  public IList<NexusResrepEditModel?> ListEditableResrepRoots(DataSourceRequest dsRequest, out int listCount)
  {
    IList<NexusResrepEditModel?> resreps;
    var pageSize = dsRequest.PageSize;
    var pageNumber = dsRequest.Page;
    IQueryable<INexusResrepRoot> query = InitDataQueryStorableNexusRoot();
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
    listCount = (from INexusResrepRoot rr in query select rr).Count();
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
    var agentGuid = NPDSCP.ClientAgentGuid;
    resreps = query.ToEditable(agentGuid).ToList();
    return resreps;
  }

  // ListStorables //

} // end class

// end file