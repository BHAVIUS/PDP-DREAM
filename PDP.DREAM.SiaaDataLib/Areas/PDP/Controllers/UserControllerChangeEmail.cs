using System;

using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
  {
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

          uxm = StoreEmail(uxm, usr);
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

          uxm = StoreEmail(uxm, usr);
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

    protected ChangeEmailUxm StoreEmail(ChangeEmailUxm uxm, QebIdentityAppUser usr)
    {
      var errorCode = QUC.QebIdentityAppUserUpdateEmail(usr.AppGuidRef, usr.UserGuidKey,
        usr.EmailAddress, usr.EmailAlternate, usr.SecurityToken, usr.DateTokenExpired,
        usr.DateEmailConfirmed, usr.DateLastEdit, usr.EmailConfirmed);
      if (errorCode < 0)
      {
        uxm.ErrorOccurred = true;
        uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
      }
      else
      {
        uxm.DbfieldReset = true;
        uxm.EmailChanged = true;
      }
      return uxm;
    }

  } // class

} // namespace
