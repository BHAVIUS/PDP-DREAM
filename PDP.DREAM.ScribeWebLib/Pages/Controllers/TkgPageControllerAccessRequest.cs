// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public virtual JsonResult OnPostReadAccessRequests([DataSourceRequest] DataSourceRequest dsRequest,
    string recordAccess = "", string requestedRole = "", bool isService = false)
  {
    var rzrHndlr = nameof(OnPostReadAccessRequests);
    OpenScribeConnection(recordAccess); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      dsResult = PSDC.ListEditableAccessRequests(requestedRole, isService)
        .ToDataSourceResult(dsRequest);
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  // ATTN: model binding on bool may be different from Telerik versus JavaScript
  public virtual JsonResult OnPostWriteAccessRequest([DataSourceRequest] DataSourceRequest dsRequest,
    AccessResrepRequestUxm uxm, string recordAccess = "", string requestedRole = "Author", bool isService = false)
  {
    OpenScribeConnection(recordAccess); // use PSDC
    var recordName = uxm.ItemXnam;
    var recordGuid = PdpGuid.ParseToNonNullable(uxm.RRRecordGuid, EGS);
    if (recordGuid == EGS) // assure presence of valid resouceGuid
    {
      uxm.NdisElemMsg = $"{nameof(uxm.RRRecordGuid)} not valid for Resrep Access Request";
      ModelState.AddModelError(recordName, uxm.NdisElemMsg);
    }
    if (ModelState.IsValid)
    {
      uxm.InfosetTypeCode = AccessPolicyHack(requestedRole, isService);
      uxm = PSDC.EditAccessRequest(uxm);
    }
    uxm.NdisElemId = eidResrepStatus;
    DataSourceResult dsResult = (new[] { uxm }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteAccessRequest([DataSourceRequest] DataSourceRequest dsRequest,
   AccessResrepRequestUxm uxm, string recordAccess = "")
  {
    OpenScribeConnection(recordAccess); // use PSDC
    if (ModelState.IsValid)
    {
      uxm = PSDC.DeleteAccessRequest(uxm);
      uxm.NdisElemId = eidResrepStatus;
    }
    DataSourceResult dsResult = (new[] { uxm }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  // ATTN: model binding on bool may be different from Telerik versus JavaScript
  public virtual JsonResult OnPostRequestAccess([DataSourceRequest] DataSourceRequest dsRequest,
   Guid recordGuid, string recordAccess = "", string requestedRole = "Author", bool isService = false)
  {
    OpenScribeConnection(recordAccess); // use PSDC
    CoreResrepRootUxm? rrr = PSDC.GetEditableResrepRootByKey(recordGuid);
    if (rrr?.RRRecordGuid == recordGuid)
    {
      rrr.InfosetTypeCode = AccessPolicyHack(requestedRole, isService);
      rrr = PSDC.RequestRecordAccess(rrr);
      // TODO: need new conventions for where/how status element/message set (move to datastore methods)
      // TODO: need new conventions for resrep vs service on Author/Reviewer/Editor record access
      rrr.NdisElemId = eidResrepStatus;
      switch (requestedRole)
      {
        case "Author":
          rrr.NdisLinkId = eidAuthorLink + rrr.RecordHandle;
          break;
        case "Reviewer":
          rrr.NdisLinkId = eidReviewerLink + rrr.RecordHandle;
          break;
        case "Editor":
          rrr.NdisLinkId = eidEditorLink + rrr.RecordHandle;
          break;
#if DEBUG
        default:
          throw new Exception("");
#endif
      }
    }
    DataSourceResult dsResult = (new[] { rrr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  // TODO: rebuild record-enumerator based lookup for  
  //  InfosetTypeCode used as an access policy hack
  protected byte AccessPolicyHack(string requestedRole, bool isService)
  {
    byte infosetTypeCode = 1;
    switch (requestedRole)
    {
      case "Author":
        infosetTypeCode = 1; break;
      case "Reviewer":
        infosetTypeCode = 2; break;
      case "Editor":
        infosetTypeCode = 3; break;
    }
    if (isService == true) { infosetTypeCode += 3; }
    return infosetTypeCode;
  }
} // end class

// end file