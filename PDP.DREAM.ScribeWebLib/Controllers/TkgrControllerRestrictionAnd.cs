// TkgrControllerRestrictionAnd.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Models;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidRestrictionAndStatus = "span#RestrictionAndStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectRestrictionAnds), "", "", ScribeWLC.ranpView)]
  public JsonResult ScribeSelectRestrictionAnds([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository();
    DataSourceResult result = PSDC.ListEditableRestrictionAnds(recordGuid).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertRestrictionAnd), "", "", ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertRestrictionAnd([DataSourceRequest] DataSourceRequest dsr, RestrictionAndEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
    if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
    { ModelState.AddModelError(recordName, "ResourceGuidRef invalid because null or empty."); }
    if (ModelState.IsValid) { nre = PSDC.EditRestrictionAnd(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusName = eidRestrictionAndStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteRestrictionAnd), "", "", ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteRestrictionAnd([DataSourceRequest] DataSourceRequest dsr, RestrictionAndEditModel nre)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    if (ModelState.IsValid) { nre = PSDC.DeleteRestrictionAnd(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusName = eidRestrictionAndStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
