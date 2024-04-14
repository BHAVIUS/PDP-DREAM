// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public OtherTextUxm EditOtherText(OtherTextUxm editObj, bool byStorProc = true)
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
    PortalOtherText storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new PortalOtherText()
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
      storObj = GetStorableOtherTextByKey(fgroupGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    storObj.OtherText = editObj.OtherText ?? ESS;
    storObj.FieldFormatCode = editObj.FieldFormatCode;
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeOtherTextEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid, storObj.FieldFormatCode,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
          storObj.OtherText);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.PortalOtherTexts.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableOtherTextByKey(fgroupGuid);
    if (editObj == null) { editObj = new OtherTextUxm(); }
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

  public OtherTextUxm DeleteOtherText(OtherTextUxm editObj, bool byStorProc = true)
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
      var storObj = GetStorableOtherTextByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeOtherTextDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.PortalOtherTexts.Attach(storObj);
        this.PortalOtherTexts.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableOtherTextByKey(fgroupGuid);
      if (editObj == null) { editObj = new OtherTextUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

} // end class

// end file
