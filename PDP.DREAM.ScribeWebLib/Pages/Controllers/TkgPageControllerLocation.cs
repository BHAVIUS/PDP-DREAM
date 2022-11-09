// TkgPageControllerLocation.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text.RegularExpressions;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageControllerBase
{
  private const string eidLocationStatus = "span#LocationStatus";

  public virtual JsonResult OnPostReadLocations([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableLocations(recordGuid, isLimited).ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteLocation([DataSourceRequest] DataSourceRequest dsRequest,
    LocationEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    Regex? rgx = null; bool isMatch = false;
    if (!string.IsNullOrWhiteSpace(fgr.DisplayImageUrl))
    {
      rgx = new Regex(PdpAppConst.RegexLocationUrl);
      isMatch = rgx.IsMatch(fgr.DisplayImageUrl);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, "String not a valid DisplayImageUrl."); }
    }
    if (!string.IsNullOrWhiteSpace(fgr.UrlWebAddress))
    {
      rgx = new Regex(PdpAppConst.RegexLocationUrl);
      isMatch = rgx.IsMatch(fgr.UrlWebAddress);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, "String not a valid UrlWebAddress."); }
    }
    if (!string.IsNullOrWhiteSpace(fgr.EmailAddress))
    {
      rgx = new Regex(PdpAppConst.RegexEmailAddress);
      isMatch = rgx.IsMatch(fgr.EmailAddress);
      if (!isMatch)
      { ModelState.AddModelError(fgr.ItemXnam, "String not a valid EmailAddress."); }
    }
    if (ModelState.IsValid) { fgr = PSDC.EditLocation(fgr); fgr.PdpStatusElement = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteLocation([DataSourceRequest] DataSourceRequest dsRequest,
    LocationEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteLocation(fgr); fgr.PdpStatusElement = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostCheckLocation([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    LocationEditModel? fgr = PSDC.GetEditableLocationByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.CheckLocation(fgr); fgr.PdpStatusElement = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostReseqLocation([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    LocationEditModel? fgr = PSDC.GetEditableLocationByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid) { fgr = PSDC.ReseqLocation(fgr); fgr.PdpStatusElement = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  // TODO: deprecate or refactor/reconfigure
  public virtual IActionResult OnPostCheckLocations(string serviceType, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, "Edit");
    ResetScribeRepository();
    foreach (var rrr in PSDC.ListEditableResrepRoots())
    {
      var recordGuid = PdpGuid.ParseToNonNullable(rrr.RRRecordGuid, Guid.Empty);
      if (!PdpGuid.IsInvalidGuid(recordGuid))
      {
        foreach (var loc in PSDC.ListEditableLocations(recordGuid))
        {
          if (!string.IsNullOrEmpty(loc.CityLocality)) { PSDC.CheckLocation(loc); }
        }
      }
    }
    return Page();
  }

} // end class

// end file