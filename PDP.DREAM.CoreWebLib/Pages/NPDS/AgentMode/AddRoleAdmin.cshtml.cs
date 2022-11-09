// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreWebLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using System;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAgent)]
public class AgentModeAddRoleAdmin : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AgentModeAddRoleAdmin);
  public AgentModeAddRoleAdmin(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel(DepAgentModeAddRoleAdmin,
       $"{PDPSS.AppOwnerShortName}: Add NPDS Admin Role");
    PSR.InitRazorPageMenus( "_AgentModeSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
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

  public IActionResult OnPost()
  {
    var usrRoles = QUDC.GetAppUserRolesForUserGuid(QebUserGuid);
    var strAgent = NamesForIdentityRoles.NpdsAdmin.ToString();
    Guid? roleGuid = null;
    if (!usrRoles.Contains(strAgent))
    {
      roleGuid = QUDC.GetAppRoleGuidByRoleName(strAgent);
      if (!roleGuid.IsNullOrEmpty())
      {
        var roleAdded = AddRoleUser(roleGuid);
        if (roleAdded) { return RedirectToPage(DepAuthModeLogoutUser); }
      }
    }
    return Page();
  }

} // end class

// end file