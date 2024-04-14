// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(QebiGuru)]
public class GuruModeEditQebiUsers : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(GuruModeEditQebiUsers);
  public GuruModeEditQebiUsers() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepGuruModeEditQebiUsers, $"{PDPSS.AppOwnerNameShort}: Edit QEBI Users");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_GuruModeSpanPageMenu");
    ResetQebiRepository();
    var isVerified = CheckQebiUserSession();
    if (!isVerified) { RedirectToPage(DepAnonModeAccessDenied); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public JsonResult OnPostReadQebiUsers([DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult dsResult = QUDC.AdminListEditableQebiUsers().ToDataSourceResult(request);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  // TODO: implement store layer directly for QebiUserUzm to avoid conversion to QebiUserUxm
  public JsonResult OnPostWriteQebiUser([DataSourceRequest] DataSourceRequest request, QebiUserUzm editObj)
  {
    // var storObj = new QebiUserUxm((IUserAdminEdit)editObj);
    if (!string.IsNullOrEmpty(editObj.UserName))
    { editObj = QUDC.AdminEditQebiUser(editObj); editObj = QUDC.AdminApproveQebiUser(editObj); }
    DataSourceResult dsResult = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }
  // TODO: implement store layer directly for QebiUserUzm to avoid conversion to QebiUserUxm
  public JsonResult OnPostDeleteQebiUser([DataSourceRequest] DataSourceRequest request, QebiUserUzm editObj)
  {
    // var storObj = new QebiUserUxm((IUserAdminEdit)editObj);
    if (!editObj.UserGuid.IsEmpty()) { editObj = QUDC.AdminDeleteQebiUser(editObj); }
    DataSourceResult dsResult = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file