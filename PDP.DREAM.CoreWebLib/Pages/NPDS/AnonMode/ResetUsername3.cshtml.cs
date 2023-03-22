// ResetUsername3.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetUsername3 : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetUsername3);
  public AnonModeResetUsername3(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

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
    PSRM = new PdpSiteRazorModel("/NPDS/AnonMode/ResetUsername3",
      $"{PDPSS.AppOwnerShortName}: Reset Username");
    PSRM.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm3(id, ct);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrClass);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  // Other page handlers and properties

  [BindProperty]
  public ChangeUsernameUxm3 UXM { get; set; } = new ChangeUsernameUxm3();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.PassWord) &&
      !string.IsNullOrWhiteSpace(UXM.SecurityToken) && !string.IsNullOrWhiteSpace(UXM.NewUsername))
    {
      var uxm = ISiaaUser.ChangeUsernameWithToken(UXM.PassWord, UXM.SecurityToken, UXM.NewUsername);
      if (uxm.UsernameChanged) { UXM.FormCompleted = true; }
      if (!string.IsNullOrEmpty(uxm.FormMessage)) { PdpPrcMvcAddErrors(uxm.FormMessage); }
    }
    return Page();
  }

} // end class

// end file