// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeIndex : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeIndex);
  public QebiAnonModeIndex() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepqAnonModeIndex, "Anonymous Mode", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file