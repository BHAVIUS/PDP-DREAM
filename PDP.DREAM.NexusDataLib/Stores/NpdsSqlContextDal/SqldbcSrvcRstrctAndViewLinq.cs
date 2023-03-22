// SqldbcUilSrvcRstrctAndViewLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<ServiceRestrictionAndViewModel> ToViewable(this IQueryable<NexusServiceRestrictionAnd> query)
  {
    IQueryable<ServiceRestrictionAndViewModel> rows =
      from r in query
      select new ServiceRestrictionAndViewModel
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
        RestrictionAndGuid = r.RestrictionAndGuidKey,
        RestrictionAndHasIndex = r.AndHasIndex,
        RestrictionAndHasPriority = r.AndHasPriority,
        RestrictionName = r.RestrictionName,
        IsExcluding = r.IsExcluding,
        IsSufficient = r.IsSufficient,
      };
    return rows;
  }

}
