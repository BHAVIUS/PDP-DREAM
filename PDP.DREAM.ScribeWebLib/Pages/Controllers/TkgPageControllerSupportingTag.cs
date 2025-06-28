// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  public const string eidSupportingTagStatus = TkndoElemPrfx + "SupportingTagStatus";

  public JsonResult OnPostReadSupportingTags([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadSupportingTags);
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
    { ModelState.AddModelError("SupportingTags", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableSupportingTags(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteSupportingTag([DataSourceRequest] DataSourceRequest dsRequest,
    SupportingTagUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (!string.IsNullOrWhiteSpace(fgr.SupportingTagText))
    {
      var rgx = new Regex(RgxsSupportingTag);
      var isMatch = rgx.IsMatch(fgr.SupportingTagText);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, $"String is not a valid {fgr.ItemXnam}."); }
    }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditSupportingTag(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidSupportingTagStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Check method in both base and extension

  public JsonResult OnPostCheckSupportingTag([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    SupportingTagUxm? fgr = NPDSCW.PSDC.GetEditableSupportingTagByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.CheckSupportingTag(fgr); }
    fgr.NdisElemId = eidSupportingTagStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Reseq and Delete methods in base but not extension
  // Parse method in extension but not base

  public JsonResult OnPostReseqSupportingTag([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    SupportingTagUxm? fgr = NPDSCW.PSDC.GetEditableSupportingTagByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.ReseqSupportingTag(fgr); }
    fgr.NdisElemId = eidSupportingTagStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostDeleteSupportingTag([DataSourceRequest] DataSourceRequest dsRequest,
    SupportingTagUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.DeleteSupportingTag(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidSupportingTagStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file