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
public class AuthCoreChangeEmail : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AuthCoreChangeEmail);
  public AuthCoreChangeEmail(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsDatabaseType.Core,
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      QebSessionValueIsRequired = true
    };
    PSR = new PdpSiteRazorModel(DepAuthCoreChangeEmail,
      $"{PDPSS.AppOwnerShortName}: Change Email");
    PSR.InitRazorPageMenus("_AuthCoreSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckCoreUserSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  public IActionResult OnGet()
  {
    UXM = new ChangeEmailUxm();
    QUDC.GetUserByPrincipal(User);
    if (OnlineUserIsAuthenticated)
    {
      UXM = new ChangeEmailUxm(QebUserGuid);
    }
    return Page();
  }

  [BindProperty]
  public ChangeEmailUxm UXM { get; set; } = new ChangeEmailUxm();

  public IActionResult OnPost()
  {
    QUDC.GetUserByPrincipal(User);
    if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
    {
      UXM.UserGuid = QebUserGuid;
      UXM.UserName = QebUserName;
      UXM = ISiaaUser.ChangeEmailWithOld(UXM, QUDC);
      if (UXM.DbfieldReset)
      {
        UXM = ISiaaUser.NotifyEmailWithToken(UXM, HttpContext);
        if (UXM.NoticeSent) { UXM.FormCompleted = true; }
      }
      PdpPrcMvcAddErrors(UXM.FormMessage);
    }
    return Page();
  }

} // end class

// end file