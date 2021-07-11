// AnonControllerContactSite.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Text;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AnonController
  {
    [HttpGet]
    public ActionResult SiteContacted() { return View(); }

    [HttpGet]
    public ActionResult ContactSite()
    {
      var uxm = new ContactUserUxm();
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult ContactSite(ContactUserUxm uxm)
    {
      if (ModelState.IsValid)
      {
        var name = uxm.FirstName + " " + uxm.LastName;
        var subj = PdpSiteSettings.GetValues.AppOwnerName + " Contact " + name;
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
        var mail = uxm.EmailAddress ?? PdpSiteSettings.GetValues.AppHostEmail;
        var emailSent = NotifyService.SendEmail(mail, subj, body.ToString());
        if (emailSent) { return View("SiteContacted"); }
        else { PdpPrcMvcAddErrors("Your message could not be sent. Please try again later."); }
      }
      else { PdpPrcMvcAddErrors("Submitted form not valid. "); }
      return View(uxm);
    }

  }

}
