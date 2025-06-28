// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModePrivacy : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModePrivacy);
  public QebiAnonModePrivacy() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    // do not include optional params in pageName
    PSRM = new PdpSiteRazorModel(DepqAnonModePrivacy, "Privacy", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file