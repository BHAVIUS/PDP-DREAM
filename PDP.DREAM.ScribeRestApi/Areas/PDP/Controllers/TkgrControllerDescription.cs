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
    private const string eidDescriptionStatus = "span#DescriptionStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeDescriptionsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableDescriptions(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeDescriptionsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableDescriptions(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeDescription([DataSourceRequest] DataSourceRequest dsr, DescriptionEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty))
      { ModelState.AddModelError(recordName, "Guid is null, not a valid RRRecordGuid."); }
      // TODO: redundant check at controller action level for XML parsing validation
      if (ModelState.IsValid) { nre = PSDC.EditDescription(nre); nre.PdpStatusName = eidDescriptionStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeDescription([DataSourceRequest] DataSourceRequest dsr, DescriptionEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteDescription(nre); nre.PdpStatusName = eidDescriptionStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeDescription([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      DescriptionEditModel? nre = PSDC.GetEditableDescriptionByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckDescription(nre); nre.PdpStatusName = eidDescriptionStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
