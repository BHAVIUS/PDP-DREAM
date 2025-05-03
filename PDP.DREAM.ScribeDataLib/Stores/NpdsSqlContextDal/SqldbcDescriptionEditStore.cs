// SqldbcUilDescriptionEditStore.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public DescriptionEditModel EditDescription(DescriptionEditModel editObj, bool byStorProc = true)
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
    NexusDescription storObj;
    if (isNewRecord)
    {
      // insert new record
      internalGuid = Guid.NewGuid();
      storObj = new NexusDescription()
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
      storObj = GetStorableDescriptionByKey(internalGuid);
      storObj.UpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    storObj.Description = editObj.Description;
    storObj.FieldFormatCodeRef = editObj.FieldFormatCode;
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeDescriptionEdit(
        agentGuid, infosetGuid, recordGuid, internalGuid, storObj.FieldFormatCodeRef,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.Description);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusDescriptions.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableDescriptionByKey(internalGuid);
    if (editObj == null) { editObj = new DescriptionEditModel(); }
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

  public DescriptionEditModel DeleteDescription(DescriptionEditModel editObj, bool byStorProc = true)
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
      var storObj = GetStorableDescriptionByKey(internalGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = QURC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeDescriptionDelete(
          storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusDescriptions.Attach(storObj);
        this.NexusDescriptions.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableDescriptionByKey(internalGuid);
      if (editObj == null) { editObj = new DescriptionEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

} // end class

// end file
