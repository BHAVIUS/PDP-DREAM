// SqldbcUilResrepSnapshotEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public NexusSnapshotEditModel EditSnapshot(NexusSnapshotEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = QURC.QebAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    NexusNexusSnapshot storObj;
    if (isNewRecord)
    {
      QURC.VerboseFormatReqst = true;
      // insert new record
      internalGuid = Guid.NewGuid();
      storObj = new NexusNexusSnapshot()
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
      if (isNewRecord) { this.NexusNexusSnapshots.Add(storObj); }
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
    var agentGuid = QURC.QebAgentGuid;
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableSnapshotByKey(internalGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = QURC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeResrepSnapshotDelete(
          storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusNexusSnapshots.Attach(storObj);
        this.NexusNexusSnapshots.Remove(storObj);
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

}
