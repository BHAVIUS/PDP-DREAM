// SqldbcUilServiceEditorRequestEdit.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static ServiceEditorRequestEditModel ToEditable(this NexusServiceEditorRequest r)
    {
      var nre = new ServiceEditorRequestEditModel()
      {
        ResrepRecordHandle = r.ResrepRecordHandle,
        ResrepEntityName = r.ResrepEntityName,
        AccessRequestedForAgentGuid = r.AccessRequestedForAgentGuidRef,
        AccessRequestedForAgentName = r.AccessRequestedForAgentUserName,
        AccessApprovedByAgentGuid = r.AccessApprovedByAgentGuidRef,
        AccessApprovedByAgentName = r.AccessApprovedByAgentUserName,
        RequestIsApproved = r.RequestIsApproved,
        RequestIsDenied = r.RequestIsDenied,
        EditorHasServiceAccess = r.EditorHasServiceAccess,

        RRInfosetGuid = r.ResrepIGuidRef,
        RRRecordGuid = r.ResrepRGuidRef,
        RRFgroupGuid = r.FgroupGuidKey,
        HasIndex = r.HasIndex,

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
        DeletedByAgentName = r.DeletedByAgentUserName
        //

      };
      return nre;
    }

    public static IEnumerable<ServiceEditorRequestEditModel> ToEditable(this IQueryable<NexusServiceEditorRequest> query)
    {
      IEnumerable<ServiceEditorRequestEditModel> rows =
        from r in query
        select new ServiceEditorRequestEditModel
        {
          ResrepRecordHandle = r.ResrepRecordHandle,
          ResrepEntityName = r.ResrepEntityName,
          AccessRequestedForAgentGuid = r.AccessRequestedForAgentGuidRef,
          AccessRequestedForAgentName = r.AccessRequestedForAgentUserName,
          AccessApprovedByAgentGuid = r.AccessApprovedByAgentGuidRef,
          AccessApprovedByAgentName = r.AccessApprovedByAgentUserName,
          RequestIsApproved = r.RequestIsApproved,
          RequestIsDenied = r.RequestIsDenied,
          EditorHasServiceAccess = r.EditorHasServiceAccess,

          RRInfosetGuid = r.ResrepIGuidRef,
          RRRecordGuid = r.ResrepRGuidRef,
          RRFgroupGuid = r.FgroupGuidKey,
          HasIndex = r.HasIndex,

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
          DeletedByAgentName = r.DeletedByAgentUserName
          //

        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<ServiceEditorRequestEditModel> ListEditableServiceEditorRequests()
    {
      IEnumerable<ServiceEditorRequestEditModel> result;
      try
      {
        IQueryable<NexusServiceEditorRequest> qry = this.NexusServiceEditorRequests;
        if (PRC.ClientHasAuthorOrEditorAccess && !PRC.ClientHasAdminAccess)
        {
          qry = qry.Where(r => (r.IsDeleted == false) &&
            (r.AccessRequestedForAgentGuidRef == PRC.AgentGuid));
        }
        result = qry.OrderBy(r => r.ResrepEntityName).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<ServiceEditorRequestEditModel>();
      }
      return result;
    }

    public IQueryable<NexusServiceEditorRequest> QueryNexusServiceEditorRequestByKey(Guid guidKey)
    {
      IQueryable<NexusServiceEditorRequest> qry = this.NexusServiceEditorRequests;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }
    public NexusServiceEditorRequest GetStorableServiceEditorRequestByKey(Guid guidKey)
    { return QueryNexusServiceEditorRequestByKey(guidKey).SingleOrDefault(); }
    public NexusServiceEditorRequest GetStorableServiceEditorRequestByKey(string guidKey)
    { return GetStorableServiceEditorRequestByKey(Guid.Parse(guidKey)); }
    public ServiceEditorRequestEditModel GetEditableServiceEditorRequestByKey(Guid guidKey)
    { return QueryNexusServiceEditorRequestByKey(guidKey).ToEditable().SingleOrDefault(); }
    public ServiceEditorRequestEditModel GetEditableServiceEditorRequestByKey(string guidKey)
    { return GetEditableServiceEditorRequestByKey(Guid.Parse(guidKey)); }

    public ServiceEditorRequestEditModel EditServiceEditorRequest(ServiceEditorRequestEditModel editObj, bool byStorProc = true)
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
      NexusServiceEditorRequest storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusServiceEditorRequest()
        {
          AccessRequestedForAgentGuidRef = agentGuid,
          CreatedByAgentGuidRef = agentGuid,
          UpdatedByAgentGuidRef = agentGuid,
          ResrepIGuidRef = infosetGuid,
          ResrepRGuidRef = recordGuid,
          FgroupGuidKey = internalGuid
        };
      }
      else
      {
        // update existing record
        storObj = GetStorableServiceEditorRequestByKey(internalGuid);
        storObj.UpdatedByAgentGuidRef = agentGuid;
        if (PRC.ClientHasAdminAccess)
        {
          storObj.RequestIsApproved = editObj.RequestIsApproved;
          storObj.RequestIsDenied = editObj.RequestIsDenied;
        }
      }

      // begin common insert/update edit
      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeServiceEditorRequestEdit(
          storObj.AccessRequestedForAgentGuidRef,
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.RequestIsApproved, storObj.RequestIsDenied);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusServiceEditorRequests.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableServiceEditorRequestByKey(internalGuid);
      if (editObj == null) { editObj = new ServiceEditorRequestEditModel(); }
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

    public ServiceEditorRequestEditModel DeleteServiceEditorRequest(ServiceEditorRequestEditModel editObj, bool byStorProc = true)
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
        var storObj = GetStorableServiceEditorRequestByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeServiceEditorRequestDelete(
            storObj.DeletedByAgentGuidRef, storObj.ResrepRGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusServiceEditorRequests.Attach(storObj);
          this.NexusServiceEditorRequests.Remove(storObj);
          errMsg = StoreChanges();
        }
        // refresh the edit object
        editObj = GetEditableServiceEditorRequestByKey(internalGuid);
        if (editObj == null) { editObj = new ServiceEditorRequestEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

  }

}