using System;
using System.Text;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
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

    // allows anonymous but requires Password and Security Q&A to reset invalid/forgotten
    protected ChangeUsernameUxm ResetUsernameWithToken(string password, string securityanswer)
    {
      var uxm = new ChangeUsernameUxm();
      uxm.PassWord = password;
      uxm.SecurityAnswer = securityanswer;
      return ResetUsernameWithToken(uxm);
    }

    // requires known current PassWord and Security Q&A to reset forgotten Username
    protected ChangeUsernameUxm ResetUsernameWithToken(ChangeUsernameUxm uxm)
    {
      uxm.DbtestPassed = false;
      uxm.DbfieldReset = false;
      try
      {
        var usr = QUC.GetUserByPassWord(uxm.PassWord);
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
            QUC.SaveChanges();
            uxm.DbfieldReset = true;
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
        uxm.Message += "Server error occurred resetting username. ";
      }
      return uxm;
    }

    protected ChangeUsernameUxm NotifyUsernameWithToken(ChangeUsernameUxm uxm)
    {
      try
      {
        var link = LinkToConfirmToken(uxm.UserName, uxm.SecurityToken, "ResetUsername", "Anon", "PDP");
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
        uxm.Message += "Server error occurred changing username. ";
      }
      return uxm;
    }

    protected ChangeUsernameUxm ChangeUsernameWithToken(string password, string securitytoken, string newusername)
    {
      var uxm = new ChangeUsernameUxm();
      uxm.PassWord = password;
      uxm.SecurityToken = securitytoken;
      uxm.NewUsername = newusername;
      return ChangeUsernameWithToken(uxm);
    }

    protected ChangeUsernameUxm ChangeUsernameWithToken(ChangeUsernameUxm uxm)
    {
      try
      {
        var usr = QUC.GetUserByUserGuid(uxm.UserGuid);
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
        else if (string.IsNullOrEmpty(uxm.NewUsername))
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "New username not submitted. ";
        }
        else
        {
          uxm.DbtestPassed = true;
          uxm.TokenConfirmed = true;

          usr.PasswordHash = PdpCryptoService.HashToken(uxm.NewUsername);
          usr.DateUserNameChanged = DateTime.UtcNow;
          usr.DateLastEdit = usr.DatePasswordChanged;
          usr.SecurityToken = null;
          usr.DateTokenExpired = null;
          QUC.SaveChanges();

          uxm.DbfieldReset = true;
          uxm.UsernameChanged = true;
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

    // requires authenticated login to change Username
    protected ChangeUsernameUxm ChangeUsernameWithOld(ChangeUsernameUxm uxm)
    {
      uxm.ErrorOccurred = false;
      uxm.DbfieldReset = false;
      uxm.UsernameChanged = false;
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
          usr.UserName = uxm.NewUsername;
          usr.DateUserNameChanged = DateTime.UtcNow;
          usr.DateLastEdit = usr.DatePasswordChanged;
          usr.SecurityToken = null;
          usr.DateTokenExpired = null;
          QUC.SaveChanges();

          uxm.DbfieldReset = true;
          uxm.UsernameChanged = true;
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

  }

}
