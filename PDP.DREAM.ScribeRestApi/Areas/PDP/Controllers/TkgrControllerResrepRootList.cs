using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeResrepRootsForView([DataSourceRequest] DataSourceRequest request, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetScribeRepository();  // use PSDC
      var resreps = PSDC.ListViewableResrepRoots(request, out int numResreps);
      var result = new DataSourceResult() { Data = resreps, Total = numResreps };
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeResrepRootsForEdit([DataSourceRequest] DataSourceRequest request, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetScribeRepository();  // use PSDC
      var resreps = PSDC.ListEditableResrepRoots(request, out int numResreps);
      var result = new DataSourceResult() { Data = resreps, Total = numResreps };
      return Json(result);
    }

  } // class

} // namespace
