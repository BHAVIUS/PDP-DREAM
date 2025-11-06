// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial class ScribeTkgPageController
{
  private const string eidLocationExtnStatus = TkndoElemPrfx + "LocationExtnStatus";

  public JsonResult OnPostReadLocationExtns([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadLocationExtns);
#if DEBUG
    NPDSCW.DebugWraceData(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    NPDSCW.OpenScribeConnection(); // use PSDC
#if DEBUG
    NPDSCW.DebugScribeRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    if (recordGuid.IsInvalid())
    { ModelState.AddModelError("LocationExtns", "RRRecordGuid invalid."); }
    else
    {
      dsResult = NPDSCW.ScribeDR.ListEditableLocationExtns(recordGuid, isLimited)
        .ToDataSourceResult(dsRequest, ModelState);
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  public JsonResult OnPostWriteLocationExtn([DataSourceRequest] DataSourceRequest dsRequest,
    LocationExtnUxm fgr,
    Guid recordGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    // Parse method in extension but not base
    if (ModelState.IsValid) { fgr = ParseLocationExtn(fgr); }
    if (ModelState.IsValid) { fgr = NPDSCW.ScribeDR.EditLocationExtn(fgr); }
    else { fgr.NdisElemMsg = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.NdisElemId = eidLocationExtnStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Delete method in base but not extension

  // Check method in both base and extension
  public JsonResult OnPostCheckLocationExtn([DataSourceRequest] DataSourceRequest dsRequest,
    Guid fgroupGuid, bool isLimited = false)
  {
    NPDSCW.OpenScribeConnection(); // use PSDC
    LocationExtnUxm? fgr = NPDSCW.ScribeDR.GetEditableLocationExtnByFGuid(fgroupGuid);
    if (fgr?.RRFgroupGuid == fgroupGuid)
    { fgr = NPDSCW.ScribeDR.CheckLocationExtn(fgr); }
    fgr.NdisElemId = eidLocationExtnStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseScribeConnection();
    return jsonData;
  }

  // Reseq method in base but not extension
  
  // Parse method in extension but not base
  public LocationExtnUxm ParseLocationExtn(LocationExtnUxm uxm)
  {
    // ATTN: this hook for parsing the extension
    Regex? rgx = null; bool isMatch = false;
    if (!string.IsNullOrWhiteSpace(uxm.DisplayImageUrl))
    {
      rgx = new Regex(RgxsLocationUrl);
      isMatch = rgx.IsMatch(uxm.DisplayImageUrl);
      if (!isMatch)
      { ModelState.AddModelError(uxm.ItemXnam, "String not a valid DisplayImageUrl."); }
    }
    if (!string.IsNullOrWhiteSpace(uxm.UrlWebAddress))
    {
      rgx = new Regex(RgxsLocationUrl);
      isMatch = rgx.IsMatch(uxm.UrlWebAddress);
      if (!isMatch)
      { ModelState.AddModelError(uxm.ItemXnam, "String not a valid UrlWebAddress."); }
    }
    if (!string.IsNullOrWhiteSpace(uxm.EmailAddress))
    {
      rgx = new Regex(RgxsEmailAddress);
      isMatch = rgx.IsMatch(uxm.EmailAddress);
      if (!isMatch)
      { ModelState.AddModelError(uxm.ItemXnam, "String not a valid EmailAddress."); }
    }
    if (!string.IsNullOrWhiteSpace(uxm.StreetAddress + uxm.CityLocality + uxm.StateRegion + uxm.Country + uxm.PostalCode))
    {
      var country = uxm.Country ?? "";
      var stateRegion = uxm.StateRegion ?? "";
      var postalCode = uxm.PostalCode ?? "";
      var cityLocality = uxm.CityLocality ?? "";
      var streetAddress = uxm.StreetAddress ?? "";
      var reqUrl = bingMaps.BingMapsRequestUrl(PDPSS.ApiKeyBingMaps, country, stateRegion, postalCode, cityLocality, streetAddress);
      NpdsSchemaLib.Services.Response? bingResp = null;
      NpdsSchemaLib.Services.Location? bingLoc = null;
      try
      {
        // TODO: dev.earth.net:80 host no longer available
        // TODO: rebuild with alternate geolocation service
      bingResp = bingMaps.GetBingResponse(reqUrl);
      bingLoc = bingMaps.ParseBingResponse(bingResp);
      }
      catch
      {
        // TODO: report error message
      }
      if (bingLoc != null)
      {
        uxm.StreetAddressValidated = DateTime.UtcNow;
        uxm.GeocodeType = bingMaps.GetBingEntityType(bingLoc);
        uxm.GeocodeConfidence = bingMaps.GetBingConfidence(bingLoc);
        uxm.Latitude = bingMaps.GetBingLatitude(bingLoc);
        uxm.Longitude = bingMaps.GetBingLongitude(bingLoc);
        uxm.FormattedAddress = bingMaps.GetBingFormattedAddress(bingLoc);
        uxm.NdisElemMsg = $"Latitude = {uxm.Latitude}, Longitude = {uxm.Longitude}, Address = {uxm.FormattedAddress}, ";
      }
      else
      {
        uxm.StreetAddressValidated = null;
      }
    }
    if (!string.IsNullOrWhiteSpace(uxm.UrlWebAddress))
    {
      var urlIsValid = uxm.UrlWebAddress.UrlIsValid();
      if (urlIsValid)
      {
        uxm.UrlWebAddressValidated = DateTime.UtcNow;
        uxm.NdisElemMsg += $"URL = {uxm.UrlWebAddressHtml}, ";
      }
      else
      {
        uxm.UrlWebAddressValidated = null;
      }
    }
    return uxm;
  }

} // end class

// end file
