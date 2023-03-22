// SqldbcUilDescriptionEditCheck.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual DescriptionEditModel CheckDescription(DescriptionEditModel editObj)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var registryGuid = (Guid)NPDSCP.RegistryGuid; // ???
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord)
    {
      // refresh object
      editObj = GetEditableDescriptionByKey(fgroupGuid);
      if (editObj == null) { editObj = new DescriptionEditModel(); }
      short statusCode = CheckSupportingStrings(editObj.Description, registryGuid); // ???
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

  public virtual short CheckDescriptions(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckDescriptions(ref rrr);
  }
  public virtual short CheckDescriptions(ref NexusResrepEditModel rrr)
  {
    short statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    var items = ListEditableDescriptions(recordGuid).Select(st => st.Description.ToLower());
    if (items.Any())
    {
      statusCode = CheckSupportingStrings(items, registryGuid);
    }
    else
    {
      statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
    }
    rrr.DescriptionsStatusCode = statusCode;
    return statusCode;
  }

  public DescriptionEditModel ReseqDescription(DescriptionEditModel ssEdit)
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
      var errCod = ScribeDescriptionReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableDescriptionByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new DescriptionEditModel();
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

} // end class

// end file