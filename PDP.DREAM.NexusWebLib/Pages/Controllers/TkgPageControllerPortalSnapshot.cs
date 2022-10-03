﻿// TkgnPageControllerBase
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageControllerBase
{
  private const string eidPortalSnapshotStatus = "span#PortalSnapshotStatus";

  public virtual JsonResult OnPostReadPortalSnapshots([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetNexusRepository(); // use PNDC
    DataSourceResult result = PNDC.ListViewablePortalSnapshots(recordGuid, isLimited).ToDataSourceResult(request);
    return new JsonResult(result);
  }

} // end class

// end file