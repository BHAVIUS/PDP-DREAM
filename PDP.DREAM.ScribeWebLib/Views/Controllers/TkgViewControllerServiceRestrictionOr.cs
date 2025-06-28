// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  public const string eidRestrictionOrStatus = TkndoElemPrfx + "ServiceRestrictionOrStatus";

  // ATTN: current implementation does allow use parameter isLimited
  // ATTN: compare OnPostReadServiceRestrictionOrsByAndGuid
  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute]
  public JsonResult ReadServiceRestrictionOrs([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid rstrctAndGuid, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(ReadServiceRestrictionAnds);
    NPDSCW.OpenScribeConnection(); // use PSDC
#if DEBUG
    NPDSCW.DebugScribeRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("RestrictionOr", "RRRecordGuid invalid."); }
      else
      {
        dsResult = NPDSCW.PSDC.ListEditableRestrictionOrsByRGuid(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
      }
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // ATTN: current implementation does not allow use of parameter isLimited
  // ATTN: compare OnPostReadServiceRestrictionOrs
  public JsonResult ReadServiceRestrictionOrsByAGuid([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid rstrctAndGuid, Guid recordGuid, bool isLimited)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    if (rstrctAndGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", $"{nameof(rstrctAndGuid)} invalid"); }
    var records = NPDSCW.PSDC.ListEditableRestrictionOrsByAGuid(rstrctAndGuid, isLimited);
    var dsResult = records.ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpRazorViewRoute]
  public JsonResult WriteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionOrUxm fgr, Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRInfosetGuid.IsInvalid()) { fgr.RRInfosetGuid = infosetGuid; }
    if (fgr.RRRecordGuid.IsInvalid()) { fgr.RRRecordGuid = recordGuid; }
    if (fgr.RestrictionAndGuid.IsInvalid()) { fgr.RestrictionAndGuid = rstrctAndGuid; }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditRestrictionOr(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidRestrictionOrStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpRazorViewRoute]
  public JsonResult DeleteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
     ServiceRestrictionOrUxm fgr, Guid rstrctAndGuid, Guid recordGuid, Guid infosetGuid)
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