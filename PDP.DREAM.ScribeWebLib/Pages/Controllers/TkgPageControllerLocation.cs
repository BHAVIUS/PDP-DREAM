// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidLocationStatus = TkndoElemPrfx + "LocationStatus";

  public virtual JsonResult OnPostReadLocations([DataSourceRequest] DataSourceRequest dsRequest,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadLocations);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
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
    LocationUxm fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    Regex? rgx = null; bool isMatch = false;
    if (ModelState.IsValid) { fgr = PSDC.EditLocation(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidLocationStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteLocation([DataSourceRequest] DataSourceRequest dsRequest,
    LocationUxm fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteLocation(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidLocationStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostCheckLocation([DataSourceRequest] DataSourceRequest dsRequest,
    Guid fgroupGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    LocationUxm? fgr = PSDC.GetEditableLocationByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = PSDC.CheckLocation(fgr); fgr.NdisElemId = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostReseqLocation([DataSourceRequest] DataSourceRequest dsRequest,
    Guid fgroupGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    LocationUxm? fgr = PSDC.GetEditableLocationByKey(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = PSDC.ReseqLocation(fgr); fgr.NdisElemId = eidLocationStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  // TODO: deprecate or refactor/reconfigure
  //public virtual IActionResult OnPostCheckLocations(string searchFilter, string serviceTag, string entityType = "")
  //{
  //  WRACE.ParseNpdsSelectFilter(searchFilter, serviceTag, entityType, "", "");
  //  OpenScribeConnection(); // use PSDC
  //  foreach (var rrr in PSDC.ListEditableResrepRoots())
  //  {
  //    var recordGuid = PdpGuid.ParseToNonNullable(rrr.RRRecordGuid, EGS);
  //    if (!PdpGuid.IsInvalidGuid(recordGuid))
  //    {
  //      foreach (var loc in PSDC.ListEditableLocations(recordGuid))
  //      {
  //       // if (!string.IsNullOrEmpty(loc.CityLocality)) { PSDC.CheckLocation(loc); }
  //      }
  //    }
  //  }
  //  CloseScribeConnection();
  //  return Page();
  //}

} // end class

// end file