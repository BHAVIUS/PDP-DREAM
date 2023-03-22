// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize]
public class DebugModeServerDefaults : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(DebugModeServerDefaults);
  public DebugModeServerDefaults() { }

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
    PSRM = new PdpSiteRazorModel(DepDebugModeServerDefaults, $"{DepPdpDream}: ServerDefaults");
    PSRM.InitRazorPageMenus("_DebugModeSpanPageMenu");
    // TODO: create subtitle as second line for title
    // PSRM.RazorBodyTitle =  $"NPDS ServerDefaults ({nameof(NPDSSD)}) table of object properties.";
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