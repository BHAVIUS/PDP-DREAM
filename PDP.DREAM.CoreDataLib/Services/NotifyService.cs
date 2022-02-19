// NotifyService.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Services
{
  // static classes cannot implement interfaces
  public static class NotifyService
  {

    // TODO: consider MailKit https://www.mailkit.com/
    // TODO: consider MailGun https://www.mailgun.com/
    // TODO: consider Postal https://github.com/postalhq/postal

    public static bool SendEmail(string toEmailAddress, string msgSubject, string msgBody)
    {
      bool emailSent;
      if (PdpSiteSettings.Values.AppUseSendGrid)
      {
        emailSent = SendgridService.SendEmail(PdpSiteSettings.Values.AppOwnerEmail, toEmailAddress, msgSubject, msgBody);
      }
      else
      {
        emailSent = SmtpmailService.SendEmail(PdpSiteSettings.Values.AppOwnerEmail, toEmailAddress, msgSubject, msgBody);
      }
      return emailSent;
    }

  }

}