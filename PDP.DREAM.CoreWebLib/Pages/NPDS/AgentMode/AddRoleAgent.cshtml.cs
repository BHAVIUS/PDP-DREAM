// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AgentModeAddRoleAgent : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AgentModeAddRoleAgent);
  public AgentModeAddRoleAgent() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepAgentModeAddRoleAgent, $"{PDPSS.AppOwnerNameShort}: Add NPDS Agent Role");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AgentModeSpanPageMenu");
    ResetQebiRepository();
    var isValid = CheckQebiUserSession();
    if (!isValid) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public IActionResult OnPost()
  {
    var roleName = NpdsAgent;
    var roleAdded = AddQebiRoleByRoleName(roleName);
    if (roleAdded) { return RedirectToPage(DepUserModeLogoutUser); }
    else { return RedirectToPage(DepAgentModeIndex); }
  }

} // end class

// end file