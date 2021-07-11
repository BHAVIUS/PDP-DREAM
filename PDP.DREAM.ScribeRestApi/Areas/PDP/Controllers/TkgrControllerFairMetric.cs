// TkgrControllerFairMetric.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidFairMetricStatus = "span#FairMetricStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeFairMetricsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableFairMetrics(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeFairMetricsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableFairMetrics(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeFairMetric([DataSourceRequest] DataSourceRequest dsr, FairMetricEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
      // TODO: redundant check at controller action level for XML parsing validation
      if (ModelState.IsValid) { nre = PSDC.EditFairMetric(nre); nre.PdpStatusName = eidFairMetricStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeFairMetric([DataSourceRequest] DataSourceRequest dsr, FairMetricEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteFairMetric(nre); nre.PdpStatusName = eidFairMetricStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeFairMetric([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      FairMetricEditModel? nre = PSDC.GetEditableFairMetricByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckFairMetric(nre); nre.PdpStatusName = eidFairMetricStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
