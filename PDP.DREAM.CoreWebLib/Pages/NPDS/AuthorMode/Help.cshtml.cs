// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAuthorRS)]
public class NpdsAuthorModeHelp : TkgcPageController
{
  private const string rzrClass = nameof(NpdsAuthorModeHelp);
  public NpdsAuthorModeHelp() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      ServiceType = NPDSCD.ServiceTypeCore,
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadWrite,
      RecordAccess = NPDSCD.RecordAccessAuthor,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepnAuthorModeHelp, "NPDS Author Help", true);
    PSRM.InitRazorPageMenus("_NpdsAuthorModeSpanPageMenu");
    NPDSCW.ResetQebiRepository(); // for QebiUser
    var isUser = CheckQebiUserSession();
    if (!isUser) { LocalRedirect(DepqAnonModeAccessDenied); }
    NPDSCW.ResetCoreRepository(); // for DevTest and NpdsAgent
    var isAgent = CheckNpdsAgentSession();
    if (!isAgent) { LocalRedirect(DepnAgentModeAddRoleAgent); }
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