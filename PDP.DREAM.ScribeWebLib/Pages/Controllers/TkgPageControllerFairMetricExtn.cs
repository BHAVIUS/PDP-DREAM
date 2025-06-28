// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidFairMetricExtnStatus = TkndoElemPrfx + "FairMetricExtnStatus";

  public JsonResult OnPostReadFairMetricExtns([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadFairMetricExtns);
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
    { ModelState.AddModelError("FairMetricExtns", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableFairMetricExtns(recordGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteFairMetricExtn([DataSourceRequest] DataSourceRequest dsRequest,
    FairMetricExtnUxm fgr, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    // Parse method in extension but not base
    if (ModelState.IsValid) { fgr = ParseFairMetricExtn(fgr); }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditFairMetricExtn(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidFairMetricExtnStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Check method in both base and extension

  public JsonResult OnPostCheckFairMetricExtn([DataSourceRequest] DataSourceRequest dsRequest,
    Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    FairMetricExtnUxm? fgr = NPDSCW.PSDC.GetEditableFairMetricExtnByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.CheckFairMetricExtn(fgr); }
    fgr.NdisElemId = eidFairMetricExtnStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Reseq and Delete methods in base but not extension
  // Parse method in extension but not base

  public FairMetricExtnUxm ParseFairMetricExtn(FairMetricExtnUxm uxm)
  {
    // ATTN: this hook for parsing the extension
    return uxm;
  }

} // end class

// end file