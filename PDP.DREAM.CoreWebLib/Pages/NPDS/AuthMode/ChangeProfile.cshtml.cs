// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AuthModeChangeProfile : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AuthModeChangeProfile);
  public AuthModeChangeProfile(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAuthModeChangeProfile,
      $"{PDPSS.AppOwnerShortName}: Change Profile");
    PSRM.InitRazorPageMenus("_AuthModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
    var isVerified = CheckCoreUserSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeProfileUxm();
    var usr = QUDC.GetUserByPrincipal(User);
    if (OnlineUserIsAuthenticated)
    {
      usr = QUDC.GetUserByUserNameAndUserGuid(QebUserName, QebUserGuid);
      UXM = usr.GetChangeProfileModel();
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeProfileUxm UXM { get; set; } = new ChangeProfileUxm();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid)
    {
      if (OnlineUserIsAuthenticated)
      {
        var usr = QUDC.GetUserByPrincipal(User);
        var tokenVerified = QebCryptoService.TokenEqualsHash(UXM.PassWord, usr.PasswordHash);
        if (tokenVerified)
        {
          usr.SetChangeProfileModel(UXM);
          var errorCode = QUDC.QebIdentityAppUserUpdateProfile(usr.AppGuidRef, usr.UserGuidKey,
            usr.UserNameDisplayed, usr.FirstName, usr.LastName, usr.Organization, usr.PhoneNumber,
            usr.SecurityAnswer, usr.SecurityQuestion, usr.WebsiteAddress, usr.DateProfileChanged, usr.DateLastEdit);
          if (errorCode < 0)
          { UXM.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else
          { UXM.FormCompleted = true; }

          QURC.ClientUserGuid = usr.UserGuidKey;
          QURC.ClientUserNameDisplayed = UXM.QebUserNameDisplayed;
        }
        else { ModelState.AddModelError("", "Invalid password."); }
      }
      else { ModelState.AddModelError("", "Invalid user."); }
    }
    else { ModelState.AddModelError("", "Invalid model."); }
    return Page();
  }

} // end class

// end file