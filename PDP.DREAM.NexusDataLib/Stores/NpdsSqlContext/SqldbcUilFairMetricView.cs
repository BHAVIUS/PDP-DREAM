// SqldbcUilFairMetricView.cs 
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
    public static FairMetricViewModel ToViewable(this NexusFairMetric r)
    {
      var nre = new FairMetricViewModel()
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
      return nre;
    }

    public static IQueryable<FairMetricViewModel> ToViewable(this IQueryable<NexusFairMetric> query)
    {
      IQueryable<FairMetricViewModel> rows =
        from r in query
        select new FairMetricViewModel
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

  public partial class NexusDbsqlContext
  {
    public IEnumerable<FairMetricViewModel> ListViewableFairMetrics(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<FairMetricViewModel> result;
      try
      {
        IQueryable<NexusFairMetric> qry = this.NexusFairMetrics;
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
        result = Enumerable.Empty<FairMetricViewModel>();
      }
      return result;
    }

    public FairMetricViewModel GetViewableFairMetricByKey(Guid guidKey)
    { return QueryStorableFairMetricByKey(guidKey).ToViewable().SingleOrDefault(); }
    public FairMetricViewModel GetViewableFairMetricByKey(string guidKey)
    { return GetViewableFairMetricByKey(Guid.Parse(guidKey)); }
    public NexusFairMetric GetStorableFairMetricByKey(Guid guidKey)
    { return QueryStorableFairMetricByKey(guidKey).SingleOrDefault(); }
    public NexusFairMetric GetStorableFairMetricByKey(string guidKey)
    { return GetStorableFairMetricByKey(Guid.Parse(guidKey)); }
    public IQueryable<NexusFairMetric> QueryStorableFairMetricByKey(Guid guidKey)
    {
      IQueryable<NexusFairMetric> qry = this.NexusFairMetrics;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
