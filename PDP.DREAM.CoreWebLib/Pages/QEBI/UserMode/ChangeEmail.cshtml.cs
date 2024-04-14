// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class UserModeChangeEmail : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(UserModeChangeEmail);
  public UserModeChangeEmail() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AuthReadWrite),
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepUserModeChangeEmail, $"{PDPSS.AppOwnerNameShort}: Change Email");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_UserModeSpanPageMenu");
    ResetQebiRepository();
    var isVerified = CheckQebiUserSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    UXM = new ChangeEmailUxm();
    QUDC.GetUserByPrincipal(QebRcp);
    if (QebRcp.IsAuthenticated)
    {
      UXM = new ChangeEmailUxm(QebRcp.UserGuid);
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  [BindProperty]
  public ChangeEmailUxm UXM { get; set; } = new ChangeEmailUxm();

  public IActionResult OnPost()
  {
    QUDC.GetUserByPrincipal(QebRcp);
    if ((ModelState.IsValid) && (QebRcp.IsAuthenticated))
    {
      UXM.UserGuid = QebRcp.UserGuid;
      UXM.UserName = QebRcp.UserName;
      UXM = IQebiUser.ChangeEmailWithOld(UXM, QUDC);
      if (UXM.DbfieldReset)
      {
        UXM = IQebiUser.NotifyEmailWithToken(UXM, HttpContext);
        if (UXM.NoticeSent) { UXM.FormCompleted = true; }
      }
      WraceUxmAddErrors(UXM.FormMessage);
    }
    return Page();
  }

} // end class

// end file