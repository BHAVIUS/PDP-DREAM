﻿// SqldbcUilEntLabelEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public EntityLabelEditModel EditEntityLabel(EntityLabelEditModel editObj, bool byStorProc = true)
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
    NexusEntityLabel storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = Guid.NewGuid();
      storObj = new NexusEntityLabel()
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
      storObj = GetStorableEntityLabelByKey(fgroupGuid);
      storObj.UpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;
    storObj.IsResolvable = editObj.IsResolvable;
    storObj.IsPrivate = editObj.IsPrivate;
    storObj.IsGenerating = editObj.IsGenerating;
    storObj.ServiceTypeCodeRef = editObj.ServiceTypeCode;
    storObj.TagToken = editObj.TagToken;
    storObj.LabelUri = editObj.LabelUri;
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeEntityLabelEdit(
         agentGuid, infosetGuid, recordGuid, fgroupGuid,
         null, null, null, null, 
         storObj.ServiceTypeCodeRef, storObj.TagToken, storObj.LabelUri,
         storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
         storObj.IsResolvable, storObj.IsPrivate, storObj.IsGenerating);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusEntityLabels.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableEntityLabelByKey(fgroupGuid);
    if (editObj == null) { editObj = new EntityLabelEditModel(); }
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

  public EntityLabelEditModel DeleteEntityLabel(EntityLabelEditModel editObj, bool byStorProc = true)
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
      var storObj = GetStorableEntityLabelByKey(fgroupGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = NPDSCP.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeEntityLabelDelete(
          storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusEntityLabels.Attach(storObj);
        this.NexusEntityLabels.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableEntityLabelByKey(fgroupGuid);
      if (editObj == null) { editObj = new EntityLabelEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

}

