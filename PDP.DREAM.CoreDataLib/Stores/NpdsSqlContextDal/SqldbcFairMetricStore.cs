// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public FairMetricUxm EditFairMetric(FairMetricUxm editObj, bool byStorProc = true)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, EGS);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    var isNewRecord = fgroupGuid.IsEmpty();
    DoorsFairMetric storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new DoorsFairMetric()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuid = recordGuid,
        FgroupGuid = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableFairMetricByKey(fgroupGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    storObj.MInvalidOldClaim = editObj.MInvalidOldClaim;
    storObj.QValidOldClaim = editObj.QValidOldClaim;
    storObj.PInvalidNewClaim = editObj.PInvalidNewClaim;
    storObj.NValidNewClaim = editObj.NValidNewClaim;
    //storObj.FAIR1Q = editObj.FAIR1Q;
    //storObj.FAIR2M = editObj.FAIR2M;
    //storObj.FAIR3P = editObj.FAIR3P;
    //storObj.FAIR4N = editObj.FAIR4N;

    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeFairMetricEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.MInvalidOldClaim, storObj.QValidOldClaim, storObj.PInvalidNewClaim, storObj.NValidNewClaim);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.DoorsFairMetrics.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableFairMetricByKey(fgroupGuid);
    if (editObj == null) { editObj = new FairMetricUxm(); }
    // refresh the recordIndex
    recordIndex = editObj.HasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} written to database";
      editObj.NdisDataStored = true;
    }
    else { editObj.NdisElemMsg = errMsg; }
    return editObj;
  }

  public FairMetricUxm DeleteFairMetric(FairMetricUxm editObj, bool byStorProc = true)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableFairMetricByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeFairMetricDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.DoorsFairMetrics.Attach(storObj);
        this.DoorsFairMetrics.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableFairMetricByKey(fgroupGuid);
      if (editObj == null) { editObj = new FairMetricUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

}

