// TkgViewControllerDistribution.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewController
{
  private const string eidDistributionStatus = "span#DistributionStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectDistributions), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeSelectDistributions([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableDistributions(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertDistribution), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeUpsertDistribution([DataSourceRequest] DataSourceRequest dsr, DistributionEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditDistribution(nre); nre.PdpStatusElement = eidDistributionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteDistribution), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeDeleteDistribution([DataSourceRequest] DataSourceRequest dsr, DistributionEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteDistribution(nre); nre.PdpStatusElement = eidDistributionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeCheckDistribution), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeCheckDistribution([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DistributionEditModel? nre = PSDC.GetEditableDistributionByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckDistribution(nre); nre.PdpStatusElement = eidDistributionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeReseqDistribution), "", PdpRatsRgil, SrlRanpView)]
  [PdpRazorViewRoute(depTSrgil)]
  public JsonResult ScribeReseqDistribution([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DistributionEditModel? nre = PSDC.GetEditableDistributionByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.ReseqDistribution(nre); nre.PdpStatusElement = eidDistributionStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class

// end file