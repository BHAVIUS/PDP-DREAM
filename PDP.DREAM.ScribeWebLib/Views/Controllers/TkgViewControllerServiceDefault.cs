// TkgViewControllerServiceDefault.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidServiceDefaultStatus = "span#ServiceDefaultStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectServiceDefaults), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeSelectServiceDefaults([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableServiceDefaults(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertServiceDefault), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeUpsertServiceDefault([DataSourceRequest] DataSourceRequest dsr, ServiceDefaultEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditServiceDefault(nre); nre.PdpStatusElement = eidServiceDefaultStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteServiceDefault), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeDeleteServiceDefault([DataSourceRequest] DataSourceRequest dsr, ServiceDefaultEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteServiceDefault(nre); nre.PdpStatusElement = eidServiceDefaultStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckServiceDefault), "", PdpRatsRg, SrlRanpView)]
  [PdpRazorViewRoute(depTSrg)]
  public JsonResult ScribeCheckServiceDefault([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    ServiceDefaultEditModel? nre = PSDC.GetEditableServiceDefaultByKey(recordGuid);
    // TODO: implement the missing PSDC.CheckServiceDefault()
    // if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckServiceDefault(nre); nre.PdpStatusName = eidServiceDefaultStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // end class

// end file