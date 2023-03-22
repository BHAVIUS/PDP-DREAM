// ConfirmEmail.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeConfirmEmail : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeConfirmEmail);
  public AnonModeConfirmEmail(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel(DepAnonModeConfirmEmail,
      $"{PDPSS.AppOwnerShortName}: Confirm Email");
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
    UXM = new ChangeEmailUxm(id, ct);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeEmailUxm UXM { get; set; } = new ChangeEmailUxm();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid)
    {
      UXM = ISiaaUser.CheckEmailUxm(UXM);
      UXM = ISiaaUser.ChangeEmailWithToken(UXM, QUDC);
      if (UXM.TokenConfirmed && UXM.EmailChanged) { UXM.FormCompleted = true; }
      else { ModelState.AddModelError("", "The security token was not validated."); }
    }
    else { ModelState.AddModelError("", "The submitted form is not valid."); }
    return Page();
  }

} // end class

// end file