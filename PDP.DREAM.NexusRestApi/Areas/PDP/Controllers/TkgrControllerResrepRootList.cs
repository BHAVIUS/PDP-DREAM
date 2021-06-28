using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusResrepRootsForView([DataSourceRequest] DataSourceRequest request, string serviceType, string serviceTag, string entityType)
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
      ResetNexusRepository();  // use PNDC
      var resreps = PNDC.ListViewableResrepRoots(request, out int numResreps);
      var result = new DataSourceResult() { Data = resreps, Total = numResreps };
      return Json(result);
    }

  } // class

} // namespace
