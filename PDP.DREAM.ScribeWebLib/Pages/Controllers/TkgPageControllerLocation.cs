// TkgPageControllerLocation.cs
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidLocationStatus = "span#LocationStatus";

  public virtual JsonResult OnPostReadLocations([DataSourceRequest] DataSourceRequest dsRequest,
   // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadLocations);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("Locations", "RRRecordGuid invalid."); }
      else
      {
          dsResult = PSDC.ListEditableLocations(recordGuid, isLimited)
          .ToDataSourceResult(dsRequest);
      }
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostWriteLocation([DataSourceRequest] DataSourceRequest dsRequest,
    LocationEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
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
    if (ModelState.IsValid) { fgr = PSDC.EditLocation(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidLocationStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteLocation([DataSourceRequest] DataSourceRequest dsRequest,
    LocationEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteLocation(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidLocationStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostCheckLocation([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    LocationEditModel? fgr = PSDC.GetEditableLocationByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.CheckLocation(fgr); fgr.PdpStatusElement = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostReseqLocation([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    LocationEditModel? fgr = PSDC.GetEditableLocationByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.ReseqLocation(fgr); fgr.PdpStatusElement = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  // TODO: deprecate or refactor/reconfigure
  public virtual IActionResult OnPostCheckLocations(string searchFilter, string serviceTag, string entityType = "")
  {
    QURC.ParseNpdsSelectFilterForPage(searchFilter, serviceTag, entityType, "Edit");
    OpenScribeConnection();
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
    CloseScribeConnection();
    return Page();
  }

} // end class

// end file