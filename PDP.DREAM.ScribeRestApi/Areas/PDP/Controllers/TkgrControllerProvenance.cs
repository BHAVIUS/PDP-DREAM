using System;
using System.Text.RegularExpressions;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidProvenanceStatus = "span#ProvenanceStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeProvenancesForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableProvenances(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeProvenancesForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableProvenances(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeProvenance([DataSourceRequest] DataSourceRequest dsr, ProvenanceEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
      // TODO: redundant check at controller action level for XML parsing validation
      if (ModelState.IsValid) { nre = PSDC.EditProvenance(nre); nre.PdpStatusName = eidProvenanceStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeProvenance([DataSourceRequest] DataSourceRequest dsr, ProvenanceEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteProvenance(nre); nre.PdpStatusName = eidProvenanceStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeProvenance([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      ProvenanceEditModel? nre = PSDC.GetEditableProvenanceByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckProvenance(nre); nre.PdpStatusName = eidProvenanceStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
