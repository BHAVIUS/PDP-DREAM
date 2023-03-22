// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AuthModeChangeUsername : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AuthModeChangeUsername);
  public AuthModeChangeUsername(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel(DepAuthModeChangeUsername,
       $"{PDPSS.AppOwnerShortName}: Change Username");
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
    UXM = new ChangeUsernameUxm();
    var usr = QUDC.GetUserByPrincipal(User);
    if (OnlineUserIsAuthenticated)
    {
      usr = QUDC.GetUserByUserNameAndUserGuid(QebUserName, QebUserGuid);
      UXM = new ChangeUsernameUxm(QebUserGuid);
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeUsernameUxm UXM { get; set; } = new ChangeUsernameUxm();

  public IActionResult OnPost()
  {
    QUDC.GetUserByPrincipal(User);
    if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
    {
      UXM.UserGuid = QebUserGuid;
      UXM.UserName = QebUserName;
      UXM = ISiaaUser.ChangeUsernameWithOld(UXM, QUDC);
      if (UXM.UsernameChanged)
      {
        UXM.FormCompleted = true;
        QebUserSignout();
      }
      PdpPrcMvcAddErrors(UXM.FormMessage);
    }
    return Page();
  }

} // end class

// end file