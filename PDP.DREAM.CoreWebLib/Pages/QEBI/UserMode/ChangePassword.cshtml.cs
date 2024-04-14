// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class UserModeChangePassword : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(UserModeChangePassword);
  public UserModeChangePassword() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepUserModeChangePassword, $"{PDPSS.AppOwnerNameShort}: Change Password");
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
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm();
    var usr = QUDC.GetUserByPrincipal(QebRcp);
    if (QebRcp.IsAuthenticated)
    {
      usr = QUDC.GetUserByUserNameAndUserGuid(QebRcp.UserName, QebRcp.UserGuid);
      UXM = new ChangePasswordUxm(QebRcp.UserGuid);
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangePasswordUxm UXM { get; set; } = new ChangePasswordUxm();

  public IActionResult OnPost()
  {
    QUDC.GetUserByPrincipal(QebRcp);
    if ((ModelState.IsValid) && (QebRcp.IsAuthenticated))
    {
      UXM.UserGuid = QebRcp.UserGuid;
      UXM.UserName = QebRcp.UserName;
      UXM = IQebiUser.ChangePasswordWithOld(UXM, QUDC);
      if (UXM.PasswordChanged)
      {
        UXM.FormCompleted = true;
      }
      WraceUxmAddErrors(UXM.FormMessage);
    }
    return Page();
  }

} // end class

// end file