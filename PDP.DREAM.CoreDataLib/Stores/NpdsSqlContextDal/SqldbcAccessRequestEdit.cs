// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static IEnumerable<AccessResrepRequestUxm> ToEditable(this IQueryable<CoreAccessRequest> query)
  {
    IEnumerable<AccessResrepRequestUxm> rows =
      from r in query
      select new AccessResrepRequestUxm
      {
        RecordHandle = r.RecordHandle,
        EntityName = r.EntityName,
        RequestedForAgentGuid = r.RequestedForAgentGuid,
        RequestedForAgentAlias = r.RequestedForAgentAlias,
        AllowedByAgentGuid = r.AllowedByAgentGuid,
        AllowedByAgentAlias = r.AllowedByAgentAlias,
        DeniedByAgentGuid = r.DeniedByAgentGuid,
        DeniedByAgentAlias = r.DeniedByAgentAlias,
        AccessAsAuthor = r.AccessAsAuthor,
        AccessAsReviewer = r.AccessAsReviewer,
        AccessAsEditor = r.AccessAsEditor,
        AccessForService = r.AccessForService,
        AccessIsAllowed = r.AccessIsAllowed,
        AccessIsDenied = r.AccessIsDenied,
        AuthorHasResrepAccess = r.AuthorHasResrepAccess ?? false,
        ReviewerHasResrepAccess = r.ReviewerHasResrepAccess ?? false,
        EditorHasResrepAccess = r.EditorHasResrepAccess ?? false,
        AuthorHasServiceAccess = r.AuthorHasServiceAccess ?? false,
        ReviewerHasServiceAccess = r.ReviewerHasServiceAccess ?? false,
        EditorHasServiceAccess = r.EditorHasServiceAccess ?? false,

        RRInfosetGuid = r.InfosetGuid,
        RRRecordGuid = r.RecordGuid,
        RRFgroupGuid = r.FgroupGuid,
        HasIndex = r.HasIndex,
        IsDeleted = r.IsDeleted,

        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuid,
        CreatedByAgentAlias = r.CreatedByAgentAlias,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuid,
        UpdatedByAgentAlias = r.UpdatedByAgentAlias,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuid,
        DeletedByAgentAlias = r.DeletedByAgentAlias
        //

      };
    return rows;
  }

}

public partial class ScribeDbsqlContext
{
  public IEnumerable<AccessResrepRequestUxm> ListEditableAccessRequests(string requestedRole = "", bool isService = false)
  {
    IEnumerable<AccessResrepRequestUxm> result;
#if DEBUG
    Debug.WriteLine($"RecordAccess = '{NPDSDC.RecordAccess}'");
#endif
    try
    {
      IQueryable<CoreAccessRequest> qry = this.CoreAccessRequests;
      if (!NPDSDC.ClientHasAdminMode)
      {
        qry = qry.Where(r => (r.IsDeleted == false));
      }
      // resrep vs service
      qry = qry.Where(r => (r.AccessForService == isService));
      // author vs reviewer vs editor
      switch (requestedRole)
      {
        case "Author":
          qry = qry.Where(r => (r.AccessAsAuthor == true));
          break;
        case "Reviewer":
          qry = qry.Where(r => (r.AccessAsReviewer == true));
          break;
        case "Editor":
          qry = qry.Where(r => (r.AccessAsEditor == true));
          break;
#if DEBUG
        default:
          Debug.WriteLine($"switch case '{requestedRole}' not valid");
          break;
#endif
      }
      result = qry.OrderBy(r => r.EntityName)
        .ThenByDescending(r => r.UpdatedOn).ToEditable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<AccessResrepRequestUxm>();
    }
    return result;
  }

  public IQueryable<CoreAccessRequest> QueryAccessRequestByKey(Guid guidKey)
  {
    IQueryable<CoreAccessRequest> qry = this.CoreAccessRequests;
    qry = qry.Where(r => (r.FgroupGuid == guidKey));
    return qry;
  }
  public CoreAccessRequest GetStorableAccessRequestByKey(Guid guidKey)
  { return QueryAccessRequestByKey(guidKey).SingleOrDefault(); }
  public CoreAccessRequest GetStorableAccessRequestByKey(string guidKey)
  { return GetStorableAccessRequestByKey(Guid.Parse(guidKey)); }
  public AccessResrepRequestUxm GetEditableAccessRequestByKey(Guid guidKey)
  { return QueryAccessRequestByKey(guidKey).ToEditable().SingleOrDefault(); }
  public AccessResrepRequestUxm GetEditableAccessRequestByKey(string guidKey)
  { return GetEditableAccessRequestByKey(Guid.Parse(guidKey)); }

  public AccessResrepRequestUxm EditAccessRequest(AccessResrepRequestUxm editObj, bool byStorProc = true)
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
    CoreAccessRequest storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = PdpNewGuid();
      storObj = new CoreAccessRequest()
      {
        RequestedForAgentGuid = agentGuid,
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        InfosetGuid = infosetGuid,
        RecordGuid = recordGuid,
        FgroupGuid = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableAccessRequestByKey(fgroupGuid);
      storObj.UpdatedByAgentGuid = agentGuid;
      if (NPDSDC.ClientCanEditorWrite)
      {
        storObj.AccessIsAllowed = editObj.AccessIsAllowed;
        storObj.AccessIsDenied = editObj.AccessIsDenied;
        // HasResrepAccess set automatically in storproc per the approved/denied
        // storObj.AuthorHasResrepAccess = editObj.AuthorHasResrepAccess;
      }
    }

    // begin common insert/update edit
    // end common insert/update edit

    if (byStorProc)
    {
      // TODO: rebuild a storObj with an InfosetTypeCode in the view from the linked tables
      var errCod = ScribeAccessRequestEdit(
        storObj.RequestedForAgentGuid,
        agentGuid, infosetGuid, recordGuid, fgroupGuid,
        editObj.InfosetTypeCode, storObj.AccessIsAllowed, storObj.AccessIsDenied);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.CoreAccessRequests.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableAccessRequestByKey(fgroupGuid);
    if (editObj == null) { editObj = new AccessResrepRequestUxm(); }
    // refresh the recordIndex
    recordIndex = editObj.HasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} written to database";
      editObj.NdisDataStored = true;
    }
    else { editObj.NdisElemMsg = errMsg; }
    return editObj;
  }

  public AccessResrepRequestUxm DeleteAccessRequest(AccessResrepRequestUxm editObj, bool byStorProc = true)
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
      var storObj = GetStorableAccessRequestByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeAccessRequestDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.CoreAccessRequests.Attach(storObj);
        this.CoreAccessRequests.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableAccessRequestByKey(fgroupGuid);
      if (editObj == null) { editObj = new AccessResrepRequestUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

}
