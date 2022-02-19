// TkgrControllerSupportingLabel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidSupportingLabelStatus = "span#SupportingLabelStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(NexusSelectSupportingLabels), "", CoreDLC.ratsRgil, NPvsp)]
  public JsonResult NexusSelectSupportingLabels([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewableSupportingLabels(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

}
