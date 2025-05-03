// SqldbcUilServiceRestrictionOrEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ServiceRestrictionOrEditModel EditRestrictionOr(ServiceRestrictionOrEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.RestrictionOrHasIndex;
    var recordPriority = editObj.RestrictionOrHasPriority;
    var agentGuid = QURC.QebAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var externalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionAndGuid, Guid.Empty);
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    NexusServiceRestrictionOr storObj;
    if (isNewRecord)
    {
      // insert new record
      internalGuid = Guid.NewGuid();
      storObj = new NexusServiceRestrictionOr()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuidRef = recordGuid,
        RestrictionAndGuidRef = externalGuid,
        RestrictionOrGuidKey = internalGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableRestrictionOrByKey(internalGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit

    storObj.RestrictionValue = editObj.RestrictionValue;
    storObj.OrHasPriority = editObj.RestrictionOrHasPriority;
    storObj.IsWordPhrase = editObj.IsWordPhrase;
    storObj.IsConceptLabel = editObj.IsConceptLabel;

    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeServiceRestrictionOrEdit(
		  agentGuid, infosetGuid, recordGuid, externalGuid, internalGuid,
        storObj.OrHasPriority, storObj.RestrictionValue, storObj.IsWordPhrase, storObj.IsConceptLabel);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusServiceRestrictionOrs.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableRestrictionOrByKey(internalGuid);
    if (editObj == null) { editObj = new ServiceRestrictionOrEditModel(); }
    // refresh the recordIndex
    recordIndex = editObj.RestrictionOrHasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} written to database";
      editObj.PdpStatusItemStored = true;
    }
    else { editObj.PdpStatusMessage = errMsg; }
    return editObj;
  }

  public ServiceRestrictionOrEditModel DeleteRestrictionOr(ServiceRestrictionOrEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.RestrictionOrHasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = QURC.QebAgentGuid;
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableRestrictionOrByKey(internalGuid);
      storObj.DeletedByAgentGuid = QURC.QebAgentGuid;
      storObj.IsDeleted = QURC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeServiceRestrictionOrDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuidRef,
          storObj.RestrictionAndGuidRef, storObj.RestrictionOrGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusServiceRestrictionOrs.Attach(storObj);
        this.NexusServiceRestrictionOrs.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableRestrictionOrByKey(internalGuid);
      if (editObj == null) { editObj = new ServiceRestrictionOrEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

}

