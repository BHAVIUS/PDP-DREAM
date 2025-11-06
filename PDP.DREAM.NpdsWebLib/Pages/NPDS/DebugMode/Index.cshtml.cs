// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class DebugModeIndex : AtlasDataRazorPageControllerBase
{
  private const string rzrClass = nameof(DebugModeIndex);
  public DebugModeIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeEnmDflt,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessAdmin,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepDebugModeIndex, "PDP-DREAM DebugMode Index", true);
    PSRM.InitRazorPageMenus("_DebugModeSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for QebiUser
    var isUser = CheckQebiUserClient();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetAtlasRepository(); // for NpdsAgent
    var isAgent = CheckNpdsAgentClient();
    if (!isAgent) { LocalRedirect(DepnAgentModeCheckNpdsAgent); }
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuting), rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnGet), rzrClass);
#endif
    return Page();
  }

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    this.DebugWraceRazorPage(nameof(OnPageHandlerExecuted), rzrClass);
#endif
  }

  // Other page handlers and properties

} // end class

// end file