// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual LocationUxm CheckLocation(LocationUxm editObj)
  {
    editObj.NdisElemMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    if (!fgroupGuid.IsEmpty())
    {
      editObj = EditLocation(editObj); // store update
      if (string.IsNullOrEmpty(editObj.NdisElemMsg)) { editObj.NdisElemMsg = "<span class='pdpStatusInvalid'>Location not validated.</span>"; }
      else { editObj.NdisElemMsg = $"<span class='pdpStatusValid'>Location validated: {editObj.NdisElemMsg}</span>"; }
    }
    return editObj;
  }

  public virtual byte CheckLocations(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckLocations(ref rrr);
  }

  public virtual byte CheckLocations(ref CoreResrepLeafUxm rrr)
  {
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var statusEnum = NPDSCD.InfosetStatusAddressInvalid;
    var locations = ListEditableLocations(recordGuid);
    foreach (LocationUxm loc in locations) { CheckLocation(loc); }
    //locations = ListEditableLocations(recordGuid);
    //foreach (LocationUxm loc in locations)
    //{
    //  }
    //}
    rrr.LocationsStatusCode = statusEnum.ECode;
    return statusEnum.ECode;
  }

  public LocationUxm ReseqLocation(LocationUxm ssEdit)
  {
    var errMsg = ESS;
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
      ssEdit = GetEditableLocationByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new LocationUxm();
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


} // end class

// end file