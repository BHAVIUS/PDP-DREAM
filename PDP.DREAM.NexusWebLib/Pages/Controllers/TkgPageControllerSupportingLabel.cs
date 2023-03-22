// TkgPageControllerSupportingLabel.cs
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageController
{
  private const string eidSupportingLabelStatus = "span#SupportingLabelStatus";

  public virtual JsonResult OnPostReadSupportingLabels([DataSourceRequest] DataSourceRequest dsRequest,
  // string searchFilter, string serviceTag, string entityType,
   Guid recordGuid, bool isLimited = false)
  {
    var rzrHndlr = nameof(OnPostReadSupportingLabels);
    // QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
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
      { ModelState.AddModelError("SupportingLabels", "RRRecordGuid invalid."); }
      else
      {
        dsResult = PNDC.ListViewableSupportingLabels(recordGuid, isLimited)
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
