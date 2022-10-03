// QebNotifyService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Services;

public static class QebNotifyService
{

  // TODO: consider MailKit https://www.mailkit.com/
  // TODO: consider MailGun https://www.mailgun.com/
  // TODO: consider Postal https://github.com/postalhq/postal

  public static bool SendEmail(string toEmailAddress, string msgSubject, string msgBody)
  {
    bool emailSent;
    if (PdpAppStatus.PDPSS.AppUseSendGrid)
    {
      emailSent = SendgridService.SendEmail(PdpAppStatus.PDPSS.AppOwnerEmail, toEmailAddress, msgSubject, msgBody);
    }
    else
    {
      emailSent = SmtpmailService.SendEmail(PdpAppStatus.PDPSS.AppOwnerEmail, toEmailAddress, msgSubject, msgBody);
    }
    return emailSent;
  }

} // end class

// end file