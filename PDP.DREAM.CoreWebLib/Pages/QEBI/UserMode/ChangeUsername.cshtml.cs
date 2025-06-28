// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class QebiUserModeChangeUsername : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiUserModeChangeUsername);
  public QebiUserModeChangeUsername() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepqUserModeChangeUsername, "Change Username", true);
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
    PSRM.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm();
    var usr = NPDSCW.QUDC.GetUserByPrincipal(QebiRcp);
    if (QebiRcp.IsAuthenticated)
    {
      usr = NPDSCW.QUDC.GetUserByUserNameAndUserGuid(QebiRcp.UserName, QebiRcp.UserGuid);
      UXM = new ChangeUsernameUxm(QebiRcp.UserGuid);
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeUsernameUxm UXM { get; set; } = new ChangeUsernameUxm();

  public IActionResult OnPost()
  {
    NPDSCW.QUDC.GetUserByPrincipal(QebiRcp);
    if ((ModelState.IsValid) && (QebiRcp.IsAuthenticated))
    {
      UXM.UserGuid = QebiRcp.UserGuid;
      UXM.UserName = QebiRcp.UserName;
      UXM = IQebiUser.ChangeUsernameWithOld(UXM, NPDSCW.QUDC);
      if (UXM.UsernameChanged)
      {
        UXM.FormCompleted = true;
        QebiUserSignoutAsync();
      }
      WraceAddErrors(UXM.FormNote);
    }
    return Page();
  }

} // end class

// end file