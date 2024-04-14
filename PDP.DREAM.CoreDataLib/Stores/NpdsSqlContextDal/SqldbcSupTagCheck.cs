// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual SupportingTagUxm CheckSupportingTag(SupportingTagUxm ssEdit)
  {
    var errMsg = ESS;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRRecordGuid, EGS);
    var ssRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRFgroupGuid, EGS);
    var ssRecordName = ssEdit.ItemXnam;
    var ssRecordIndex = ssEdit.HasIndex;
    var ssRecordPriority = ssEdit.HasPriority;
    var isNewRecord = ssRecordGuid.IsEmpty();
    if (!isNewRecord) // refresh object
    {
      ssEdit = GetEditableSupportingTagByKey(ssRecordGuid);
      if (ssEdit == null) { ssEdit = new SupportingTagUxm(); }
      if (string.IsNullOrEmpty(errMsg))
      {
        ssEdit.NdisElemMsg =
          $"{ssRecordName} record with index {ssRecordIndex} checked in database";
      }
      else { ssEdit.NdisElemMsg = errMsg; }
    }
    return ssEdit;
  }

  public virtual byte CheckSupportingTags(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckSupportingTags(ref rrr);
  }
  public virtual byte CheckSupportingTags(ref CoreResrepLeafUxm rrEdit)
  {
    byte statusCode = 0;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(rrEdit.RRRecordGuid, EGS);
    var rrRegistryGuid = PdpGuid.ParseToNonNullable(rrEdit.RecordRegistryGuid, EGS);
    var ssItems = ListEditableSupportingTags(rrRecordGuid).Select(ss => ss.SupportingTag.ToLower());
    if (ssItems.Any())
    {
      statusCode = CheckSupportingStrings(ssItems, rrRegistryGuid);
    }
    else
    {
      statusCode = NPDSCD.InfosetStatusNone.ECode;
    }
    rrEdit.SupportingTagsStatusCode = statusCode;
    return statusCode;
  }

  public SupportingTagUxm ReseqSupportingTag(SupportingTagUxm ssEdit)
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
      var errCod = ScribeSupportingTagReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableSupportingTagByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new SupportingTagUxm();
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