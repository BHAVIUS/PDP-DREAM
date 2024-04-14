// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public CoreResrepRootUxm EditResrepRoot(CoreResrepRootUxm uxm, bool byStorProc = true)
  {
    int? errCod = 0;
    var errMsg = ESS;
    var recordName = uxm.ItemXnam;
    var recordHandle = uxm.RecordHandle;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var infosetGuid = ParseToNonNullable(uxm.RRInfosetGuid, EGS);
    var recordGuid = ParseToNonNullable(uxm.RRRecordGuid, EGS);
    var isNewRecord = recordGuid.IsEmpty();
    CoreResrepRoot dto;
    if (isNewRecord)
    {
      // insert new record
      recordGuid = PdpNewGuid();
      infosetGuid = PdpNewGuid();
      dto = new CoreResrepRoot()
      {
        RecordCreatedByAgentGuid = agentGuid,
        RecordUpdatedByAgentGuid = agentGuid,
        RecordManagedByAgentGuid = agentGuid,
        RecordGuid = recordGuid,
        InfosetGuid = infosetGuid
      };
    }
    else
    {
      // update existing record
      dto = (CoreResrepRoot)GetStorableResrepRootByRKey(recordGuid);
      dto.RecordUpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit

    // get current preferred services for selected diristry from database
    Guid? diristryGuid = EGS, registryGuid = EGS, directoryGuid = EGS, registrarGuid = EGS;
    errCod = CoreServiceDefaultList(NPDSDC.DiristryTag, ref diristryGuid, ref registryGuid, ref directoryGuid, ref registrarGuid);
    dto.RecordDiristryGuid = ParseToNonNullable(uxm.RecordDiristryGuid, diristryGuid); // Nexus
    dto.RecordRegistryGuid = ParseToNonNullable(uxm.RecordRegistryGuid, registryGuid); // PORTAL
    dto.RecordDirectoryGuid = ParseToNonNullable(uxm.RecordDirectoryGuid, directoryGuid); // DOORS
    dto.RecordRegistrarGuid = ParseToNonNullable(uxm.RecordRegistrarGuid, registrarGuid); // Scribe

    // TODO: must redesign/rebuild to address current redundancy in NPDS scheme with diristry = directory + registry
    // ATTN: current scheme only resets diristry to registry if registry = directory and if diristry invalid/empty
    // assure consistency of current scheme until rebuilt with consistency checks
    if (IsInvalidGuid(dto.RecordDiristryGuid))
    { dto.RecordDiristryGuid = dto.RecordRegistryGuid; }
    if (IsInvalidGuid(dto.RecordDiristryGuid))
    { dto.RecordDiristryGuid = dto.RecordDirectoryGuid; }
    if (IsInvalidGuid(dto.RecordDiristryGuid))
    { dto.RecordDiristryGuid = dto.RecordRegistrarGuid; }

    // allowed for Agents
    dto.EntityNature = uxm.EntityNature.ParseLeft(EntityNatureMaxchars);

    // not allowed for Agents
    if (NPDSDC.ClientCanAuthorWrite)
    {
      // when not already initialized elsewhere
      if (string.IsNullOrWhiteSpace(uxm.EntityInitPrinTag))
      {
        if (string.IsNullOrWhiteSpace(uxm.EntityName)) { uxm.EntityInitPrinTag = PdpRandom.RandGuidString(); }
        else { uxm.EntityInitPrinTag = uxm.EntityName.CleanPhrase().CreateAcronym(); }
      }
      dto.EntityInitialPrincipalTag = uxm.EntityInitPrinTag.ParseLeft(PrincipalTagMaxchars);
      dto.EntityInitialSupportingTag = uxm.EntityInitSuppTag.ParseLeft(SupportingTagMaxchars);
      dto.EntityTypeCode = uxm.EntityTypeCode;
      dto.EntityName = uxm.EntityName.ParseLeft(EntityNameMaxchars);
      // dto.EntityOwnerLabel = uxm.EntityOwnerLabel;
      // dto.EntityContactLabel = uxm.EntityContactLabel;
      // dto.EntityOtherLabel = uxm.EntityOtherLabel;
      dto.InfosetIsAuthorPrivate = uxm.InfosetIsAuthorPrivate;
      dto.InfosetIsAgentShared = uxm.InfosetIsAgentShared;
      dto.InfosetIsUpdaterLimited = uxm.InfosetIsUpdaterLimited;
      dto.InfosetIsManagerReleased = uxm.InfosetIsManagerReleased;
    }

    // end common insert/update edit

    if (byStorProc)
    {
      errCod = ScribeResrepRootEdit(agentGuid, dto.InfosetGuid, recordGuid,
       dto.EntityTypeCode, dto.EntityName, dto.EntityNature,
       dto.EntityInitialPrincipalTag, dto.EntityInitialSupportingTag,
       // dto.EntityOwnerLabel, dto.EntityContactLabel, dto.EntityOtherLabel,
       dto.RecordDiristryGuid, dto.RecordRegistryGuid, dto.RecordDirectoryGuid, dto.RecordRegistrarGuid,
       dto.InfosetIsAuthorPrivate, dto.InfosetIsAgentShared, dto.InfosetIsUpdaterLimited, dto.InfosetIsManagerReleased);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with handle {recordHandle}"; }
    }
    else
    {
      if (isNewRecord) { this.CoreResrepRoots.Add(dto); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    uxm = GetEditableResrepRootByRKey(recordGuid);
    if (uxm == null) { uxm = new CoreResrepRootUxm(); }
    // refresh the record handle
    recordHandle = uxm.RecordHandle;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      uxm.NdisElemMsg =
        $"{recordName} record with handle {recordHandle} written to database";
      uxm.NdisDataStored = true;
    }
    else { uxm.NdisElemMsg = errMsg; }
    return uxm;
  }

  public CoreResrepRootUxm DeleteResrepRoot(CoreResrepRootUxm editObj, bool byStorProc = true)
  {
    var errorMessage = ESS;
    var recordMessage = $" {editObj.ItemXnam} record ";
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var recordGuid = ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var isNewRecord = recordGuid.IsEmpty();
    CoreResrepRoot storObj;
    if (!isNewRecord) // delete existing record
    {
      storObj = (CoreResrepRoot)GetStorableResrepRootByRKey(recordGuid);
      if (storObj == null) { errorMessage = $"Database error while getting {recordMessage}"; }
      else
      {
        recordMessage = $" {recordMessage} with handle {storObj.RecordHandle} ";
        storObj.RecordDeletedByAgentGuid = agentGuid;
        storObj.RecordIsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errorCode = ScribeResrepRootDelete(
          storObj.RecordDeletedByAgentGuid, storObj.RecordGuid, storObj.RecordIsDeleted);
          if (errorCode < 0) { errorMessage = $"Database error = {errorCode} while deleting {recordMessage}"; }
        }
        else
        {
          this.CoreResrepRoots.Attach(storObj);
          this.CoreResrepRoots.Remove(storObj);
          errorMessage = StoreChanges();
        }
      }
      // refresh the edit object
      editObj = GetEditableResrepRootByKey(recordGuid);
      if (editObj == null) { editObj = new CoreResrepRootUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errorMessage)) { editObj.NdisElemMsg = $"{recordMessage} deleted from database"; }
      else { editObj.NdisElemMsg = errorMessage; }
    }
    return editObj;
  }

  public CoreResrepRootUxm RequestRecordAccess(CoreResrepRootUxm editObj)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var infosetGuid = ParseToNonNullable(editObj.RRInfosetGuid, EGS);
    var recordGuid = ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    var infosetTypeCode = (short?)(editObj.InfosetTypeCode <= 0 ? 1 : editObj.InfosetTypeCode);
    var isNewRecord = recordGuid.IsEmpty();
    if (!isNewRecord) // check existing record
    {
      var errCod = ScribeAccessRequestEdit(agentGuid, agentGuid, infosetGuid, recordGuid,
        fgroupGuid, infosetTypeCode, false, false);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing {recordName} record to database"; }
      if (string.IsNullOrEmpty(errMsg))
      {
        editObj.NdisElemMsg = $"Access requested in database for {recordName} record with handle {recordHandle}";
        editObj.NdisLinkMsg = "Requested";
      }
      else
      {
        editObj.NdisElemMsg = errMsg;
        editObj.NdisLinkMsg = "Error";
      }
    }
    return editObj;
  }

  public CoreResrepRootUxm ArchiveResrepRecord(CoreResrepRootUxm editObj)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordHandle = editObj.RecordHandle;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var recordGuid = ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var isNewRecord = recordGuid.IsEmpty();
    if (isNewRecord)
    {
      errMsg = "Record not archived: missing record handle.";
    }
    else  // validate existing record
    {
      var archObj = new ResrepSnapshotUxm()
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
    editObj.NdisElemMsg = $"{recordName} record with handle {recordHandle} has {errMsg}";
    return editObj;
  }

} // end class

// end file