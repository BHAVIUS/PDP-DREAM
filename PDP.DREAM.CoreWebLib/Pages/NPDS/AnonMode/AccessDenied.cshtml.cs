// AnonCoreAccessDenied
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreWebLib.Controllers;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using Microsoft.AspNetCore.Authorization;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeAccessDenied : CoreDataRazorPageControllerBase
{
  public AnonModeAccessDenied(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    npdsUsrRolRequired = NamesForIdentityRoles.NpdsAnon;
    base.OnPageHandlerExecuting(exeCntxt);
  }

  public IActionResult OnGet()
  {
    return Page();
  }

} // end class

// end file