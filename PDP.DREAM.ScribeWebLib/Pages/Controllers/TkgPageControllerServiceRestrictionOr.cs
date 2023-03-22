// TkgPageControllerServiceRestrictionOr.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidRestrictionOrStatus = "span#ServiceRestrictionOrStatus";

  public virtual JsonResult OnPostReadServiceRestrictionOrs([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("RestrictionOr", $"{nameof(recordGuid)} invalid"); }
    var dsResult = PSDC.ListEditableRestrictionOrsByRGuid(recordGuid).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }
  public virtual JsonResult OnPostReadServiceRestrictionOrsByAndGuid([DataSourceRequest] DataSourceRequest dsRequest,
     Guid rstrctAndGuid, bool isLimited = false)
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

  public virtual JsonResult OnPostWriteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
    ServiceRestrictionOrEditModel fgr)
  {
    OpenScribeConnection(); // use PSDC
    var recordName = fgr.ItemXnam;
    //if (rstrctAndGuid.IsInvalid())
    //{ NullRefException(nameof(rstrctAndGuid), nameof(OnPostWriteServiceRestrictionOr), nameof(TkgsPageControllerBase)); }
    //fgr.RestrictionAndGuid = rstrctAndGuid;
    //if (recordGuid.IsInvalid())
    //{ NullRefException(nameof(recordGuid), nameof(OnPostWriteServiceRestrictionOr), nameof(TkgsPageControllerBase)); }
    //fgr.RRRecordGuid = recordGuid;
    //if (infosetGuid.IsInvalid())
    //{ NullRefException(nameof(infosetGuid), nameof(OnPostWriteServiceRestrictionOr), nameof(TkgsPageControllerBase)); }
    //fgr.RRInfosetGuid = infosetGuid;
    if (fgr.RRInfosetGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRInfosetGuid invalid because null or empty."); }
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (fgr.RestrictionAndGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RestrictionAndGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = PSDC.EditRestrictionOr(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidRestrictionOrStatus;
    var dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteServiceRestrictionOr([DataSourceRequest] DataSourceRequest dsRequest,
     ServiceRestrictionOrEditModel fgr)
  {
    OpenScribeConnection(); // use PSDC
    var recordName = fgr.ItemXnam;
    if (ModelState.IsValid) { fgr = PSDC.DeleteRestrictionOr(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidRestrictionOrStatus;
    var dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file