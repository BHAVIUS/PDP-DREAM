// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public SupportingLabelUxm EditSupportingLabel(SupportingLabelUxm editObj, bool byStorProc = true)
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
    PortalSupportingLabel storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new PortalSupportingLabel()
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
      storObj = GetStorableSupportingLabelByKey(fgroupGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    storObj.SupportingLabel = editObj.SupportingLabel;
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeSupportingLabelEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.SupportingLabel);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.PortalSupportingLabels.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableSupportingLabelByKey(fgroupGuid);
    if (editObj == null) { editObj = new SupportingLabelUxm(); }
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

  public SupportingLabelUxm DeleteSupportingLabel(SupportingLabelUxm editObj, bool byStorProc = true)
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
      var storObj = GetStorableSupportingLabelByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeSupportingLabelDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.PortalSupportingLabels.Attach(storObj);
        this.PortalSupportingLabels.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableSupportingLabelByKey(fgroupGuid);
      if (editObj == null) { editObj = new SupportingLabelUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

} // end class

// end file