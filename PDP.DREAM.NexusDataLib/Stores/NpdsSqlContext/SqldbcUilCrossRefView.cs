// SqldbcUilCrossRefView.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores
{
  public static partial class NpdsLinqSqlOperators
  {
    public static CrossReferenceViewModel ToViewable(this NexusCrossReference r)
    {
      var nre = new CrossReferenceViewModel()
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
        CrossReference = r.CrossReference
      };
      return nre;
    }

    public static IQueryable<CrossReferenceViewModel> ToViewable(this IQueryable<NexusCrossReference> query)
    {
      IQueryable<CrossReferenceViewModel> rows =
        from r in query
        select new CrossReferenceViewModel
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
          CrossReference = r.CrossReference
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<CrossReferenceViewModel> ListViewableCrossReferences(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<CrossReferenceViewModel> result;
      try
      {
        IQueryable<NexusCrossReference> qry = this.NexusCrossReferences;
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
        result = Enumerable.Empty<CrossReferenceViewModel>();
      }
      return result;
    }

    public CrossReferenceViewModel GetViewableCrossReferenceByKey(Guid guidKey)
    { return QueryStorableCrossReferenceByKey(guidKey).ToViewable().SingleOrDefault(); }
    public CrossReferenceViewModel GetViewableCrossReferenceByKey(string guidKey)
    { return GetViewableCrossReferenceByKey(Guid.Parse(guidKey)); }
    public NexusCrossReference GetStorableCrossReferenceByKey(Guid guidKey)
    { return QueryStorableCrossReferenceByKey(guidKey).SingleOrDefault(); }
    public NexusCrossReference GetStorableCrossReferenceByKey(string guidKey)
    { return GetStorableCrossReferenceByKey(Guid.Parse(guidKey)); }
    public IQueryable<NexusCrossReference> QueryStorableCrossReferenceByKey(Guid guidKey)
    {
      IQueryable<NexusCrossReference> qry = this.NexusCrossReferences;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
