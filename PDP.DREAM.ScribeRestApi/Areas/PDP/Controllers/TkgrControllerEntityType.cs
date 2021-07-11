// TkgrControllerEntityType.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidEntityTypeStatus = "span#EntityTypeStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectNexusEntityTypesForView([DataSourceRequest] DataSourceRequest request)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableEntityTypes().ToDataSourceResult(request);
      return Json(result);
    }

  } // end class

} // namespace

