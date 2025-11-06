// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, Authorize]
public class QebiUserModeChangePassword : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiUserModeChangePassword);
  public QebiUserModeChangePassword() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepqUserModeChangePassword, "Change Password", true);
    PSRM.InitRazorPageMenus("_QebiUserModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm();
    var usr = NPDSCW.QebiDR.GetUserByPrincipal(QebiRcp);
    if (QebiRcp.IsAuthenticated)
    {
      usr = NPDSCW.QebiDR.GetUserByUserNameAndUserGuid(QebiRcp.UserName, QebiRcp.UserGuid);
      UXM = new ChangePasswordUxm(QebiRcp.UserGuid);
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangePasswordUxm UXM { get; set; } = new ChangePasswordUxm();

  public IActionResult OnPost()
  {
    NPDSCW.QebiDR.GetUserByPrincipal(QebiRcp);
    if ((ModelState.IsValid) && (QebiRcp.IsAuthenticated))
    {
      UXM.UserGuid = QebiRcp.UserGuid;
      UXM.UserName = QebiRcp.UserName;
      UXM = Controllers.IQebiUser.ChangePasswordWithOld(UXM, NPDSCW.QebiDR);
      if (UXM.PasswordChanged)
      {
        UXM.FormCompleted = true;
      }
      WraceAddErrors(UXM.FormNote);
    }
    return Page();
  }

} // end class

// end file