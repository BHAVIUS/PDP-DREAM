// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual DescriptionExtnUxm CheckDescription(DescriptionExtnUxm editObj)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var registryGuid = (Guid)NPDSDC.RegistryGuid; // ???
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    if (!fgroupGuid.IsEmpty())
    {
      // refresh object
      editObj = GetEditableDescriptionExtnByKey(fgroupGuid);
      if (editObj == null) { editObj = new DescriptionExtnUxm(); }
      short statusCode = CheckSupportingStrings(editObj.Description, registryGuid); // ???
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} checked in database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

  public virtual byte CheckDescriptionExtns(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckDescriptionExtns(ref rrr);
  }

  public virtual byte CheckDescriptionExtns(ref CoreResrepLeafUxm rrr)
  {
    byte statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    var items = ListEditableDescriptionExtns(recordGuid).Select(st => st.Description.ToLower());
    if (items.Any())
    {
      statusCode = CheckSupportingStrings(items, registryGuid);
    }
    else
    {
      statusCode = NPDSCD.InfosetStatusNone.ECode;
    }
    rrr.DescriptionsStatusCode = statusCode;
    return statusCode;
  }

  public DescriptionExtnUxm ReseqDescription(DescriptionExtnUxm ssEdit)
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
      var errCod = ScribeDescriptionReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableDescriptionExtnByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new DescriptionExtnUxm();
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