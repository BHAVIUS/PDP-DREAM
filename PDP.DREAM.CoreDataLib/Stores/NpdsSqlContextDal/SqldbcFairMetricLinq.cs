// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<FairMetricUxm> ToEditable(this IQueryable<DoorsFairMetric> query)
  {
    IQueryable<FairMetricUxm> rows =
      from r in query
      select new FairMetricUxm
      {
        RRFgroupGuid = r.FgroupGuid,
        RRRecordGuid = r.RecordGuid,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        CreatedByAgentAlias = r.CreatedByAgentAlias ?? "",
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        UpdatedByAgentAlias = r.UpdatedByAgentAlias ?? "",
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuid,
        DeletedByAgentAlias = r.DeletedByAgentAlias ?? "",
        //
        ManagedByAgentGuid = r.ManagedByAgentGuid,
        ManagedByAgentAlias = r.ManagedByAgentAlias ?? "",
        //
        MInvalidOldClaim = r.MInvalidOldClaim,
        QValidOldClaim = r.QValidOldClaim,
        PInvalidNewClaim = r.PInvalidNewClaim,
        NValidNewClaim = r.NValidNewClaim,
        FAIR1Q = r.FAIR1Q,
        FAIR2M = r.FAIR2M,
        FAIR3P = r.FAIR3P,
        FAIR4N = r.FAIR4N
      };
    return rows;
  }

}

