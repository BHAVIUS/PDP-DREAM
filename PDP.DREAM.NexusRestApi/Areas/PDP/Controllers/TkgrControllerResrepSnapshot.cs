// TkgrControllerResrepSnapshot.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

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
