// LoginUser.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeLoginUser : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeLoginUser);
  public AnonModeLoginUser(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepAnonModeLoginUser, $"{PDPSS.AppOwnerShortName}: Login User");
    PSRM.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string? returnUrl) // TODO: null vs ""
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    returnUrl = ArgCheckReturnUrl(returnUrl);
    if (OnlineUserIsAuthenticated)
    {
      return Redirect(returnUrl);
    }
    else
    {
      QebUserSignoutAsync(); // clear authentication cookie
      UXM = new LoginUserUxm() { ReturnUrl = returnUrl };
      return Page();
    }
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

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
        if (qebUsrRoles.Contains(NamesForClientRoles.NpdsAgent.ToString()))
        {
          var agentGuid = (Guid?)QURC.ClientAgentGuid;
          var sessionGuid = (Guid?)QURC.ClientSessionGuid;
          var errorCode = PCDC.CoreSessionAgentEdit(PDPSS.AppSecureUiaaGuid, qebUsr.UserGuidKey,
            qebUsr.UserNameDisplayed, ref agentGuid, ref sessionGuid);
          if (errorCode == 0)
          {
            QURC.ClientAgentGuid = agentGuid ?? Guid.Empty;
            QURC.ClientSessionGuid = sessionGuid ?? Guid.Empty;
            errorCode = QUDC.QebIdentityAppUserStamp(PDPSS.AppSecureUiaaGuid, qebUsr.UserGuidKey, QURC.ClientSessionGuid);
          }
          if (errorCode == 0)
          {
            // Signin with user roles and agent session
            qebSignin = QebUserSignin(qebUsr.UserName, qebUsr.UserGuidKey, QURC.ClientAgentGuid, QURC.ClientSessionGuid, qebUsrRoles);
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