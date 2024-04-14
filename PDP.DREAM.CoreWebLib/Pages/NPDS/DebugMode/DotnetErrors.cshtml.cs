// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class DebugModeDotnetErrors : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(DebugModeDotnetErrors);
  public DebugModeDotnetErrors() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepDebugModeDotnetErrors, $"{DepPdpDream}: DotnetErrors");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_DebugModeSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    UXM = new DotnetErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public DotnetErrorModel UXM { get; set; }

} // end class

// end file