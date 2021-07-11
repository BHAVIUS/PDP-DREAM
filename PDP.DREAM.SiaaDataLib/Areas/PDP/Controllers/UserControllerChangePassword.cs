// UserControllerChangePassword.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
  {
    protected ChangePasswordUxm ChangePasswordWithToken(string username, string securitytoken, string newpassword)
    {
      var uxm = new ChangePasswordUxm();
      uxm.UserName = username;
      uxm.SecurityToken = securitytoken;
      uxm.NewPassword = newpassword;
      uxm = ChangePasswordWithToken(uxm);
      return uxm;
    }

    protected ChangePasswordUxm ChangePasswordWithToken(ChangePasswordUxm uxm)
    {
      try
      {
        var usr = QUC.GetUserByUserNameAndToken(uxm.UserName, uxm.SecurityToken);
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

          uxm = StorePassword(uxm, usr);
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

          uxm = StorePassword(uxm, usr);
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

    protected ChangePasswordUxm StorePassword(ChangePasswordUxm uxm, QebIdentityAppUser usr)
    {

      var errorCode = QUC.QebIdentityAppUserUpdatePassword(usr.AppGuidRef,
        usr.UserGuidKey, usr.PasswordHash, usr.SecurityToken,
        usr.DateTokenExpired, usr.DatePasswordChanged, usr.DateLastEdit);

      if (errorCode < 0)
      {
        uxm.ErrorOccurred = true;
        uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
      }
      else
      {
        uxm.DbfieldReset = true;
        uxm.PasswordChanged = true;
      }
      return uxm;
    }

  } // class

} // namespace
