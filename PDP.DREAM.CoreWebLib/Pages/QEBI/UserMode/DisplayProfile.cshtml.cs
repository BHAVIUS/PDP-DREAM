// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class UserModeDisplayProfile : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(UserModeDisplayProfile);
  public UserModeDisplayProfile() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepUserModeDisplayProfile, $"{PDPSS.AppOwnerNameShort}: Display Profile");
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
    UXM = new ChangeProfileUxm();
    var usr = QUDC.GetUserByPrincipal(QebRcp);
    if (QebRcp.IsAuthenticated)
    {
      // TODO: refactor for single call to database
      usr = QUDC.GetUserByUserNameAndUserGuid(QebRcp.UserName, QebRcp.UserGuid);
      UXM = usr.GetChangeProfileModel();
      UXM.UserRoleNames = QUDC.GetUserRoleNamesByUserGuid(QebRcp.UserGuid).JoinListToOrString();
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeProfileUxm UXM { get; set; } = new ChangeProfileUxm();

} // end class

// end file