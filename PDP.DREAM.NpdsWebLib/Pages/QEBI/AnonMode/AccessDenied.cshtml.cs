// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class QebiAnonModeAccessDenied : QebiDataRazorPageControllerBase
{
  private const string rzrClass = nameof(QebiAnonModeAccessDenied);
  public QebiAnonModeAccessDenied() : base() { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
  }

  public IActionResult OnGet()
  {
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file