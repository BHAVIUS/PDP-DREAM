// SqldbcUilSupTagView.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static SupportingTagViewModel ToViewable(this NexusSupportingTag r)
    {
      var nre = new SupportingTagViewModel()
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
        SupportingTag = r.SupportingTag
      };
      return nre;
    }

    public static IQueryable<SupportingTagViewModel> ToViewable(this IQueryable<NexusSupportingTag> query)
    {
      IQueryable<SupportingTagViewModel> rows =
        from r in query
        select new SupportingTagViewModel
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
          SupportingTag = r.SupportingTag
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<SupportingTagViewModel> ListViewableSupportingTags(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<SupportingTagViewModel> result;
      try
      {
        IQueryable<NexusSupportingTag> qry = this.NexusSupportingTags;
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
        result = Enumerable.Empty<SupportingTagViewModel>();
      }
      return result;
    }

    public SupportingTagViewModel GetViewableSupportingTagByKey(Guid guidKey)
    { return QueryStorableSupportingTagByKey(guidKey).ToViewable().SingleOrDefault(); }
    public SupportingTagViewModel GetViewableSupportingTagByKey(string guidKey)
    { return GetViewableSupportingTagByKey(Guid.Parse(guidKey)); }

    public NexusSupportingTag GetStorableSupportingTagByKey(Guid guidKey)
    { return QueryStorableSupportingTagByKey(guidKey).SingleOrDefault(); }
    public NexusSupportingTag GetStorableSupportingTagByKey(string guidKey)
    { return GetStorableSupportingTagByKey(Guid.Parse(guidKey)); }

    public IQueryable<NexusSupportingTag> QueryStorableSupportingTagByKey(Guid guidKey)
    {
      IQueryable<NexusSupportingTag> qry = this.NexusSupportingTags;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
