// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class NexusWebLibIndex : TkgnPageController
{
  private const string rzrClass = nameof(NexusWebLibIndex);
  public NexusWebLibIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepNexusWebLibIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_NwlPdpHelpSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file