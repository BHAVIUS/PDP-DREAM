// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAdmin)]
public class AdminModeEditSiaaUsers : TkgcPageController
{
  private const string rzrClass = nameof(AdminModeEditSiaaUsers);
  public AdminModeEditSiaaUsers(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Admin,
      AdminModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepAdminModeEditSiaaUsers, $"{PDPSS.AppOwnerShortName}: Edit SIAA Users");
    PSRM.InitRazorPageMenus("_AdminModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

  public JsonResult OnPostReadSiaaUsers([DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult dsResult = QUDC.ListEditableAppUsers().ToDataSourceResult(request);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public JsonResult OnPostWriteSiaaUser([DataSourceRequest] DataSourceRequest request, QebiUserUxm editObj)
  {
    if (!editObj.UserGuid.IsEmpty()) { editObj = QUDC.EditSiaaUser(editObj); editObj = QUDC.ApproveSiaaUser(editObj); }
    DataSourceResult dsResult = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public JsonResult OnPostDeleteSiaaUser([DataSourceRequest] DataSourceRequest request, QebiUserUxm editObj)
  {
    if (!editObj.UserGuid.IsEmpty()) { editObj = QUDC.DeleteSiaaUser(editObj); }
    DataSourceResult dsResult = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file