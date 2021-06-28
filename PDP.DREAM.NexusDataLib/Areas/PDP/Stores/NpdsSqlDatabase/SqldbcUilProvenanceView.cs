using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static ProvenanceViewModel ToViewable(this NexusProvenance r)
    {
      var nre = new ProvenanceViewModel()
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
        Provenance = r.Provenance
      };
      return nre;
    }

    public static IQueryable<ProvenanceViewModel> ToViewable(this IQueryable<NexusProvenance> query)
    {
      IQueryable<ProvenanceViewModel> rows =
        from r in query
        select new ProvenanceViewModel
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
          Provenance = r.Provenance
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<ProvenanceViewModel> ListViewableProvenances(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<ProvenanceViewModel> result;
      try
      {
        IQueryable<NexusProvenance> qry = this.NexusProvenances;
        if (PRC.ClientHasAdminAccess || PRC.ClientHasEditorAccess)
        { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
        else
        {
          if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == PRC.AgentGuid)); }
          else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
        }
        result = qry.OrderBy(r => r.HasPriority).ToViewable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<ProvenanceViewModel>();
      }
      return result;
    }

    public ProvenanceViewModel GetViewableProvenanceByKey(Guid guidKey)
    { return QueryStorableProvenanceByKey(guidKey).ToViewable().SingleOrDefault(); }
    public ProvenanceViewModel GetViewableProvenanceByKey(string guidKey)
    { return GetViewableProvenanceByKey(Guid.Parse(guidKey)); }

    public NexusProvenance GetStorableProvenanceByKey(Guid guidKey)
    { return QueryStorableProvenanceByKey(guidKey).SingleOrDefault(); }
    public NexusProvenance GetStorableProvenanceByKey(string guidKey)
    { return GetStorableProvenanceByKey(Guid.Parse(guidKey)); }

    public IQueryable<NexusProvenance> QueryStorableProvenanceByKey(Guid guidKey)
    {
      IQueryable<NexusProvenance> qry = this.NexusProvenances;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
