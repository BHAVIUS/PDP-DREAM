// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial class TkgcPageController
{

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
   string searchFilter, string serviceTag, string entityType, string resrepFormat)
  {
    var rzrHndlr = nameof(OnPostReadResrepRoots);
    WRACE.ParseNpdsSelectFilter(DdeServiceType.Core, serviceTag, entityType, "", searchFilter, resrepFormat);
    OpenCoreConnection();  // use PCDC
#if DEBUG
    DebugCoreRepo(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      IList<CoreResrepRootUxm?> resreps; int numResreps;
      resreps = PCDC.ListEditableResrepRoots(dsRequest, out numResreps);
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
    CloseCoreConnection();
    return jsonData;
  }


} // end class

// end file