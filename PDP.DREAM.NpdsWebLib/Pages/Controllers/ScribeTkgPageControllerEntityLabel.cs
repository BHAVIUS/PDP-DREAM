// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial class ScribeTkgPageController
{
  private const string eidEntityLabelStatus = TkndoElemPrfx + "EntityLabelStatus";

  public JsonResult OnPostReadEntityLabels([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadEntityLabels);
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
      dsResult = NPDSCW.ScribeDR.ListEditableEntityLabels(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    EntityLabelUxm fgr, 
    Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    // TODO: make these Regexs static
    // var rgx = new Regex(PdpAppConst.RegexLabelUri);
    // TODO: allow different regex patterns for each of PrincipalTag and AliasTag
    fgr.EntityTag = IsRegexMatch(fgr.EntityTag, RgxsPrincipalTag); // EntityTag cannot be empty
    fgr.DomainUri = IsRegexMatch(fgr.DomainUri, RgxsLabelUri); // LabelUri can be empty
    if (fgr.EntityTag == "")
    { ModelState.AddModelError(fgr.ItemXnam, $"Model is not a valid {fgr.ItemXnam}."); }
    if (ModelState.IsValid) { fgr = NPDSCW.ScribeDR.EditEntityLabel(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Delete method in base but not extension
  public JsonResult OnPostDeleteEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    EntityLabelUxm fgr, 
    Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = NPDSCW.ScribeDR.DeleteEntityLabel(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Check method in both base and extension
  public JsonResult OnPostCheckEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    EntityLabelUxm? fgr = NPDSCW.ScribeDR.GetEditableEntityLabelByFGuid(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.ScribeDR.CheckEntityLabel(fgr); }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Reseq method in base but not extension
  public JsonResult OnPostReseqEntityLabel([DataSourceRequest] DataSourceRequest dsRequest,
    Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    EntityLabelUxm? fgr = NPDSCW.ScribeDR.GetEditableEntityLabelByFGuid(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.ScribeDR.ReseqEntityLabel(fgr); }
    fgr.NdisElemId = eidEntityLabelStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Parse method in extension but not base

} // end class

// end file