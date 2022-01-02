// TkgrControllerRestrictionOr.cs 
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
  private const string eidRestrictionOrStatus = "span#RestrictionOrStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectRestrictionOrs), "", "", NPmvc)]
  public JsonResult ScribeSelectRestrictionOrs([DataSourceRequest] DataSourceRequest request, Guid restAndGuid)
  {
    ResetScribeRepository();
    DataSourceResult result = PSDC.ListEditableRestrictionOrs(restAndGuid).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertRestrictionOr), "", "", NPmvc)]
  public JsonResult ScribeUpsertRestrictionOr([DataSourceRequest] DataSourceRequest dsr, RestrictionOrEditModel nre, Guid restAndGuid, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    nre.RestrictionAndGuid = PdpGuid.ParseToNonNullable(nre.RestrictionAndGuid, restAndGuid);
    if ((nre.RestrictionAndGuid == null) || (nre.RestrictionAndGuid == Guid.Empty))
    { ModelState.AddModelError(recordName, "RestrictionAndGuid invalid because null or empty."); }
    nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
    if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
    { ModelState.AddModelError(recordName, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { nre = PSDC.EditRestrictionOr(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusName = eidRestrictionOrStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteRestrictionOr), "", "", NPmvc)]
  public JsonResult ScribeDeleteRestrictionOr([DataSourceRequest] DataSourceRequest dsr, RestrictionOrEditModel nre)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    if (ModelState.IsValid) { nre = PSDC.DeleteRestrictionOr(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusName = eidRestrictionOrStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
