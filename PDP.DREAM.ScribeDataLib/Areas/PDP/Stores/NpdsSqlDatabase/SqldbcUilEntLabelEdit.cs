using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public static partial class NpdsLinqSqlOperators
  {
    public static EntityLabelEditModel ToEditable(this NexusEntityLabel r)
    {
      var nre = new EntityLabelEditModel()
      {
        RRFgroupGuid = r.FgroupGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        ManagedByAgentGuid = r.ManagedByAgentGuidRef,
        ManagedByAgentName = r.ManagedByAgentUserName,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuidRef,
        CreatedByAgentName = r.CreatedByAgentUserName,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
        UpdatedByAgentName = r.UpdatedByAgentUserName,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuidRef,
        DeletedByAgentName = r.DeletedByAgentUserName,
        //
        IsResolvable = r.IsResolvable,
        IsPrivate = r.IsPrivate,
        IsGenerating = r.IsGenerating,
        ServiceTypeCode = r.ServiceTypeCodeRef,
        TagToken = r.TagToken,
        LabelUri = r.LabelUri,
        EntityLabel = r.EntityLabel
      };
      return nre;
    }

    public static IQueryable<EntityLabelEditModel> ToEditable(this IQueryable<NexusEntityLabel> query)
    {
      IQueryable<EntityLabelEditModel> rows =
        from r in query
        select new EntityLabelEditModel
        {
          RRFgroupGuid = r.FgroupGuidKey,
          RRRecordGuid = r.RecordGuidRef,
          HasIndex = r.HasIndex,
          HasPriority = r.HasPriority,
          IsMarked = r.IsMarked,
          IsPrincipal = r.IsPrincipal,
          IsDeleted = r.IsDeleted,
          ManagedByAgentGuid = r.ManagedByAgentGuidRef,
          ManagedByAgentName = r.ManagedByAgentUserName,
          CreatedOn = r.CreatedOn,
          CreatedByAgentGuid = r.CreatedByAgentGuidRef,
          CreatedByAgentName = r.CreatedByAgentUserName,
          UpdatedOn = r.UpdatedOn,
          UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
          UpdatedByAgentName = r.UpdatedByAgentUserName,
          DeletedOn = r.DeletedOn,
          DeletedByAgentGuid = r.DeletedByAgentGuidRef,
          DeletedByAgentName = r.DeletedByAgentUserName,
          //
          IsResolvable = r.IsResolvable,
          IsPrivate = r.IsPrivate,
          IsGenerating = r.IsGenerating,
          ServiceTypeCode = r.ServiceTypeCodeRef,
          TagToken = r.TagToken,
          LabelUri = r.LabelUri,
          EntityLabel = r.EntityLabel
        };
      return rows;
    }

  }

  public partial class ScribeDbsqlContext
  {
    public IEnumerable<EntityLabelEditModel> ListEditableEntityLabels(Guid guidKey, bool isLimited = false)
    {
      IEnumerable<EntityLabelEditModel> result;
      try
      {
        IQueryable<NexusEntityLabel> qry = this.NexusEntityLabels;
        if (PRC.ClientHasAdminAccess || PRC.ClientHasEditorAccess)
        { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
        else
        {
          if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == PRC.AgentGuid)); }
          else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
        }
        result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
      }
      catch
      {
        result = Enumerable.Empty<EntityLabelEditModel>();
      }
      return result;
    }

    public EntityLabelEditModel GetEditableEntityLabelByKey(Guid guidKey)
    { return QueryStorableEntityLabelByKey(guidKey).ToEditable().SingleOrDefault(); }
    public EntityLabelEditModel GetEditableEntityLabelByKey(string guidKey)
    { return GetEditableEntityLabelByKey(Guid.Parse(guidKey)); }

    public EntityLabelEditModel EditEntityLabel(EntityLabelEditModel editObj, bool byStorProc = true)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.HasIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      NexusEntityLabel storObj;
      if (isNewRecord)
      {
        // insert new record
        internalGuid = Guid.NewGuid();
        storObj = new NexusEntityLabel()
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
        storObj = GetStorableEntityLabelByKey(internalGuid);
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
           agentGuid, infosetGuid, recordGuid, internalGuid,
           null, null, null, null, storObj.ServiceTypeCodeRef, storObj.TagToken, storObj.LabelUri,
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
      editObj = GetEditableEntityLabelByKey(internalGuid);
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
      var agentGuid = PRC.AgentGuid;
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      if (!isNewRecord) // delete existing record
      {
        var storObj = GetStorableEntityLabelByKey(internalGuid);
        storObj.DeletedByAgentGuidRef = agentGuid;
        storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
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
        editObj = GetEditableEntityLabelByKey(internalGuid);
        if (editObj == null) { editObj = new EntityLabelEditModel(); }
        // update the status message
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual EntityLabelEditModel CheckEntityLabel(EntityLabelEditModel editObj)
    {
      var errMsg = string.Empty;
      var recordName = editObj.ItemXnam;
      var recordIndex = editObj.HasIndex;
      var recordPriority = editObj.HasPriority;
      var agentGuid = PRC.AgentGuid;
      var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
      var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
      var isNewRecord = internalGuid.IsEmpty();
      if (!isNewRecord)
      {
        // refresh object
        editObj = GetEditableEntityLabelByKey(internalGuid);
        if (editObj == null) { editObj = new EntityLabelEditModel(); }
        if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
        else { editObj.PdpStatusMessage = errMsg; }
      }
      return editObj;
    }

    public virtual short CheckEntityLabels(Guid recordGuid)
    {
      var rrr = GetEditableResrepStemByRKey(recordGuid);
      return CheckEntityLabels(ref rrr);
    }
    public virtual short CheckEntityLabels(ref NexusResrepEditModel rrr)
    {
      short statusCode = 0;
      var recordGuid = (Guid)rrr.RRRecordGuid;
      var registryGuid = (Guid)rrr.RecordRegistryGuid;
      var items = ListEditableEntityLabels(recordGuid).Select(st => st.EntityLabel.ToLower());
      if (items.Any())
      {
        statusCode = CheckSupportingStrings(items, registryGuid);
      }
      else
      {
        statusCode = (short)NpdsConst.InfosetStatus.None;
      }
      rrr.EntityLabelsStatusCode = statusCode;
      return statusCode;
    }

  }

}
