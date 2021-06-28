using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static ResrepAuthorRequestEditModel ToEditable(this NexusResrepAuthorRequest r)
    {
      var nre = new ResrepAuthorRequestEditModel()
      {
        ResrepRecordHandle = r.ResrepRecordHandle,
        ResrepEntityName = r.ResrepEntityName,
        AccessRequestedForAgentGuid = r.AccessRequestedForAgentGuidRef,
        AccessRequestedForAgentName = r.AccessRequestedForAgentUserName,
        AccessApprovedByAgentGuid = r.AccessApprovedByAgentGuidRef,
        AccessApprovedByAgentName = r.AccessApprovedByAgentUserName,
        RequestIsApproved = r.RequestIsApproved,
        RequestIsDenied = r.RequestIsDenied,
        AuthorHasResrepAccess = r.AuthorHasResrepAccess,

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

    public static IEnumerable<ResrepAuthorRequestEditModel> ToEditable(this IQueryable<NexusResrepAuthorRequest> query)
    {
      IEnumerable<ResrepAuthorRequestEditModel> rows =
        from r in query
        select new ResrepAuthorRequestEditModel
        {
          ResrepRecordHandle = r.ResrepRecordHandle,
          ResrepEntityName = r.ResrepEntityName,
          AccessRequestedForAgentGuid = r.AccessRequestedForAgentGuidRef,
          AccessRequestedForAgentName = r.AccessRequestedForAgentUserName,
          AccessApprovedByAgentGuid = r.AccessApprovedByAgentGuidRef,
          AccessApprovedByAgentName = r.AccessApprovedByAgentUserName,
          RequestIsApproved = r.RequestIsApproved,
          RequestIsDenied = r.RequestIsDenied,
          AuthorHasResrepAccess = r.AuthorHasResrepAccess,

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
    public IEnumerable<ResrepAuthorRequestEditModel> ListEditableResrepAuthorRequests()
    {
      IEnumerable<ResrepAuthorRequestEditModel> result;
      try
      {
        IQueryable<NexusResrepAuthorRequest> qry = this.NexusResrepAuthorRequests;
        if (PRC.ClientHasAuthorOrEditorAccess && !PRC.ClientHasAdminAccess)
        {
          qry = qry.Where(r => (r.IsDeleted == false) &&
            (r.AccessRequestedForAgentGuidRef == PRC.AgentGuid));
        }
        result = qry.OrderBy(r => r.ResrepEntityName).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<ResrepAuthorRequestEditModel>();
      }
      return result;
    }

    public IQueryable<NexusResrepAuthorRequest> QueryNexusResrepAuthorRequestByKey(Guid guidKey)
    {
      IQueryable<NexusResrepAuthorRequest> qry = this.NexusResrepAuthorRequests;
      qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
      return qry;
    }
    public NexusResrepAuthorRequest GetStorableResrepAuthorRequestByKey(Guid guidKey)
    { return QueryNexusResrepAuthorRequestByKey(guidKey).SingleOrDefault(); }
    public NexusResrepAuthorRequest GetStorableResrepAuthorRequestByKey(string guidKey)
    { return GetStorableResrepAuthorRequestByKey(Guid.Parse(guidKey)); }
    public ResrepAuthorRequestEditModel GetEditableResrepAuthorRequestByKey(Guid guidKey)
    { return QueryNexusResrepAuthorRequestByKey(guidKey).ToEditable().SingleOrDefault(); }
    public ResrepAuthorRequestEditModel GetEditableResrepAuthorRequestByKey(string guidKey)
    { return GetEditableResrepAuthorRequestByKey(Guid.Parse(guidKey)); }

    public ResrepAuthorRequestEditModel EditResrepAuthorRequest(ResrepAuthorRequestEditModel editObj, bool byStorProc = true)
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
      NexusResrepAuthorRequest storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusResrepAuthorRequest()
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
        storObj = GetStorableResrepAuthorRequestByKey(internalGuid);
        storObj.UpdatedByAgentGuidRef = agentGuid;
        if (pdpRestCntxt.ClientHasAdminAccess)
        {
          storObj.RequestIsApproved = editObj.RequestIsApproved;
          storObj.RequestIsDenied = editObj.RequestIsDenied;
        }
      }

      // begin common insert/update edit
      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeResrepAuthorRequestEdit(
          storObj.AccessRequestedForAgentGuidRef,
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.RequestIsApproved, storObj.RequestIsDenied);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusResrepAuthorRequests.Add(storObj); }
        errMsg = ExecuteChanges();
      }
      // refresh the edit object
      editObj = GetEditableResrepAuthorRequestByKey(internalGuid);
      if (editObj == null) { editObj = new ResrepAuthorRequestEditModel(); }
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

    public ResrepAuthorRequestEditModel DeleteResrepAuthorRequest(ResrepAuthorRequestEditModel editObj, bool byStorProc = true)
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
        var storObj = GetStorableResrepAuthorRequestByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeResrepAuthorRequestDelete(
            storObj.DeletedByAgentGuidRef, storObj.ResrepRGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusResrepAuthorRequests.Attach(storObj);
          this.NexusResrepAuthorRequests.Remove(storObj);
          errMsg = ExecuteChanges();
        }
        // refresh the edit object
        editObj = GetEditableResrepAuthorRequestByKey(internalGuid);
        if (editObj == null) { editObj = new ResrepAuthorRequestEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

  }

}