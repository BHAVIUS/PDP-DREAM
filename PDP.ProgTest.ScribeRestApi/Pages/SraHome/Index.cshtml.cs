// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class SraHomeIndex : TkgsPageController
{
  private const string rzrClass = nameof(SraHomeIndex);
  public SraHomeIndex() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe,
      DatabaseType = NPDSCD.DatabaseTypeScribe,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel("/SraHome/Index", PdpSitePathKey);
    PSRM.InitRazorPageMenus("_SraHomeSpanPageMenu");
    ResetCoreRepository();
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
#endif
    ResetScribeRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullWrace(rzrHndlr, rzrClass);
    CatchNullCore(rzrHndlr, rzrClass);
    CatchNullScribe(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

} // end class

// end file