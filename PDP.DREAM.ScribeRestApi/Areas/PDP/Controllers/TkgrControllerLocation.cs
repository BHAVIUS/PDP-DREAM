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
    private const string eidLocationStatus = "span#LocationStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeLocationsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableLocations(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeLocationsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableLocations(recordGuid).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty)) // assure valid RRRecordGuid
      { ModelState.AddModelError(recordName, "Guid is null, not a valid recordGuid."); }
      Regex? rgx = null; bool isMatch = false;
      if (!string.IsNullOrWhiteSpace(nre.DisplayImageUrl))
      {
        rgx = new Regex(NpdsConst.RegexLocationUrl);
        isMatch = rgx.IsMatch(nre.DisplayImageUrl);
        if (!isMatch)
        { ModelState.AddModelError(recordName, "String not a valid DisplayImageUrl."); }
      }
      if (!string.IsNullOrWhiteSpace(nre.UrlWebAddress))
      {
        rgx = new Regex(NpdsConst.RegexLocationUrl);
        isMatch = rgx.IsMatch(nre.UrlWebAddress);
        if (!isMatch)
        { ModelState.AddModelError(recordName, "String not a valid UrlWebAddress."); }
      }
      if (!string.IsNullOrWhiteSpace(nre.EmailAddress))
      {
        rgx = new Regex(NpdsConst.RegexEmailAddress);
        isMatch = rgx.IsMatch(nre.EmailAddress);
        if (!isMatch)
        { ModelState.AddModelError(recordName, "String not a valid EmailAddress."); }
      }
      if (ModelState.IsValid) { nre = PSDC.EditLocation(nre); nre.PdpStatusName = eidLocationStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteLocation(nre); nre.PdpStatusName = eidLocationStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult CheckScribeLocation([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      // id is Location RRFgroupGuid
      ResetScribeRepository(); // use PSDC
      LocationEditModel? nre = PSDC.GetEditableLocationByKey(id);
      if (nre?.RRFgroupGuid == id) { nre = PSDC.CheckLocation(nre); nre.PdpStatusName = eidLocationStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
