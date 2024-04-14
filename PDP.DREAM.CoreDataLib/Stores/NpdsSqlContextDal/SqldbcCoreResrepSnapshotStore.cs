// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ResrepSnapshotUxm EditSnapshot(ResrepSnapshotUxm editObj, bool byStorProc = true)
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
    CoreResrepSnapshot storObj;
    if (isNewRecord)
    {
      NPDSDC.VerboseFormat = true;
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new CoreResrepSnapshot()
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
      storObj = GetStorableSnapshotByKey(fgroupGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
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
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
      storObj.ResrepSnapshotXml);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.CoreResrepSnapshots.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableSnapshotByKey(fgroupGuid);
    if (editObj == null) { editObj = new ResrepSnapshotUxm(); }
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

  public ResrepSnapshotUxm DeleteSnapshot(ResrepSnapshotUxm editObj, bool byStorProc = true)
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
      var storObj = GetStorableSnapshotByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeResrepSnapshotDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.CoreResrepSnapshots.Attach(storObj);
        this.CoreResrepSnapshots.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableSnapshotByKey(fgroupGuid);
      if (editObj == null) { editObj = new ResrepSnapshotUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

}
