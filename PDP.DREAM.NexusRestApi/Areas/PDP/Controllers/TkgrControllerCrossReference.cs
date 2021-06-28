using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidCrossReferenceStatus = "span#CrossReferenceStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusCrossReferencesForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableCrossReferences(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

  }

}
