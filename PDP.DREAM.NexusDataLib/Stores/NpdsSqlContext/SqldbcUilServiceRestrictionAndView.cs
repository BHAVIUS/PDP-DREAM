// SqldbcUilServiceRestrictionAndView.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static RestrictionAndViewModel ToViewable(this NexusServiceRestrictionAnd r)
  {
    var nre = new RestrictionAndViewModel()
    {
      RRRecordGuid = r.RecordGuidRef,
      RRInfosetGuid = r.InfosetGuidRef,
      RestrictionAndGuidKey = r.RestrictionAndGuidKey,
      CreatedOn = r.CreatedOn,
      CreatedByAgentGuid = r.CreatedByAgentGuid,
      UpdatedOn = r.UpdatedOn,
      UpdatedByAgentGuid = r.UpdatedByAgentGuid,
      //
      RestrictionName = r.RestrictionName,
      RestrictionAndIndex = r.HasIndex,
      RestrictionIsSufficient = r.IsSufficient
    };
    return nre;
  }

  public static IQueryable<RestrictionAndViewModel> ToViewable(this IQueryable<NexusServiceRestrictionAnd> query)
  {
    IQueryable<RestrictionAndViewModel> rows =
      from r in query
      select new RestrictionAndViewModel
      {
        RRRecordGuid = r.RecordGuidRef,
        RRInfosetGuid = r.InfosetGuidRef,
        RestrictionAndGuidKey = r.RestrictionAndGuidKey,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        //
        RestrictionName = r.RestrictionName,
        RestrictionAndIndex = r.HasIndex,
        RestrictionIsSufficient = r.IsSufficient
      };
    return rows;
  }

}

public partial class NexusDbsqlContext
{
  public IEnumerable<RestrictionAndViewModel> ListViewableRestrictionAndsByIGuid(Guid infosetGuid)
  {
    IEnumerable<RestrictionAndViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid));
      result = qry.OrderBy(r => r.HasIndex).ToViewable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<RestrictionAndViewModel>();
    }
    return result;
  }

  public IEnumerable<RestrictionAndViewModel> ListViewableRestrictionAndsByRGuid(Guid recordGuid)
  {
    IEnumerable<RestrictionAndViewModel> result;
    try
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r => (r.RecordGuidRef == recordGuid));
      result = qry.OrderBy(r => r.HasIndex).ToViewable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<RestrictionAndViewModel>();
    }
    return result;
  }

}
