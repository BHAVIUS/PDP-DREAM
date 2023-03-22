// AnonCoreAccessDenied
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeAccessDenied : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeAccessDenied);
  public AnonModeAccessDenied(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    npdsUsrRolRequired = NamesForClientRoles.NpdsAnon;
    base.OnPageHandlerExecuting(exeCntxt);
  }

  public IActionResult OnGet()
  {
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml

  // Other page handlers and properties

} // end class

// end file