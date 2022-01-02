// SqldbcUilDescriptionView.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores
{
  public static partial class NpdsLinqSqlOperators
  {
    public static DescriptionViewModel ToViewable(this NexusDescription r)
    {
      var nre = new DescriptionViewModel()
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
        Description = r.Description
      };
      return nre;
    }

    public static IQueryable<DescriptionViewModel> ToViewable(this IQueryable<NexusDescription> query)
    {
      IQueryable<DescriptionViewModel> rows =
        from r in query
        select new DescriptionViewModel
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
          Description = r.Description
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<DescriptionViewModel> ListViewableDescriptions(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<DescriptionViewModel> result;
      try
      {
        IQueryable<NexusDescription> qry = this.NexusDescriptions;
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
        result = Enumerable.Empty<DescriptionViewModel>();
      }
      return result;
    }

    public DescriptionViewModel GetViewableDescriptionByKey(Guid guidKey)
    { return QueryStorableDescriptionByKey(guidKey).ToViewable().SingleOrDefault(); }
    public DescriptionViewModel GetViewableDescriptionByKey(string guidKey)
    { return GetViewableDescriptionByKey(Guid.Parse(guidKey)); }

    public NexusDescription GetStorableDescriptionByKey(Guid guidKey)
    { return QueryStorableDescriptionByKey(guidKey).SingleOrDefault(); }
    public NexusDescription GetStorableDescriptionByKey(string guidKey)
    { return GetStorableDescriptionByKey(Guid.Parse(guidKey)); }

    public IQueryable<NexusDescription> QueryStorableDescriptionByKey(Guid guidKey)
    {
      IQueryable<NexusDescription> qry = this.NexusDescriptions;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
