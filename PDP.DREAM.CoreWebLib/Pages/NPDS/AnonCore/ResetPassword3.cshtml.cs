// ResetPassword3.cshtml.cs 
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

[RequireHttps, AllowAnonymous]
public class AnonCoreResetPassword3 : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonCoreResetPassword3);
  public AnonCoreResetPassword3(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsDatabaseType.Core,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      QebSessionValueIsRequired = false
    };
    PSR = new PdpSiteRazorModel("/NPDS/AnonCore/ResetPassword3",
    $"{PDPSS.AppOwnerShortName}: Reset Password");
    PSR.InitRazorPageMenus("_AnonCoreSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm3(id, ct);
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
  public ChangePasswordUxm3 UXM { get; set; } = new ChangePasswordUxm3();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.UserName) &&
      !string.IsNullOrWhiteSpace(UXM.SecurityToken) && !string.IsNullOrWhiteSpace(UXM.NewPassword))
    {
      var uxm = ISiaaUser.ChangePasswordWithToken(UXM.UserName, UXM.SecurityToken, UXM.NewPassword);
      if (uxm.PasswordChanged) { UXM.FormCompleted = true; }
      if (!string.IsNullOrEmpty(uxm.FormMessage)) { PdpPrcMvcAddErrors(uxm.FormMessage); }
    }
    return Page();
  }

} // end class

// end file