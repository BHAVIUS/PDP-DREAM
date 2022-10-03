// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreWebLib.Controllers;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, AllowAnonymous]
public class AnonCoreContactSite : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AnonCoreContactSite);
  public AnonCoreContactSite(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext.Request)
    {
      DatabaseType = NpdsDatabaseType.Core,
      DatabaseAccess = NpdsDatabaseAccess.AnonReadOnly,
      RecordAccess = NpdsRecordAccess.AnonUser,
      UserModeClientRequired = false,
      QebSessionValueIsRequired = false
    };
    PSR = new PdpSiteRazorModel(DepAnonCoreContactSite,
      $"{PDPSS.AppOwnerShortName}: Contact Site");
    PSR.InitRazorPageMenus("_AnonCoreSpanPageMenu");
    ResetCoreRepository();
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet(string pageTitle = "")
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    if (!string.IsNullOrWhiteSpace(pageTitle))
    { PSR.RazorBodyTitle = pageTitle; }
    UXM = new ContactUserUxm(PSR.RazorBodyTitle);
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrCntrllr);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  [BindProperty]
  public ContactUserUxm UXM { get; set; } = new ContactUserUxm();

  public IActionResult OnPost()
  {
    if (!string.IsNullOrWhiteSpace(UXM.FormTitle))
    { PSR.RazorBodyTitle = UXM.FormTitle; }
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