// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public ServiceRestrictionAndUxm EditRestrictionAnd(ServiceRestrictionAndUxm editObj, bool byStorProc = true)
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
    CoreServiceRestrictionAnd storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new CoreServiceRestrictionAnd()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuid = recordGuid,
        RestrictionAndGuidKey = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableRestrictionAndByKey(fgroupGuid);
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
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        storObj.AndHasPriority, storObj.RestrictionName, storObj.IsExcluding, storObj.IsSufficient);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.CoreServiceRestrictionAnds.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableRestrictionAndByKey(fgroupGuid);
    if (editObj == null) { editObj = new ServiceRestrictionAndUxm(); }
    // refresh the recordIndex
    recordIndex = editObj.RestrictionAndHasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} written to database";
      editObj.NdisDataStored = true;
    }
    else { editObj.NdisElemMsg = errMsg; }
    return editObj;
  }

  public ServiceRestrictionAndUxm DeleteRestrictionAnd(ServiceRestrictionAndUxm editObj, bool byStorProc = true)
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
      var storObj = GetStorableRestrictionAndByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = NPDSDC.NpdsAgentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeServiceRestrictionAndDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid,
      storObj.RestrictionAndGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.CoreServiceRestrictionAnds.Attach(storObj);
        this.CoreServiceRestrictionAnds.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableRestrictionAndByKey(fgroupGuid);
      if (editObj == null) { editObj = new ServiceRestrictionAndUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

  public IList<SelectListItem> GetItemsForRestrictionAndSelectList(Guid guidKey)
  {
    IEnumerable<ServiceRestrictionAndUxm> restAndList = ListEditableRestrictionAndsByRGuid(guidKey);
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
    list.Add(new SelectListItem() { Text = "Empty Guid", Value = EGS.ToString() });
    return list;
  }

}

