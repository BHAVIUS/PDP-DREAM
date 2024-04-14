// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual LocationExtnUxm CheckLocationExtn(LocationExtnUxm editObj)
  {
    editObj.NdisElemMsg = "";
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    if (!fgroupGuid.IsEmpty())
    {
      if (!string.IsNullOrWhiteSpace(editObj.StreetAddress + editObj.CityLocality + editObj.StateRegion + editObj.Country + editObj.PostalCode))
      {
        var country = editObj.Country ?? "";
        var stateRegion = editObj.StateRegion ?? "";
        var postalCode = editObj.PostalCode ?? "";
        var cityLocality = editObj.CityLocality ?? "";
        var streetAddress = editObj.StreetAddress ?? "";
        var reqUrl = bingMaps.BingMapsRequestUrl(PDPSS.ApiKeyBingMaps, country, stateRegion, postalCode, cityLocality, streetAddress);
        var bingResp = bingMaps.GetJsonResponse(reqUrl);
        var loc = bingMaps.ParseLocationFromJsonResponse(bingResp);
        if (loc != null)
        {
          editObj.StreetAddressValidated = DateTime.UtcNow;
          editObj.GeocodeType = bingMaps.GetBingEntityType(loc);
          editObj.GeocodeConfidence = bingMaps.GetBingConfidence(loc);
          editObj.Latitude = bingMaps.GetLatitude(loc);
          editObj.Longitude = bingMaps.GetLongitude(loc);
          editObj.FormattedAddress = bingMaps.GetBingFormattedAddress(loc);
          editObj.NdisElemMsg = $"Latitude = {editObj.Latitude}, Longitude = {editObj.Longitude}, Address = {editObj.FormattedAddress}, ";
        }
        else
        {
          editObj.StreetAddressValidated = null;
        }
      }
      if (!string.IsNullOrWhiteSpace(editObj.UrlWebAddress))
      {
        var urlIsValid = editObj.UrlWebAddress.UrlIsValid();
        if (urlIsValid)
        {
          editObj.UrlWebAddressValidated = DateTime.UtcNow;
          editObj.NdisElemMsg += $"URL = {editObj.UrlWebAddressHtml}, ";
        }
        else
        {
          editObj.UrlWebAddressValidated = null;
        }
      }
      editObj = EditLocation(editObj); // store update
      if (string.IsNullOrEmpty(editObj.NdisElemMsg)) { editObj.NdisElemMsg = "<span class='pdpStatusInvalid'>Location not validated.</span>"; }
      else { editObj.NdisElemMsg = $"<span class='pdpStatusValid'>LocationExtn validated: {editObj.NdisElemMsg}</span>"; }
    }
    return editObj;
  }

  public virtual byte CheckLocationExtns(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckLocations(ref rrr);
  }

  public virtual byte CheckLocationExtns(ref CoreResrepLeafUxm rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusEnum = NPDSCD.InfosetStatusAddressInvalid;
    var locations = ListEditableLocationExtns(recordGuid);
    foreach (LocationExtnUxm loc in locations) { CheckLocation(loc); }
    locations = ListEditableLocationExtns(recordGuid);
    foreach (LocationExtnUxm loc in locations)
    {
      if ((!string.IsNullOrEmpty(loc.UrlWebAddress) && (loc.UrlWebAddressValidated != null))
      || (!string.IsNullOrEmpty(loc.EmailAddress) && (loc.EmailAddressValidated != null))
      || (!string.IsNullOrEmpty(loc.StreetAddress) && (loc.StreetAddressValidated != null)))
      {
        statusEnum = NPDSCD.InfosetStatusAddressValid;
        break;
      }
    }
    rrr.LocationsStatusCode = statusEnum.ECode;
    return statusEnum.ECode;
  }

  public LocationExtnUxm ReseqLocationExtn(LocationExtnUxm ssEdit)
  {
    var errMsg = "";
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRRecordGuid, EGS);
    var ssRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRFgroupGuid, EGS);
    var ssRecordName = ssEdit.ItemXnam;
    var ssRecordIndex = ssEdit.HasIndex;
    var ssRecordPriority = (short?)ssEdit.HasPriority;
    var isNewRecord = ssRecordGuid.IsEmpty();
    // resequence object
    if (!isNewRecord)
    {
      var rrInfosetGuid = EGS;
      var errCod = ScribeLocationReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableLocationExtnByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new LocationExtnUxm();
        errMsg += $"{ssRecordName} record with index {ssRecordIndex} not found";
      }
      // update status message
      if (string.IsNullOrEmpty(errMsg))
      {
        ssEdit.NdisElemMsg =
          $"{ssRecordName} record with index {ssRecordIndex} resequenced in database";
      }
      else { ssEdit.NdisElemMsg = errMsg; }
    }
    return ssEdit;
  }

  //public string CreateLocationExtnVcard(LocationExtnUxm eo)
  //{
  //  string xhtml = @"<div class='vcard'>";

  //  //"<div class='fn'>" + @PersonFirstName + " " + @PersonLastName + "</div>" +
  //  //"<div class='n'>" +
  //  //    "<span class='honorific-prefixes'></span>" +
  //  //    "<span class='given-name'>" + @PersonFirstName + "</span>" +
  //  //    "<span class='additional-names'></span>" +
  //  //    "<span class='family-name'>" + @PersonLastName + "</span>" +
  //  //    "<span class='honorific-suffixes'></span>" +
  //  //"</div>" +
  //  //"<div class='org'>" + @OrganizationName + "</div>" +

  //  if (!string.IsNullOrWhiteSpace(eo.StreetAddress) || !string.IsNullOrWhiteSpace(eo.CityLocality)
  //    || !string.IsNullOrWhiteSpace(eo.PostalCode) || !string.IsNullOrWhiteSpace(eo.Country))
  //  {
  //    xhtml = xhtml + "<div class='adr'>";
  //    if ((eo.Latitude != 0) && (eo.Longitude != 0))
  //    {
  //      xhtml = xhtml +
  //        "<div class='geo'>" +
  //          "<span class='latitude'>" + eo.Latitude.ToString() + "</span>" +
  //          "<span class='longitude'>" + eo.Longitude.ToString() + "</span>" +
  //        "</div>";
  //    }
  //    xhtml = xhtml +
  //      "<div class='street-address'>" + eo.StreetAddress?.ToString() + "</div>" +
  //      "<div class='extended-address'>" + eo.ExtendedAddress?.ToString() + "</div>" +
  //      "<div>" +
  //        "<span class='locality'>" + eo.CityLocality?.ToString() + "</span>" +
  //        "<span class='region'>" + eo.StateRegion?.ToString() + "</span>" +
  //        "<span class='postal-code'>" + eo.PostalCode?.ToString() + "</span>" +
  //        "<span class='country-name'>" + eo.Country?.ToString() + "</span>" +
  //      "</div>";
  //    xhtml = xhtml + "</div>";
  //  }
  //  if (!string.IsNullOrWhiteSpace(eo.Telephone))
  //  {
  //    xhtml = xhtml + "<div class='tel'>" + eo.Telephone.ToString() + "</div>";
  //  }
  //  if (!string.IsNullOrWhiteSpace(eo.EmailAddress))
  //  {
  //    xhtml = xhtml + "<div class='email' href='mailto:" + eo.EmailAddress.ToString() + "'>" + eo.EmailAddress.ToString() + "</div>";
  //  }
  //  if (!string.IsNullOrWhiteSpace(eo.UrlWebAddress))
  //  {
  //    xhtml = xhtml + "<div class='url' href='" + eo.UrlWebAddress.ToString() + "'>" + eo.UrlWebAddress.ToString() + "</div>";
  //  }
  //  xhtml = xhtml + "</div>";
  //  return xhtml;
  //}

} // end class

// end file