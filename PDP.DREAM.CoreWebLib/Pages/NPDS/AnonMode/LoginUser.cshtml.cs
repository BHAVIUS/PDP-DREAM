// LoginUser.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

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
public class AnonModeLoginUser : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonModeLoginUser);
  public AnonModeLoginUser(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel(DepAnonModeLoginUser, $"{PDPSS.AppOwnerShortName}: Login User");
    PSR.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string? returnUrl = null) // TODO: null vs ""
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    returnUrl = ArgCheckReturnUrl(returnUrl);
    if (OnlineUserIsAuthenticated)
    {
      return Redirect(returnUrl);
    }
    else
    {
      QebUserSignoutAsync(); // clear authentication cookie
      UXM = new LoginUserUxm()
      {
        ReturnUrl = returnUrl
      };

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
  public LoginUserUxm UXM { get; set; } = new LoginUserUxm();

  public IActionResult OnPost()
  {
    QebUserSignoutAsync(); // clear authentication cookie
    var qebSignin = new QebIdentityResult();
    var qebUsr = new QebIdentityAppUser();

    UXM.FormMessage = string.Empty;
    UXM.ReturnUrl = ArgCheckReturnUrl(UXM.ReturnUrl);
    if (string.IsNullOrEmpty(UXM.UserName))
    {
      UXM.ErrorOccurred = true;
      UXM.FormMessage += "Username not submitted. ";
    }
    else if (string.IsNullOrEmpty(UXM.PassWord))
    {
      UXM.ErrorOccurred = true;
      UXM.FormMessage += "Password not submitted. ";
    }
    else
    {
      UXM.ErrorOccurred = false;
    }

    if (ModelState.IsValid)
    {
      if (!string.IsNullOrWhiteSpace(UXM.UserName))
      {
        qebUsr = this.QUDC.GetUserByUserName(UXM.UserName);
        if ((string.IsNullOrWhiteSpace(qebUsr?.UserName) || (qebUsr?.ConcurrencyStamp == PdpInvalidToken)))
        {
          ModelState.AddModelError(string.Empty, $"UserName '{UXM.UserName}' invalid. User not found.");
        }
      }
      else
      {
        ModelState.AddModelError(string.Empty, $"UserName is null or whitespace. User not found.");
      }

      if ((qebUsr != null) && (qebUsr.ConcurrencyStamp != PdpInvalidToken))
      {
        // Signin without user roles or agent session
        qebSignin = QebUserSignin(UXM.UserName, UXM.PassWord);
      }

      if (qebSignin.Succeeded)
      {
        // create/update PDP Agent Session
        List<string>? qebUsrRoles = QUDC.GetUserRoleNamesByUserGuid(qebUsr.UserGuidKey);
        if (qebUsrRoles.Contains(NamesForIdentityRoles.NpdsAgent.ToString()))
        {
          var agentGuid = (Guid?)QURC.QebAgentGuid;
          var sessionGuid = (Guid?)QURC.QebSessionGuid;
          var errorCode = PCDC.CoreSessionAgentEdit(PDPSS.AppSecureUiaaGuid, qebUsr.UserGuidKey,
            qebUsr.UserNameDisplayed, ref agentGuid, ref sessionGuid);
          if (errorCode == 0)
          {
            QURC.QebAgentGuid = agentGuid ?? Guid.Empty;
            QURC.QebSessionGuid = sessionGuid ?? Guid.Empty;
            errorCode = QUDC.QebIdentityAppUserStamp(PDPSS.AppSecureUiaaGuid, qebUsr.UserGuidKey, QURC.QebSessionGuid);
          }
          if (errorCode == 0)
          {
            // Signin with user roles and agent session
            qebSignin = QebUserSignin(qebUsr.UserName, qebUsr.UserGuidKey, QURC.QebAgentGuid, QURC.QebSessionGuid, qebUsrRoles);
            // uxm.ReturnUrl = AppendKeys(uxm.ReturnUrl, agt.SessionGuidKey, agt.AgentGuidKey, agt.IdentityUserGuidRef);
          }
        }
      }
      if (!qebSignin.Succeeded)
      {
        ModelState.AddModelError(string.Empty, "User login invalid");
      }
    }
    else
    {
      ModelState.AddModelError(string.Empty, "User model invalid");
    }

    if (qebSignin.Succeeded)
    {
      qebLogger.LogInformation(1, "User login succeeded");
      return Redirect(UXM.ReturnUrl);
    }
    else
    {
      qebLogger.LogInformation(2, "User login failed");
      return Page();
    }

  }

} // end class

// end file