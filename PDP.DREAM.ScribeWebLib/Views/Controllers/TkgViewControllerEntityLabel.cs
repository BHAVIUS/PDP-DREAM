// TkgViewControllerEntityLabel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidEntityLabelStatus = "span#EntityLabelStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectEntityLabels), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeSelectEntityLabels([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableEntityLabels(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertEntityLabel), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeUpsertEntityLabel([DataSourceRequest] DataSourceRequest dsr, EntityLabelEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (!string.IsNullOrWhiteSpace(nre.LabelUri))
    {
      var rgx = new Regex(PdpAppConst.RegexLabelUri);
      var isMatch = rgx.IsMatch(nre.LabelUri);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, $"String is not a valid {nre.ItemXnam}."); }
    }
    if (ModelState.IsValid) { nre = PSDC.EditEntityLabel(nre); nre.PdpStatusElement = eidEntityLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteEntityLabel), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeDeleteEntityLabel([DataSourceRequest] DataSourceRequest dsr, EntityLabelEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteEntityLabel(nre); nre.PdpStatusElement = eidEntityLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckEntityLabel), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeCheckEntityLabel([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    EntityLabelEditModel? nre = PSDC.GetEditableEntityLabelByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckEntityLabel(nre); nre.PdpStatusElement = eidEntityLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqEntityLabel), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeReseqEntityLabel([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    EntityLabelEditModel? nre = PSDC.GetEditableEntityLabelByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqEntityLabel(nre); nre.PdpStatusElement = eidEntityLabelStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file