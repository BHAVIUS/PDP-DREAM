using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidFairMetricStatus = "span#FairMetricStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusFairMetricsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableFairMetrics(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

  }

}
