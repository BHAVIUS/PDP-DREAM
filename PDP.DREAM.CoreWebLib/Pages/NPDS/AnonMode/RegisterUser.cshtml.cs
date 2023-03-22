// RegisterUser.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeRegisterUser : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeRegisterUser);
  public AnonModeRegisterUser(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel(DepAnonModeRegisterUser, $"{PDPSS.AppOwnerShortName}: Register User");
    PSRM.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new RegisterUserUxm();
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
  public RegisterUserUxm UXM { get; set; } = new RegisterUserUxm();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid)
    {
      if (QUDC.CountUsersByUserName(UXM.QebUserName) == 0)
      {
        QUDC.RegisterSiaaUser(UXM);
        var usr = QUDC.GetUserByUserName(UXM.QebUserName); // updated with current SecurityToken
        if (string.Equals(UXM.QebUserName, usr.UserName, StringComparison.OrdinalIgnoreCase) == true) // consistency check on UserName
        {
          UXM.UserRegistered = true;
          var exm = new ChangeEmailUxm(usr.UserName, usr.SecurityToken, usr.FirstName, usr.LastName, usr.EmailAddress);
          exm = ISiaaUser.CheckEmailUxm(exm);
          exm = ISiaaUser.NotifyEmailWithToken(exm, HttpContext);
          if (exm.NoticeSent) { UXM.FormCompleted = true; }
          else { UXM.Message += exm.FormMessage; }
        }
        else
        {
          UXM.UserRegistered = false;
          UXM.Message += "User not registered for requested Username.";
        }
      }
      else
      {
        UXM.UserRegistered = false;
        UXM.Message += "Username already exists. Please try a different one. ";
      }
      PdpPrcMvcAddErrors(UXM.Message);
    }
    return Page();
  }

} // end class

// end file