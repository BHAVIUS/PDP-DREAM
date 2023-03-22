// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAgent)]
public class SwlHomeAgentHelp : ScribeDataRazorPageControllerBase
{
  private const string rzrClass = nameof(SwlHomeAgentHelp);
  public SwlHomeAgentHelp() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    PSRM = new PdpSiteRazorModel("/NPDS/SwlHome/AgentHelp", PdpSitePathKey);
    PSRM.InitRazorPageMenus("_SwlHomeSpanPageMenu");
    ResetScribeRepository(true); // demo the selectlists
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string serviceType, string serviceTag, string entityType)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

} // end class

// end file