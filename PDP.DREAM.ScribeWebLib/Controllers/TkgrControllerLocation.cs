// TkgrControllerLocation.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text.RegularExpressions;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidLocationStatus = "span#LocationStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectLocations), "", TSrgil, NPmvc)]
  public JsonResult ScribeSelectLocations([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableLocations(recordGuid, isLimited).ToDataSourceResult(request);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertLocation), "", TSrgil, NPmvc)]
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
  [PdpMvcRoute(nameof(ScribeDeleteLocation), "", TSrgil, NPmvc)]
  public JsonResult ScribeDeleteLocation([DataSourceRequest] DataSourceRequest dsr, LocationEditModel nre, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    nre.RRRecordGuid = ParseResRepRecordGuid(nre.ItemXnam, nre.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { nre = PSDC.DeleteLocation(nre); nre.PdpStatusName = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeCheckLocation), "", TSrg, NPmvc)]
  public JsonResult ScribeCheckLocation([DataSourceRequest] DataSourceRequest dsr, Guid recordGuid)
  {
    ResetScribeRepository(); // use PSDC
    LocationEditModel? nre = PSDC.GetEditableLocationByKey(recordGuid);
    if (nre?.RRFgroupGuid == recordGuid) { nre = PSDC.CheckLocation(nre); nre.PdpStatusName = eidLocationStatus; }
    DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
