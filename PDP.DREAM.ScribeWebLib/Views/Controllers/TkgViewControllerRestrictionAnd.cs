// TkgViewControllerRestrictionAnd.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsViewControllerBase
{
  private const string eidRestrictionAndStatus = "span#ServiceRestrictionAndStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeSelectRestrictionAnds), "", "", SrlRanpView)]
  [PdpRazorViewRoute]
  public JsonResult ScribeSelectRestrictionAnds([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository();
    DataSourceResult result = PSDC.ListEditableRestrictionAndsByRGuid(recordGuid).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeUpsertRestrictionAnd), "", "", SrlRanpView)]
  [PdpRazorViewRoute]
  public JsonResult ScribeUpsertRestrictionAnd([DataSourceRequest] DataSourceRequest dsr, ServiceRestrictionAndEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
    if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
    { ModelState.AddModelError(recordName, "ResourceGuidRef invalid because null or empty."); }
    if (ModelState.IsValid) { nre = PSDC.EditRestrictionAnd(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusElement = eidRestrictionAndStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  // [PdpMvcRoute(nameof(ScribeDeleteRestrictionAnd), "", "", SrlRanpView)]
  [PdpRazorViewRoute]
  public JsonResult ScribeDeleteRestrictionAnd([DataSourceRequest] DataSourceRequest dsr, ServiceRestrictionAndEditModel nre)
  {
    ResetScribeRepository();
    var recordName = nre.ItemXnam;
    if (ModelState.IsValid) { nre = PSDC.DeleteRestrictionAnd(nre); }
    else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    nre.PdpStatusElement = eidRestrictionAndStatus;
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // end class

// end file