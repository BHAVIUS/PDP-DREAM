// SqldbcUilResrepRootEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public NexusResrepEditModel EditResrepRoot(NexusResrepEditModel editObj, bool byStorProc = true)
  {
    int? errCod = 0;
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    NexusResrepRoot storObj;
    if (isNewRecord)
    {
      // insert new record
      recordGuid = PdpGuid.NewGuid();
      infosetGuid = PdpGuid.NewGuid();
      storObj = new NexusResrepRoot()
      {
        RecordCreatedByAgentGuidRef = agentGuid,
        RecordUpdatedByAgentGuidRef = agentGuid,
        RecordManagedByAgentGuidRef = agentGuid,
        RecordGuidKey = recordGuid,
        InfosetGuidKey = infosetGuid
      };
    }
    else
    {
      // update existing record
      storObj = (NexusResrepRoot)GetStorableNexusRootByRKey(recordGuid);
      storObj.RecordUpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit

    //if (QURC.ClientHasAdminAccess)
    //{
    //  // allow admin curators to edit values for each of NPDS
    //  storObj.RecordDiristryGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordDiristryGuid, QURC.DiristryGuidDeflt); // Nexus
    //  storObj.RecordRegistryGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordRegistryGuid, QURC.RegistryGuidDeflt); // PORTAL
    //  storObj.RecordDirectoryGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordDirectoryGuid, QURC.DirectoryGuidDeflt); // DOORS
    //  storObj.RecordRegistrarGuidRef = PdpGuid.ParseToNonNullable(editObj.RecordRegistrarGuid, QURC.RegistrarGuidDeflt); // Scribe
    //}

    // do not allow change from current request settings for non-admin curators
    // get current preferred services for selected diristry from database
    Guid? diristryGuid = Guid.Empty;
    Guid? registryGuid = Guid.Empty;
    Guid? directoryGuid = Guid.Empty;
    Guid? registrarGuid = Guid.Empty;
    errCod = CoreDefaultServices(NPDSCP.DiristryTag, ref diristryGuid, ref registryGuid, ref directoryGuid, ref registrarGuid);
    storObj.RecordDiristryGuidRef = PdpGuid.ParseToNonNullable(diristryGuid, Guid.Empty); // Nexus
    storObj.RecordRegistryGuidRef = PdpGuid.ParseToNonNullable(registryGuid, Guid.Empty); // PORTAL
    storObj.RecordDirectoryGuidRef = PdpGuid.ParseToNonNullable(directoryGuid, Guid.Empty); // DOORS
    storObj.RecordRegistrarGuidRef = PdpGuid.ParseToNonNullable(registrarGuid, Guid.Empty); // Scribe

    // TODO: must redesign/rebuild to address current redundancy in NPDS scheme with diristry = directory + registry
    // ATTN: current scheme only resets diristry to registry if registry = directory and if diristry invalid/empty
    // assure consistency of current scheme until rebuilt with consistency checks
    if (PdpGuid.IsInvalidGuid(storObj.RecordDiristryGuidRef))
    { storObj.RecordDiristryGuidRef = storObj.RecordRegistryGuidRef; }
    if (PdpGuid.IsInvalidGuid(storObj.RecordDiristryGuidRef))
    { storObj.RecordDiristryGuidRef = storObj.RecordDirectoryGuidRef; }
    if (PdpGuid.IsInvalidGuid(storObj.RecordDiristryGuidRef))
    { storObj.RecordDiristryGuidRef = storObj.RecordRegistrarGuidRef; }

    // max chars for gridcol display and database store
    // EntityTag 32 / 64 
    // EntityName 64 / 256
    // EntityNature 128 / 1024

    // allowed for Agents
    storObj.EntityNature = editObj.EntityNature.ParseLeft(1024);

    // not allowed for Agents
    if (NPDSCP.ClientHasScribeEditAccess)
    {
      // when not already initialized elsewhere
      if (string.IsNullOrWhiteSpace(editObj.EntityInitialTag))
      {
        if (string.IsNullOrWhiteSpace(editObj.EntityName)) { editObj.EntityInitialTag = PdpRandom.RandGuidString(); }
        else { editObj.EntityInitialTag = editObj.EntityName.CleanPhrase().CreateAcronym(); }
      }
      storObj.EntityInitialTag = editObj.EntityInitialTag.ParseLeft(64);
      storObj.EntityTypeCodeRef = editObj.EntityTypeCode;
      storObj.EntityName = editObj.EntityName.ParseLeft(256);
      // storObj.EntityOwnerLabel = editObj.EntityOwnerLabel;
      // storObj.EntityContactLabel = editObj.EntityContactLabel;
      // storObj.EntityOtherLabel = editObj.EntityOtherLabel;
      storObj.InfosetIsAuthorPrivate = editObj.InfosetIsAuthorPrivate;
      storObj.InfosetIsAgentShared = editObj.InfosetIsAgentShared;
      storObj.InfosetIsUpdaterLimited = editObj.InfosetIsUpdaterLimited;
      storObj.InfosetIsManagerReleased = editObj.InfosetIsManagerReleased;
    }

    // end common insert/update edit

    if (byStorProc)
    {
      errCod = ScribeResrepRootEdit(agentGuid, storObj.InfosetGuidKey, recordGuid,
       storObj.EntityTypeCodeRef, storObj.EntityInitialTag, storObj.EntityName, storObj.EntityNature,
       // storObj.EntityOwnerLabel, storObj.EntityContactLabel, storObj.EntityOtherLabel,
       storObj.RecordDiristryGuidRef, storObj.RecordRegistryGuidRef, storObj.RecordDirectoryGuidRef, storObj.RecordRegistrarGuidRef,
       storObj.InfosetIsAuthorPrivate, storObj.InfosetIsAgentShared, storObj.InfosetIsUpdaterLimited, storObj.InfosetIsManagerReleased);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with handle {recordHandle}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusResrepRoots.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableResrepRootByRKey(recordGuid);
    if (editObj == null) { editObj = new NexusResrepEditModel(); }
    // refresh the record handle
    recordHandle = editObj.RecordHandle;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.PdpStatusMessage =
        $"{recordName} record with handle {recordHandle} written to database"; editObj.PdpStatusItemStored = true;
    }
    else { editObj.PdpStatusMessage = errMsg; }
    return editObj;
  }

  public NexusResrepEditModel DeleteResrepRoot(NexusResrepEditModel editObj, bool byStorProc = true)
  {
    var errorMessage = string.Empty;
    var recordMessage = $" {editObj.ItemXnam} record ";
    var agentGuid = NPDSCP.ClientAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    NexusResrepRoot storObj;
    if (!isNewRecord) // delete existing record
    {
      storObj = (NexusResrepRoot)GetStorableNexusRootByRKey(recordGuid);
      if (storObj == null) { errorMessage = $"Database error while getting {recordMessage}"; }
      else
      {
        recordMessage = $" {recordMessage} with handle {storObj.RecordHandle} ";
        storObj.RecordDeletedByAgentGuidRef = agentGuid;
        storObj.RecordIsDeleted = NPDSCP.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errorCode = ScribeResrepRootDelete(
          storObj.RecordDeletedByAgentGuidRef, storObj.RecordGuidKey, storObj.RecordIsDeleted);
          if (errorCode < 0) { errorMessage = $"Database error = {errorCode} while deleting {recordMessage}"; }
        }
        else
        {
          this.NexusResrepRoots.Attach(storObj);
          this.NexusResrepRoots.Remove(storObj);
          errorMessage = StoreChanges();
        }
      }
      // refresh the edit object
      editObj = GetEditableResrepRootByKey(recordGuid);
      if (editObj == null) { editObj = new NexusResrepEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errorMessage)) { editObj.PdpStatusMessage = $"{recordMessage} deleted from database"; }
      else { editObj.PdpStatusMessage = errorMessage; }
    }
    return editObj;
  }

  public NexusResrepEditModel RequestReleaseResrepRecord(NexusResrepEditModel editObj)
  {
    var errMsg = string.Empty;
    var reqrel = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    if (!isNewRecord) // check existing record
    {
      var errCod = ScribeResrepAuthorRequestEdit(agentGuid, agentGuid, infosetGuid, recordGuid, null, false, false);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing {recordName} record to database"; }

      if (string.IsNullOrEmpty(errMsg))
      {
        reqrel = ((editObj.ManagedByAgentGuid == NPDSCP.ClientAgentGuid) ? "released" : "requested");
        editObj.PdpStatusLabel = reqrel;
        editObj.PdpStatusMessage = $"Authorship for record {recordHandle} has been {reqrel}";
      }
      else
      {
        editObj.PdpStatusMessage = errMsg;
      }
    }
    return editObj;
  }

  public NexusResrepEditModel ArchiveResrepRecord(NexusResrepEditModel editObj)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var isNewRecord = recordGuid.IsEmpty();
    if (isNewRecord)
    {
      errMsg = "Record not archived: missing record handle.";
    }
    else  // validate existing record
    {
      var archObj = new NexusSnapshotEditModel()
      {
        RRRecordGuid = recordGuid
      };
      try
      {
        archObj = EditSnapshot(archObj);
        errMsg = "been archived successfully.";
      }
      catch
      {
        errMsg = "not been archived; a server database error occurred.";
      }
    }
    editObj.PdpStatusMessage = $"{recordName} record with handle {recordHandle} has {errMsg}";
    return editObj;
  }

} // end class

// end file