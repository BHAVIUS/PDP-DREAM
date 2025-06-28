// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class QebiUserModeChangeProfile : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiUserModeChangeProfile);
  public QebiUserModeChangeProfile() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseType = NPDSCD.DatabaseTypeQEBI,
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AuthReadWrite),
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepqUserModeChangeProfile, "Change Profile", true);
    PSRM.InitRazorPageMenus("_QebiUserModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
#endif
    UXM = new ChangeProfileUxm();
    var usr = NPDSCW.QUDC.GetUserByPrincipal(QebiRcp);
    if (QebiRcp.IsAuthenticated)
    {
      usr = NPDSCW.QUDC.GetUserByUserNameAndUserGuid(QebiRcp.UserName, QebiRcp.UserGuid);
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
      if (QebiRcp.IsAuthenticated)
      {
        var usr = NPDSCW.QUDC.GetUserByPrincipal(QebiRcp);
        var tokenVerified = QebCryptoService.TokenEqualsHash(UXM.PassWord, usr.PasswordHash);
        if (tokenVerified)
        {
          usr.SetChangeProfileModel(UXM);
          var errorCode = NPDSCW.QUDC.QebiUserUpdateProfile(usr.AppGuid, usr.UserGuid,
            usr.UserAlias, usr.FirstName, usr.LastName,
            usr.Organization, usr.PhoneNumber, usr.PhoneAlternate,
            usr.SecurityQuestion, usr.SecurityAnswer, usr.WebsiteAddress,
            PDPSS.AppUseSecureAlias, usr.DateProfileChanged, usr.DateLastEdit);
          if (errorCode < 0)
          { UXM.FormNote += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else
          { UXM.FormCompleted = true; }

          NPDSCW.CiaamUserGuid = usr.UserGuid;
          NPDSCW.CiaamUserAlias = UXM.UserAlias;
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