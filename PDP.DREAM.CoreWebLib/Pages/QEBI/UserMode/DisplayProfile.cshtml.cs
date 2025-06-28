// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class QebiUserModeDisplayProfile : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiUserModeDisplayProfile);
  public QebiUserModeDisplayProfile() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepqUserModeDisplayProfile, "Display Profile", true);
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
    UXM = new ChangeProfileUxm();
    var usr = NPDSCW.QUDC.GetUserByPrincipal(QebiRcp);
    if (QebiRcp.IsAuthenticated)
    {
      // TODO: refactor for single call to database
      usr = NPDSCW.QUDC.GetUserByUserNameAndUserGuid(QebiRcp.UserName, QebiRcp.UserGuid);
      UXM = usr.GetChangeProfileModel();
      UXM.UserRoleNames = NPDSCW.QUDC.GetUserRoleNamesByUserGuid(QebiRcp.UserGuid).JoinListToOrString();
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeProfileUxm UXM { get; set; } = new ChangeProfileUxm();

} // end class

// end file