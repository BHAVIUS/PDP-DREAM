// AnonCoreResetEmail 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreWebLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps]
public class AnonModeResetEmail : CoreDataRazorPageControllerBase
{
  public AnonModeResetEmail(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    npdsUsrRolRequired = NamesForIdentityRoles.NpdsAnon;
    base.OnPageHandlerExecuting(exeCntxt);
  }


  public IActionResult OnGet()
  {
    UXM = new ChangeEmailUxm();
    return Page();
  }

  [BindProperty]
  public ChangeEmailUxm UXM { get; set; }

  public IActionResult OnPost()
  {
    if (ModelState.IsValid)
    {
      UXM = ISiaaUser.ChangeEmailWithToken(UXM, QUDC);
      if (UXM.TokenConfirmed && UXM.EmailChanged) { return Redirect("EmailConfirmed"); }
      else { ModelState.AddModelError("", "The security token was not validated."); }
    }
    else { ModelState.AddModelError("", "The submitted form is not valid."); }
    return Page();
  }

} // end class

// end file