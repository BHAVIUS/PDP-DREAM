// SqldbcUilServiceDefaultEdit.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static ServiceDefaultEditModel ToEditable(this NexusServiceCoreDefault r)
  {
    var nre = new ServiceDefaultEditModel()
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
      DiristryGuid = r.DiristryInfosetGuidRef,
      DiristryName = r.DiristryName,
      RegistryGuid = r.RegistryInfosetGuidRef,
      RegistryName = r.RegistryName,
      DirectoryGuid = r.DirectoryInfosetGuidRef,
      DirectoryName = r.DirectoryName,
      RegistrarGuid = r.RegistrarInfosetGuidRef,
      RegistrarName = r.RegistrarName,
    };
    return nre;
  }

  public static IQueryable<ServiceDefaultEditModel> ToEditable(this IQueryable<NexusServiceCoreDefault> query)
  {
    IQueryable<ServiceDefaultEditModel> rows =
      from r in query
      select new ServiceDefaultEditModel
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
        DiristryGuid = r.DiristryInfosetGuidRef,
        DiristryName = r.DiristryName,
        RegistryGuid = r.RegistryInfosetGuidRef,
        RegistryName = r.RegistryName,
        DirectoryGuid = r.DirectoryInfosetGuidRef,
        DirectoryName = r.DirectoryName,
        RegistrarGuid = r.RegistrarInfosetGuidRef,
        RegistrarName = r.RegistrarName,
      };
    return rows;
  }

}

public partial class ScribeDbsqlContext
{
  public IEnumerable<ServiceDefaultEditModel> ListEditableServiceDefaults(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<ServiceDefaultEditModel> result;
    try
    {
      IQueryable<NexusServiceCoreDefault> qry = this.NexusServiceCoreDefaults;
      if (QURC.ClientHasAdminAccess || QURC.ClientHasEditorAccess)
      { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == QURC.QebAgentGuid)); }
        else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
      }
      result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<ServiceDefaultEditModel>();
    }
    return result;
  }

  public IQueryable<NexusServiceCoreDefault> QueryScribeServiceDefaultByKey(Guid guidKey)
  {
    IQueryable<NexusServiceCoreDefault> qry = this.NexusServiceCoreDefaults;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

  public NexusServiceCoreDefault GetStorableServiceDefaultByKey(Guid guidKey)
  { return QueryScribeServiceDefaultByKey(guidKey).SingleOrDefault(); }
  public NexusServiceCoreDefault GetStorableServiceDefaultByKey(string guidKey)
  { return GetStorableServiceDefaultByKey(Guid.Parse(guidKey)); }

  public ServiceDefaultEditModel GetEditableServiceDefaultByKey(Guid guidKey)
  { return QueryScribeServiceDefaultByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceDefaultEditModel GetEditableServiceDefaultByKey(string guidKey)
  { return GetEditableServiceDefaultByKey(Guid.Parse(guidKey)); }

  public ServiceDefaultEditModel EditServiceDefault(ServiceDefaultEditModel editObj, bool byStorProc = true)
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
    NexusServiceCoreDefault storObj;
    if (isNewRecord)
    {
      // insert new record
      internalGuid = Guid.NewGuid();
      storObj = new NexusServiceCoreDefault()
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
      storObj = GetStorableServiceDefaultByKey(internalGuid);
      storObj.UpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    storObj.DiristryInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.DiristryGuid, QURC.DiristryGuidDeflt); // Nexus
    storObj.RegistryInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.RegistryGuid, QURC.RegistryGuidDeflt); // PORTAL
    storObj.DirectoryInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.DirectoryGuid, QURC.DirectoryGuidDeflt); // DOORS
    storObj.RegistrarInfosetGuidRef = PdpGuid.ParseToNonNullable(editObj.RegistrarGuid, QURC.RegistrarGuidDeflt); // Scribe
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeServiceCoreDefaultEdit(
        agentGuid, infosetGuid, recordGuid, internalGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.DiristryInfosetGuidRef, storObj.RegistryInfosetGuidRef, storObj.DirectoryInfosetGuidRef, storObj.RegistrarInfosetGuidRef);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusServiceCoreDefaults.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableServiceDefaultByKey(internalGuid);
    if (editObj == null) { editObj = new ServiceDefaultEditModel(); }
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

  public ServiceDefaultEditModel DeleteServiceDefault(ServiceDefaultEditModel editObj, bool byStorProc = true)
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
      var storObj = GetStorableServiceDefaultByKey(internalGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = QURC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeServiceCoreDefaultDelete(
          storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusServiceCoreDefaults.Attach(storObj);
        this.NexusServiceCoreDefaults.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableServiceDefaultByKey(internalGuid);
      if (editObj == null) { editObj = new ServiceDefaultEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

}

