// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, PdpAuthorizeRoles(QebiGuru)]
public class GuruModeViewQebiUsers : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(GuruModeViewQebiUsers);
  public GuruModeViewQebiUsers() : base() { }

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
    PSRM = new PdpSiteRazorModel(DepGuruModeViewQebiUsers, $"{PDPSS.AppOwnerNameShort}: View QEBI Users");
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

} // end class

// end file
