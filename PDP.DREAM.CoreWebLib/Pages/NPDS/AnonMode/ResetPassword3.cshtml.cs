// ResetPassword3.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetPassword3 : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetPassword3);
  public AnonModeResetPassword3(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel("/NPDS/AnonMode/ResetPassword3",
    $"{PDPSS.AppOwnerShortName}: Reset Password");
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
    UXM = new ChangePasswordUxm3(id, ct);
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
  public ChangePasswordUxm3 UXM { get; set; } = new ChangePasswordUxm3();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.UserName) &&
      !string.IsNullOrWhiteSpace(UXM.SecurityToken) && !string.IsNullOrWhiteSpace(UXM.NewPassword))
    {
      var uxm = ISiaaUser.ChangePasswordWithToken(UXM.UserName, UXM.SecurityToken, UXM.NewPassword);
      if (uxm.PasswordChanged) { UXM.FormCompleted = true; }
      if (!string.IsNullOrEmpty(uxm.FormMessage)) { PdpPrcMvcAddErrors(uxm.FormMessage); }
    }
    return Page();
  }

} // end class

// end file