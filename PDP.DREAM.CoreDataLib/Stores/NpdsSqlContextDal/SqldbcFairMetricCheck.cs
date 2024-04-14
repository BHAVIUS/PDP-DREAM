// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public virtual FairMetricUxm CheckFairMetric(FairMetricUxm editObj)
  {
    var errMsg = ESS;
    var recordName = editObj.ItemXnam;
    var recordIndex = editObj.HasIndex;
    var recordPriority = editObj.HasPriority;
    var agentGuid = NPDSDC.NpdsAgentGuid;
    var recordGuid = PdpGuid.ParseToNonNullable(editObj.RRRecordGuid, EGS);
    var fgroupGuid = PdpGuid.ParseToNonNullable(editObj.RRFgroupGuid, EGS);
    var isNewRecord = fgroupGuid.IsEmpty();
    if (!isNewRecord)
    {
      // refresh object
      editObj = GetEditableFairMetricByKey(fgroupGuid);
      if (editObj == null) { editObj = new FairMetricUxm(); }
      if (string.IsNullOrEmpty(errMsg)) { editObj.NdisElemMsg = $"{recordName} record with index {recordIndex} checked in database"; }
      else { editObj.NdisElemMsg = errMsg; }
    }
    return editObj;
  }

  public virtual byte CheckFairMetrics(Guid recordGuid)
  {
    var rrr = GetEditableResrepLeafByRKey(recordGuid);
    return CheckFairMetrics(ref rrr);
  }
  public virtual byte CheckFairMetrics(ref CoreResrepLeafUxm rrr)
  {
    byte statusCode = 0;
    var recordGuid = (Guid)rrr.RRRecordGuid;
    var registryGuid = (Guid)rrr.RecordRegistryGuid;
    var items = ListEditableFairMetrics(recordGuid).Select(m => new { m.FAIR1Q, m.FAIR2M, m.FAIR3P, m.FAIR4N });
    if (items.Count() > 0)
    {
      statusCode = NPDSCD.InfosetStatusValid.ECode;
    }
    else
    {
      statusCode = NPDSCD.InfosetStatusNone.ECode;
    }
    rrr.FairMetricsStatusCode = statusCode;
    return statusCode;
  }

  public FairMetricUxm ReseqFairMetric(FairMetricUxm ssEdit)
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
      var errCod = ScribeFairMetricReseq(agentGuid, rrInfosetGuid, rrRecordGuid, ref ssRecordPriority);
      if (errCod < 0) { errMsg = $"Error code = {errCod} with record priority = {ssRecordPriority} while resequencing {ssRecordName} record with index {ssRecordIndex}"; }
      // refresh object
      ssEdit = GetEditableFairMetricByKey(ssRecordGuid);
      if (ssEdit == null)
      {
        ssEdit = new FairMetricUxm();
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