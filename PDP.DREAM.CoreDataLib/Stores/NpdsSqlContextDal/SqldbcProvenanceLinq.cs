// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IQueryable<ProvenanceUxm> ToEditable(this IQueryable<DoorsProvenance> query)
  {
    IQueryable<ProvenanceUxm> rows =
      from r in query
      select new ProvenanceUxm
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
        Provenance = (r.Provenance ?? ESS),
        FieldFormatCode = r.FieldFormatCode,
        FieldFormatName = r.FieldFormatName,
      };
    return rows;
  }

} // end class

// end file