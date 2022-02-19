// SqldbcUilSupLabelView.cs 
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
    public static SupportingLabelViewModel ToViewable(this NexusSupportingLabel r)
    {
      var nre = new SupportingLabelViewModel()
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
        SupportingLabel = r.SupportingLabel
      };
      return nre;
    }

    public static IQueryable<SupportingLabelViewModel> ToViewable(this IQueryable<NexusSupportingLabel> query)
    {
      IQueryable<SupportingLabelViewModel> rows =
        from r in query
        select new SupportingLabelViewModel
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
          SupportingLabel = r.SupportingLabel
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<SupportingLabelViewModel> ListViewableSupportingLabels(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<SupportingLabelViewModel> result;
      try
      {
        IQueryable<NexusSupportingLabel> qry = this.NexusSupportingLabels;
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
        result = Enumerable.Empty<SupportingLabelViewModel>();
      }
      return result;
    }

    public SupportingLabelViewModel GetViewableSupportingLabelByKey(Guid guidKey)
    { return QueryStorableSupportingLabelByKey(guidKey).ToViewable().SingleOrDefault(); }
    public SupportingLabelViewModel GetViewableSupportingLabelByKey(string guidKey)
    { return GetViewableSupportingLabelByKey(Guid.Parse(guidKey)); }


    public NexusSupportingLabel GetStorableSupportingLabelByKey(Guid guidKey)
    { return QueryStorableSupportingLabelByKey(guidKey).SingleOrDefault(); }
    public NexusSupportingLabel GetStorableSupportingLabelByKey(string guidKey)
    { return GetStorableSupportingLabelByKey(Guid.Parse(guidKey)); }

    public IQueryable<NexusSupportingLabel> QueryStorableSupportingLabelByKey(Guid guidKey)
    {
      IQueryable<NexusSupportingLabel> qry = this.NexusSupportingLabels;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
