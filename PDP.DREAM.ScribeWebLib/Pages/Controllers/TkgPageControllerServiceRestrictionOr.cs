// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public const string eidRestrictionOrStatus = TkndoElemPrfx + "ServiceRestrictionOrStatus";

  // ATTN: current implementation does not allow use of parameter isLimited
  // ATTN: compare OnPostReadServiceRestrictionOrsByAndGuid
  public JsonResult OnPostReadServiceRestrictionOrs([DataSourceRequest] DataSourceRequest dsRequest,
    Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
  {
    var rzrHndlr = nameof(OnPostReadServiceRestrictionAnds);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("RestrictionOr", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PSDC.ListEditableRestrictionOrsByRGuid(recordGuid)
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

  // ATTN: current implementation does not allow use of parameter isLimited
  // ATTN: compare OnPostReadServiceRestrictionOrs
  public JsonResult OnPostReadServiceRestrictionOrsByAndGuid([DataSourceRequest] DataSourceRequest dsRequest,
     Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
  {
    OpenScribeConnection(); // use PSDC
    if (rstrctAndGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", $"{nameof(rstrctAndGuid)} invalid"); }
    var records = PSDC.ListEditableRestrictionOrsByAndGuid(rstrctAndGuid);
    var dsResult = records.ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionOrUxm fgr, Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRInfosetGuid.IsInvalid()) { fgr.RRInfosetGuid = infosetGuid; }
    if (fgr.RRRecordGuid.IsInvalid()) { fgr.RRRecordGuid = recordGuid; }
    if (fgr.RestrictionAndGuid.IsInvalid()) { fgr.RestrictionAndGuid = rstrctAndGuid; }
    if (ModelState.IsValid) { fgr = PSDC.EditRestrictionOr(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidRestrictionOrStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostDeleteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
     ServiceRestrictionOrUxm fgr, Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteRestrictionOr(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidRestrictionOrStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file