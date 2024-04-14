// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AgentModeAddRoleEditor : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AgentModeAddRoleEditor);
  public AgentModeAddRoleEditor() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepAgentModeAddRoleEditor, $"{PDPSS.AppOwnerNameShort}: Add NPDS Editor Role");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AgentModeSpanPageMenu");
    ResetQebiRepository();
    var isVerified = CheckQebiUserSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public IActionResult OnPost()
  {
    var rol1Name = NpdsAgent;
    var rol1Added = AddQebiRoleByRoleName(rol1Name);
    var rol2Name = NpdsEditor;
    var rol2Added = AddQebiRoleByRoleName(rol2Name);
    if (rol1Added || rol2Added) { return RedirectToPage(DepUserModeLogoutUser); }
    else { return RedirectToPage(DepAgentModeIndex); }
  }

} // end class

// end file