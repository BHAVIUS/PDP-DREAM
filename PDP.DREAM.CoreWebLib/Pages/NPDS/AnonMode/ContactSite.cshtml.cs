// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonModeContactSite : CoreDataRazorPageControllerBase
{
  private const string rzrClass = nameof(AnonModeContactSite);
  public AnonModeContactSite(ILoggerFactory lgrFtry,
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
    PSRM = new PdpSiteRazorModel(DepAnonModeContactSite,
      $"{PDPSS.AppOwnerShortName}: Contact Site");
    PSRM.InitRazorPageMenus("_AnonModeSpanPageMenu");
    ResetQebiRepository();
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string pageTitle = "")
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrClass);
    PSRM.DebugRazorPageStrings();
#endif
    if (!string.IsNullOrWhiteSpace(pageTitle))
    { PSRM.RazorBodyTitle = pageTitle; }
    UXM = new ContactUserUxm(PSRM.RazorBodyTitle);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrClass);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  [BindProperty]
  public ContactUserUxm UXM { get; set; } = new ContactUserUxm();

  public IActionResult OnPost()
  {
    if (!string.IsNullOrWhiteSpace(UXM.FormTitle))
    { PSRM.RazorBodyTitle = UXM.FormTitle; }
    if (ModelState.IsValid)
    {
      var name = UXM.FirstName + " " + UXM.LastName;
      var subj = PdpAppStatus.PDPSS.AppOwnerLongName + " Contact " + name;
      var body = new StringBuilder();
      body.AppendLine("Name: " + name);
      body.AppendLine();
      body.AppendLine("Phone: " + UXM.PhoneNumber);
      body.AppendLine("Email: " + UXM.EmailAddress);
      body.AppendLine("Website: " + UXM.WebsiteAddress);
      body.AppendLine("Organization: " + UXM.Organization);
      body.AppendLine();
      body.AppendLine("Subject: " + UXM.EmailSubject);
      body.AppendLine("Message: " + UXM.EmailBody);
      body.AppendLine();
      var mail = UXM.EmailAddress ?? PdpAppStatus.PDPSS.AppHostEmail;
      UXM.FormCompleted = QebNotifyService.SendEmail(mail, subj, body.ToString());
      if (!UXM.FormCompleted) { PdpPrcMvcAddErrors("Your message could not be sent. Please try again later."); }
    }
    else { PdpPrcMvcAddErrors("Submitted form not valid. "); }
    return Page();
  }

} // end class

// end file