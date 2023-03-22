// TkgnPageControllerDoorsSnapshot.cs
// Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageController
{
  private const string eidDoorsSnapshotStatus = "span#DoorsSnapshotStatus";

  public virtual JsonResult OnPostReadDoorsSnapshots([DataSourceRequest] DataSourceRequest dsRequest,
   // string searchFilter, string serviceTag, string serviceType, string entityType, string recordAccess,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadDoorsSnapshots);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag);
    OpenNexusConnection(); // use PNDC
#if DEBUG
    DebugNexusRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      if (recordGuid.IsInvalid())
      { ModelState.AddModelError("DoorsSnapshots", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PNDC.ListViewableDoorsSnapshots(recordGuid, isLimited)
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
    CloseNexusConnection();
    return jsonData;
  }

} // end class

// end file