// TkgPageControllerDistribution.cs
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageController
{
  private const string eidDistributionStatus = "span#DistributionStatus";

  public virtual JsonResult OnPostReadDistributions([DataSourceRequest] DataSourceRequest dsRequest,
   // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadDistributions);
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
      { ModelState.AddModelError("Distributions", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PSDC.ListEditableDistributions(recordGuid, isLimited)
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

  public virtual JsonResult OnPostWriteDistribution([DataSourceRequest] DataSourceRequest dsRequest,
    DistributionEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (fgr.RRRecordGuid.IsInvalid())
    { ModelState.AddModelError(fgr.ItemXnam, "RRRecordGuid invalid because null or empty."); }
    if (ModelState.IsValid) { fgr = PSDC.EditDistribution(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidDistributionStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteDistribution([DataSourceRequest] DataSourceRequest dsRequest,
    DistributionEditModel fgr, Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    fgr.RRRecordGuid = ParseResRepRecordGuid(fgr.ItemXnam, fgr.RRRecordGuid, recordGuid);
    if (ModelState.IsValid) { fgr = PSDC.DeleteDistribution(fgr); }
    else { fgr.PdpStatusMessage = $"ModelState invalid with {ModelState.ErrorCount} errors."; }
    fgr.PdpStatusElement = eidDistributionStatus;
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostCheckDistribution([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    DistributionEditModel? fgr = PSDC.GetEditableDistributionByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.CheckDistribution(fgr); fgr.PdpStatusElement = eidDistributionStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

  public virtual JsonResult OnPostReseqDistribution([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    OpenScribeConnection(); // use PSDC
    DistributionEditModel? fgr = PSDC.GetEditableDistributionByKey(recordGuid);
    if (fgr?.RRFgroupGuid == recordGuid)
    { fgr = PSDC.ReseqDistribution(fgr); fgr.PdpStatusElement = eidDistributionStatus; }
    DataSourceResult dsResult = (new[] { fgr }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    CloseScribeConnection();
    return jsonData;
  }

} // end class

// end file