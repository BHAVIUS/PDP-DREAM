// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetPassword1 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetPassword1);
  public AnonModeResetPassword1() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    WRACE = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.ParseDatabaseAccess(DdeDatabaseAccess.AnonReadOnly),
      RecordAccess = NPDSCD.RecordAccessAnon,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel(DepAnonModeResetPassword1, $"{PDPSS.AppOwnerNameShort}: Reset Password");
    PSRM.InitRazorPageMenus("_CoreWebLibSpanPageMenu", "_AnonModeSpanPageMenu");
    ResetQebiRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullWrace(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(ct))
    {
      return RedirectToPage(DepAnonModeResetPassword3, new { id = id, ct = ct });
    }
    else
    {
      UXM = new ChangePasswordUxm1();
      return Page();
    }
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  //  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  //  {
  //#if DEBUG
  //    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrClass);
  //    DebugQurcData(exeCntxt.Result);
  //#endif
  //  }

  // Other page handlers and properties
  [BindProperty]
  public ChangePasswordUxm1 UXM { get; set; } = new ChangePasswordUxm1();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.UserName))
    {
      var usr = QUDC.GetUserByUserName(UXM.UserName);
      if ((usr != null) && (!usr.UserGuid.IsInvalid()))
      {
        return RedirectToPage(DepAnonModeResetPassword2, new { username = UXM.UserName });
      }
      else { WraceUxmAddErrors("Username not found. "); }
    }
    else { WraceUxmAddErrors("Username not input. "); }
    return Page();
  }

} // end class

// end file