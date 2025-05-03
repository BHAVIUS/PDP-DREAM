// ISiaaUserChangeUsername.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial interface ISiaaUser
{
  protected static ChangeUsernameUxm ChangeUsernameWithToken(string password, string securitytoken, string newusername)
  {
    var uxm = new ChangeUsernameUxm();
    uxm.PassWord = password;
    uxm.SecurityToken = securitytoken;
    uxm.NewUsername = newusername;
    uxm = ChangeUsernameWithToken(uxm, new QebIdentityContext());
    return uxm;
  }

  protected static ChangeUsernameUxm ChangeUsernameWithToken(ChangeUsernameUxm uxm, QebIdentityContext qudc)
  {
    try
    {
      var usr = qudc.GetUserByPassWordAndToken(uxm.PassWord, uxm.SecurityToken);
      if (usr == null)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "User not found. ";
      }
      else if (!IsTokenDateValid(usr.DateTokenExpired))
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "Security token expired. ";
      }
      else if (!QebCryptoService.TokenEqualsToken(uxm.SecurityToken, usr.SecurityToken))
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "Security token invalid. ";
      }
      else if (string.IsNullOrEmpty(uxm.NewUsername))
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "New username not submitted. ";
      }
      else
      {
        uxm.DbtestPassed = true;
        uxm.TokenConfirmed = true;

        usr.UserName = uxm.NewUsername;
        usr.UserNameDisplayed = uxm.PersonName;
        usr.DateUserNameChanged = DateTime.UtcNow;
        usr.DateLastEdit = usr.DatePasswordChanged;
        usr.SecurityToken = string.Empty;
        usr.DateTokenExpired = null;

        uxm = StoreUsername(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.Error = error;
      uxm.ErrorOccurred = true;
      uxm.FormMessage += "Server error occurred changing username. ";
    }
    return uxm;
  }

  // requires authenticated login to change Username
  protected static ChangeUsernameUxm ChangeUsernameWithOld(ChangeUsernameUxm uxm, QebIdentityContext qudc)
  {
    uxm.ErrorOccurred = false;
    uxm.DbfieldReset = false;
    uxm.UsernameChanged = false;
    try
    {
      var usr = qudc.GetUserByUserGuid(uxm.UserGuid);
      if (usr == null)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "User not found. ";
      }
      else if (!QebCryptoService.VerifyHashedToken(usr.PasswordHash, uxm.PassWord))
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "Password not matched to current. ";
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

        uxm = StoreUsername(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.Error = error;
      uxm.ErrorOccurred = true;
      uxm.FormMessage += "Server error occurred changing username. ";
    }
    return uxm;
  }

  protected static ChangeUsernameUxm StoreUsername(ChangeUsernameUxm uxm, QebIdentityAppUser usr, QebIdentityContext qudc)
  {
    var errorCode = qudc.QebIdentityAppUserUpdateUsername(usr.AppGuidRef,
      usr.UserGuidKey, usr.UserName, usr.UserNameDisplayed, usr.SecurityToken,
      usr.DateTokenExpired, usr.DateUserNameChanged, usr.DateLastEdit);

    if (errorCode < 0)
    {
      uxm.ErrorOccurred = true;
      uxm.FormMessage += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
    }
    else
    {
      uxm.DbfieldReset = true;
      uxm.UsernameChanged = true;
    }
    return uxm;
  }

  protected static bool IsTokenDateValid(DateTime? tokenDate)
  {
    var current = DateTime.UtcNow;
    var expired = Convert.ToDateTime(tokenDate ?? DateTime.MinValue);
    var isValid = (DateTime.Compare(current, expired) < 0);
    return isValid;
  }

} // end interface

// end file