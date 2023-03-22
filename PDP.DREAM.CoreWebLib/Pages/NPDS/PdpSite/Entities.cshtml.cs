// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class PdpSiteEntities : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(PdpSiteEntities);
  public PdpSiteEntities() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepPdpSiteEntities, $"{DepPdpDream}: PdpSite Entities");
    PSRM.InitRazorPageMenus("_PdpSiteSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  // OnPostRead after the [RazorPage].cshtml

  public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
  {
    ResetCoreRepository(); // use PCDC
    DataSourceResult result = PCDC.ListViewableEntityTypes().ToDataSourceResult(request);
    return new JsonResult(result);
  }

} // end class

// end file