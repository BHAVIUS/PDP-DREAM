// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdmin)]
public class AdminModeViewQebiRoles : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AdminModeViewQebiRoles);
  public AdminModeViewQebiRoles() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAuthReadOnly,
      RecordAccess = NPDSCD.RecordAccessAdmin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAdminModeViewQebiRoles, $"{PDPSS.AppOwnerNameShort}: View QEBI Roles");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AdminModeSpanPageMenu");
    ResetQebiRepository();
    var isVerified = CheckQebiUserSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public JsonResult OnPostReadQebiRoles([DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult result = QUDC.ListEditableQebiRoles().ToDataSourceResult(request);
    return new JsonResult(result);
  }

} // end class

// end file
