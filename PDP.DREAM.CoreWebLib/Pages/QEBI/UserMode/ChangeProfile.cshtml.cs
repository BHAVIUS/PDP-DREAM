// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class UserModeChangeProfile : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(UserModeChangeProfile);
  public UserModeChangeProfile() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseType = NPDSCD.DatabaseTypeQEBI,
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AuthReadWrite),
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepUserModeChangeProfile, $"{PDPSS.AppOwnerNameShort}: Change Profile");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_UserModeSpanPageMenu");
    ResetQebiRepository();
    var isVerified = CheckQebiUserSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
#endif
    UXM = new ChangeProfileUxm();
    var usr = QUDC.GetUserByPrincipal(QebRcp);
    if (QebRcp.IsAuthenticated)
    {
      usr = QUDC.GetUserByUserNameAndUserGuid(QebRcp.UserName, QebRcp.UserGuid);
      UXM = usr.GetChangeProfileModel();
    }
#if DEBUG
    PSRM.DebugRazorPageStrings();
#endif
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
      if (QebRcp.IsAuthenticated)
      {
        var usr = QUDC.GetUserByPrincipal(QebRcp);
        var tokenVerified = QebCryptoService.TokenEqualsHash(UXM.PassWord, usr.PasswordHash);
        if (tokenVerified)
        {
          usr.SetChangeProfileModel(UXM);
          var errorCode = QUDC.QebiUserUpdateProfile(usr.AppGuid, usr.UserGuid,
            usr.UserAlias, usr.FirstName, usr.LastName, 
            usr.Organization, usr.PhoneNumber, usr.PhoneAlternate,
            usr.SecurityQuestion, usr.SecurityAnswer, usr.WebsiteAddress,
            PDPSS.AppUseSecureAlias, usr.DateProfileChanged, usr.DateLastEdit);
          if (errorCode < 0)
          { UXM.FormMessage += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else
          { UXM.FormCompleted = true; }

          WRACE.QebiUserGuid = usr.UserGuid;
          WRACE.CiaamUserAlias = UXM.UserAlias;
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