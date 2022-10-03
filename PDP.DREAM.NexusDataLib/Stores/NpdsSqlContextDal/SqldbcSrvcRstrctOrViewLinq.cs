// SqldbcUilSrvcRstrctOrViewLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<ServiceRestrictionOrViewModel> ToViewable(this IQueryable<NexusServiceRestrictionOr> query)
  {
    IQueryable<ServiceRestrictionOrViewModel> rows =
      from r in query
      select new ServiceRestrictionOrViewModel
      {
        RRInfosetGuid = r.InfosetGuidRef,
        RRRecordGuid = r.RecordGuidRef,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        //
        RestrictionAndGuid = r.RestrictionAndGuidRef,
        RestrictionAndHasIndex = r.AndHasIndex,
        RestrictionAndHasPriority = r.AndHasPriority,
        RestrictionName = r.RestrictionName,
        RestrictionOrGuid = r.RestrictionOrGuidKey,
        RestrictionOrHasIndex = r.OrHasIndex,
        RestrictionOrHasPriority = r.OrHasPriority,
        RestrictionValue = r.RestrictionValue,
        IsWordPhrase = r.IsWordPhrase,
        IsConceptLabel = r.IsConceptLabel,
      };
    return rows;
  }

} // end class

// end file