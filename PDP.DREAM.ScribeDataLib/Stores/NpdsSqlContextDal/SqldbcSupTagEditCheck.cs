// SqldbcUilSupTagEditCheck.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual SupportingTagEditModel CheckSupportingTag(SupportingTagEditModel ssEdit)
  {
    var errMsg = string.Empty;
    var agentGuid = QURC.QebAgentGuid;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRRecordGuid, Guid.Empty);
    var ssRecordGuid = PdpGuid.ParseToNonNullable(ssEdit.RRFgroupGuid, Guid.Empty);
    var ssRecordName = ssEdit.ItemXnam;
    var ssRecordIndex = ssEdit.HasIndex;
    var ssRecordPriority = ssEdit.HasPriority;
    var isNewRecord = ssRecordGuid.IsEmpty();
    if (!isNewRecord) // refresh object
    {
      ssEdit = GetEditableSupportingTagByKey(ssRecordGuid);
      if (ssEdit == null) { ssEdit = new SupportingTagEditModel(); }
      if (string.IsNullOrEmpty(errMsg))
      {
        ssEdit.PdpStatusMessage =
          $"{ssRecordName} record with index {ssRecordIndex} checked in database";
      }
      else { ssEdit.PdpStatusMessage = errMsg; }
    }
    return ssEdit;
  }

  public virtual short CheckSupportingTags(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckSupportingTags(ref rrr);
  }
  public virtual short CheckSupportingTags(ref NexusResrepEditModel rrEdit)
  {
    short statusCode = 0;
    var rrRecordGuid = PdpGuid.ParseToNonNullable(rrEdit.RRRecordGuid, Guid.Empty);
    var rrRegistryGuid = PdpGuid.ParseToNonNullable(rrEdit.RecordRegistryGuid, Guid.Empty);
    var ssItems = ListEditableSupportingTags(rrRecordGuid).Select(ss => ss.SupportingTag.ToLower());
    if (ssItems.Any())
    {
      statusCode = CheckSupportingStrings(ssItems, rrRegistryGuid);
    }
    else
    {
      statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
    }
    rrEdit.SupportingTagsStatusCode = statusCode;
    return statusCode;
  }

  public SupportingTagEditModel ReseqSupportingTag(SupportingTagEditModel ssEdit)
  {
    var errMsg = string.Empty;
    var agentGuid = QURC.QebAgentGuid;
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
      var errCod = ScribeSupportingTagReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableSupportingTagByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new SupportingTagEditModel();
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