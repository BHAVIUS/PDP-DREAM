using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidDescriptionStatus = "span#DescriptionStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusDescriptionsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableDescriptions(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

  }

}
