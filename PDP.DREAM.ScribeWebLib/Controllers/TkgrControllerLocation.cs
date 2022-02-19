// TkgrControllerLocation.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Text.RegularExpressions;

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
  private const string eidLocationStatus = "span#LocationStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectLocations), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeSelectLocations([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableLocations(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertLocation), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    Regex? rgx = null; bool isMatch = false;
    if (!string.IsNullOrWhiteSpace(nre.DisplayImageUrl))
    {
      rgx = new Regex(NpdsConst.RegexLocationUrl);
      isMatch = rgx.IsMatch(nre.DisplayImageUrl);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, "String not a valid DisplayImageUrl."); }
    }
    if (!string.IsNullOrWhiteSpace(nre.UrlWebAddress))
    {
      rgx = new Regex(NpdsConst.RegexLocationUrl);
      isMatch = rgx.IsMatch(nre.UrlWebAddress);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, "String not a valid UrlWebAddress."); }
    }
    if (!string.IsNullOrWhiteSpace(nre.EmailAddress))
    {
      rgx = new Regex(NpdsConst.RegexEmailAddress);
      isMatch = rgx.IsMatch(nre.EmailAddress);
      if (!isMatch)
      { ModelState.AddModelError(nre.ItemXnam, "String not a valid EmailAddress."); }
    }
    if (ModelState.IsValid) { nre = PSDC.EditLocation(nre); nre.PdpStatusName = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteLocation), "", CoreDLC.ratsRgil, ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteLocation(nre); nre.PdpStatusName = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckLocation), "", CoreDLC.ratsRg, ScribeWLC.ranpView)]
  public JsonResult ScribeCheckLocation([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    LocationEditModel? nre = PSDC.GetEditableLocationByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckLocation(nre); nre.PdpStatusName = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
