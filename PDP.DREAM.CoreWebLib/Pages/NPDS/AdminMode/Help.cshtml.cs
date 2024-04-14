// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdmin)]
public class AdminModeHelp : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AdminModeHelp);
  public AdminModeHelp() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AuthReadOnly),
      RecordAccess = NPDSCD.RecordAccessAdmin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAdminModeHelp, $"{PDPSS.AppOwnerNameShort}: Admin Help");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AdminModeSpanPageMenu");
    ResetQebiRepository();
    var isUserVerified = CheckQebiUserSession();
    if (!isUserVerified) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file