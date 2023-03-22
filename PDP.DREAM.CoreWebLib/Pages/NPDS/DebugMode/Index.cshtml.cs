// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class DebugModeIndex : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(DebugModeIndex);
  public DebugModeIndex() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AuthReadOnly,
      RecordAccess = NpdsRecordAccess.AuthUser,
      UserModeClientRequired = true,
      SessionClientRequired = true
    };
    PSRM = new PdpSiteRazorModel(DepCwlHomeIndex, $"{DepPdpDream}: DebugMode Index");
    PSRM.InitRazorPageMenus("_DebugModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
    var isVerified = CheckCoreUserSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file