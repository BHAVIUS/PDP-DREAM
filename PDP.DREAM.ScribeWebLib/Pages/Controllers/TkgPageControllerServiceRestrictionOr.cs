// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public const string eidRestrictionOrStatus = TkndoElemPrfx + "ServiceRestrictionOrStatus";

  public JsonResult OnPostReadServiceRestrictionOrs([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid rstrctAndGuid, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadServiceRestrictionAnds);
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
#if DEBUG
    NPDSCW.DebugWraceData(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    NPDSCW.OpenScribeConnection(); // use PSDC
#if DEBUG
    NPDSCW.DebugScribeRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", "RRRecordGuid invalid."); }
    else if (rstrctAndGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", "RestrictionAndGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableRestrictionOrsByAGuid(rstrctAndGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionOrUxm fgr, Guid rstrctAndGuid, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (fgr.RestrictionAndGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RestrictionAndGuid invalid because null or empty."); }
    // Parse method in extension but not base
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditRestrictionOr(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidRestrictionOrStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Check method in both base and extension


  // Reseq and Delete methods in base but not extension
  // Parse method in extension but not base

  public JsonResult OnPostDeleteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
     ServiceRestrictionOrUxm fgr, Guid rstrctAndGuid, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.DeleteRestrictionOr(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidRestrictionOrStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file