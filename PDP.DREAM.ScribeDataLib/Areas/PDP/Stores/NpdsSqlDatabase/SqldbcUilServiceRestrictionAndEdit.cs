using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static RestrictionAndEditModel ToEditable(this NexusServiceRestrictionAnd r)
    {
      var nre = new RestrictionAndEditModel()
      {
        RestrictionAndGuidKey = r.RestrictionAndGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        RRInfosetGuid = r.InfosetGuidRef,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        //
        RestrictionName = r.RestrictionName,
        RestrictionAndIndex = r.HasIndex,
        RestrictionAndPriority = r.HasPriority,
        RestrictionIsSufficient = r.IsSufficient
      };
      return nre;
    }

    public static IQueryable<RestrictionAndEditModel> ToEditable(this IQueryable<NexusServiceRestrictionAnd> query)
    {
      IQueryable<RestrictionAndEditModel> rows =
        from r in query
        select new RestrictionAndEditModel
        {
          RestrictionAndGuidKey = r.RestrictionAndGuidKey,
          RRRecordGuid = r.RecordGuidRef,
          RRInfosetGuid = r.InfosetGuidRef,
          CreatedOn = r.CreatedOn,
          CreatedByAgentGuid = r.CreatedByAgentGuid,
          UpdatedOn = r.UpdatedOn,
          UpdatedByAgentGuid = r.UpdatedByAgentGuid,
          //
          RestrictionName = r.RestrictionName,
          RestrictionAndIndex = r.HasIndex,
          RestrictionAndPriority = r.HasPriority,
          RestrictionIsSufficient = r.IsSufficient
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<RestrictionAndEditModel> ListEditableRestrictionAnds(Guid recordGuid)
    {
      IEnumerable<RestrictionAndEditModel> result;
      try
      {
        IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
        qry = qry.Where(r => (r.RecordGuidRef == recordGuid));
        result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<RestrictionAndEditModel>();
      }
      return result;
    }

    public IQueryable<NexusServiceRestrictionAnd> QueryScribeRestrictionAndByKey(Guid guidKey)
    {
      IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
      qry = qry.Where(r => (r.RestrictionAndGuidKey == guidKey));
      return qry;
    }
    public NexusServiceRestrictionAnd GetStorableRestrictionAndByKey(Guid guidKey)
    { return QueryScribeRestrictionAndByKey(guidKey).SingleOrDefault(); }
    public NexusServiceRestrictionAnd GetStorableRestrictionAndByKey(string guidKey)
    { return GetStorableRestrictionAndByKey(Guid.Parse(guidKey)); }
    public RestrictionAndEditModel GetEditableRestrictionAndByKey(Guid guidKey)
    { return QueryScribeRestrictionAndByKey(guidKey).ToEditable().SingleOrDefault(); }
    public RestrictionAndEditModel GetEditableRestrictionAndByKey(string guidKey)
    { return GetEditableRestrictionAndByKey(Guid.Parse(guidKey)); }

    public RestrictionAndEditModel EditRestrictionAnd(RestrictionAndEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.RestrictionAndIndex;
      var recordPriority = editObj.RestrictionAndPriority;
      var agentGuid = PRC.AgentGuid;
      var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionAndGuidKey, Guid.Empty);
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
      storObj.HasPriority = editObj.RestrictionAndPriority;
      storObj.IsSufficient = editObj.RestrictionIsSufficient;

      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeServiceRestrictionAndEdit(
          agentGuid, infosetGuid, recordGuid, internalGuid,
          storObj.HasPriority, storObj.RestrictionName, storObj.IsSufficient);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusServiceRestrictionAnds.Add(storObj); }
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableRestrictionAndByKey(internalGuid);
      if (editObj == null) { editObj = new RestrictionAndEditModel(); }
      // refresh the recordIndex
      recordIndex = editObj.RestrictionAndIndex;
      // update the status message
      if (string.IsNullOrEmpty(errMsg))
      {
        editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} written to database";
        editObj.PdpStatusItemStored = true;
      }
      else { editObj.PdpStatusMessage = errMsg; }
      return editObj;
    }

    public RestrictionAndEditModel DeleteRestrictionAnd(RestrictionAndEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.RestrictionAndIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionAndGuidKey, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      if (!isNewRecord) // delete existing record
      {
        var storObj = GetStorableRestrictionAndByKey(internalGuid);
        storObj.DeletedByAgentGuid = PRC.AgentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
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
        if (editObj == null) { editObj = new RestrictionAndEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public IList<SelectListItem> GetItemsForRestrictionAndSelectList(Guid guidKey)
    {
      IEnumerable<RestrictionAndEditModel> restAndList = ListEditableRestrictionAnds(guidKey);
      IEnumerable<SelectListItem> uilItems
        = from and in restAndList
          orderby and.RestrictionAndIndex
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

}
