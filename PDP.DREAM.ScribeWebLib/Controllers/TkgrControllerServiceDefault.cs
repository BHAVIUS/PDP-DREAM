// TkgrControllerServiceDefault.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidServiceDefaultStatus = "span#ServiceDefaultStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectServiceDefaults), "", TSrgil, NPmvc)]
  public JsonResult ScribeSelectServiceDefaults([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableServiceDefaults(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertServiceDefault), "", TSrgil, NPmvc)]
  public JsonResult ScribeUpsertServiceDefault([DataSourceRequest] DataSourceRequest dsr, ServiceDefaultEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.EditServiceDefault(nre); nre.PdpStatusName = eidServiceDefaultStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteServiceDefault), "", TSrgil, NPmvc)]
  public JsonResult ScribeDeleteServiceDefault([DataSourceRequest] DataSourceRequest dsr, ServiceDefaultEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteServiceDefault(nre); nre.PdpStatusName = eidServiceDefaultStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckServiceDefault), "", TSrg, NPmvc)]
  public JsonResult ScribeCheckServiceDefault([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    ServiceDefaultEditModel? nre = PSDC.GetEditableServiceDefaultByKey(recordGuid);
    // TODO: implement the missing PSDC.CheckServiceDefault()
    // if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckServiceDefault(nre); nre.PdpStatusName = eidServiceDefaultStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
