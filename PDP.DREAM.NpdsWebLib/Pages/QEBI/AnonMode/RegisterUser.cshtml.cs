// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeRegisterUser : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeRegisterUser);
  public QebiAnonModeRegisterUser() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      // no session on initial anon login/register
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeRegisterUser, "Register User", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    QebiUserSignoutAsync(); // clear authentication cookie
    UXM = new RegisterUserUxm();
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public RegisterUserUxm UXM { get; set; } = new RegisterUserUxm();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid)
    {
      if (NPDSCW.QebiDR.CountUsersByUserName(UXM.UserName) == 0)
      {
        var usrNam1 = UXM.UserName;
        UXM.UserRoleNames = PDPSS.AppCiaamRolesInit;
        UXM = NPDSCW.QebiDR.RegisterQebiUser(UXM);
        var usrNam2 = UXM.UserName;
        if (string.Equals(usrNam1, usrNam2, StringComparison.OrdinalIgnoreCase) == true) // consistency check on UserName
        {
          UXM.UserRegistered = true;
          var exm = new ChangeEmailUxm(UXM.UserName, UXM.SecurityToken, UXM.FirstName, UXM.LastName, UXM.EmailAddress);
          exm = Controllers.IQebiUser.ArgCheckModel(exm);
          exm = Controllers.IQebiUser.NotifyEmailWithToken(exm, HttpContext);
          if (exm.NoticeSent) { UXM.FormCompleted = true; }
          else { UXM.FormNote += exm.FormNote; }
        }
        else
        {
          UXM.UserRegistered = false;
          UXM.FormNote += "User not registered for requested Username.";
        }
      }
      else
      {
        UXM.UserRegistered = false;
        UXM.FormNote += "Username already exists. Please try a different one. ";
      }
      WraceAddErrors(UXM.FormNote);
    }
    return Page();
  }

} // end class

// end file