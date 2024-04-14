// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeRegisterUser : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeRegisterUser);
  public AnonModeRegisterUser() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AnonReadOnly),
      RecordAccess = NPDSCD.RecordAccessAnon,
      // no session on initial anon login/register
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepAnonModeRegisterUser, $"{PDPSS.AppOwnerNameShort}: Register User");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
      QebUserSignoutAsync(); // clear authentication cookie
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
      if (QUDC.CountUsersByUserName(UXM.UserName) == 0)
      {
        QUDC.RegisterQebiUser(UXM);
        var usr = QUDC.GetUserByUserName(UXM.UserName); // updated with current SecurityToken
        if (string.Equals(UXM.UserName, usr.UserName, StringComparison.OrdinalIgnoreCase) == true) // consistency check on UserName
        {
          UXM.UserRegistered = true;
          var exm = new ChangeEmailUxm(usr.UserName, usr.SecurityToken, usr.FirstName, usr.LastName, usr.EmailAddress);
          exm = IQebiUser.ArgCheckModel(exm);
          exm = IQebiUser.NotifyEmailWithToken(exm, HttpContext);
          if (exm.NoticeSent) { UXM.FormCompleted = true; }
          else { UXM.FormMessage += exm.FormMessage; }
        }
        else
        {
          UXM.UserRegistered = false;
          UXM.FormMessage += "User not registered for requested Username.";
        }
      }
      else
      {
        UXM.UserRegistered = false;
        UXM.FormMessage += "Username already exists. Please try a different one. ";
      }
      WraceUxmAddErrors(UXM.FormMessage);
    }
    return Page();
  }

} // end class

// end file