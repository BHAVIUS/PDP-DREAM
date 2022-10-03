// RegisterUser.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

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
public class AnonCoreRegisterUser : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonCoreRegisterUser);
  public AnonCoreRegisterUser(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
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
    PSR = new PdpSiteRazorModel(DepAnonCoreRegisterUser, $"{PDPSS.AppOwnerShortName}: Register User");
    PSR.InitRazorPageMenus("_AnonCoreSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    UXM = new RegisterUserUxm();
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
  public RegisterUserUxm UXM { get; set; } = new RegisterUserUxm();

  public IActionResult OnPost()
  {
    UXM.FormCompleted = false;
    if (ModelState.IsValid)
    {
      if (QUDC.CountUsersByUserName(UXM.QebUserName) == 0)
      {
        QUDC.RegisterSiaaUser(UXM);
        var usr = QUDC.GetUserByUserName(UXM.QebUserName); // updated with current SecurityToken
        if (string.Equals(UXM.QebUserName, usr.UserName, StringComparison.OrdinalIgnoreCase) == true) // consistency check on UserName
        {
          UXM.UserRegistered = true;
          var exm = new ChangeEmailUxm(usr.UserName, usr.SecurityToken, usr.FirstName, usr.LastName, usr.EmailAddress);
          exm = ISiaaUser.CheckEmailUxm(exm);
          exm = ISiaaUser.NotifyEmailWithToken(exm, HttpContext);
          if (exm.NoticeSent) { UXM.FormCompleted = true; }
          else { UXM.Message += exm.FormMessage; }
        }
        else
        {
          UXM.UserRegistered = false;
          UXM.Message += "User not registered for requested Username.";
        }
      }
      else
      {
        UXM.UserRegistered = false;
        UXM.Message += "Username already exists. Please try a different one. ";
      }
      PdpPrcMvcAddErrors(UXM.Message);
    }
    return Page();
  }

} // end class

// end file