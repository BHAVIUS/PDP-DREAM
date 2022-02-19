// SqldbcUilResrepSnapshotEdit.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores
{
  public static partial class NpdsLinqSqlOperators
  {
    public static NexusSnapshotEditModel ToEditable(this NexusResrepSnapshot r)
    {
      var nre = new NexusSnapshotEditModel()
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
        NexusSnapshot = r.ResrepSnapshotXml
      };
      return nre;
    }

    public static IQueryable<NexusSnapshotEditModel> ToEditable(this IQueryable<NexusResrepSnapshot> query)
    {
      IQueryable<NexusSnapshotEditModel> rows =
        from r in query
        select new NexusSnapshotEditModel
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
          NexusSnapshot = r.ResrepSnapshotXml
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<NexusSnapshotEditModel> ListEditableSnapshots(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<NexusSnapshotEditModel> result;
      try
      {
        IQueryable<NexusResrepSnapshot> qry = this.NexusResrepSnapshots;
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
        result = Enumerable.Empty<NexusSnapshotEditModel>();
      }
      return result;
    }

    public NexusSnapshotEditModel GetEditableSnapshotByKey(Guid guidKey)
    { return QueryStorableSnapshotByKey(guidKey).ToEditable().SingleOrDefault(); }
    public NexusSnapshotEditModel GetEditableSnapshotByKey(string guidKey)
    { return GetEditableSnapshotByKey(Guid.Parse(guidKey)); }

    public NexusSnapshotEditModel EditSnapshot(NexusSnapshotEditModel editObj, bool byStorProc = true)
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
      NexusResrepSnapshot storObj;
      if (isNewRecord)
      {
        PRC.VerboseFormatReqst = true;
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusResrepSnapshot()
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
        storObj = GetStorableSnapshotByKey(internalGuid);
        storObj.UpdatedByAgentGuidRef = agentGuid;
      }

      // begin common insert/update edit
      storObj.HasPriority = editObj.HasPriority;
      storObj.IsMarked = editObj.IsMarked;
      storObj.IsPrincipal = editObj.IsPrincipal;

      try
      {
        // TODO: recode this block
        // TODO: need new method for a record by key to XElement
        // TODO: need new method for a record by key to XDocument
        //NpdsResrepXmlRoot? xmlMsg = GetXmlmsgFormtdNpdsResrepByKey(storObj.RecordGuidRef);
        //string resXml = PdpXml.PdpSerialize(PRC, xmlMsg);
        //storObj.ResrepSnapshotXml = XElement.Parse(resXml).ToString();
      }
      catch
      {
        storObj.ResrepSnapshotXml =
        $"<anyXmlTag> ATTENTION: current implementation requires that {recordName} XML text must be in valid XML format </anyXmlTag>";
      }

      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeResrepSnapshotEdit(
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
		  storObj.ResrepSnapshotXml);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusResrepSnapshots.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableSnapshotByKey(internalGuid);
      if (editObj == null) { editObj = new NexusSnapshotEditModel(); }
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

    public NexusSnapshotEditModel DeleteSnapshot(NexusSnapshotEditModel editObj, bool byStorProc = true)
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
        var storObj = GetStorableSnapshotByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeResrepSnapshotDelete(
            storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusResrepSnapshots.Attach(storObj);
          this.NexusResrepSnapshots.Remove(storObj);
          errMsg = StoreChanges();
        }
        // refresh the edit object
        editObj = GetEditableSnapshotByKey(internalGuid);
        if (editObj == null) { editObj = new NexusSnapshotEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public NexusSnapshotEditModel CheckNexusSnapshot(NexusSnapshotEditModel editObj)
    {
      editObj.PdpStatusMessage = string.Empty;
      return editObj;
    }

    public virtual short CheckNexusSnapshots(Guid recordGuid)
    {
      var rrr = GetEditableResrepStemByRKey(recordGuid);
      return CheckNexusSnapshots(ref rrr);
    }
    public virtual short CheckNexusSnapshots(ref NexusResrepEditModel rrr)
    {
      short statusCode = 0;
      var recordGuid = (Guid)rrr.RRRecordGuid;
      // TODO: complete this check function and other check functions
      var items = ListEditableSnapshots(recordGuid).Select(r => r.NexusSnapshot);
      if (items.Count() > 0)
      {
        statusCode = (short)NpdsConst.InfosetStatus.Unknown;
      }
      else
      {
        statusCode = (short)NpdsConst.InfosetStatus.None;
      }
      rrr.NexusSnapshotsStatusCode = statusCode;
      return statusCode;
    }

  }

}
