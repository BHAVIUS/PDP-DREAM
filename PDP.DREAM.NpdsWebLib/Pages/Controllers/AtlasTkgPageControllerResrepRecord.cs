// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial class AtlasTkgPageController
{
  public JsonResult OnPostReadResrepRecords([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest dsRequest,
    string serviceType, string serviceTag, string entityType, string resrepFormat)
  {
    var rzrHndlr = nameof(OnPostReadResrepRecords);
    NPDSCW.ParseNpdsSelectFilter(serviceType, serviceTag, entityType, resrepFormat);
    NPDSCW.OpenAtlasConnection(); // use PCDC
#if DEBUG
    NPDSCW.DebugAtlasRepo(rzrHndlr, rzrClass);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
    NPDSCW.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
#endif
    Kendo.Mvc.UI.DataSourceResult? dsResult = null;
    try
    {
      IList<AtlasResrepRecordUxm>? resreps; int numResreps;
      //  resreps = NPDSCW.NexusDR.AtlasEditableResrepRecords(15, 1, out numResreps);
      resreps = NPDSCW.AtlasDR.ListEditableResrepRecords(dsRequest, out numResreps);
      dsResult = new Kendo.Mvc.UI.DataSourceResult() { Data = resreps, Total = numResreps };
#if DEBUG
      Debug.WriteLine($"Method '{rzrHndlr}' in Class '{rzrClass}'");
      Debug.WriteLine($"DSRequest PageSize Value = {dsRequest.PageSize}, Page Value = {dsRequest.Page}.");
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
    NPDSCW.CloseAtlasConnection();
    return jsonData;
  }

} // end class

// end file
