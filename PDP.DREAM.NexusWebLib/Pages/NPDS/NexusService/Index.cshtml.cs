// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class NexusServiceIndex : TkgnPageController
{
  private const string rzrClass = nameof(NexusServiceIndex);
  public NexusServiceIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
      AuthenticatedClientRequired = false,
      SessionClientRequired = false
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepNexusServiceIndex, PdpSitePathKey);
    PSRM.InitRazorPageMenus("_NexusWebLibSpanPageMenu", "_NexusServiceSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted after [RazorPage].cshtml but before result

  // Other page handlers and properties

} // end class

// end file