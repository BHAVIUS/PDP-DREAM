// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreWebLib.Controllers;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AuthModeDisplayProfile : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AuthModeDisplayProfile);
  public AuthModeDisplayProfile(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Core,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      QebSessionValueIsRequired = true
    };
    PSR = new PdpSiteRazorModel(DepAuthModeDisplayProfile,
       $"{PDPSS.AppOwnerShortName}: Display Profile");
    PSR.InitRazorPageMenus("_AuthModeSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckCoreUserSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    UXM = new ChangeProfileUxm();
    var usr = QUDC.GetUserByPrincipal(User);
    if (OnlineUserIsAuthenticated)
    {
      usr = QUDC.GetUserByUserNameAndUserGuid(QebUserName, QebUserGuid);
      UXM = usr.GetChangeProfileModel();
    }
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrCntrllr);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  [BindProperty]
  public ChangeProfileUxm UXM { get; set; } = new ChangeProfileUxm();

} // end class

// end file