// SqldbcUilServiceDefaultEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ServiceDefaultEditModel EditServiceDefault(ServiceDefaultEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    NexusCoreServiceDefault storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = Guid.NewGuid();
      storObj = new NexusCoreServiceDefault()
      {
        CreatedByAgentGuidRef = agentGuid,
        UpdatedByAgentGuidRef = agentGuid,
        RecordGuidRef = recordGuid,
        FgroupGuidKey = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableServiceDefaultByKey(fgroupGuid);
      storObj.UpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    var emptyGuid = Guid.Empty;
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordDiristryTag); // Nexus
    storObj.DiristryInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordDiristryGuid, emptyGuid); // Nexus
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordRegistryTag); // PORTAL
    storObj.RegistryInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordRegistryGuid, emptyGuid); // PORTAL
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordDirectoryTag); // DOORS
    storObj.DirectoryInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordDirectoryGuid, emptyGuid); // DOORS
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordRegistrarTag); // Scribe
    storObj.RegistrarInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordRegistrarGuid, emptyGuid); // Scribe
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeCoreDefaultServiceEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.DiristryInfosetGuidRef, storObj.RegistryInfosetGuidRef, storObj.DirectoryInfosetGuidRef, storObj.RegistrarInfosetGuidRef);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusCoreServiceDefaults.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableServiceDefaultByKey(fgroupGuid);
    if (editObj == null) { editObj = new ServiceDefaultEditModel(); }
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

  public ServiceDefaultEditModel DeleteServiceDefault(ServiceDefaultEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableServiceDefaultByKey(fgroupGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = NPDSCP.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeCoreDefaultServiceDelete(
          storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusCoreServiceDefaults.Attach(storObj);
        this.NexusCoreServiceDefaults.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableServiceDefaultByKey(fgroupGuid);
      if (editObj == null) { editObj = new ServiceDefaultEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

} // end class

// end file
