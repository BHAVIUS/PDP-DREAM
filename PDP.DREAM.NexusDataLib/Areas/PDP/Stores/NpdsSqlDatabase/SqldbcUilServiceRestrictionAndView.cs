// SqldbcUilServiceRestrictionAndView.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
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
    public IEnumerable<RestrictionAndViewModel> ListViewableRestrictionAnds(Guid recordGuid)
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

  }


}
