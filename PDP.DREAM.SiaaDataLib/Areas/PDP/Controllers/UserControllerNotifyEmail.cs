using System;
using System.Text;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
  {
    protected ChangeEmailUxm ArgCheckModel(ChangeEmailUxm uxm)
    {
      uxm.TokenConfirmed = false;
      uxm.Message = string.Empty;
      if (string.IsNullOrEmpty(uxm.UserName))
      {
        uxm.ErrorOccurred = true;
        uxm.Message += "Username not submitted. ";
      }
      else if ((uxm.RequireSecTok) && (string.IsNullOrEmpty(uxm.SecurityToken)))
      {
        uxm.ErrorOccurred = true;
        uxm.Message += "Security token not submitted. ";
      }
      else
      {
        uxm.ErrorOccurred = false;
      }
      return uxm;
    }

    protected ChangeEmailUxm NotifyEmailWithToken(ChangeEmailUxm uxm)
    {
      try
      {
        var link = LinkToConfirmToken(uxm.UserName, uxm.SecurityToken, "ConfirmEmail", "Anon", "PDP");
        var subj = PdpSiteSettings.GetValues.AppSiteTitle + " user account for " + uxm.PersonName;
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

        var mail = uxm.NewEmail ?? PdpSiteSettings.GetValues.AppHostEmail;
        var sent = NotifyService.SendEmail(mail, subj, body.ToString());
        uxm.NoticeSent = sent;
        if (!uxm.NoticeSent)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Notification email could not be sent. Please try again later. ";
        }
      }
      catch (Exception error)
      {
        uxm.Error = error;
        uxm.ErrorOccurred = true;
        uxm.Message += "Server error occurred changing email. ";
      }
      return uxm;
    }

  } // class

} // namespace
