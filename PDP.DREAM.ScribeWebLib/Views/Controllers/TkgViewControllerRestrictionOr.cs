// TkgViewControllerRestrictionOr.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidRestrictionOrStatus = "span#ServiceRestrictionOrStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectRestrictionOrs), "", "", SrlRanpView)]
  [PdpRazorViewRoute]
  public JsonResult ScribeSelectRestrictionOrs([DataSourceRequest] DataSourceRequest request, Guid restAndGuid)
  {
    ResetScribeRepository();
    DataSourceResult result = PSDC.ListEditableRestrictionOrsByAndGuid(restAndGuid).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertRestrictionOr), "", "", SrlRanpView)]
  [PdpRazorViewRoute]
  public JsonResult ScribeUpsertRestrictionOr([DataSourceRequest] DataSourceRequest dsr,
    ServiceRestrictionOrEditModel nre, Guid restAndGuid, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    nre.RestrictionAndGuid = PdpGuid.ParseToNonNullable(nre.RestrictionAndGuid, restAndGuid);
    if (nre.RestrictionAndGuid.IsInvalid())
    { ModelState.AddModelError(recordName, "RestrictionAndGuid invalid because null or empty."); }
    nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
    if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
    { ModelState.AddModelError(recordName, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { nre = PSDC.EditRestrictionOr(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusElement = eidRestrictionOrStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteRestrictionOr), "", "", SrlRanpView)]
  [PdpRazorViewRoute]
  public JsonResult ScribeDeleteRestrictionOr([DataSourceRequest] DataSourceRequest dsr, ServiceRestrictionOrEditModel nre)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    if (ModelState.IsValid) { nre = PSDC.DeleteRestrictionOr(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusElement = eidRestrictionOrStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // end class

// end file