// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidCrossReferenceStatus = TkndoElemPrfx + "CrossReferenceStatus";

  public JsonResult OnPostReadCrossReferences([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadCrossReferences);
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
    { ModelState.AddModelError("CrossReferences", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableCrossReferences(recordGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    CrossReferenceUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (!string.IsNullOrWhiteSpace(fgr.CrossReferenceText))
    {
      var rgx = new Regex(RgxsCrossReference);
      var isMatch = rgx.IsMatch(fgr.CrossReferenceText);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, $"String is not a valid {fgr.ItemXnam}."); }
    }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditCrossReference(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidCrossReferenceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostCheckCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    CrossReferenceUxm? fgr = NPDSCW.PSDC.GetEditableCrossReferenceByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.CheckCrossReference(fgr); }
    fgr.NdisElemId = eidCrossReferenceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostReseqCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    CrossReferenceUxm? fgr = NPDSCW.PSDC.GetEditableCrossReferenceByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.ReseqCrossReference(fgr); }
    fgr.NdisElemId = eidCrossReferenceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostDeleteCrossReference([DataSourceRequest] DataSourceRequest dsRequest,
    CrossReferenceUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.DeleteCrossReference(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidCrossReferenceStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file