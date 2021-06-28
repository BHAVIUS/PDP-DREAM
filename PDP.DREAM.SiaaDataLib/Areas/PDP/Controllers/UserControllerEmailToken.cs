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
        uxm.Message += "Username not submitted.";
      }
      else if (string.IsNullOrEmpty(uxm.NewEmail))
      {
        uxm.ErrorOccurred = true;
        uxm.Message += "New email not submitted.";
      }
      else if ((uxm.RequireSecTok) && (string.IsNullOrEmpty(uxm.SecurityToken)))
      {
        uxm.ErrorOccurred = true;
        uxm.Message += "Security token not submitted.";
      }
      else
      {
        uxm.ErrorOccurred = false;
      }
      return uxm;
    }

    // allows anonymous but requires Username and Security Q&A to reset invalid/forgotten
    protected ChangeEmailUxm ResetEmailWithToken(ChangeEmailUxm uxm)
    {
      uxm.DbtestPassed = false;
      uxm.DbfieldReset = false;
      try
      {
        var usr = QUC.GetUserByUserName(uxm.UserName);
        if (usr == null)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not found. ";
        }
        else if (!usr.UserIsApproved)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not approved. ";
        }
        else
        {
          uxm.DbtestPassed = (string.Equals(usr.SecurityAnswer, uxm.SecurityAnswer, StringComparison.OrdinalIgnoreCase));
        }
        if (!uxm.DbtestPassed)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Security answer not matched to current. ";
        }
        else
        {
          if (usr == null) { throw new NullReferenceException(); }
          uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
          uxm.EmailAddress = usr.EmailAddress;
          // token confirmation required
          uxm.SecurityToken = PdpCryptoService.GenerateToken();
          if (!string.IsNullOrEmpty(uxm.SecurityToken))
          {
            usr.SecurityToken = uxm.SecurityToken;
            usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
            usr.DateLastEdit = DateTime.UtcNow;
            usr.EmailAlternate = uxm.NewEmail;
            usr.DateEmailConfirmed = null;
            usr.EmailConfirmed = false;
            var errorCode = QUC.QebIdentityAppUserEmailUpdate(usr.AppGuidRef, usr.UserGuidKey,
              usr.EmailAddress, usr.EmailAlternate, usr.SecurityToken, usr.DateTokenExpired,
              usr.DateEmailConfirmed, usr.DateLastEdit, usr.EmailConfirmed);
            if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
            else { uxm.DbfieldReset = true; uxm.EmailChanged = true; uxm.EmailConfirmed = false; }
          }
          else
          {
            uxm.ErrorOccurred = true;
            uxm.Message += "Security token not generated. ";
          }
        }
      }
      catch (Exception error)
      {
        uxm.Error = error;
        uxm.ErrorOccurred = true;
        uxm.Message += "Server error occurred resetting email.";
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

    protected ChangeEmailUxm ChangeEmailWithToken(ChangeEmailUxm uxm)
    {
      try
      {
        var usr = QUC.GetUserByUserName(uxm.UserName);
        if (usr == null)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not found. ";
        }
        else if (!IsTokenValid(usr.DateTokenExpired))
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Security token expired. ";
        }
        else if (!PdpCryptoService.TokenEqualsToken(uxm.SecurityToken, usr.SecurityToken))
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Security token invalid. ";
        }
        else
        {
          uxm.TokenConfirmed = true;

          string tempEmail = usr.EmailAddress;  // swap addresses
          usr.EmailAddress = usr.EmailAlternate;
          usr.EmailAlternate = tempEmail;
          usr.SecurityToken = string.Empty;     // update token
          usr.DateTokenExpired = null;
          usr.DateEmailConfirmed = DateTime.UtcNow;
          usr.DateLastEdit = usr.DateEmailConfirmed;
          usr.EmailConfirmed = true;

          var errorCode = QUC.QebIdentityAppUserEmailUpdate(usr.AppGuidRef, usr.UserGuidKey,
            usr.EmailAddress, usr.EmailAlternate, usr.SecurityToken, usr.DateTokenExpired,
            usr.DateEmailConfirmed, usr.DateLastEdit, usr.EmailConfirmed);
          if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else { uxm.DbfieldReset = true; uxm.EmailChanged = true; }
        }
      }
      catch (Exception error)
      {
        uxm.Error = error;
        uxm.ErrorOccurred = true;
        uxm.Message += "Server error occurred confirming email. ";
      }
      return uxm;
    }

    // requires authenticated login to change Email
    protected ChangeEmailUxm ChangeEmailWithOld(ChangeEmailUxm uxm)
    {
      uxm.ErrorOccurred = false;
      uxm.DbfieldReset = false;
      uxm.EmailChanged = false;
      try
      {
        var usr = QUC.GetUserByUserGuid(uxm.UserGuid);
        uxm.DbtestPassed = string.Equals(usr.EmailAddress, uxm.OldEmail, StringComparison.OrdinalIgnoreCase);
        if (usr == null)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not found. ";
        }
        else if (!uxm.DbtestPassed)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Email not matched to current. ";
        }
        else
        {
          uxm.UserName = usr.UserName;
          uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
          uxm.SecurityToken = PdpCryptoService.GenerateToken();
          uxm.EmailChanged = true; uxm.EmailConfirmed = false;

          usr.SecurityToken = uxm.SecurityToken;
          usr.EmailAlternate = uxm.NewEmail;
          usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
          usr.DateEmailConfirmed = null;
          usr.DateLastEdit = DateTime.UtcNow;
          usr.EmailConfirmed = uxm.EmailConfirmed;

          var errorCode = QUC.QebIdentityAppUserEmailUpdate(usr.AppGuidRef, usr.UserGuidKey,
            usr.EmailAddress, usr.EmailAlternate, usr.SecurityToken, usr.DateTokenExpired,
            usr.DateEmailConfirmed, usr.DateLastEdit, usr.EmailConfirmed);
          if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else { uxm.DbfieldReset = true;  }
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

  }

}
