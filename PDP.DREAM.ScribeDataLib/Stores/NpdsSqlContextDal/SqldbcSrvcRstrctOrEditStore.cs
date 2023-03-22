// SqldbcUilServiceRestrictionOrEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ServiceRestrictionOrEditModel EditRestrictionOr(ServiceRestrictionOrEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.RestrictionOrHasIndex;
    var recordPriority = editObj.RestrictionOrHasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var externalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionAndGuid, Guid.Empty);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    NexusServiceRestrictionOr storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = Guid.NewGuid();
      storObj = new NexusServiceRestrictionOr()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuidRef = recordGuid,
        RestrictionAndGuidRef = externalGuid,
        RestrictionOrGuidKey = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableRestrictionOrByKey(fgroupGuid);
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
		  agentGuid, infosetGuid, recordGuid, externalGuid, fgroupGuid,
        storObj.OrHasPriority, storObj.RestrictionValue, storObj.IsWordPhrase, storObj.IsConceptLabel);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusServiceRestrictionOrs.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableRestrictionOrByKey(fgroupGuid);
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
    var agentGuid = NPDSCP.ClientAgentGuid;
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableRestrictionOrByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = NPDSCP.ClientAgentGuid;
      storObj.IsDeleted = NPDSCP.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
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
      editObj = GetEditableRestrictionOrByKey(fgroupGuid);
      if (editObj == null) { editObj = new ServiceRestrictionOrEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

}

