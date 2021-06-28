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
    private const string eidRestrictionAndStatus = "span#RestrictionAndStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeRestrictionAndsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository();
      DataSourceResult result = PSDC.ListEditableRestrictionAnds(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeRestrictionAnd([DataSourceRequest] DataSourceRequest dsr, RestrictionAndEditModel nre, Guid recordGuid)
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
    public JsonResult DeleteScribeRestrictionAnd([DataSourceRequest] DataSourceRequest dsr, RestrictionAndEditModel nre)
    {
      ResetScribeRepository();
      var recordName = nre.ItemXnam;
      if (ModelState.IsValid) { nre = PSDC.DeleteRestrictionAnd(nre); }
      else { nre.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
      nre.PdpStatusName = eidRestrictionAndStatus;
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
