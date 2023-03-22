// SqldbcUilLocationEditStore.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public LocationEditModel EditLocation(LocationEditModel editObj, bool byStorProc = true)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, Guid.Empty);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    NexusLocation storObj;
    if (isNewRecord)
    {
      // insert new record
      fgroupGuid = Guid.NewGuid();
      storObj = new NexusLocation()
      {
        CreatedByAgentGuidRef = agentGuid,
        UpdatedByAgentGuidRef = agentGuid,
        RecordGuidRef = recordGuid,
        FgroupGuidKey = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj = GetStorableLocationByKey(fgroupGuid);
      storObj.UpdatedByAgentGuidRef = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;

    storObj.Location = editObj.Location;
    storObj.FieldFormatCodeRef = editObj.FieldFormatCode;

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
    storObj.Location = vcard; // override Location if fieldFormat == None
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeLocationEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid, storObj.FieldFormatCodeRef,
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
    editObj = GetEditableLocationByKey(fgroupGuid);
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
    var agentGuid = NPDSCP.ClientAgentGuid;
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableLocationByKey(fgroupGuid);
      storObj.DeletedByAgentGuidRef = agentGuid;
      storObj.IsDeleted = NPDSCP.ClientHasAdminAccess;  // maps to IsRealDelete input parameter in storproc
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
      editObj = GetEditableLocationByKey(fgroupGuid);
      if (editObj == null) { editObj = new LocationEditModel(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

} // end class

// end file
