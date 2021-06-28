using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidSnapshotStatus = "span#SnapshotStatus";

    [HttpGet, HttpPost]
    public JsonResult SelectNexusSnapshotsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableSnapshots(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }



  }

}
