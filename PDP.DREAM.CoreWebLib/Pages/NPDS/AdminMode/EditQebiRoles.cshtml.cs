// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(NpdsAdmin)]
public class AdminModeEditQebiRoles : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AdminModeEditQebiRoles);
  public AdminModeEditQebiRoles() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AuthReadWrite),
      RecordAccess = NPDSCD.RecordAccessAdmin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAdminModeEditQebiRoles, $"{PDPSS.AppOwnerNameShort}: Edit QEBI Roles");
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

  public JsonResult OnPostWriteQebiRole([DataSourceRequest] DataSourceRequest request, QebiAppRoleUxm editObj)
  {
    if (!string.IsNullOrEmpty(editObj.RoleName)) { editObj = QUDC.EditQebiAppRole(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    return new JsonResult(result);
  }

  public JsonResult OnPostDeleteQebiRole([DataSourceRequest] DataSourceRequest request, QebiAppRoleUxm editObj)
  {
    if (!editObj.RoleGuid.IsEmpty()) { editObj = QUDC.DeleteQebiAppRole(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    return new JsonResult(result);
  }

} // end class

// end file