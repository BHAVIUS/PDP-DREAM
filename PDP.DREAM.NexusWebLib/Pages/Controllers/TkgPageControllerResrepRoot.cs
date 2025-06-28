// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageController
{
  public JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
   string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    var rzrHndlr = nameof(OnPostReadResrepRoots);
    NPDSCW.ParseNpdsSelectFilter(DdeServiceType.Nexus, serviceTag, entityType, "", searchFilter, resrepFormat);
    NPDSCW.OpenNexusConnection(); // use PNDC
#if DEBUG
    NPDSCW.DebugNexusRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      IList<CoreResrepRootUxm> resreps; int numResreps;
      resreps = NPDSCW.PNDC.ListEditableResrepRoots(dsRequest, out numResreps);
      dsResult = new DataSourceResult() { Data = resreps, Total = numResreps };
#if DEBUG
      Debug.WriteLine($"Resreps Page Count = {resreps.Count}, Total Count = {numResreps}.");
#endif
    }
    catch (SqlException exc)
    {
#if DEBUG
      Debug.WriteLine(ParseSqlException(exc));
#endif
    }
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    NPDSCW.CloseNexusConnection();
    return jsonData;
  }

} // end class

// end file