// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdminRS)]
public class DebugModeServerDefaults : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(DebugModeServerDefaults);
  public DebugModeServerDefaults() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseType = NPDSCD.DatabaseTypeCore,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessUser,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepDebugModeServerDefaults, "PDP-DREAM ServerDefaults", true);
    PSRM.InitRazorPageMenus("_DebugModeSpanPageMenu");
    // TODO: create subtitle as second line for title
    // PSRM.RazorBodyTitle =  $"NPDS ServerDefaults ({nameof(NPDSSD)}) table of object properties.";
    NPDSCW.ResetCoreRepository();
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeCheckNpdsAgent); }
#if DEBUG
    var rzrHndlr = nameof(OnPageHandlerExecuting);
    NPDSCW.DebugClientAccess(rzrHndlr, rzrClass);
#endif
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file