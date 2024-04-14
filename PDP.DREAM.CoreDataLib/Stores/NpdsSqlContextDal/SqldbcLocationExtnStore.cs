// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public LocationExtnUxm EditLocation(LocationExtnUxm editObj, bool byStorProc = true)
  {
    var isNewRecord = false;
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var infosetGuid = PdpGuid.ParseToNonNullable(editObj.RRInfosetGuid, EGS);
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    if (fgroupGuid.IsEmpty()) { fgroupGuid = PdpNewGuid(); }
    var storObj = GetStorableLocationExtnByKey(fgroupGuid);
    if ((storObj == null) || (storObj.FgroupGuid.IsEmpty())) { isNewRecord = true; }
    if (isNewRecord)
    {
      // insert new record
      storObj = new DoorsLocationExtn()
      {
        CreatedByAgentGuid = agentGuid,
        UpdatedByAgentGuid = agentGuid,
        RecordGuid = recordGuid,
        FgroupGuid = fgroupGuid
      };
    }
    else
    {
      // update existing record
      storObj.UpdatedByAgentGuid = agentGuid;
    }

    // begin common insert/update edit
    storObj.HasPriority = editObj.HasPriority;
    storObj.IsMarked = editObj.IsMarked;
    storObj.IsPrincipal = editObj.IsPrincipal;
    storObj.FieldFormatCode = editObj.FieldFormatCode;
    // Location Extension may NOT be explicitly editable in UIL
    storObj.Location = editObj.Location ?? "";

    // TODO: validation of editObj in UIL or storObj in DAL ???
    //  apply principle of minimizing exposed surface of editObj
    // so move these checks on location validation to the storObj

    // from user input controls
    storObj.DisplayText = editObj.DisplayText ?? "";
    storObj.DisplayImageUrl = editObj.DisplayImageUrl ?? "";
    storObj.UrlWebAddress = editObj.UrlWebAddress ?? "";
    storObj.UrlWebAddressValidated = editObj.UrlWebAddressValidated;
    storObj.EmailAddress = editObj.EmailAddress ?? "";
    storObj.EmailAddressValidated = editObj.EmailAddressValidated;
    storObj.StreetAddress = editObj.StreetAddress ?? "";
    storObj.StreetAddressValidated = editObj.StreetAddressValidated;
    storObj.ExtendedAddress = editObj.ExtendedAddress ?? "";
    storObj.CityLocality = editObj.CityLocality ?? "";
    storObj.StateRegion = editObj.StateRegion ?? "";
    storObj.Country = editObj.Country ?? "";
    storObj.PostalCode = editObj.PostalCode ?? "";
    storObj.Telephone = editObj.Telephone ?? "";
    // from GeoLocation lookup service
    storObj.GeocodeType = editObj.GeocodeType ?? "";
    storObj.GeocodeConfidence = editObj.GeocodeConfidence ?? "";
    storObj.FormattedAddress = editObj.FormattedAddress ?? "";
    storObj.Latitude = editObj.Latitude ?? 0;
    storObj.Longitude = editObj.Longitude ?? 0;

    // update storObj.Location if formatter exists for format
    var fieldFormatCod = (byte)editObj.FieldFormatCode;
    var fieldFormatRec = NPDSCD.ParseFieldFormat(fieldFormatCod);
    var fieldFormatNam = fieldFormatRec.EName;
    switch (fieldFormatNam)
    {
      case (DdeFieldFormat.URL):
        var url = editObj.FormatLocationUrl();
        storObj.Location = url;
        break;
      case (DdeFieldFormat.VCF): // aka vCard
        string vcard = editObj.FormatLocationVcard();
        storObj.Location = vcard;
        break;
      // TODO: implement other cases
      case (DdeFieldFormat.FreeForm):
      default:
        // do not update unless formatter exists for format
        break;
    }
    // end common insert/update edit

    if (byStorProc)
    {
      var errCod = ScribeLocationExtnEdit(
        agentGuid, infosetGuid, recordGuid, fgroupGuid, storObj.FieldFormatCode,
        storObj.HasPriority, storObj.IsMarked, storObj.IsPrincipal,
        storObj.Location, storObj.DisplayText, storObj.DisplayImageUrl,
        storObj.UrlWebAddress, storObj.UrlWebAddressValidated, storObj.EmailAddress, storObj.EmailAddressValidated, storObj.StreetAddress, storObj.StreetAddressValidated,
        storObj.ExtendedAddress, storObj.FormattedAddress, storObj.CityLocality, storObj.StateRegion, storObj.Country, storObj.PostalCode, storObj.Telephone,
        storObj.GeocodeType, storObj.GeocodeConfidence, storObj.Latitude, storObj.Longitude);
      if (errCod < 0) { errMsg = $"Error code = {errCod} while writing to database {recordName} record with index {recordIndex}"; }
    }
    else
    {
      if (isNewRecord) { this.DoorsLocationExtns.Add(storObj); }
      errMsg = StoreChanges();
    }
    // refresh the edit object
    editObj = GetEditableLocationExtnByKey(fgroupGuid);
    if (editObj == null) { editObj = new LocationExtnUxm(); }
    // refresh the recordIndex
    recordIndex = editObj.HasIndex;
    // update the status message
    if (string.IsNullOrEmpty(errMsg))
    {
      editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} written to database";
      editObj.NdisDataStored = true;
    }
    else { editObj.NdisElemMsg = errMsg; }
    return editObj;
  }

  public LocationExtnUxm DeleteLocation(LocationExtnUxm editObj, bool byStorProc = true)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord) // delete existing record
    {
      var storObj = GetStorableLocationExtnByKey(fgroupGuid);
      storObj.DeletedByAgentGuid = agentGuid;
      storObj.IsDeleted = NPDSDC.ClientHasAdminMode;  // maps to IsRealDelete input parameter in storproc
      if (byStorProc)
      {
        var errCod = ScribeLocationDelete(
          storObj.DeletedByAgentGuid, storObj.RecordGuid, storObj.FgroupGuid, storObj.IsDeleted);
        if (errCod < 0) { errMsg = $"Error code = {errCod} while deleting {recordName} record with index {recordIndex} from database"; }
      }
      else
      {
        this.DoorsLocationExtns.Attach(storObj);
        this.DoorsLocationExtns.Remove(storObj);
        errMsg = StoreChanges();
      }
      // refresh the edit object
      editObj = GetEditableLocationExtnByKey(fgroupGuid);
      if (editObj == null) { editObj = new LocationExtnUxm(); }
      // update the status message
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} deleted from database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

} // end class

// end file
