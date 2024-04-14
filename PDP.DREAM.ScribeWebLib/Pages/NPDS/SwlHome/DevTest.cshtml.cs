// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved.
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdmin)]
public class SwlHomeDevTest : TkgsPageController
{
  private const string rzrClass = nameof(SwlHomeDevTest);
  public SwlHomeDevTest() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeScribe,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAdmin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepSwlHomeDevTest, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_ScribeWebLibSpanPageMenu", "_ScribeWebLibDebugPageMenu");
    //ResetQebiRepository(); // for QebiUser
    //var isUserVerified = CheckQebiUserSession();
    //if (!isUserVerified) { RedirectToPage(DepAnonModeAccessDenied); }
    ResetCoreRepository(); // for DevTest and NpdsAgent
    var isAgentVerified = CheckNpdsAgentSession();
    if (!isAgentVerified) { RedirectToPage(DepAgentModeCheckNpdsAgent); }
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullWrace(rzrHndlr, rzrClass);
    CatchNullCore(rzrHndlr, rzrClass);
    WRACE.DebugClientAccess(rzrHndlr, rzrClass);
    WRACE.DebugNpdsSelectFilter(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuted);
    CatchNullWrace(rzrHndlr, rzrClass);
    CatchNullCore(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
    DebugWraceData(rzrHndlr, rzrClass);
#endif
  }

  // Other page handlers and properties

} // end class

// end file