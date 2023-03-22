// AgentExamples.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class CwlHomeAgentHelp : TkgcPageController
{
  private const string rzrClass = nameof(CwlHomeAgentHelp);
  public CwlHomeAgentHelp()  { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    PSRM = new PdpSiteRazorModel("/NPDS/CwlHome/AgentHelp", PdpSitePathKey);
    PSRM.InitRazorPageMenus("_CwlHomeSpanPageMenu");
    ResetCoreRepository();
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