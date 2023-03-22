// TkgPageControllerResrepRootStemLeaf.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageController
{
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest dsRequest,
   string searchFilter, string serviceTag, string entityType)
  {
    var rzrHndlr = nameof(OnPostReadResrepRoots);
    QURC.ParseNpdsResrepFilter(searchFilter, serviceTag, entityType);
    OpenNexusConnection(); // use PNDC
#if DEBUG
    DebugNexusRepo(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
#endif
    DataSourceResult? dsResult = null;
    try
    {
      IList<NexusResrepViewModel?> resreps; int numResreps;
      resreps = PNDC.ListViewableResrepRoots(dsRequest, out numResreps);
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