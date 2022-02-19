// UserControllerNotifyUsername.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;

namespace PDP.DREAM.NexusDataLib.Controllers;

public partial class UserNexusController
{
  protected ChangeUsernameUxm ArgCheckModel(ChangeUsernameUxm uxm)
  {
    uxm.TokenConfirmed = false;
    uxm.Message = string.Empty;
    if (string.IsNullOrEmpty(uxm.PassWord))
    {
      uxm.ErrorOccurred = true;
      uxm.Message += "Password not submitted. ";
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

  protected ChangeUsernameUxm NotifyUsernameWithToken(ChangeUsernameUxm uxm)
  {
    try
    {
      var link = LinkToConfirmToken(uxm.UserName, uxm.SecurityToken, "ResetUsername", "AnonSiaa", "PDP");
      var subj = PdpSiteSettings.Values.AppSiteTitle + " user account for " + uxm.PersonName;
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

      var mail = uxm.EmailAddress ?? PdpSiteSettings.Values.AppHostEmail;
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
      uxm.Message += "Server error occurred changing username. ";
    }
    return uxm;
  }

} // class

