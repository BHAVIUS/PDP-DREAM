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
    private const string eidOtherTextStatus = "span#OtherTextStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeOtherTextsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableOtherTexts(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeOtherTextsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableOtherTexts(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeOtherText([DataSourceRequest] DataSourceRequest dsr, OtherTextEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
      if (ModelState.IsValid) { nre = PSDC.EditOtherText(nre); nre.PdpStatusName = eidOtherTextStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeOtherText([DataSourceRequest] DataSourceRequest dsr, OtherTextEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteOtherText(nre); nre.PdpStatusName = eidOtherTextStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeOtherText([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      OtherTextEditModel? nre = PSDC.GetEditableOtherTextByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckOtherText(nre); nre.PdpStatusName = eidOtherTextStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
