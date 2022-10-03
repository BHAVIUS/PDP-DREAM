﻿// TkgViewControllerDescription.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnViewControllerBase
{
  private const string eidDescriptionStatus = "span#DescriptionStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(NexusSelectDescriptions), "", PdpRatsRgil, NrlRanpView)]
  [PdpRazorViewRoute(TSrgil)]
  public JsonResult NexusSelectDescriptions([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewableDescriptions(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

}