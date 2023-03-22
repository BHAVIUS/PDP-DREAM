// SqldbcUilLocationEditCheck.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public LocationEditModel CheckLocation(LocationEditModel editObj)
  {
    editObj.PdpStatusMessage = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    if (!fgroupGuid.IsEmpty())
    {
      if (!string.IsNullOrWhiteSpace(editObj.StreetAddress + editObj.CityLocality + editObj.StateRegion + editObj.Country + editObj.PostalCode))
      {
        var country = editObj.Country ?? string.Empty;
        var stateRegion = editObj.StateRegion ?? string.Empty;
        var postalCode = editObj.PostalCode ?? string.Empty;
        var cityLocality = editObj.CityLocality ?? string.Empty;
        var streetAddress = editObj.StreetAddress ?? string.Empty;
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
          editObj.PdpStatusMessage = $"Latitude = {editObj.Latitude}, Longitude = {editObj.Longitude}, Address = {editObj.FormattedAddress}, ";
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
          editObj.PdpStatusMessage += $"URL = {editObj.UrlWebAddressHtml}, ";
        }
        else
        {
          editObj.UrlWebAddressValidated = null;
        }
      }
      EditLocation(editObj); // store update
      if (string.IsNullOrEmpty(editObj.PdpStatusMessage)) { editObj.PdpStatusMessage = "<span class='pdpStatusInvalid'>Location not validated.</span>"; }
      else { editObj.PdpStatusMessage = $"<span class='pdpStatusValid'>Location validated: {editObj.PdpStatusMessage}</span>"; }
    }
    return editObj;
  }

  public virtual short CheckLocations(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckLocations(ref rrr);
  }

  public virtual short CheckLocations(ref NexusResrepEditModel rrr)
  {
    short statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusEnum = PdpAppConst.NpdsInfosetStatus.AddressInvalid;
    var locations = ListEditableLocations(recordGuid);
    foreach (LocationEditModel loc in locations) { CheckLocation(loc); }
    locations = ListEditableLocations(recordGuid);
    foreach (LocationEditModel loc in locations)
    {
      if ((!string.IsNullOrEmpty(loc.UrlWebAddress) && (loc.UrlWebAddressValidated != null))
      || (!string.IsNullOrEmpty(loc.EmailAddress) && (loc.EmailAddressValidated != null))
      || (!string.IsNullOrEmpty(loc.StreetAddress) && (loc.StreetAddressValidated != null)))
      {
        statusEnum = PdpAppConst.NpdsInfosetStatus.AddressValid;
        break;
      }
    }
    statusCode = (short)statusEnum;
    rrr.LocationsStatusCode = statusCode;
    return statusCode;
  }

  public LocationEditModel ReseqLocation(LocationEditModel ssEdit)
  {
    var errMsg = string.Empty;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRRecordGuid, Guid.Empty);
    var ssRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRFgroupGuid, Guid.Empty);
    var ssRecordName = ssEdit.ItemXnam;
    var ssRecordIndex = ssEdit.HasIndex;
    var ssRecordPriority = (short?)ssEdit.HasPriority;
    var isNewRecord = ssRecordGuid.IsEmpty();
    // resequence object
    if (!isNewRecord)
    {
      var rrInfosetGuid = Guid.Empty;
      var errCod = ScribeLocationReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableLocationByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new LocationEditModel();
        errMsg += $"{ssRecordName} record with index {ssRecordIndex} not found";
      }
      // update status message
      if (string.IsNullOrEmpty(errMsg))
      {
        ssEdit.PdpStatusMessage =
          $"{ssRecordName} record with index {ssRecordIndex} resequenced in database";
      }
      else { ssEdit.PdpStatusMessage = errMsg; }
    }
    return ssEdit;
  }

  // TODO: code analogous Vcards for persons and organizations when EntityType is Person or Organization
  // in other words make Vcard formatted xhtml dependent on the EntityType, but how to prevent redundancy with other fields?
  public string CreateLocationVcard(LocationEditModel eo)
  {
    string xhtml = @"<div class='vcard'>";

    //"<div class='fn'>" + @PersonFirstName + " " + @PersonLastName + "</div>" +
    //"<div class='n'>" +
    //    "<span class='honorific-prefixes'></span>" +
    //    "<span class='given-name'>" + @PersonFirstName + "</span>" +
    //    "<span class='additional-names'></span>" +
    //    "<span class='family-name'>" + @PersonLastName + "</span>" +
    //    "<span class='honorific-suffixes'></span>" +
    //"</div>" +
    //"<div class='org'>" + @OrganizationName + "</div>" +

    if (!string.IsNullOrWhiteSpace(eo.StreetAddress) || !string.IsNullOrWhiteSpace(eo.CityLocality)
      || !string.IsNullOrWhiteSpace(eo.PostalCode) || !string.IsNullOrWhiteSpace(eo.Country))
    {
      xhtml = xhtml + "<div class='adr'>";
      if ((eo.Latitude != 0) && (eo.Longitude != 0))
      {
        xhtml = xhtml +
          "<div class='geo'>" +
            "<span class='latitude'>" + eo.Latitude.ToString() + "</span>" +
            "<span class='longitude'>" + eo.Longitude.ToString() + "</span>" +
          "</div>";
      }
      xhtml = xhtml +
        "<div class='street-address'>" + eo.StreetAddress?.ToString() + "</div>" +
        "<div class='extended-address'>" + eo.ExtendedAddress?.ToString() + "</div>" +
        "<div>" +
          "<span class='locality'>" + eo.CityLocality?.ToString() + "</span>" +
          "<span class='region'>" + eo.StateRegion?.ToString() + "</span>" +
          "<span class='postal-code'>" + eo.PostalCode?.ToString() + "</span>" +
          "<span class='country-name'>" + eo.Country?.ToString() + "</span>" +
        "</div>";
      xhtml = xhtml + "</div>";
    }
    if (!string.IsNullOrWhiteSpace(eo.Telephone))
    {
      xhtml = xhtml + "<div class='tel'>" + eo.Telephone.ToString() + "</div>";
    }
    if (!string.IsNullOrWhiteSpace(eo.EmailAddress))
    {
      xhtml = xhtml + "<div class='email' href='mailto:" + eo.EmailAddress.ToString() + "'>" + eo.EmailAddress.ToString() + "</div>";
    }
    if (!string.IsNullOrWhiteSpace(eo.UrlWebAddress))
    {
      xhtml = xhtml + "<div class='url' href='" + eo.UrlWebAddress.ToString() + "'>" + eo.UrlWebAddress.ToString() + "</div>";
    }
    xhtml = xhtml + "</div>";
    return xhtml;
  }

} // end class

// end file