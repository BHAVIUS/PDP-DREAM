// AnonControllerContactSite.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Text;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

public partial class AnonNexusController
{
  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public ActionResult ContactSite()
  {
    var uxm = new ContactUserUxm();
    return View(uxm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public ActionResult ContactSite(ContactUserUxm uxm)
  {
    if (ModelState.IsValid)
    {
      var name = uxm.FirstName + " " + uxm.LastName;
      var subj = PdpSiteSettings.Values.AppOwnerName + " Contact " + name;
      var body = new StringBuilder();
      body.AppendLine("Name: " + name);
      body.AppendLine();
      body.AppendLine("Phone: " + uxm.PhoneNumber);
      body.AppendLine("Email: " + uxm.EmailAddress);
      body.AppendLine("Website: " + uxm.WebsiteAddress);
      body.AppendLine("Organization: " + uxm.Organization);
      body.AppendLine();
      body.AppendLine("Subject: " + uxm.EmailSubject);
      body.AppendLine("Message: " + uxm.EmailBody);
      body.AppendLine();
      var mail = uxm.EmailAddress ?? PdpSiteSettings.Values.AppHostEmail;
      var emailSent = NotifyService.SendEmail(mail, subj, body.ToString());
      if (emailSent) { return View("SiteContacted"); }
      else { PdpPrcMvcAddErrors("Your message could not be sent. Please try again later."); }
    }
    else { PdpPrcMvcAddErrors("Submitted form not valid. "); }
    return View(uxm);
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public ActionResult SiteContacted() { return View(); }

} // class
