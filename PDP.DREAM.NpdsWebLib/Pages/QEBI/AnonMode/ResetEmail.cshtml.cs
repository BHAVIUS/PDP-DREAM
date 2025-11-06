// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeResetEmail : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeResetEmail);
  public QebiAnonModeResetEmail() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
  }


  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    UXM = new ChangeEmailUxm();
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties
  [BindProperty]
  public ChangeEmailUxm UXM { get; set; }

  public IActionResult OnPost()
  {
    if (ModelState.IsValid)
    {
      UXM = Controllers.IQebiUser.ConfirmEmailWithToken(UXM, NPDSCW.QebiDR);
      if (UXM.TokenConfirmed && UXM.EmailChanged) { return Redirect("EmailConfirmed"); }
      else { ModelState.AddModelError("", "The security token was not validated."); }
    }
    else { ModelState.AddModelError("", "The submitted form is not valid."); }
    return Page();
  }

} // end class

// end file