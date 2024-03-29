﻿// SqldbcUilOtherTextEditCheck.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual OtherTextEditModel CheckOtherText(OtherTextEditModel ssEdit)
  {
    var errMsg = string.Empty;
    var agentGuid = NPDSCP.ClientAgentGuid;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRRecordGuid, Guid.Empty);
    var ssRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRFgroupGuid, Guid.Empty);
    var ssRecordName = ssEdit.ItemXnam;
    var ssRecordIndex = ssEdit.HasIndex;
    var ssRecordPriority = ssEdit.HasPriority;
    var isNewRecord = ssRecordGuid.IsEmpty();
    if (!isNewRecord) // refresh object
    {
      ssEdit = GetEditableOtherTextByKey(ssRecordGuid);
      if (ssEdit == null) { ssEdit = new OtherTextEditModel(); }
      if (string.IsNullOrEmpty(errMsg))
      {
        ssEdit.PdpStatusMessage =
          $"{ssRecordName} record with index {ssRecordIndex} checked in database";
      }
      else { ssEdit.PdpStatusMessage = errMsg; }
    }
    return ssEdit;
  }

  public virtual short CheckOtherTexts(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckOtherTexts(ref rrr);
  }
  public virtual short CheckOtherTexts(ref NexusResrepEditModel rrEdit)
  {
    short statusCode = 0;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(rrEdit.RRRecordGuid, Guid.Empty);
    var rrRegistryGuid = PdpGuid.ParseToNonNullable(rrEdit.RecordRegistryGuid, Guid.Empty);
    var ssItems = ListEditableOtherTexts(rrRecordGuid).Select(ss => ss.OtherText.ToLower());
    if (ssItems.Any())
    {
      statusCode = CheckSupportingStrings(ssItems, rrRegistryGuid);
    }
    else
    {
      statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
    }
    rrEdit.OtherTextsStatusCode = statusCode;
    return statusCode;
  }

  public OtherTextEditModel ReseqOtherText(OtherTextEditModel ssEdit)
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
      var errCod = ScribeOtherTextReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableOtherTextByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new OtherTextEditModel();
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