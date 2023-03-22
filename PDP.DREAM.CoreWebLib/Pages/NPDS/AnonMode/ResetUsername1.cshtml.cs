// ResetUsername1.cshtml.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeResetUsername1 : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeResetUsername1);
  public AnonModeResetUsername1(ILoggerFactory lgrFtry,
    IEmailSender emlSndr, ISmsSender smsSndr)
    : base(lgrFtry, emlSndr, smsSndr) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebiUserRestContext(exeCntxt.HttpContext)
    {
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      SessionClientRequired = false
    };
    PSRM = new PdpSiteRazorModel("/NPDS/AnonMode/ResetUsername1",
      $"{PDPSS.AppOwnerShortName}: Reset Username");
    PSRM.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string id, string ct)
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(ct))
    {
      return RedirectToPage("/NPDS/AnonMode/ResetUsername3", new { id = id, ct = ct });
    }
    else
    {
      UXM = new ChangeUsernameUxm1();
      return Page();
    }
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrClass);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  // Other page handlers and properties
  [BindProperty]
  public ChangeUsernameUxm1 UXM { get; set; } = new ChangeUsernameUxm1();

  public IActionResult OnPost()
  {
    if (ModelState.IsValid && !string.IsNullOrWhiteSpace(UXM.PassWord))
    {
      return RedirectToPage("ResetUsername2", new { passWord = UXM.PassWord });
    }
    else { PdpPrcMvcAddErrors("Password not input. "); }
    return Page();
  }

} // end class

// end file