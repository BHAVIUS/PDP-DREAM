// SqldbcUilCrossRefEdit.cs 
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
    public static CrossReferenceEditModel ToEditable(this NexusCrossReference r)
    {
      var nre = new CrossReferenceEditModel()
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
        CrossReference = r.CrossReference
      };
      return nre;
    }

    public static IQueryable<CrossReferenceEditModel> ToEditable(this IQueryable<NexusCrossReference> query)
    {
      IQueryable<CrossReferenceEditModel> rows =
        from r in query
        select new CrossReferenceEditModel
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
          CrossReference = r.CrossReference
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<CrossReferenceEditModel> ListEditableCrossReferences(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<CrossReferenceEditModel> result;
      try
      {
        IQueryable<NexusCrossReference> qry = this.NexusCrossReferences;
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
        result = Enumerable.Empty<CrossReferenceEditModel>();
      }
      return result;
    }

    public CrossReferenceEditModel GetEditableCrossReferenceByKey(Guid guidKey)
    { return QueryStorableCrossReferenceByKey(guidKey).ToEditable().SingleOrDefault(); }
    public CrossReferenceEditModel GetEditableCrossReferenceByKey(string guidKey)
    { return GetEditableCrossReferenceByKey(Guid.Parse(guidKey)); }

    public CrossReferenceEditModel EditCrossReference(CrossReferenceEditModel editObj, bool byStorProc = true)
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
      NexusCrossReference storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusCrossReference()
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
        storObj = GetStorableCrossReferenceByKey(internalGuid);
        storObj.UpdatedByAgentGuidRef = agentGuid;
      }

      // begin common insert/update edit
      storObj.HasPriority = editObj.HasPriority;
      storObj.IsMarked = editObj.IsMarked;
      storObj.IsPrincipal = editObj.IsPrincipal;

      storObj.CrossReference = editObj.CrossReference;
      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeCrossReferenceEdit(
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
		      storObj.CrossReference);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusCrossReferences.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableCrossReferenceByKey(internalGuid);
      if (editObj == null) { editObj = new CrossReferenceEditModel(); }
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

    public CrossReferenceEditModel DeleteCrossReference(CrossReferenceEditModel editObj, bool byStorProc = true)
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
        var storObj = GetStorableCrossReferenceByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeCrossReferenceDelete(
            storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusCrossReferences.Attach(storObj);
          this.NexusCrossReferences.Remove(storObj);
          errMsg = StoreChanges();
        }
        // refresh the edit object
        editObj = GetEditableCrossReferenceByKey(internalGuid);
        if (editObj == null) { editObj = new CrossReferenceEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual CrossReferenceEditModel CheckCrossReference(CrossReferenceEditModel editObj)
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
        editObj = GetEditableCrossReferenceByKey(internalGuid);
        if (editObj == null) { editObj = new CrossReferenceEditModel(); }
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual short CheckCrossReferences(Guid recordGuid)
    {
      var rrr = GetEditableResrepStemByRKey(recordGuid);
      return CheckCrossReferences(ref rrr);
    }
    public virtual short CheckCrossReferences(ref NexusResrepEditModel rrr)
    {
      short statusCode = 0;
      var recordGuid = (Guid)rrr.RRRecordGuid;
      var registryGuid = (Guid)rrr.RecordRegistryGuid;
      var items = ListEditableCrossReferences(recordGuid).Select(st => st.CrossReference.ToLower());
      if (items.Any())
      {
        statusCode = CheckSupportingStrings(items, registryGuid);
      }
      else
      {
        statusCode = (short)NpdsConst.InfosetStatus.None;
      }
      rrr.CrossReferencesStatusCode = statusCode;
      return statusCode;
    }

  }

}
