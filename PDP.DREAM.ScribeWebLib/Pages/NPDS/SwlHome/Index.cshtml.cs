// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAgent, NpdsAuthor, NpdsEditor, NpdsAdmin)]
public class SwlHomeIndex : TkgsPageController
{
  private const string rzrClass = nameof(SwlHomeIndex);
  public SwlHomeIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepSwlHomeIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_SwlHomeSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file