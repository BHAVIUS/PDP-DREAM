using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static RestrictionOrViewModel ToViewable(this NexusServiceRestrictionOr r)
    {
      var nre = new RestrictionOrViewModel()
      {
        RRRecordGuid = r.RecordGuidRef,
        RestrictionOrGuidKey = r.RestrictionOrGuidKey,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        //
        RestrictionAndGuidRef = r.RestrictionAndGuidRef,
        RestrictionOrIndex = r.OrHasIndex,
        RestrictionOrPriority = r.OrHasPriority,
        Restriction = r.RestrictionValue,
        IsWordPhrase = r.IsWordPhrase,
        IsConceptLabel = r.IsConceptLabel
      };
      return nre;
    }

    public static IQueryable<RestrictionOrViewModel> ToViewable(this IQueryable<NexusServiceRestrictionOr> query)
    {
      IQueryable<RestrictionOrViewModel> rows =
        from r in query
        select new RestrictionOrViewModel
        {
          RRRecordGuid = r.RecordGuidRef,
          RestrictionOrGuidKey = r.RestrictionOrGuidKey,
          CreatedOn = r.CreatedOn,
          CreatedByAgentGuid = r.CreatedByAgentGuid,
          UpdatedOn = r.UpdatedOn,
          UpdatedByAgentGuid = r.UpdatedByAgentGuid,
          //
          RestrictionAndGuidRef = r.RestrictionAndGuidRef,
          RestrictionOrIndex = r.OrHasIndex,
          RestrictionOrPriority = r.OrHasPriority,
          Restriction = r.RestrictionValue,
          IsWordPhrase = r.IsWordPhrase,
          IsConceptLabel = r.IsConceptLabel,
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<RestrictionOrViewModel> ListViewableRestrictionOrsByAnd(Guid? andGuidKey)
    {
      IEnumerable<RestrictionOrViewModel> result;
      try
      {
        IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
        qry = qry.Where(r => (r.RestrictionAndGuidRef== andGuidKey));
        result = qry.OrderBy(r => r.OrHasIndex).ToViewable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<RestrictionOrViewModel>();
      }
      return result;
    }

    public IEnumerable<RestrictionOrViewModel> ListViewableRestrictionOrs(Guid? infosetGuid)
    {
      IEnumerable<RestrictionOrViewModel> result;
      try
      {
        IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
        qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid));
        result = qry.OrderBy(r => r.AndHasIndex).ThenBy(r => r.OrHasIndex).ToViewable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<RestrictionOrViewModel>();
      }
      return result;
    }

    public IEnumerable<RestrictionOrViewModel> ListViewableRestrictionOrs(Guid? infosetGuid, bool isLabel)
    {
      IEnumerable<RestrictionOrViewModel> result;
      try
      {
        IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
        qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid) && (r.IsConceptLabel == isLabel));
        result = qry.OrderBy(r => r.AndHasIndex).ThenBy(r => r.OrHasIndex).ToViewable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<RestrictionOrViewModel>();
      }
      return result;
    }

    public IEnumerable<RestrictionOrViewModel> ListViewableRestrictionOrs(Guid? infosetGuid, bool isLabel, bool isPhrase)
    {
      IEnumerable<RestrictionOrViewModel> result;
      try
      {
        IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
        qry = qry.Where(r => (r.InfosetGuidRef == infosetGuid) && (r.IsConceptLabel == isLabel) && (r.IsWordPhrase == isPhrase));
        result = qry.OrderBy(r => r.AndHasIndex).ThenBy(r => r.OrHasIndex).ToViewable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<RestrictionOrViewModel>();
      }
      return result;
    }

  }

}
