// TkgViewControllerSupportingTag.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidSupportingTagStatus = "span#SupportingTagStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectSupportingTags), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeSelectSupportingTags([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableSupportingTags(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertSupportingTag), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeUpsertSupportingTag([DataSourceRequest] DataSourceRequest dsr, SupportingTagEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (!string.IsNullOrWhiteSpace(nre.SupportingTag))
    {
      var rgx = new Regex(PdpAppConst.RegexSupportingTag);
      var isMatch = rgx.IsMatch(nre.SupportingTag);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, $"String is not a valid {nre.ItemXnam}."); }
    }
    if (ModelState.IsValid) { nre = PSDC.EditSupportingTag(nre); nre.PdpStatusElement = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteSupportingTag), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeDeleteSupportingTag([DataSourceRequest] DataSourceRequest dsr, SupportingTagEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteSupportingTag(nre); nre.PdpStatusElement = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckSupportingTag), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeCheckSupportingTag([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    SupportingTagEditModel? nre = PSDC.GetEditableSupportingTagByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckSupportingTag(nre); nre.PdpStatusElement = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqSupportingTag), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeReseqSupportingTag([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    SupportingTagEditModel? nre = PSDC.GetEditableSupportingTagByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqSupportingTag(nre); nre.PdpStatusElement = eidSupportingTagStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file