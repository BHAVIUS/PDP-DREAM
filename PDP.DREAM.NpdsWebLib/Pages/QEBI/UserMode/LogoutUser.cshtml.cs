// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps]
public class QebiUserModeLogoutUser : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiUserModeLogoutUser);
  public QebiUserModeLogoutUser() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepqUserModeLogoutUser, "Logout User", true);
    PSRM.InitRazorPageMenus("_QebiUserModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
    if (QebiRcp.IsAuthenticated)
    {
      QebiUserSignoutAsync(); // clear authentication cookie
    }
    return Redirect(DepqAnonModeLoginUser);
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

} // end class

// end file