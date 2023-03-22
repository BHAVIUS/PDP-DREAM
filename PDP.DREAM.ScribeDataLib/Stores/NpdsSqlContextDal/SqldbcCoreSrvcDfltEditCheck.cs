// SqldbcUilServiceDefaultEditCheck.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
   public virtual ServiceDefaultEditModel CheckServiceDefault(ServiceDefaultEditModel editObj)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord)
    {
      // refresh object
      editObj = GetEditableServiceDefaultByKey(fgroupGuid);
      if (editObj == null) { editObj = new ServiceDefaultEditModel(); }
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

  public virtual short CheckServiceDefaults(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckServiceDefaults(ref rrr);
  }
  public virtual short CheckServiceDefaults(ref NexusResrepEditModel rrr)
  {
    short statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    var items = ListEditableServiceDefaults(recordGuid);
    if (items.Any())
    {
      statusCode = (short)NpdsInfosetStatus.Valid;
    }
    else
    {
      statusCode = (short)NpdsInfosetStatus.None;
    }
    rrr.ServiceDefaultsStatusCode = statusCode;
    return statusCode;
  }

  public ServiceDefaultEditModel ReseqServiceDefault(ServiceDefaultEditModel ssEdit)
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
      // TODO: implement the storproc
      // var errCod = ScribeServiceDefaultReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      // if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableServiceDefaultByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new ServiceDefaultEditModel();
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