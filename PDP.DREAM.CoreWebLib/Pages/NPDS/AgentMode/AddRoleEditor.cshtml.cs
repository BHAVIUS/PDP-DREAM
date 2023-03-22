// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class AgentModeAddRoleEditor : TkgcPageController
{
  private const string rzrClass = nameof(AgentModeAddRoleEditor);
  public AgentModeAddRoleEditor(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAgentModeAddRoleEditor, $"{PDPSS.AppOwnerShortName}: Add NPDS Editor Role");
    PSRM.InitRazorPageMenus("_AgentModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public IActionResult OnPost()
  {
    var usrRoles = QUDC.GetAppUserRolesForUserGuid(QebUserGuid);
    var strAgent = NamesForClientRoles.NpdsEditor.ToString();
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