// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI; // do not use global because of conflict on System.Data.CommandType

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  // ListEditables //
  public IList<CoreResrepRootUxm?> ListEditableResrepRoots()
  {
    IList<CoreResrepRootUxm?> result;
    try
    {
      IQueryable<ICoreResrepRoot> query = QueryStorableResrepRoot();
      result = query.ToEditable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<CoreResrepRootUxm?>().ToList();
    }
    return result;
  }
  public IList<CoreResrepRootUxm?> ListEditableResrepRoots(int pageSize, int pageNumber, out int listCount)
  {
    IList<CoreResrepRootUxm?> result;
    try
    {
      IQueryable<ICoreResrepRoot> query = QueryStorableResrepRoot();
      listCount = (from ICoreResrepRoot rr in query select rr).Count();
      if (pageSize > 0)
      {
        if (pageNumber > 1)
        {
          query = query.Skip(pageSize * (pageNumber - 1));
        }
        query = query.Take(pageSize);
      }
      result = query.ToEditable().ToList();
    }
    catch
    {
      listCount = 0;
      result = Enumerable.Empty<CoreResrepRootUxm?>().ToList();
    }
    return result;
  }
  public IList<CoreResrepRootUxm?> ListEditableResrepRoots(DataSourceRequest dsRequest, out int listCount)
  {
    IList<CoreResrepRootUxm?> result;
    try
    {
      var pageSize = dsRequest.PageSize;
      var pageNumber = dsRequest.Page;
      IQueryable<ICoreResrepRoot> query = QueryStorableResrepRoot();
      foreach (Kendo.Mvc.FilterDescriptor filterDescriptor in dsRequest.Filters)
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
      listCount = (from ICoreResrepRoot rr in query select rr).Count();
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
      var agentGuid = NPDSDC.NpdsAgentGuid;
      result = query.ToEditable(agentGuid).ToList();
    }
    catch
    {
      listCount = 0;
      result = Enumerable.Empty<CoreResrepRootUxm?>().ToList();
    }
    return result;
  }

  // ListStorables //
  public IList<ICoreResrepRoot> ListStorableResrepRoots(IQueryable<ICoreResrepRoot>? query = null, int listCount = 0)
  {
    IList<ICoreResrepRoot> result;
    try
    {
      if (listCount == 0) { listCount = NPDSDC.PageListCount; }
      if (query == null) { query = QueryStorableResrepRoot(); }
      query = query.Select((ICoreResrepRoot rr) => rr)
        .OrderByDescending((ICoreResrepRoot rr) => rr.RecordUpdatedOn)
        .Take(listCount);
      result = query.ToList();
    }
    catch
    {
      listCount = 0;
      result = Enumerable.Empty<ICoreResrepRoot>().ToList();
    }
    return result;
  }
  public IList<ICoreResrepRoot> ListStorableResrepRootsWithFacets(IQueryable<ICoreResrepRoot>? query = null, int listCount = 0)
  {
    IList<ICoreResrepRoot> result;
    try
    {
      if (listCount == 0) { listCount = NPDSDC.PageListCount; }
      if (query == null) { query = QueryStorableResrepRoot(); }
      query = query.Select((ICoreResrepRoot rr) => rr)
        .OrderByDescending((ICoreResrepRoot rr) => rr.RecordUpdatedOn)
        .Take(listCount)
        .Include((ICoreResrepRoot rr) => rr.CoreEntityLabels)
        .Include((ICoreResrepRoot rr) => rr.PortalSupportingTags)
        .Include((ICoreResrepRoot rr) => rr.PortalSupportingLabels)
        .Include((ICoreResrepRoot rr) => rr.PortalCrossReferences)
        .Include((ICoreResrepRoot rr) => rr.PortalOtherTexts)
        .Include((ICoreResrepRoot rr) => rr.DoorsLocations)
        .Include((ICoreResrepRoot rr) => rr.DoorsDescriptions)
        .Include((ICoreResrepRoot rr) => rr.DoorsProvenances)
        .Include((ICoreResrepRoot rr) => rr.DoorsDistributions)
        .Include((ICoreResrepRoot rr) => rr.DoorsFairMetrics);
      result = query.ToList();
    }
    catch
    {
      listCount = 0;
      result = Enumerable.Empty<ICoreResrepRoot>().ToList();
    }
    return result;
  }

} // end class

// end file