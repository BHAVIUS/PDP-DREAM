// SqldbcUilSrvcRstrctAndEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ServiceRestrictionAndEditModel EditRestrictionAnd(ServiceRestrictionAndEditModel editObj, bool byStorProc = true)
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
    NexusServiceRestrictionAnd storObj;
    if (isNewRecord)
    {
      // insert new record
      internalGuid = Guid.NewGuid();
      storObj = new NexusServiceRestrictionAnd()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuidRef = recordGuid,
        RestrictionAndGuidKey = internalGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableRestrictionAndByKey(internalGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit

    storObj.RestrictionName = editObj.RestrictionName;
    storObj.AndHasPriority = editObj.RestrictionAndHasPriority;
    storObj.IsExcluding = editObj.IsExcluding;
    storObj.IsSufficient = editObj.IsSufficient;

    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeServiceRestrictionAndEdit(
        agentGuid, infosetGuid, recordGuid, internalGuid,
        storObj.AndHasPriority, storObj.RestrictionName, storObj.IsExcluding, storObj.IsSufficient);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusServiceRestrictionAnds.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableRestrictionAndByKey(internalGuid);
    if (editObj == null) { editObj = new ServiceRestrictionAndEditModel(); }
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

  public ServiceRestrictionAndEditModel DeleteRestrictionAnd(ServiceRestrictionAndEditModel editObj, bool byStorProc = true)
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
      var storObj = GetStorableRestrictionAndByKey(internalGuid);
      storObj.DeletedByAgentGuid = QURC.QebAgentGuid;
      storObj.IsDeleted = QURC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeServiceRestrictionAndDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuidRef,
			storObj.RestrictionAndGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusServiceRestrictionAnds.Attach(storObj);
        this.NexusServiceRestrictionAnds.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableRestrictionAndByKey(internalGuid);
      if (editObj == null) { editObj = new ServiceRestrictionAndEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

  public IList<SelectListItem> GetItemsForRestrictionAndSelectList(Guid guidKey)
  {
    IEnumerable<ServiceRestrictionAndEditModel> restAndList = ListEditableRestrictionAndsByRGuid(guidKey);
    IEnumerable<SelectListItem> uilItems
      = from and in restAndList
        orderby and.HasIndex
        select new SelectListItem
        {
          Text = and.RestrictionName,
          Value = and.RRFgroupGuid.ToString()
        };
    IList<SelectListItem> list = uilItems.ToList();
    if (list.Count == 0) { list = GetEmptyGuidSelectList(); }
    return list;
  }
  private IList<SelectListItem> GetEmptyGuidSelectList()
  {
    List<SelectListItem> list = new List<SelectListItem>();
    list.Add(new SelectListItem() { Text = "Empty Guid", Value = Guid.Empty.ToString() });
    return list;
  }

}

