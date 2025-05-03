// ResetPassword2.cshtml.cs 
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
public class AnonModeResetPassword2 : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonModeResetPassword2);
  public AnonModeResetPassword2(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseType = NpdsDatabaseType.Core,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      QebSessionValueIsRequired = false
    };
    PSR = new PdpSiteRazorModel("/NPDS/AnonMode/ResetPassword2",
    $"{PDPSS.AppOwnerShortName}: Reset Password");
    PSR.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string username)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    UXM = new ChangePasswordUxm2(username);
    var usr = QUDC.GetUserByUserName(UXM.UserName);
    UXM.SecurityQuestion = usr.SecurityQuestion;
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
  public ChangePasswordUxm2 UXM { get; set; } = new ChangePasswordUxm2();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid &&
      !string.IsNullOrWhiteSpace(UXM.UserName) && !string.IsNullOrWhiteSpace(UXM.SecurityAnswer))
    {
      var uxm = ISiaaUser.ResetPasswordWithToken(UXM.UserName, UXM.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = ISiaaUser.NotifyPasswordWithToken(uxm, HttpContext); // send message
        if (uxm.NoticeSent) { UXM.FormCompleted = true; }
      }
      PdpPrcMvcAddErrors(uxm.FormMessage);
    }
    else { PdpPrcMvcAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file