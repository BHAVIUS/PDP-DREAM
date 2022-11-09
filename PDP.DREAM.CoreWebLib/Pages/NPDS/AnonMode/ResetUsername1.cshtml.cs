// ResetUsername1.cshtml.cs 
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
public class AnonModeResetUsername1 : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonModeResetUsername1);
  public AnonModeResetUsername1(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel("/NPDS/AnonMode/ResetUsername1",
    $"{PDPSS.AppOwnerShortName}: Reset Username");
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
      return RedirectToPage("/NPDS/AnonMode/ResetUsername3", new { id = id, ct = ct });
    }
    else
    {
      UXM = new ChangeUsernameUxm1();
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
  public ChangeUsernameUxm1 UXM { get; set; } = new ChangeUsernameUxm1();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.PassWord))
    {
      return RedirectToPage("ResetUsername2", new { passWord = UXM.PassWord });
    }
    else { PdpPrcMvcAddErrors("Password not input. "); }
    return Page();
  }

} // end class

// end file