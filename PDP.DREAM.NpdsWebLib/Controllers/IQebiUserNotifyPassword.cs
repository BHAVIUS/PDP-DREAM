// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial interface IQebiUser
{
  protected static ChangePasswordUxm ArgCheckModel(ChangePasswordUxm uxm)
  {
    uxm.TokenConfirmed = false;
    uxm.FormNote = ESS;
    if (string.IsNullOrEmpty(uxm.UserName))
    {
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Username not submitted. ";
    }
    else if ((uxm.RequireSecTok) && (string.IsNullOrEmpty(uxm.SecurityToken)))
    {
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Security token not submitted. ";
    }
    else
    {
      uxm.ErrorOccurred = false;
    }
    return uxm;
  }

  protected static ChangePasswordUxm NotifyPasswordWithToken(ChangePasswordUxm uxm, HttpContext cntxt)
  {
    try
    {
      // TODO: eliminate magic strings on route
      var link = LinkToConfirmToken(cntxt.Request, uxm.UserName, uxm.SecurityToken, uxm.ReturnUrlPath);
      var subj = PDPSS.AppSiteDefTitle + " user account for " + uxm.PersonName;
      var body = new StringBuilder();
      body.AppendLine("Name: " + uxm.PersonName);
      body.AppendLine("Username: " + uxm.UserName);
      body.AppendLine();
      body.AppendLine("Within 24 hours, confirm your email address and user account by clicking the following link:");
      body.AppendLine();
      body.AppendLine("   " + link);
      body.AppendLine();
      body.AppendLine("If necessary, please enter your username and this security token:");
      body.AppendLine();
      body.AppendLine("   " + uxm.SecurityToken);
      body.AppendLine();

      var rcpt = uxm.EmailAddress ?? uxm.EmailAlternate; // recipient (toEmailAddress for user)
      uxm.NoticeSent = QebNotifyService.SendEmail(rcpt, subj, body.ToString());
      if (!uxm.NoticeSent)
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "Notification email could not be sent. Please try again later. ";
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred changing password. ";
    }
    return uxm;
  }

} // end interface

// end file