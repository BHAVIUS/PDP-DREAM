// TkgrControllerFairMetric.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidFairMetricStatus = "span#FairMetricStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(NexusSelectFairMetrics), "", TSrgil, NPtkgr)]
  public JsonResult NexusSelectFairMetrics([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewableFairMetrics(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

} // class
