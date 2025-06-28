// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class PdpInfoEntities : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(PdpInfoEntities);
  public PdpInfoEntities() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepPdpInfoEntities, "PDP Info NDPS Entities");
    PSRM.InitRazorPageMenus("_PdpInfoSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  // OnPostRead after the [RazorPage].cshtml

  public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
  {
    NPDSCW.ResetCoreRepository(); // use PCDC
    DataSourceResult result = NPDSCW.PCDC.ListViewableEntityTypes().ToDataSourceResult(request);
    return new JsonResult(result);
  }

} // end class

// end file