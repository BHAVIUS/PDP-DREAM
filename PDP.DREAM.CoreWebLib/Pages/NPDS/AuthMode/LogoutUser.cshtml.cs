// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.Extensions.Logging;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps]
public class AuthModeLogoutUser : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AuthModeLogoutUser);
  public AuthModeLogoutUser(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AuthReadOnly,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAuthModeLogoutUser,
       $"{PDPSS.AppOwnerShortName}: Logout User");
    PSRM.InitRazorPageMenus("_AuthModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    if (OnlineUserIsAuthenticated)
    {
      QebUserSignoutAsync(); // clear authentication cookie
      return Redirect(DepQebIdentLogin);
    }
    else
    {
      return Redirect(DepPdpSiteErrors);
    }

  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

} // end class

// end file