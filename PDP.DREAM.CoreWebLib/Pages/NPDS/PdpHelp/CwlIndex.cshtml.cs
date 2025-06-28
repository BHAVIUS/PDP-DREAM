// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class CoreWebLibIndex : TkgcPageController
{
  private const string rzrClass = nameof(CoreWebLibIndex);
  public CoreWebLibIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepCoreWebLibIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_CwlPdpHelpSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file