// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps]
public class UserModeLogoutUser : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(UserModeLogoutUser);
  public UserModeLogoutUser() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepUserModeLogoutUser, $"{PDPSS.AppOwnerNameShort}: Logout User");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_UserModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    if (QebRcp.IsAuthenticated)
    {
      QebUserSignoutAsync(); // clear authentication cookie
    }
    return Redirect(DepAnonModeLoginUser);
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

} // end class

// end file