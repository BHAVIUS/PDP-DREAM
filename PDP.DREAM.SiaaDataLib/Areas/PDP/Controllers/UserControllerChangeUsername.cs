// UserControllerChangeUsername.cs 
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
    protected ChangeUsernameUxm ChangeUsernameWithToken(string password, string securitytoken, string newusername)
    {
      var uxm = new ChangeUsernameUxm();
      uxm.PassWord = password;
      uxm.SecurityToken = securitytoken;
      uxm.NewUsername = newusername;
      uxm = ChangeUsernameWithToken(uxm);
      return uxm;
    }

    protected ChangeUsernameUxm ChangeUsernameWithToken(ChangeUsernameUxm uxm)
    {
      try
      {
        var usr = QUC.GetUserByPassWordAndToken(uxm.PassWord, uxm.SecurityToken);
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

          usr.DateUserNameChanged = DateTime.UtcNow;
          usr.DateLastEdit = usr.DatePasswordChanged;
          usr.SecurityToken = string.Empty;
          usr.DateTokenExpired = null;

          uxm = StoreUsername(uxm, usr);
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
          usr.UserNameDisplayed = uxm.PersonName;
          usr.DateUserNameChanged = DateTime.UtcNow;
          usr.DateLastEdit = usr.DatePasswordChanged;
          usr.SecurityToken = string.Empty;
          usr.DateTokenExpired = null;

          uxm = StoreUsername(uxm, usr);
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

    protected ChangeUsernameUxm StoreUsername(ChangeUsernameUxm uxm, QebIdentityAppUser usr)
    {
      var errorCode = QUC.QebIdentityAppUserUpdateUsername(usr.AppGuidRef,
        usr.UserGuidKey, usr.UserName, usr.UserNameDisplayed, usr.SecurityToken,
        usr.DateTokenExpired, usr.DateUserNameChanged, usr.DateLastEdit);

      if (errorCode < 0)
      {
        uxm.ErrorOccurred = true;
        uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
      }
      else
      {
        uxm.DbfieldReset = true;
        uxm.UsernameChanged = true;
      }
      return uxm;
    }

  } // class

} // namespace
