// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AuthModeChangeEmail : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AuthModeChangeEmail);
  public AuthModeChangeEmail(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel(DepAuthModeChangeEmail, $"{PDPSS.AppOwnerShortName}: Change Email");
    PSRM.InitRazorPageMenus("_AuthModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
    var isVerified = CheckCoreUserSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    UXM = new ChangeEmailUxm();
    QUDC.GetUserByPrincipal(User);
    if (OnlineUserIsAuthenticated)
    {
      UXM = new ChangeEmailUxm(QebUserGuid);
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeEmailUxm UXM { get; set; } = new ChangeEmailUxm();

  public IActionResult OnPost()
  {
    QUDC.GetUserByPrincipal(User);
    if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
    {
      UXM.UserGuid = QebUserGuid;
      UXM.UserName = QebUserName;
      UXM = ISiaaUser.ChangeEmailWithOld(UXM, QUDC);
      if (UXM.DbfieldReset)
      {
        UXM = ISiaaUser.NotifyEmailWithToken(UXM, HttpContext);
        if (UXM.NoticeSent) { UXM.FormCompleted = true; }
      }
      PdpPrcMvcAddErrors(UXM.FormMessage);
    }
    return Page();
  }

} // end class

// end file