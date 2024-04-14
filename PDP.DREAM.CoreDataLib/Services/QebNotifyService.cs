// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Services;

public static class QebNotifyService
{
  // wrapper for current active mail service
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