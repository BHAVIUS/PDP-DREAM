// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ResrepServiceUxm EditResrepService(ResrepServiceUxm editObj, bool byStorProc = true)
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
    CoreResrepService storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new CoreResrepService()
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
      storObj = GetStorableResrepServiceByKey(fgroupGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    var emptyGuid = EGS;  // TODO: rebuild with defaults
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordDiristryTag); // Nexus
    storObj.DiristryIGuid = PdpGuid.ParseToNonNullable(editObj.RecordDiristryGuid, emptyGuid); // Nexus
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordRegistryTag); // PORTAL
    storObj.RegistryIGuid = PdpGuid.ParseToNonNullable(editObj.RecordRegistryGuid, emptyGuid); // PORTAL
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordDirectoryTag); // DOORS
    storObj.DirectoryIGuid = PdpGuid.ParseToNonNullable(editObj.RecordDirectoryGuid, emptyGuid); // DOORS
    // tempGuid = NPDSSD.NpdsServiceCache.GetByTag(editObj.RecordRegistrarTag); // Scribe
    storObj.RegistrarIGuid = PdpGuid.ParseToNonNullable(editObj.RecordRegistrarGuid, emptyGuid); // Scribe
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeResrepServiceEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.DiristryIGuid, storObj.RegistryIGuid, storObj.DirectoryIGuid, storObj.RegistrarIGuid);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.CoreResrepServices.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableResrepServiceByKey(fgroupGuid);
    if (editObj == null) { editObj = new ResrepServiceUxm(); }
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

  public ResrepServiceUxm DeleteResrepService(ResrepServiceUxm editObj, bool byStorProc = true)
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
      var storObj = GetStorableResrepServiceByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeResrepServiceDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.CoreResrepServices.Attach(storObj);
        this.CoreResrepServices.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableResrepServiceByKey(fgroupGuid);
      if (editObj == null) { editObj = new ResrepServiceUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

} // end class

// end file
