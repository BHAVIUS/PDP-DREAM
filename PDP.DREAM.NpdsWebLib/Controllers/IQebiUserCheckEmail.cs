// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial interface IQebiUser
{
  protected static ContactSiteUxm ContactSupportViaUserEmail(ContactSiteUxm uxm, QebiDbsqlContext qudc)
  {
    try
    {
      var usr = qudc.GetUserByUserName(uxm.UserName);
      if ((usr == null) || (usr.UserGuid == EGS))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "User not found. ";
      }
      else
      {
        uxm.ConcatNames(usr.FirstName, usr.LastName);
        var emlSubject = ESS; var emlBody = ESS;
        if (string.IsNullOrEmpty(uxm.EmailSubject)) { emlSubject = PDPSS.AppSiteDefTitle; }
        else { emlSubject = uxm.EmailSubject; }
        if (string.IsNullOrEmpty(uxm.EmailBody))
        {
          var body = new StringBuilder();
          body.AppendLine("Name: " + uxm.PersonName);
          body.AppendLine("Username: " + uxm.UserName);
          body.AppendLine();
          emlBody = body.ToString();
        }
        else
        {
          emlBody = uxm.EmailBody;
        }

        // recipient (toEmailAddress for user)
        var emlRecpt = usr.EmailAddress ?? PDPSS.AppOwnerEmail;
        // TODO: is bcc working now ?
        uxm.NoticeSent = QebNotifyService.SendEmail(emlRecpt, emlSubject, emlBody);
        if (uxm.NoticeSent)
        {
          uxm.FormCompleted = true;
        }
        else
        {
          uxm.ErrorOccurred = true;
          uxm.FormNote += "Email could not be sent. Please contact support via alternate email.";
        }
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred checking email. ";
    }
    return uxm;
  }



} // end interface

// end file