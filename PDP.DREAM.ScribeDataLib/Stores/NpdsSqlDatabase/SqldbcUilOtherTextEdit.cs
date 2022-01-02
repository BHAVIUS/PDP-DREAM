// SqldbcUilOtherTextEdit.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
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
    public static OtherTextEditModel ToEditable(this NexusOtherText r)
    {
      var nre = new OtherTextEditModel()
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

    public static IQueryable<OtherTextEditModel> ToEditable(this IQueryable<NexusOtherText> query)
    {
      IQueryable<OtherTextEditModel> rows =
        from r in query
        select new OtherTextEditModel
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

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<OtherTextEditModel> ListEditableOtherTexts(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<OtherTextEditModel> result;
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
        result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<OtherTextEditModel>();
      }
      return result;
    }

    public OtherTextEditModel GetEditableOtherTextByKey(Guid guidKey)
    { return QueryStorableOtherTextByKey(guidKey).ToEditable().SingleOrDefault(); }
    public OtherTextEditModel GetEditableOtherTextByKey(string guidKey)
    { return GetEditableOtherTextByKey(Guid.Parse(guidKey)); }

    public OtherTextEditModel EditOtherText(OtherTextEditModel editObj, bool byStorProc = true)
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
      NexusOtherText storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusOtherText()
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
        storObj = GetStorableOtherTextByKey(internalGuid);
        storObj.UpdatedByAgentGuidRef = agentGuid;
      }

      // begin common insert/update edit
      storObj.HasPriority = editObj.HasPriority;
      storObj.IsMarked = editObj.IsMarked;
      storObj.IsPrincipal = editObj.IsPrincipal;

      storObj.OtherText = editObj.OtherText;
      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeOtherTextEdit(
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
            storObj.OtherText);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusOtherTexts.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableOtherTextByKey(internalGuid);
      if (editObj == null) { editObj = new OtherTextEditModel(); }
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

    public OtherTextEditModel DeleteOtherText(OtherTextEditModel editObj, bool byStorProc = true)
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
        var storObj = GetStorableOtherTextByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeOtherTextDelete(
            storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusOtherTexts.Attach(storObj);
          this.NexusOtherTexts.Remove(storObj);
          errMsg = StoreChanges();
        }
        // refresh the edit object
        editObj = GetEditableOtherTextByKey(internalGuid);
        if (editObj == null) { editObj = new OtherTextEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual OtherTextEditModel CheckOtherText(OtherTextEditModel editObj)
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
        editObj = GetEditableOtherTextByKey(internalGuid);
        if (editObj == null) { editObj = new OtherTextEditModel(); }
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual short CheckOtherTexts(Guid recordGuid)
    {
      var rrr = GetEditableResrepStemByRKey(recordGuid);
      return CheckOtherTexts(ref rrr);
    }
    public virtual short CheckOtherTexts(ref NexusResrepEditModel rrr)
    {
      short statusCode = 0;
      var recordGuid = (Guid)rrr.RRRecordGuid;
      var registryGuid = (Guid)rrr.RecordRegistryGuid;
      var items = ListEditableOtherTexts(recordGuid).Select(st => st.OtherText.ToLower());
      if (items.Any())
      {
        statusCode = CheckSupportingStrings(items, registryGuid);
      }
      else
      {
        statusCode = (short)NpdsConst.InfosetStatus.None;
      }
      rrr.OtherTextsStatusCode = statusCode;
      return statusCode;
    }

  }

}
