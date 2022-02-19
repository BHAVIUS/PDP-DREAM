// SqldbcUilOtherTextView.cs 
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
    public static OtherTextViewModel ToViewable(this NexusOtherText r)
    {
      var nre = new OtherTextViewModel()
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
        OtherText = r.OtherText
      };
      return nre;
    }

    public static IQueryable<OtherTextViewModel> ToViewable(this IQueryable<NexusOtherText> query)
    {
      IQueryable<OtherTextViewModel> rows =
        from r in query
        select new OtherTextViewModel
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
          OtherText = r.OtherText
        };
      return rows;
    }

  }

  public partial class NexusDbsqlContext
  {
    public IEnumerable<OtherTextViewModel> ListViewableOtherTexts(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<OtherTextViewModel> result;
      try
      {
        IQueryable<NexusOtherText> qry = this.NexusOtherTexts;
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
        result = Enumerable.Empty<OtherTextViewModel>();
      }
      return result;
    }

    public OtherTextViewModel GetViewableOtherTextByKey(Guid guidKey)
    { return QueryStorableOtherTextByKey(guidKey).ToViewable().SingleOrDefault(); }
    public OtherTextViewModel GetViewableOtherTextByKey(string guidKey)
    { return GetViewableOtherTextByKey(Guid.Parse(guidKey)); }
    public NexusOtherText GetStorableOtherTextByKey(Guid guidKey)
    { return QueryStorableOtherTextByKey(guidKey).SingleOrDefault(); }
    public NexusOtherText GetStorableOtherTextByKey(string guidKey)
    { return GetStorableOtherTextByKey(Guid.Parse(guidKey)); }
    public IQueryable<NexusOtherText> QueryStorableOtherTextByKey(Guid guidKey)
    {
      IQueryable<NexusOtherText> qry = this.NexusOtherTexts;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }

  }

}
