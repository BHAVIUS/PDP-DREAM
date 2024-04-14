// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeLoginUser : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeLoginUser);
  public AnonModeLoginUser() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AnonReadOnly),
      RecordAccess = NPDSCD.RecordAccessAnon,
      // no session on initial anon login/register
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepAnonModeLoginUser, $"{PDPSS.AppOwnerNameShort}: Login User");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository(); // create session in QEBI
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string? returnUrl) // TODO: null vs ""
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    returnUrl = ArgCheckReturnUrl(returnUrl);
    if (QebRcp.IsAuthenticated)
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
    var qbiUsr = new QebiUser();

    UXM.FormMessage = ESS;
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
        qbiUsr = this.QUDC.GetUserByUserName(UXM.UserName);
        if ((string.IsNullOrWhiteSpace(qbiUsr?.UserName) || (qbiUsr?.ConcurrencyStamp == PdpInvalidToken)))
        {
          ModelState.AddModelError(ESS, $"UserName '{UXM.UserName}' invalid. User not found.");
        }
      }
      else
      {
        ModelState.AddModelError(ESS, $"UserName is null or whitespace. User not found.");
      }

      if ((qbiUsr != null) && (qbiUsr.ConcurrencyStamp != PdpInvalidToken))
      {
        // signin without user roles or agent session
        qebSignin = QebUserSignin(UXM.UserName, UXM.PassWord);
      }

      if (qebSignin.Succeeded)
      {
        var qebiSession = false;
        var npdsSession = false;
        // update WRACE and QEBI repository
        // TODO: create a utility method for this mapping from QEBI user to WRACE client
        // does WRACE have access to QEBI versus QEBI have access to WRACE ?
        WRACE.CiaamUserAlias = qbiUsr.UserAlias;
        WRACE.CiaamUserEmail = qbiUsr.EmailAddress;
        WRACE.CiaamUserName = qbiUsr.UserName;
        WRACE.QebiUserGuid = qbiUsr.UserGuid;
        // TODO: deprecate use of Session in QUDC and QebiUsr
        // WRACE.CiaamSessionGuid = qbiUsr.SessionGuid;
        // create placeholders in QebiUser for PersonGuid and AgentGuid
        // WRACE.BridgePersonGuid = qbiUsr.BridgePersonGuid;
        // WRACE.NpdsAgentGuid = qbiUsr.NpdsAgentGuid;
        // must ref the field, not the property
        qebiSession = QUDC.EditSessionQebiUser(ref wrace);
        qebiSession = QUDC.CheckSessionQebiUser(ref wrace);
        // TODO: integrate QebiUserstamp into EditSessionQebiUser
        // TODO: check where QebiUserStamp and QebiUserSessionTimestamp are used
        var errorCode = QUDC.QebiUserStamp(PDPSS.CiaamAppGuid, WRACE.QebiUserGuid);
        // TODO: disentangle the QEBI, NPDS, and ACMS roles using the bits RoleIsNpds, RoleIsAcms
        List<string>? qebUsrRoles = QUDC.GetUserRoleNamesByUserGuid(WRACE.QebiUserGuid);
        if ((qebiSession) && (qebUsrRoles != null)) // Signin with user roles and user session
        {
          // create/update NPDS Agent Session
          if (qebUsrRoles.Contains(NpdsAgent))
          {
            if (qebUsrRoles.Contains(NpdsAuthor))
            { WRACE.ClientIsAuthor = true; }
            else { WRACE.ClientIsAuthor = false; }
            if (qebUsrRoles.Contains(NpdsReviewer))
            { WRACE.ClientIsReviewer = true; }
            else { WRACE.ClientIsReviewer = false; }
            if (qebUsrRoles.Contains(NpdsEditor))
            { WRACE.ClientIsEditor = true; }
            else { WRACE.ClientIsEditor = false; }
            if (qebUsrRoles.Contains(NpdsAdmin))
            { WRACE.ClientIsAdmin = true; }
            else { WRACE.ClientIsAdmin = false; }
            var tkgc = new TkgcPageController();
            tkgc.ResetCoreRepository(); // create session in Core
            tkgc.OpenCoreConnection();
            // must ref the field, not the property
            npdsSession = tkgc.PCDC.EditSessionNpdsAgent(ref wrace);
            npdsSession = tkgc.PCDC.CheckSessionNpdsAgent(ref wrace);
            tkgc.CloseCoreConnection();
            // TODO: create analogous sessions in Nexus, Scribe
            // when those repositories are independent of Core
            // ie, when mapped to different databases
          }
          // update QEBI User Signin session with roles
          // calls QebiExtensions.CreateUserPrincipal, iterates over list of claims 
          // by adding new Claim(ClaimTypes.Role, userRole) for each userRole
          qebSignin = QebUserSignin(WRACE.CiaamUserAlias, WRACE.CiaamUserEmail, WRACE.CiaamUserName,
            WRACE.QebiUserGuid, qebUsrRoles);
        }
#if DEBUG
        WRACE.DebugClientAccess("OnPost", "AnonModeLoginUser");
#endif
      }
      if (!qebSignin.Succeeded)
      {
        ModelState.AddModelError(ESS, "User login invalid");
      }
    }
    else
    {
      ModelState.AddModelError(ESS, "User model invalid");
    }
    if (qebSignin.Succeeded)
    {
      return Redirect(UXM.ReturnUrl);
    }
    else
    {
      return Page();
    }

  } // end method

} // end class

// end file