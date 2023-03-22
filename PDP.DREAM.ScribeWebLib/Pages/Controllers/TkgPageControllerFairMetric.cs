// TkgPageControllerFairMetric.cs
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidFairMetricStatus = "span#FairMetricStatus";

  public virtual JsonResult OnPostReadFairMetrics([DataSourceRequest] DataSourceRequest dsRequest,
   // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadFairMetrics);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenScribeConnection(); // use PSDC
#if DEBUG
    DebugScribeRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("FairMetrics", "RRRecordGuid invalid."); }
      else
      {
          dsResult = PSDC.ListEditableFairMetrics(recordGuid, isLimited)
          .ToDataSourceResult(dsRequest);
      }
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostWriteFairMetric([DataSourceRequest] DataSourceRequest dsRequest,
    FairMetricEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = PSDC.EditFairMetric(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidFairMetricStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteFairMetric([DataSourceRequest] DataSourceRequest dsRequest,
    FairMetricEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteFairMetric(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidFairMetricStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostCheckFairMetric([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    FairMetricEditModel? fgr = PSDC.GetEditableFairMetricByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.CheckFairMetric(fgr); fgr.PdpStatusElement = eidFairMetricStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostReseqFairMetric([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    FairMetricEditModel? fgr = PSDC.GetEditableFairMetricByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.ReseqFairMetric(fgr); fgr.PdpStatusElement = eidFairMetricStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file