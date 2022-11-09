// ResetUsername2.cshtml.cs 
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
public class AnonModeResetUsername2 : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonModeResetUsername2);
  public AnonModeResetUsername2(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel("/NPDS/AnonMode/ResetUsername2",
    $"{PDPSS.AppOwnerShortName}: Reset Username");
    PSR.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string password)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    UXM = new ChangeUsernameUxm2(password);
    var usr = QUDC.GetUserByPassWord(UXM.PassWord);
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
  public ChangeUsernameUxm2 UXM { get; set; } = new ChangeUsernameUxm2();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid &&
      !string.IsNullOrWhiteSpace(UXM.PassWord) && !string.IsNullOrWhiteSpace(UXM.SecurityAnswer))
    {
      var uxm = ISiaaUser.ResetUsernameWithToken(UXM.PassWord, UXM.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = ISiaaUser.NotifyUsernameWithToken(uxm, HttpContext); // send message
        if (uxm.NoticeSent) { UXM.FormCompleted = true; }
      }
      PdpPrcMvcAddErrors(uxm.FormMessage);
    }
    else { PdpPrcMvcAddErrors("Question not answered"); }
    return Page();
  }

} // end class

// end file