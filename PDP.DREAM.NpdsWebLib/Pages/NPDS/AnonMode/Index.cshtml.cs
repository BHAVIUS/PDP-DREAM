// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class NpdsAnonModeIndex : AtlasTkgPageController
{
  private const string rzrClass = nameof(NpdsAnonModeIndex);
  public NpdsAnonModeIndex() : base () { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepnAnonModeIndex, "Anonymous Mode", true);
    PSRM.InitRazorPageMenus("_NpdsAnonModeSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file