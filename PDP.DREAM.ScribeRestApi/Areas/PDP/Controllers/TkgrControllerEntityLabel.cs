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
    private const string eidEntityLabelStatus = "span#EntityLabelStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeEntityLabelsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableEntityLabels(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeEntityLabelsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableEntityLabels(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeEntityLabel([DataSourceRequest] DataSourceRequest dsr, EntityLabelEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
      if (!string.IsNullOrWhiteSpace(nre.LabelUri))
      {
        var rgx = new Regex(NpdsConst.RegexLabelUri);
        var isMatch = rgx.IsMatch(nre.LabelUri);
        if (!isMatch)
        { ModelState.AddModelError(recordName, "String not a valid LabelUri."); }
      }
      if (ModelState.IsValid) { nre = PSDC.EditEntityLabel(nre); nre.PdpStatusName = eidEntityLabelStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeEntityLabel([DataSourceRequest] DataSourceRequest dsr, EntityLabelEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteEntityLabel(nre); nre.PdpStatusName = eidEntityLabelStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeEntityLabel([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      EntityLabelEditModel? nre = PSDC.GetEditableEntityLabelByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckEntityLabel(nre); nre.PdpStatusName = eidEntityLabelStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
