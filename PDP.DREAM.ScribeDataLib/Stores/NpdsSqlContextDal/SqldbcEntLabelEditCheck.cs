// SqldbcUilEntLabelEditCheck.cs 
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
  public virtual EntityLabelEditModel CheckEntityLabel(EntityLabelEditModel editObj)
  {
    var errMsg = string.Empty;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = QURC.QebAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, Guid.Empty);
    var internalGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, Guid.Empty);
    var isNewRecord = internalGuid.IsEmpty();
    if (!isNewRecord)
    {
      // refresh object
      editObj = GetEditableEntityLabelByKey(internalGuid);
      if (editObj == null) { editObj = new EntityLabelEditModel(); }
      if (string.IsNullOrEmpty(errMsg)) { editObj.PdpStatusMessage = $"{recordName} record with index {recordIndex} checked in database"; }
      else { editObj.PdpStatusMessage = errMsg; }
    }
    return editObj;
  }

  public virtual short CheckEntityLabels(Guid recordGuid)
  {
    var rrr = GetEditableResrepStemByRKey(recordGuid);
    return CheckEntityLabels(ref rrr);
  }
  public virtual short CheckEntityLabels(ref NexusResrepEditModel rrr)
  {
    short statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    var items = ListEditableEntityLabels(recordGuid).Select(st => st.EntityLabel.ToLower());
    if (items.Any())
    {
      statusCode = CheckSupportingStrings(items, registryGuid);
    }
    else
    {
      statusCode = (short)PdpAppConst.NpdsInfosetStatus.None;
    }
    rrr.EntityLabelsStatusCode = statusCode;
    return statusCode;
  }

  public EntityLabelEditModel ReseqEntityLabel(EntityLabelEditModel ssEdit)
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
      var errCod = ScribeEntityLabelReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableEntityLabelByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new EntityLabelEditModel();
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