using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidDistributionStatus = "span#DistributionStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusDistributionsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableDistributions(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

  }

}
