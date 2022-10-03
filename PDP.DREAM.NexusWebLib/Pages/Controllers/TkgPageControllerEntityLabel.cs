﻿// TkgnPageControllerBase 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageControllerBase
{
  private const string eidEntityLabelStatus = "span#EntityLabelStatus";

  public virtual JsonResult OnPostReadEntityLabels([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewableEntityLabels(recordGuid, isLimited).ToDataSourceResult(request);
    return new JsonResult(result);
  }

}