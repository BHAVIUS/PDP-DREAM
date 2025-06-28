// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidEntityLabelStatus = TkndoElemPrfx + "EntityLabelStatus";

  public JsonResult OnPostReadEntityLabels([DataSourceRequest] DataSourceRequest dsRequest,
   string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadEntityLabels);
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
    { ModelState.AddModelError("EntityLabels", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.PSDC.ListEditableEntityLabels(recordGuid, isLimited)
      .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    EntityLabelUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    // TODO: make these Regexs static
    // var rgx = new Regex(PdpAppConst.RegexLabelUri);
    // TODO: allow different regex patterns for each of PrincipalTag and AliasTag
    fgr.TagToken = IsRegexMatch(fgr.TagToken, RgxsPrincipalTag); // TagToken cannot be empty
    fgr.LabelUri = IsRegexMatch(fgr.LabelUri, RgxsLabelUri); // LabelUri can be empty
    if (fgr.TagToken == "")
    { ModelState.AddModelError(fgr.ItemXnam, $"Model is not a valid {fgr.ItemXnam}."); }
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.EditEntityLabel(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostCheckEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    EntityLabelUxm? fgr = NPDSCW.PSDC.GetEditableEntityLabelByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.CheckEntityLabel(fgr); }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostReseqEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    string serviceTag, Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    EntityLabelUxm? fgr = NPDSCW.PSDC.GetEditableEntityLabelByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.PSDC.ReseqEntityLabel(fgr); }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostDeleteEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    EntityLabelUxm fgr, string serviceTag, Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Scribe, serviceTag);
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.PSDC.DeleteEntityLabel(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file