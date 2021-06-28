using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static RestrictionOrEditModel ToEditable(this NexusServiceRestrictionOr r)
    {
      var nre = new RestrictionOrEditModel()
      {
        RestrictionOrGuidKey = r.RestrictionOrGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        RRInfosetGuid = r.InfosetGuidRef,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        //
        RestrictionAndGuidRef = r.RestrictionAndGuidRef,
        RestrictionOrIndex = r.OrHasIndex,
        RestrictionOrPriority = r.OrHasPriority,
        Restriction = r.RestrictionValue,
        IsWordPhrase = r.IsWordPhrase,
        IsConceptLabel = r.IsConceptLabel
      };
      return nre;
    }

    public static IQueryable<RestrictionOrEditModel> ToEditable(this IQueryable<NexusServiceRestrictionOr> query)
    {
      IQueryable<RestrictionOrEditModel> rows =
        from r in query
        select new RestrictionOrEditModel
        {
          RestrictionOrGuidKey = r.RestrictionOrGuidKey,
          RRRecordGuid = r.RecordGuidRef,
          RRInfosetGuid = r.InfosetGuidRef,
          CreatedOn = r.CreatedOn,
          CreatedByAgentGuid = r.CreatedByAgentGuid,
          UpdatedOn = r.UpdatedOn,
          UpdatedByAgentGuid = r.UpdatedByAgentGuid,
          //
          RestrictionAndGuidRef = r.RestrictionAndGuidRef,
          RestrictionOrIndex = r.OrHasIndex,
          RestrictionOrPriority = r.OrHasPriority,
          Restriction = r.RestrictionValue,
          IsWordPhrase = r.IsWordPhrase,
          IsConceptLabel = r.IsConceptLabel,
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<RestrictionOrEditModel> ListEditableRestrictionOrs(Guid guidKey)
    {
      IEnumerable<RestrictionOrEditModel> result;
      try
      {
        IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
        qry = qry.Where(r => (r.RestrictionAndGuidRef == guidKey));
        result = qry.OrderBy(r => r.OrHasPriority).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<RestrictionOrEditModel>();
      }
      return result;
    }

    public IQueryable<NexusServiceRestrictionOr> QueryScribeRestrictionOrByKey(Guid guidKey)
    {
      IQueryable<NexusServiceRestrictionOr> qry = this.NexusServiceRestrictionOrs;
      qry = qry.Where(r => (r.RestrictionOrGuidKey == guidKey));
      return qry;
    }
    public NexusServiceRestrictionOr GetStorableRestrictionOrByKey(Guid guidKey)
    { return QueryScribeRestrictionOrByKey(guidKey).SingleOrDefault(); }
    public NexusServiceRestrictionOr GetStorableRestrictionOrByKey(string guidKey)
    { return GetStorableRestrictionOrByKey(Guid.Parse(guidKey)); }
    public RestrictionOrEditModel GetEditableRestrictionOrByKey(Guid guidKey)
    { return QueryScribeRestrictionOrByKey(guidKey).ToEditable().SingleOrDefault(); }
    public RestrictionOrEditModel GetEditableRestrictionOrByKey(string guidKey)
    { return GetEditableRestrictionOrByKey(Guid.Parse(guidKey)); }

    public RestrictionOrEditModel EditRestrictionOr(RestrictionOrEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.RestrictionOrIndex;
      var recordPriority = editObj.RestrictionOrPriority;
      var agentGuid = PRC.AgentGuid;
      var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var foreignGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionAndGuidRef, Guid.Empty);
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuidKey, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      NexusServiceRestrictionOr storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusServiceRestrictionOr()
        {
          CreatedByAgentGuid = agentGuid,
          UpdatedByAgentGuid = agentGuid,
          RecordGuidRef = recordGuid,
          RestrictionAndGuidRef = foreignGuid,
          RestrictionOrGuidKey = internalGuid
        };
      }
      else
      {
        // update existing record
        storObj = GetStorableRestrictionOrByKey(internalGuid);
        storObj.UpdatedByAgentGuid = agentGuid;
      }

      // begin common insert/update edit

      storObj.RestrictionValue = editObj.Restriction;
      storObj.OrHasPriority = editObj.RestrictionOrPriority;
      storObj.IsWordPhrase = editObj.IsWordPhrase;
      storObj.IsConceptLabel = editObj.IsConceptLabel;

      // end common insert/update edit

      if (byStorProc)
      {
        var errCod = ScribeServiceRestrictionOrEdit(
		  agentGuid, infosetGuid, recordGuid, foreignGuid, internalGuid,
          storObj.OrHasPriority, storObj.RestrictionValue, storObj.IsWordPhrase, storObj.IsConceptLabel);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
      }
      else
      {
        if (isNewRecord) { this.NexusServiceRestrictionOrs.Add(storObj); }
        errMsg = ExecuteChanges();
      }
      // refresh the edit object
      editObj = GetEditableRestrictionOrByKey(internalGuid);
      if (editObj == null) { editObj = new RestrictionOrEditModel(); }
      // refresh the recordIndex
      recordIndex = editObj.RestrictionOrIndex;
      // update the status message
      if (string.IsNullOrEmpty(errMsg))
      {
        editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} written to database";
        editObj.PdpStatusItemStored = true;
      }
      else { editObj.PdpStatusMessage = errMsg; }
      return editObj;
    }

    public RestrictionOrEditModel DeleteRestrictionOr(RestrictionOrEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.RestrictionOrIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RestrictionOrGuidKey, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      if (!isNewRecord) // delete existing record
      {
        var storObj = GetStorableRestrictionOrByKey(internalGuid);
        storObj.DeletedByAgentGuid = PRC.AgentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
        if (byStorProc)
        {
          var errCod = ScribeServiceRestrictionOrDelete(
            storObj.DeletedByAgentGuid, storObj.RecordGuidRef,
            storObj.RestrictionAndGuidRef, storObj.RestrictionOrGuidKey, storObj.IsDeleted);
          if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
        }
        else
        {
          this.NexusServiceRestrictionOrs.Attach(storObj);
          this.NexusServiceRestrictionOrs.Remove(storObj);
          errMsg = ExecuteChanges();
        }
        // refresh the edit object
        editObj = GetEditableRestrictionOrByKey(internalGuid);
        if (editObj == null) { editObj = new RestrictionOrEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

  }

}
