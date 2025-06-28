// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  public const string eidResrepServiceStatus = TkndoElemPrfx + "ResrepServiceStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ReadResrepServices([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(ReadResrepServices);
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
    { ModelState.AddModelError("ResrepServices", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableResrepServices(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult WriteResrepService([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepServiceUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditResrepService(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidResrepServiceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSfgil)]
  public JsonResult CheckResrepService([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    ResrepServiceUxm? fgr = NPDSCW.PSDC.GetEditableResrepServiceByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.CheckResrepService(fgr); }
    fgr.NdisElemId = eidResrepServiceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSfgil)]
  public JsonResult ReseqResrepService([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    ResrepServiceUxm? fgr = NPDSCW.PSDC.GetEditableResrepServiceByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.ReseqResrepService(fgr); }
    fgr.NdisElemId = eidResrepServiceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult DeleteResrepService([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepServiceUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.DeleteResrepService(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidResrepServiceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file