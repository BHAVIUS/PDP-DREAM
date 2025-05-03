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
public class AuthModeChangeProfile : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AuthModeChangeProfile);
  public AuthModeChangeProfile(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel(DepAuthModeChangeProfile,
      $"{PDPSS.AppOwnerShortName}: Change Profile");
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

  public IActionResult OnPost()
  {
    if (ModelState.IsValid)
    {
      if (OnlineUserIsAuthenticated)
      {
        var usr = QUDC.GetUserByPrincipal(User);
        var tokenVerified = QebCryptoService.TokenEqualsHash(UXM.PassWord, usr.PasswordHash);
        if (tokenVerified)
        {
          usr.SetChangeProfileModel(UXM);
          var errorCode = QUDC.QebIdentityAppUserUpdateProfile(usr.AppGuidRef, usr.UserGuidKey,
            usr.UserNameDisplayed, usr.FirstName, usr.LastName, usr.Organization, usr.PhoneNumber,
            usr.SecurityAnswer, usr.SecurityQuestion, usr.WebsiteAddress, usr.DateProfileChanged, usr.DateLastEdit);
          if (errorCode < 0)
          { UXM.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else
          { UXM.FormCompleted = true; }

          QURC.QebUserGuid = usr.UserGuidKey;
          QURC.QebUserNameDisplayed = UXM.QebUserNameDisplayed;
        }
        else { ModelState.AddModelError("", "Invalid password."); }
      }
      else { ModelState.AddModelError("", "Invalid user."); }
    }
    else { ModelState.AddModelError("", "Invalid model."); }
    return Page();
  }

} // end class

// end file