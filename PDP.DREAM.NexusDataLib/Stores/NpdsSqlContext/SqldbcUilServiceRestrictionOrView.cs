// SqldbcUilServiceRestrictionOrView.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

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
      qry = qry.Where(r => (r.RestrictionAndGuidRef == andGuidKey));
      result = qry.OrderBy(r => r.OrHasIndex).ToViewable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<RestrictionOrViewModel>();
    }
    return result;
  }

  public IEnumerable<RestrictionOrViewModel> ListViewableRestrictionOrsByIGuid(Guid? infosetGuid, bool isLabel)
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

  public IEnumerable<RestrictionOrViewModel> ListViewableRestrictionOrsByIGuid(Guid? infosetGuid, bool isLabel, bool isPhrase)
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
