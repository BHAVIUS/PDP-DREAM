// SqldbcUilSrvcRstrctOrEditLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Linq;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<ServiceRestrictionOrEditModel> ToEditable(this IQueryable<NexusServiceRestrictionOr> query)
  {
    IQueryable<ServiceRestrictionOrEditModel> rows =
      from r in query
      select new ServiceRestrictionOrEditModel
      {
        RRRecordGuid = r.RecordGuidRef,
        RRInfosetGuid = r.InfosetGuidRef,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuid,
        //
        RestrictionAndGuid = r.RestrictionAndGuidRef,
        RestrictionAndHasIndex = r.AndHasIndex,
        RestrictionAndHasPriority = r.AndHasPriority,
        RestrictionName = r.RestrictionName,
        IsExcluding = r.IsExcluding,
        IsSufficient = r.IsSufficient,
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