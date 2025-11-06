// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeResetPassword1 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeResetPassword1);
  public QebiAnonModeResetPassword1() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeResetPassword1, "Reset Password", true);
    PSRM.InitRazorPageMenus("_QebiAnonModeSpanPageMenu");
    NPDSCW.ResetQebiRepository();
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
      return RedirectToPage(DepqAnonModeResetPassword3, new { id = id, ct = ct });
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
      var usr = NPDSCW.QebiDR.GetUserByUserName(UXM.UserName);
      if ((usr != null) && (!usr.UserGuid.IsInvalid()))
      {
        return RedirectToPage(DepqAnonModeResetPassword2, new { username = UXM.UserName });
      }
      else { WraceAddErrors("Username not found. "); }
    }
    else { WraceAddErrors("Username not input. "); }
    return Page();
  }

} // end class

// end file