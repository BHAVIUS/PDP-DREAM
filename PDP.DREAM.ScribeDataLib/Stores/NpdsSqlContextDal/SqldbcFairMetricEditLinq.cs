// SqldbcUilFairMetricEditLinq.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<FairMetricEditModel> ToEditable(this IQueryable<NexusFairMetric> query)
  {
    IQueryable<FairMetricEditModel> rows =
      from r in query
      select new FairMetricEditModel
      {
        RRFgroupGuid = r.FgroupGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        ManagedByAgentGuid = r.ManagedByAgentGuidRef,
        ManagedByAgentName = r.ManagedByAgentUserName,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuidRef,
        CreatedByAgentName = r.CreatedByAgentUserName,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
        UpdatedByAgentName = r.UpdatedByAgentUserName,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuidRef,
        DeletedByAgentName = r.DeletedByAgentUserName,
        //
        MInvalidOldClaim = r.MInvalidOldClaim,
        QValidOldClaim = r.QValidOldClaim,
        PInvalidNewClaim = r.PInvalidNewClaim,
        NValidNewClaim = r.NValidNewClaim,
        FAIR1 = r.FAIR1,
        FAIR2 = r.FAIR2,
        FAIR3 = r.FAIR3,
        FAIR4 = r.FAIR4
      };
    return rows;
  }

}

