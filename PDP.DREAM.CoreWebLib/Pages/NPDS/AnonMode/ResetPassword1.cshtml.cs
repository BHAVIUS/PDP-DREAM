// ResetPassword1.cshtml.cs 
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
public class AnonModeResetPassword1 : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonModeResetPassword1);
  public AnonModeResetPassword1(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel("/NPDS/AnonMode/ResetPassword1",
    $"{PDPSS.AppOwnerShortName}: Reset Password");
    PSR.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(ct))
    {
      return RedirectToPage("/NPDS/AnonMode/ResetPassword3", new { id = id, ct = ct });
    }
    else
    {
      UXM = new ChangePasswordUxm1();
      return Page();
    }
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
  public ChangePasswordUxm1 UXM { get; set; } = new ChangePasswordUxm1();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.UserName))
    {
      return RedirectToPage("/NPDS/AnonMode/ResetPassword2", new { username = UXM.UserName });
    }
    else { PdpPrcMvcAddErrors("Username not input. "); }
    return Page();
  }

} // end class

// end file