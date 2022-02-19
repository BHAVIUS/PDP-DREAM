// TkgrControllerFairMetric.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Models;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidFairMetricStatus = "span#FairMetricStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectFairMetrics), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeSelectFairMetrics([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableFairMetrics(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertFairMetric), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertFairMetric([DataSourceRequest] DataSourceRequest dsr, FairMetricEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditFairMetric(nre); nre.PdpStatusName = eidFairMetricStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteFairMetric), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteFairMetric([DataSourceRequest] DataSourceRequest dsr, FairMetricEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteFairMetric(nre); nre.PdpStatusName = eidFairMetricStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckFairMetric), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeCheckFairMetric([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    FairMetricEditModel? nre = PSDC.GetEditableFairMetricByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckFairMetric(nre); nre.PdpStatusName = eidFairMetricStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
