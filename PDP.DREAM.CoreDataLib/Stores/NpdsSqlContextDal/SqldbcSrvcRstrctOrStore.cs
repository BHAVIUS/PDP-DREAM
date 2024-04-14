// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ServiceRestrictionOrUxm EditRestrictionOr(ServiceRestrictionOrUxm editObj, bool byStorProc = true)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.RestrictionOrHasIndex;
    var recordPriority = editObj.RestrictionOrHasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, EGS);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var externalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionAndGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuid, EGS);
    var isNewRecord = fgroupGuid.IsEmpty();
    CoreServiceRestrictionOr storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new CoreServiceRestrictionOr()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuid = recordGuid,
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
      if (isNewRecord) { this.CoreServiceRestrictionOrs.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableRestrictionOrByKey(fgroupGuid);
    if (editObj == null) { editObj = new ServiceRestrictionOrUxm(); }
    // refresh the recordIndex
    recordIndex = editObj.RestrictionOrHasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} written to database";
      editObj.NdisDataStored = true;
    }
    else { editObj.NdisElemMsg = errMsg; }
    return editObj;
  }

  public ServiceRestrictionOrUxm DeleteRestrictionOr(ServiceRestrictionOrUxm editObj, bool byStorProc = true)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.RestrictionOrHasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuid, EGS);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableRestrictionOrByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = NPDSDC.NpdsAgentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeServiceRestrictionOrDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid,
          storObj.RestrictionAndGuidRef, storObj.RestrictionOrGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.CoreServiceRestrictionOrs.Attach(storObj);
        this.CoreServiceRestrictionOrs.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableRestrictionOrByKey(fgroupGuid);
      if (editObj == null) { editObj = new ServiceRestrictionOrUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

}

