// TkgViewControllerLocation.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidLocationStatus = "span#LocationStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectLocations), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeSelectLocations([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableLocations(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertLocation), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeUpsertLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    Regex? rgx = null; bool isMatch = false;
    if (!string.IsNullOrWhiteSpace(nre.DisplayImageUrl))
    {
      rgx = new Regex(PdpAppConst.RegexLocationUrl);
      isMatch = rgx.IsMatch(nre.DisplayImageUrl);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, "String not a valid DisplayImageUrl."); }
    }
    if (!string.IsNullOrWhiteSpace(nre.UrlWebAddress))
    {
      rgx = new Regex(PdpAppConst.RegexLocationUrl);
      isMatch = rgx.IsMatch(nre.UrlWebAddress);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, "String not a valid UrlWebAddress."); }
    }
    if (!string.IsNullOrWhiteSpace(nre.EmailAddress))
    {
      rgx = new Regex(PdpAppConst.RegexEmailAddress);
      isMatch = rgx.IsMatch(nre.EmailAddress);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, "String not a valid EmailAddress."); }
    }
    if (ModelState.IsValid) { nre = PSDC.EditLocation(nre); nre.PdpStatusElement = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteLocation), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeDeleteLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteLocation(nre); nre.PdpStatusElement = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckLocation), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeCheckLocation([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    LocationEditModel? nre = PSDC.GetEditableLocationByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckLocation(nre); nre.PdpStatusElement = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqLocation), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeReseqLocation([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    LocationEditModel? nre = PSDC.GetEditableLocationByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqLocation(nre); nre.PdpStatusElement = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file