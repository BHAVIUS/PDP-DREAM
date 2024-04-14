// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeConfirmEmail : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeConfirmEmail);
  public AnonModeConfirmEmail() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepAnonModeConfirmEmail, $"{PDPSS.AppOwnerNameShort}: Confirm Email");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
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
      UXM = IQebiUser.ArgCheckModel(UXM);
      UXM = IQebiUser.ConfirmEmailWithToken(UXM, QUDC);
      if (UXM.TokenConfirmed && UXM.EmailChanged) { UXM.FormCompleted = true; }
      else { ModelState.AddModelError("", "The security token was not validated."); }
    }
    else { ModelState.AddModelError("", "The submitted form is not valid."); }
    return Page();
  }

} // end class

// end file