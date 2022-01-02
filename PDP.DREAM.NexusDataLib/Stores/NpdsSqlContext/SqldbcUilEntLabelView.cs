// SqldbcUilEntLabelView.cs 
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
    public static EntityLabelViewModel ToViewable(this NexusEntityLabel r)
    {
      var nre = new EntityLabelViewModel()
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
        IsResolvable = r.IsResolvable,
        IsPrivate = r.IsPrivate,
        IsGenerating = r.IsGenerating,
        ServiceTypeCode = r.ServiceTypeCodeRef,
        TagToken = r.TagToken,
        LabelUri = r.LabelUri,
        EntityLabel = r.EntityLabel
      };
      return nre;
    }

    public static IQueryable<EntityLabelViewModel> ToViewable(this IQueryable<NexusEntityLabel> query)
    {
      IQueryable<EntityLabelViewModel> rows =
        from r in query
        select new EntityLabelViewModel
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
          IsResolvable = r.IsResolvable,
          IsPrivate = r.IsPrivate,
          IsGenerating = r.IsGenerating,
          ServiceTypeCode = r.ServiceTypeCodeRef,
          TagToken = r.TagToken,
          LabelUri = r.LabelUri,
          EntityLabel = r.EntityLabel
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<EntityLabelViewModel> ListViewableEntityLabels(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<EntityLabelViewModel> result;
      try
      {
        IQueryable<NexusEntityLabel> qry = this.NexusEntityLabels;
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
        result = Enumerable.Empty<EntityLabelViewModel>();
      }
      return result;
    }

    public EntityLabelViewModel GetViewableEntityLabelByKey(Guid guidKey)
    { return QueryStorableEntityLabelByKey(guidKey).ToViewable().SingleOrDefault(); }
    public EntityLabelViewModel GetViewableEntityLabelByKey(string guidKey)
    { return GetViewableEntityLabelByKey(Guid.Parse(guidKey)); }
    public NexusEntityLabel GetStorableEntityLabelByKey(Guid guidKey)
    { return QueryStorableEntityLabelByKey(guidKey).SingleOrDefault(); }
    public NexusEntityLabel GetStorableEntityLabelByKey(string guidKey)
    { return GetStorableEntityLabelByKey(Guid.Parse(guidKey)); }

    public IQueryable<NexusEntityLabel> QueryStorableEntityLabelByKey(Guid guidKey)
    {
      IQueryable<NexusEntityLabel> qry = this.NexusEntityLabels;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
