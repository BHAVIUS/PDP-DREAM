// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual OtherTextUxm CheckOtherText(OtherTextUxm ssEdit)
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
      ssEdit = GetEditableOtherTextByKey(ssRecordGuid);
      if (ssEdit == null) { ssEdit = new OtherTextUxm(); }
      if (string.IsNullOrEmpty(errMsg))
      {
        ssEdit.NdisElemMsg =
          $"{ssRecordName} record with index {ssRecordIndex} checked in database";
      }
      else { ssEdit.NdisElemMsg = errMsg; }
    }
    return ssEdit;
  }

  public virtual byte CheckOtherTexts(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckOtherTexts(ref rrr);
  }
  public virtual byte CheckOtherTexts(ref CoreResrepLeafUxm rrEdit)
  {
    byte statusCode = 0;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(rrEdit.RRRecordGuid, EGS);
    var rrRegistryGuid = PdpGuid.ParseToNonNullable(rrEdit.RecordRegistryGuid, EGS);
    var ssItems = ListEditableOtherTexts(rrRecordGuid).Select(ss => ss.OtherText.ToLower());
    if (ssItems.Any())
    {
      statusCode = CheckSupportingStrings(ssItems, rrRegistryGuid);
    }
    else
    {
      statusCode = NPDSCD.InfosetStatusNone.ECode;
    }
    rrEdit.OtherTextsStatusCode = statusCode;
    return statusCode;
  }

  public OtherTextUxm ReseqOtherText(OtherTextUxm ssEdit)
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
      var errCod = ScribeOtherTextReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableOtherTextByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new OtherTextUxm();
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