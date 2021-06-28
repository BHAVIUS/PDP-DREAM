using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static NexusSnapshotViewModel ToViewable(this NexusResrepSnapshot r)
    {
      var nre = new NexusSnapshotViewModel()
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
        NexusSnapshot = r.ResrepSnapshotXml
      };
      return nre;
    }

    public static IQueryable<NexusSnapshotViewModel> ToViewable(this IQueryable<NexusResrepSnapshot> query)
    {
      IQueryable<NexusSnapshotViewModel> rows =
        from r in query
        select new NexusSnapshotViewModel
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
          NexusSnapshot = r.ResrepSnapshotXml
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<NexusSnapshotViewModel> ListViewableSnapshots(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<NexusSnapshotViewModel> result;
      try
      {
        IQueryable<NexusResrepSnapshot> qry = this.NexusResrepSnapshots;
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
        result = Enumerable.Empty<NexusSnapshotViewModel>();
      }
      return result;
    }

    public NexusSnapshotViewModel GetViewableSnapshotByKey(Guid guidKey)
    { return QueryStorableSnapshotByKey(guidKey).ToViewable().SingleOrDefault(); }
    public NexusSnapshotViewModel GetViewableSnapshotByKey(string guidKey)
    { return GetViewableSnapshotByKey(Guid.Parse(guidKey)); }
    public NexusResrepSnapshot GetStorableSnapshotByKey(Guid guidKey)
    { return QueryStorableSnapshotByKey(guidKey).SingleOrDefault(); }
    public NexusResrepSnapshot GetStorableSnapshotByKey(string guidKey)
    { return GetStorableSnapshotByKey(Guid.Parse(guidKey)); }
    public IQueryable<NexusResrepSnapshot> QueryStorableSnapshotByKey(Guid guidKey)
    {
      IQueryable<NexusResrepSnapshot> qry = this.NexusResrepSnapshots;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
