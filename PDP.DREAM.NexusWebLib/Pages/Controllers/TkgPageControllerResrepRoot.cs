// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageController
{
  private const string eidResrepStatus = TkndoElemPrfx + "ResrepStatus";

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
   string searchFilter, string serviceTag, string entityType)
  {
    var rzrHndlr = nameof(OnPostReadResrepRoots);
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Nexus, serviceTag, entityType, "", searchFilter);
    OpenNexusConnection(); // use PNDC
#if DEBUG
    DebugNexusRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      IList<CoreResrepRootUxm?> resreps; int numResreps;
      resreps = PNDC.ListEditableResrepRoots(dsRequest, out numResreps);
      dsResult = new DataSourceResult() { Data = resreps, Total = numResreps };
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