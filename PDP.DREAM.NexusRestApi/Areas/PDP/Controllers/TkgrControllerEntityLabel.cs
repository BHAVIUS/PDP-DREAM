using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidEntityLabelStatus = "span#EntityLabelStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusEntityLabelsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableEntityLabels(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

  }

}
