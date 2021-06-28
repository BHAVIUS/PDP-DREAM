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
    private const string eidSupportingLabelStatus = "span#SupportingLabelStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeSupportingLabelsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableSupportingLabels(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeSupportingLabelsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableSupportingLabels(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeSupportingLabel([DataSourceRequest] DataSourceRequest dsr, SupportingLabelEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
      if (!string.IsNullOrWhiteSpace(nre.SupportingLabel))
      {
        var rgx = new Regex(NpdsConst.RegexSupportingLabel);
        var isMatch = rgx.IsMatch(nre.SupportingLabel);
        if (!isMatch)
        { ModelState.AddModelError(recordName, "String is not a valid SupportingLabel."); }
      }
      if (ModelState.IsValid) { nre = PSDC.EditSupportingLabel(nre); nre.PdpStatusName = eidSupportingLabelStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeSupportingLabel([DataSourceRequest] DataSourceRequest dsr, SupportingLabelEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteSupportingLabel(nre); nre.PdpStatusName = eidSupportingLabelStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeSupportingLabel([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      SupportingLabelEditModel? nre = PSDC.GetEditableSupportingLabelByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckSupportingLabel(nre); nre.PdpStatusName = eidSupportingLabelStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
