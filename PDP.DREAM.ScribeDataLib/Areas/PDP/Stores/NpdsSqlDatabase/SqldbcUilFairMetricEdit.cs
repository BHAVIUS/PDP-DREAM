﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static FairMetricEditModel ToEditable(this NexusFairMetric r)
    {
      var nre = new FairMetricEditModel()
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

    public static IQueryable<FairMetricEditModel> ToEditable(this IQueryable<NexusFairMetric> query)
    {
      IQueryable<FairMetricEditModel> rows =
        from r in query
        select new FairMetricEditModel
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

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<FairMetricEditModel> ListEditableFairMetrics(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<FairMetricEditModel> result;
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
        result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<FairMetricEditModel>();
      }
      return result;
    }

    public FairMetricEditModel GetEditableFairMetricByKey(Guid guidKey)
    { return QueryStorableFairMetricByKey(guidKey).ToEditable().SingleOrDefault(); }
    public FairMetricEditModel GetEditableFairMetricByKey(string guidKey)
    { return GetEditableFairMetricByKey(Guid.Parse(guidKey)); }

    public FairMetricEditModel EditFairMetric(FairMetricEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.HasIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      NexusFairMetric storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusFairMetric()
        {
          CreatedByAgentGuidRef = agentGuid,
          UpdatedByAgentGuidRef = agentGuid,
          RecordGuidRef = recordGuid,
          FgroupGuidKey = internalGuid
        };
      }
      else
      {
        // update existing record
        storObj = GetStorableFairMetricByKey(internalGuid);
        storObj.UpdatedByAgentGuidRef = agentGuid;
      }

      // begin common insert/update edit
      storObj.HasPriority = editObj.HasPriority;
      storObj.IsMarked = editObj.IsMarked;
      storObj.IsPrincipal = editObj.IsPrincipal;

      storObj.MInvalidOldClaim = editObj.MInvalidOldClaim;
      storObj.QValidOldClaim = editObj.QValidOldClaim;
      storObj.PInvalidNewClaim = editObj.PInvalidNewClaim;
      storObj.NValidNewClaim = editObj.NValidNewClaim;
      //storObj.FAIR1 = editObj.FAIR1;
      //storObj.FAIR2 = editObj.FAIR2;
      //storObj.FAIR3 = editObj.FAIR3;
      //storObj.FAIR4 = editObj.FAIR4;

      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeFairMetricEdit(
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
          storObj.MInvalidOldClaim, storObj.QValidOldClaim, storObj.PInvalidNewClaim, storObj.NValidNewClaim);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusFairMetrics.Add(storObj); }
        errMsg = ExecuteChanges();
      }
      // refresh the edit object
      editObj = GetEditableFairMetricByKey(internalGuid);
      if (editObj == null) { editObj = new FairMetricEditModel(); }
      // refresh the recordIndex
      recordIndex = editObj.HasIndex;
      // update the status message
      if (string.IsNullOrEmpty(errMsg))
      {
        editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} written to database";
        editObj.PdpStatusItemStored = true;
      }
      else { editObj.PdpStatusMessage = errMsg; }
      return editObj;
    }

    public FairMetricEditModel DeleteFairMetric(FairMetricEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.HasIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      if (!isNewRecord) // delete existing record
      {
        var storObj = GetStorableFairMetricByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeFairMetricDelete(
            storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusFairMetrics.Attach(storObj);
          this.NexusFairMetrics.Remove(storObj);
          errMsg = ExecuteChanges();
        }
        // refresh the edit object
        editObj = GetEditableFairMetricByKey(internalGuid);
        if (editObj == null) { editObj = new FairMetricEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual FairMetricEditModel CheckFairMetric(FairMetricEditModel editObj)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.HasIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      if (!isNewRecord)
      {
        // refresh object
        editObj = GetEditableFairMetricByKey(internalGuid);
        if (editObj == null) { editObj = new FairMetricEditModel(); }
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual short CheckFairMetrics(Guid recordGuid)
    {
      var rrr = GetEditableResrepStemByRKey(recordGuid);
      return CheckFairMetrics(ref rrr);
    }
    public virtual short CheckFairMetrics(ref NexusResrepEditModel rrr)
    {
      short statusCode = 0;
      var recordGuid = (Guid)rrr.RRRecordGuid;
      var registryGuid = (Guid)rrr.RecordRegistryGuid;
      var items = ListEditableFairMetrics(recordGuid).Select(m => new { m.FAIR1, m.FAIR2, m.FAIR3, m.FAIR4 });
      if (items.Count() > 0)
      {
        statusCode = (short)NpdsConst.InfosetStatus.Unknown;
      }
      else
      {
        statusCode = (short)NpdsConst.InfosetStatus.None;
      }
      rrr.FairMetricsStatusCode = statusCode;
      return statusCode;
    }

  }

}
