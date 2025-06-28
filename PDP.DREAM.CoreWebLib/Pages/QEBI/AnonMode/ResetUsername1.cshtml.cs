// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeResetUsername1 : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeResetUsername1);
  public QebiAnonModeResetUsername1() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    NPDSCW = new NpdsClientWrace(exeCntxt.HttpContext)
    {
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon,
    };
    PSRM = new PdpSiteRazorModel(DepqAnonModeResetUsername1, "Reset Username", true);
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
      return RedirectToPage(DepqAnonModeResetUsername3, new { id = id, ct = ct });
    }
    else
    {
      UXM = new ChangeUsernameUxm1();
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
  public ChangeUsernameUxm1 UXM { get; set; } = new ChangeUsernameUxm1();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.PassWord))
    {
      return RedirectToPage(DepqAnonModeResetUsername2, new { password = UXM.PassWord });
    }
    else { WraceAddErrors("Password not input. "); }
    return Page();
  }

} // end class

// end file