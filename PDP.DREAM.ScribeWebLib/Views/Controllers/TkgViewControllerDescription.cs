// TkgViewControllerDescription.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidDescriptionStatus = "span#DescriptionStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectDescriptions), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeSelectDescriptions([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableDescriptions(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertDescription), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeUpsertDescription([DataSourceRequest] DataSourceRequest dsr, DescriptionEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditDescription(nre); nre.PdpStatusElement = eidDescriptionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteDescription), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeDeleteDescription([DataSourceRequest] DataSourceRequest dsr, DescriptionEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteDescription(nre); nre.PdpStatusElement = eidDescriptionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckDescription), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeCheckDescription([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DescriptionEditModel? nre = PSDC.GetEditableDescriptionByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckDescription(nre); nre.PdpStatusElement = eidDescriptionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqDescription), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeReseqDescription([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DescriptionEditModel? nre = PSDC.GetEditableDescriptionByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqDescription(nre); nre.PdpStatusElement = eidDescriptionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file