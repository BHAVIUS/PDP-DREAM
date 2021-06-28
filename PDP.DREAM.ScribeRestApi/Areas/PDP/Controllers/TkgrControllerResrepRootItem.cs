﻿using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidResrepRootStatus = "span#ResrepRootStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public ActionResult<string> SummarizeScribeResrepRoot(Guid recordGuid)
    {
      ResetScribeRepository();  // use PSDC
      NexusResrepEditModel? nre = PSDC.GetEditableResrepRootByKey(recordGuid);
      var result = string.Empty;
      if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
      return result;
    }

    [HttpPost] // Post for Rest, Post for Ajax
    public JsonResult EditScribeResrepRoot([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetScribeRepository();  // use PSDC
      if (ModelState.IsValid) { nre = PSDC.EditResrepRoot(nre); nre.PdpStatusName = eidResrepRootStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeResrepRoot([DataSourceRequest] DataSourceRequest dsr, NexusResrepEditModel nre, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetScribeRepository();  // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteResrepRoot(nre); nre.PdpStatusName = eidResrepRootStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  } // class

} // namespace