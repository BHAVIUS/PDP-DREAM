// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebApp.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdmin)]
public class SwaHomeDebugDemo : TkgsPageController
{
  private const string rzrClass = nameof(SwaHomeDebugDemo);
  public SwaHomeDebugDemo(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before the OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      ServiceType = NpdsServiceType.Scribe,
      DatabaseType = NpdsDatabaseType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.AuthUser,
      AgentModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel("/SwaHome/DebugDemo", PdpSitePathKey);
    PSRM.InitRazorPageMenus("_SwaHomeSpanPageMenu", "_ScribeWebLibDebugPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
    ResetScribeRepository();
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before the OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    var rzrHndlr = nameof(OnGet);
    CatchNullQurc(rzrHndlr, rzrClass);
    CatchNullCore(rzrHndlr, rzrClass);
    QURC.DebugClientAccess(rzrHndlr, rzrClass);
    QURC.DebugNpdsParams(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuted);
    CatchNullQurc(rzrHndlr, rzrClass);
    CatchNullCore(rzrHndlr, rzrClass);
    PSRM.DebugRazorPageStrings(rzrHndlr, rzrClass);
    DebugQurcData(exeCntxt.Result);
#endif
  }

} // end class

// end file