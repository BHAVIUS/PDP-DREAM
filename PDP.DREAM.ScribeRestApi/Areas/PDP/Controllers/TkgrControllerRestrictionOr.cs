// TkgrControllerRestrictionOr.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidRestrictionOrStatus = "span#RestrictionOrStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeRestrictionOrsForEdit([DataSourceRequest] DataSourceRequest request, Guid restAndGuid)
    {
      ResetScribeRepository();
      DataSourceResult result = PSDC.ListEditableRestrictionOrs(restAndGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeRestrictionOr([DataSourceRequest] DataSourceRequest dsr, RestrictionOrEditModel nre, Guid restAndGuid, Guid recordGuid)
    {
      ResetScribeRepository();
      var recordName = nre.ItemXnam;
      nre.RestrictionAndGuidRef = PdpGuid.ParseToNonNullable(nre.RestrictionAndGuidRef, restAndGuid);
      if ((nre.RestrictionAndGuidRef == null) || (nre.RestrictionAndGuidRef == Guid.Empty))
      { ModelState.AddModelError(recordName, "RestrictionAndGuidRef invalid because null or empty."); }
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "ResourceGuidRef invalid because null or empty."); }
      if (ModelState.IsValid) { nre = PSDC.EditRestrictionOr(nre); }
      else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
      nre.PdpStatusName = eidRestrictionOrStatus;
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeRestrictionOr([DataSourceRequest] DataSourceRequest dsr, RestrictionOrEditModel nre)
    {
      ResetScribeRepository();
      var recordName = nre.ItemXnam;
      if (ModelState.IsValid) { nre = PSDC.DeleteRestrictionOr(nre); }
      else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
      nre.PdpStatusName = eidRestrictionOrStatus;
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
