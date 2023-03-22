// AnonCoreResetEmail 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetEmail : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetEmail);
  public AnonModeResetEmail(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    npdsUsrRolRequired = NamesForClientRoles.NpdsAnon;
    base.OnPageHandlerExecuting(exeCntxt);
  }


  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    UXM = new ChangeEmailUxm();
    return Page();
  }

  // Other page handlers and properties
  [BindProperty]
  public ChangeEmailUxm UXM { get; set; }

  public IActionResult OnPost()
  {
    if (ModelState.IsValid)
    {
      UXM = ISiaaUser.ChangeEmailWithToken(UXM, QUDC);
      if (UXM.TokenConfirmed && UXM.EmailChanged) { return Redirect("EmailConfirmed"); }
      else { ModelState.AddModelError("", "The security token was not validated."); }
    }
    else { ModelState.AddModelError("", "The submitted form is not valid."); }
    return Page();
  }

} // end class

// end file