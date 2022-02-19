// SqldbcUilLocationEdit.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public static partial class NpdsLinqSqlOperators
{
  public static LocationEditModel ToEditable(this NexusLocation r)
  {
    var nre = new LocationEditModel()
    {
      RRFgroupGuid = r.FgroupGuidKey,
      RRRecordGuid = r.RecordGuidRef,
      HasIndex = r.HasIndex,
      HasPriority = r.HasPriority,
      IsMarked = r.IsMarked,
      IsPrincipal = r.IsPrincipal,
      IsDeleted = r.IsDeleted,
      ManagedByAgentGuid = r.ManagedByAgentGuidRef,
      ManagedByAgentName = r.ManagedByAgentUserName,
      CreatedOn = r.CreatedOn,
      CreatedByAgentGuid = r.CreatedByAgentGuidRef,
      CreatedByAgentName = r.CreatedByAgentUserName,
      UpdatedOn = r.UpdatedOn,
      UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
      UpdatedByAgentName = r.UpdatedByAgentUserName,
      DeletedOn = r.DeletedOn,
      DeletedByAgentGuid = r.DeletedByAgentGuidRef,
      DeletedByAgentName = r.DeletedByAgentUserName,
      //
      Location = r.Location,
      DisplayText = r.DisplayText,
      DisplayImageUrl = r.DisplayImageUrl,
      UrlWebAddress = r.UrlWebAddress,
      UrlWebAddressValidated = r.UrlWebAddressValidated,
      EmailAddress = r.EmailAddress,
      EmailAddressValidated = r.EmailAddressValidated,
      StreetAddress = r.StreetAddress,
      StreetAddressValidated = r.StreetAddressValidated,
      ExtendedAddress = r.ExtendedAddress,
      CityLocality = r.CityLocality,
      StateRegion = r.StateRegion,
      Country = r.Country,
      PostalCode = r.PostalCode,
      Telephone = r.Telephone,
      GeocodeType = r.GeocodeType,
      GeocodeConfidence = r.GeocodeConfidence,
      FormattedAddress = r.FormattedAddress,
      Latitude = r.Latitude,
      Longitude = r.Longitude
    };
    return nre;
  }

  public static IQueryable<LocationEditModel> ToEditable(this IQueryable<NexusLocation> query)
  {
    IQueryable<LocationEditModel> rows =
      from r in query
      select new LocationEditModel
      {
        RRFgroupGuid = r.FgroupGuidKey,
        RRRecordGuid = r.RecordGuidRef,
        HasIndex = r.HasIndex,
        HasPriority = r.HasPriority,
        IsMarked = r.IsMarked,
        IsPrincipal = r.IsPrincipal,
        IsDeleted = r.IsDeleted,
        ManagedByAgentGuid = r.ManagedByAgentGuidRef,
        ManagedByAgentName = r.ManagedByAgentUserName,
        CreatedOn = r.CreatedOn,
        CreatedByAgentGuid = r.CreatedByAgentGuidRef,
        CreatedByAgentName = r.CreatedByAgentUserName,
        UpdatedOn = r.UpdatedOn,
        UpdatedByAgentGuid = r.UpdatedByAgentGuidRef,
        UpdatedByAgentName = r.UpdatedByAgentUserName,
        DeletedOn = r.DeletedOn,
        DeletedByAgentGuid = r.DeletedByAgentGuidRef,
        DeletedByAgentName = r.DeletedByAgentUserName,
          //
          Location = r.Location,
        DisplayText = r.DisplayText,
        DisplayImageUrl = r.DisplayImageUrl,
        UrlWebAddress = r.UrlWebAddress,
        UrlWebAddressValidated = r.UrlWebAddressValidated,
        EmailAddress = r.EmailAddress,
        EmailAddressValidated = r.EmailAddressValidated,
        StreetAddress = r.StreetAddress,
        StreetAddressValidated = r.StreetAddressValidated,
        ExtendedAddress = r.ExtendedAddress,
        CityLocality = r.CityLocality,
        StateRegion = r.StateRegion,
        Country = r.Country,
        PostalCode = r.PostalCode,
        Telephone = r.Telephone,
        GeocodeType = r.GeocodeType,
        GeocodeConfidence = r.GeocodeConfidence,
        FormattedAddress = r.FormattedAddress,
        Latitude = r.Latitude,
        Longitude = r.Longitude
      };
    return rows;
  }

}

public partial class ScribeDbsqlContext
{
  public IEnumerable<LocationEditModel> ListEditableLocations(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<LocationEditModel> result;
    try
    {
      IQueryable<NexusLocation> qry = this.NexusLocations;
      if (PRC.ClientHasAdminAccess || PRC.ClientHasEditorAccess)
      { qry = qry.Where(r => (r.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited) { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false) && (r.UpdatedByAgentGuidRef == PRC.AgentGuid)); }
        else { qry = qry.Where(r => (r.RecordGuidRef == guidKey) && (r.IsDeleted == false)); }
      }
      result = qry.OrderBy(r => r.HasPriority).ToEditable().ToList();
    }
    catch
    {
      result = Enumerable.Empty<LocationEditModel>();
    }
    return result;
  }

  public LocationEditModel GetEditableLocationByKey(Guid guidKey)
  { return QueryStorableLocationByKey(guidKey).ToEditable().SingleOrDefault(); }
  public LocationEditModel GetEditableLocationByKey(string guidKey)
  { return GetEditableLocationByKey(Guid.Parse(guidKey)); }

  public LocationEditModel EditLocation(LocationEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = PRC.AgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    NexusLocation storObj;
    if (isNewRecord)
    {
      // insert new record
      internalGuid = Guid.NewGuid();
      storObj = new NexusLocation()
      {
        CreatedByAgentGuidRef = agentGuid,
        UpdatedByAgentGuidRef = agentGuid,
        RecordGuidRef = recordGuid,
        FgroupGuidKey = internalGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableLocationByKey(internalGuid);
      storObj.UpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    // from user input controls
    storObj.DisplayText = editObj.DisplayText ?? string.Empty;
    storObj.DisplayImageUrl = editObj.DisplayImageUrl ?? string.Empty;
    storObj.UrlWebAddress = editObj.UrlWebAddress ?? string.Empty;
    storObj.UrlWebAddressValidated = editObj.UrlWebAddressValidated;
    storObj.EmailAddress = editObj.EmailAddress ?? string.Empty;
    storObj.EmailAddressValidated = editObj.EmailAddressValidated;
    storObj.StreetAddress = editObj.StreetAddress ?? string.Empty;
    storObj.StreetAddressValidated = editObj.StreetAddressValidated;
    storObj.ExtendedAddress = editObj.ExtendedAddress ?? string.Empty;
    storObj.CityLocality = editObj.CityLocality ?? string.Empty;
    storObj.StateRegion = editObj.StateRegion ?? string.Empty;
    storObj.Country = editObj.Country ?? string.Empty;
    storObj.PostalCode = editObj.PostalCode ?? string.Empty;
    storObj.Telephone = editObj.Telephone ?? string.Empty;
    // from GeoLocation lookup service
    storObj.GeocodeType = editObj.GeocodeType ?? string.Empty;
    storObj.GeocodeConfidence = editObj.GeocodeConfidence ?? string.Empty;
    storObj.FormattedAddress = editObj.FormattedAddress ?? string.Empty;
    storObj.Latitude = editObj.Latitude ?? 0;
    storObj.Longitude = editObj.Longitude ?? 0;
    // parsed for vcard in Location field
    string vcard = CreateLocationVcard(editObj);
    storObj.Location = vcard;
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeLocationEdit(
        agentGuid, infosetGuid, recordGuid, internalGuid,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.Location, storObj.DisplayText, storObj.DisplayImageUrl,
        storObj.UrlWebAddress, storObj.UrlWebAddressValidated, storObj.EmailAddress, storObj.EmailAddressValidated, storObj.StreetAddress, storObj.StreetAddressValidated,
        storObj.ExtendedAddress, storObj.FormattedAddress, storObj.CityLocality, storObj.StateRegion, storObj.Country, storObj.PostalCode, storObj.Telephone,
        storObj.GeocodeType, storObj.GeocodeConfidence, storObj.Latitude, storObj.Longitude);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.NexusLocations.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableLocationByKey(internalGuid);
    if (editObj == null) { editObj = new LocationEditModel(); }
    // refresh the recordIndex
    recordIndex = editObj.HasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} written to database";
      editObj.PdpStatusItemStored = true;
    }
    else { editObj.PdpStatusMessage = errMsg; }
    return editObj;
  }

  public LocationEditModel DeleteLocation(LocationEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = PRC.AgentGuid;
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableLocationByKey(internalGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = PRC.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeLocationDelete(
          storObj.DeletedByAgentGuidRef, storObj.RecordGuidRef, storObj.FgroupGuidKey, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.NexusLocations.Attach(storObj);
        this.NexusLocations.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableLocationByKey(internalGuid);
      if (editObj == null) { editObj = new LocationEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

  public LocationEditModel CheckLocation(LocationEditModel editObj)
  {
    editObj.PdpStatusMessage = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = PRC.AgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    if (!internalGuid.IsEmpty())
    {
      if (!string.IsNullOrWhiteSpace(editObj.StreetAddress + editObj.CityLocality + editObj.StateRegion + editObj.Country + editObj.PostalCode))
      {
        var country = editObj.Country ?? string.Empty;
        var stateRegion = editObj.StateRegion ?? string.Empty;
        var postalCode = editObj.PostalCode ?? string.Empty;
        var cityLocality = editObj.CityLocality ?? string.Empty;
        var streetAddress = editObj.StreetAddress ?? string.Empty;
        var reqUrl = bingMaps.BingMapsRequestUrl(PdpSiteSettings.Values.ApiKeyBingMaps, country, stateRegion, postalCode, cityLocality, streetAddress);
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
    var statusEnum = NpdsConst.InfosetStatus.AddressInvalid;
    var locations = ListEditableLocations(recordGuid);
    foreach (LocationEditModel loc in locations) { CheckLocation(loc); }
    locations = ListEditableLocations(recordGuid);
    foreach (LocationEditModel loc in locations)
    {
      if ((!string.IsNullOrEmpty(loc.UrlWebAddress) && (loc.UrlWebAddressValidated != null))
      || (!string.IsNullOrEmpty(loc.EmailAddress) && (loc.EmailAddressValidated != null))
      || (!string.IsNullOrEmpty(loc.StreetAddress) && (loc.StreetAddressValidated != null)))
      {
        statusEnum = NpdsConst.InfosetStatus.AddressValid;
        break;
      }
    }
    statusCode = (short)statusEnum;
    rrr.LocationsStatusCode = statusCode;
    return statusCode;
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

}
