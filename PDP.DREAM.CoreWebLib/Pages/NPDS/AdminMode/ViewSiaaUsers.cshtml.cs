// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAdmin)]
public class AdminModeViewSiaaUsers : TkgcPageController
{
  private const string rzrClass = nameof(AdminModeViewSiaaUsers);
  public AdminModeViewSiaaUsers(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel(DepAdminModeViewSiaaUsers, $"{PDPSS.AppOwnerShortName}: View SIAA Users");
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
    DataSourceResult result = QUDC.ListEditableAppUsers().ToDataSourceResult(request);
    return new JsonResult(result);
  }

} // end class

// end file
