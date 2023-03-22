// TkgPageControllerServiceRestrictionAnd.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidRestrictionAndStatus = "span#ServiceRestrictionAndStatus";

  // ATTN: current implementation does not allow use of parameter isLimited
  public virtual JsonResult OnPostReadServiceRestrictionAnds([DataSourceRequest] DataSourceRequest dsRequest,
  // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadServiceRestrictionAnds);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionAnd", "RRRecordGuid invalid."); }
      else
      {
          dsResult = PSDC.ListEditableRestrictionAndsByRGuid(recordGuid)
          .ToDataSourceResult(dsRequest);
      }
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

  public virtual JsonResult OnPostWriteServiceRestrictionAnd([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionAndEditModel fgr)
  {
    OpenScribeConnection(); // use PSDC
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = PSDC.EditRestrictionAnd(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidRestrictionAndStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteServiceRestrictionAnd([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionAndEditModel fgr)
  {
    OpenScribeConnection(); // use PSDC
    if (ModelState.IsValid) { fgr = PSDC.DeleteRestrictionAnd(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidRestrictionAndStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file