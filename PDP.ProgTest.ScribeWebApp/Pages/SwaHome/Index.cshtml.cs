// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebApp.Pages;

[RequireHttps, AllowAnonymous]
public class SwaHomeIndex : TkgsPageController
{
  private const string rzrClass = nameof(SwaHomeIndex);
  public SwaHomeIndex(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before the OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      ServiceType = NpdsServiceType.Scribe,
      DatabaseType = NpdsDatabaseType.Scribe,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel("/SwaHome/Index", PdpSitePathKey);
    PSRM.InitRazorPageMenus("_SwaHomeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
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