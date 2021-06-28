using System;
using System.Text;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
  {
    protected ChangePasswordUxm ArgCheckModel(ChangePasswordUxm uxm)
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

    // allows anonymous but requires Username and Security Q&A to reset invalid/forgotten
    protected ChangePasswordUxm ResetPasswordWithToken(string username, string securityanswer)
    {
      var uxm = new ChangePasswordUxm();
      uxm.UserName = username;
      uxm.SecurityAnswer = securityanswer;
      return ResetPasswordWithToken(uxm);
    }

    // requires known current UserName and Security Q&A to reset forgotten PassWord
    protected ChangePasswordUxm ResetPasswordWithToken(ChangePasswordUxm uxm)
    {
      uxm.DbtestPassed = false;
      uxm.DbfieldReset = false;
      try
      {
        var usr = QUC.GetUserByUserName(uxm.UserName);
        uxm.DbtestPassed = (string.Equals(usr.SecurityAnswer, uxm.SecurityAnswer, StringComparison.OrdinalIgnoreCase));
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
        else if (!uxm.DbtestPassed)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Security answer not matched to current. ";
        }
        else
        {
          uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
          uxm.EmailAddress = usr.EmailAddress;
          // token confirmation required
          uxm.SecurityToken = PdpCryptoService.GenerateToken();
          if (!string.IsNullOrEmpty(uxm.SecurityToken))
          {
            usr.SecurityToken = uxm.SecurityToken;
            usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
            usr.DateLastEdit = DateTime.UtcNow;
            usr.PasswordHash = PdpCryptoService.HashToken(uxm.SecurityToken);

            var errorCode = QUC.QebIdentityAppUserPasswordUpdate(usr.AppGuidRef, usr.UserGuidKey,
              usr.PasswordHash, usr.SecurityToken, usr.DateTokenExpired, usr.DatePasswordChanged, usr.DateLastEdit);
            if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
            else { uxm.DbfieldReset = true; uxm.PasswordChanged = true; }
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
        uxm.Message += "Server error occurred resetting password. ";
      }
      return uxm;
    }

    protected ChangePasswordUxm NotifyPasswordWithToken(ChangePasswordUxm uxm)
    {
      try
      {
        var link = LinkToConfirmToken(uxm.UserName, uxm.SecurityToken, "ResetPassword", "Anon", "PDP");
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

        var sent = NotifyService.SendEmail(uxm.EmailAddress, subj, body.ToString());
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
        uxm.Message += "Server error occurred changing password. ";
      }
      return uxm;
    }

    protected ChangePasswordUxm ChangePasswordWithToken(string username, string securitytoken, string newpassword)
    {
      var uxm = new ChangePasswordUxm();
      uxm.UserName = username;
      uxm.SecurityToken = securitytoken;
      uxm.NewPassword = newpassword;
      return ChangePasswordWithToken(uxm);
    }

    protected ChangePasswordUxm ChangePasswordWithToken(ChangePasswordUxm uxm)
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
        else if (string.IsNullOrEmpty(uxm.NewPassword))
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "New password not submitted. ";
        }
        else
        {
          uxm.DbtestPassed = true;
          uxm.TokenConfirmed = true;

          usr.PasswordHash = PdpCryptoService.HashToken(uxm.NewPassword);
          usr.DatePasswordChanged = DateTime.UtcNow;
          usr.DateLastEdit = usr.DatePasswordChanged;
          usr.SecurityToken = string.Empty;
          usr.DateTokenExpired = null;
          var errorCode = QUC.QebIdentityAppUserPasswordUpdate(usr.AppGuidRef, usr.UserGuidKey,
            usr.PasswordHash, usr.SecurityToken, usr.DateTokenExpired, usr.DatePasswordChanged, usr.DateLastEdit);
          if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else { uxm.DbfieldReset = true; uxm.PasswordChanged = true; }
        }
      }
      catch (Exception error)
      {
        uxm.Error = error;
        uxm.ErrorOccurred = true;
        uxm.Message += "Server error occurred changing password. ";
      }
      return uxm;
    }

    // requires authenticated login to change Password
    protected ChangePasswordUxm ChangePasswordWithOld(ChangePasswordUxm uxm)
    {
      uxm.ErrorOccurred = false;
      uxm.DbfieldReset = false;
      uxm.PasswordChanged = false;
      try
      {
        var usr = QUC.GetUserByUserGuid(uxm.UserGuid);
        if (usr == null)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not found. ";
        }
        else if (!PdpCryptoService.VerifyHashedToken(usr.PasswordHash, uxm.PassWord))
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Password not matched to current. ";
        }
        else
        {
          uxm.DbtestPassed = true;
          uxm.UserName = usr.UserName;
          uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);

          usr.PasswordHash = PdpCryptoService.HashToken(uxm.NewPassword);
          usr.DatePasswordChanged = DateTime.UtcNow;
          usr.DateLastEdit = usr.DatePasswordChanged;
          usr.SecurityToken = string.Empty;
          usr.DateTokenExpired = null;

          var errorCode = QUC.QebIdentityAppUserPasswordUpdate(usr.AppGuidRef, usr.UserGuidKey,
            usr.PasswordHash, usr.SecurityToken, usr.DateTokenExpired, usr.DatePasswordChanged, usr.DateLastEdit);
          if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else { uxm.DbfieldReset = true; uxm.PasswordChanged = true; }
        }
      }
      catch (Exception error)
      {
        uxm.Error = error;
        uxm.ErrorOccurred = true;
        uxm.Message += "Server error occurred changing password. ";
      }
      return uxm;
    }

  }

}
