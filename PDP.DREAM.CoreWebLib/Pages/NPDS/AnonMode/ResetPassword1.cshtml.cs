// ResetPassword1.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetPassword1 : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetPassword1);
  public AnonModeResetPassword1(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel("/NPDS/AnonMode/ResetPassword1",
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
    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(ct))
    {
      return RedirectToPage("/NPDS/AnonMode/ResetPassword3", new { id = id, ct = ct });
    }
    else
    {
      UXM = new ChangePasswordUxm1();
      return Page();
    }
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
  public ChangePasswordUxm1 UXM { get; set; } = new ChangePasswordUxm1();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.UserName))
    {
      return RedirectToPage("/NPDS/AnonMode/ResetPassword2", new { username = UXM.UserName });
    }
    else { PdpPrcMvcAddErrors("Username not input. "); }
    return Page();
  }

} // end class

// end file