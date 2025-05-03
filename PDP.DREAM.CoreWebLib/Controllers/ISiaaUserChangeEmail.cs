// ISiaaUserChangeEmail.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial interface ISiaaUser
{
  protected static ChangeEmailUxm ChangeEmailWithToken(ChangeEmailUxm uxm, QebIdentityContext qudc)
  {
    try
    {
      var usr = qudc.GetUserByUserName(uxm.UserName);
      if ((usr == null) || (usr.UserGuidKey == Guid.Empty))
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

        uxm = StoreEmail(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.Error = error;
      uxm.ErrorOccurred = true;
      uxm.FormMessage += "Server error occurred confirming email. ";
    }
    return uxm;
  }

  // requires authenticated login to change Email
  protected static ChangeEmailUxm ChangeEmailWithOld(ChangeEmailUxm uxm, QebIdentityContext qudc)
  {
    uxm.ErrorOccurred = false;
    uxm.DbfieldReset = false;
    uxm.EmailChanged = false;
    try
    {
      var usr = qudc.GetUserByUserGuid(uxm.UserGuid);
      uxm.DbtestPassed = string.Equals(usr.EmailAddress, uxm.OldEmail, StringComparison.OrdinalIgnoreCase);
      if (usr == null)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "User not found. ";
      }
      else if (!uxm.DbtestPassed)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "Email not matched to current. ";
      }
      else
      {
        uxm.UserName = usr.UserName;
        uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
        uxm.SecurityToken = QebCryptoService.GenerateToken();
        uxm.EmailChanged = true; uxm.EmailConfirmed = false;

        usr.SecurityToken = uxm.SecurityToken;
        usr.EmailAlternate = uxm.NewEmail;
        usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
        usr.DateEmailConfirmed = null;
        usr.DateLastEdit = DateTime.UtcNow;
        usr.EmailConfirmed = uxm.EmailConfirmed;

        uxm = StoreEmail(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.Error = error;
      uxm.ErrorOccurred = true;
      uxm.FormMessage += "Server error occurred changing email. ";
    }
    return uxm;
  }

  protected static ChangeEmailUxm StoreEmail(ChangeEmailUxm uxm, QebIdentityAppUser usr, QebIdentityContext qudc)
  {
    var errorCode = qudc.QebIdentityAppUserUpdateEmail(usr.AppGuidRef, usr.UserGuidKey,
      usr.EmailAddress, usr.EmailAlternate, usr.SecurityToken, usr.DateTokenExpired,
      usr.DateEmailConfirmed, usr.DateLastEdit, usr.EmailConfirmed);

    if (errorCode < 0)
    {
      uxm.ErrorOccurred = true;
      uxm.FormMessage += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
    }
    else
    {
      uxm.DbfieldReset = true;
      uxm.EmailChanged = true;
    }
    return uxm;
  }

} // end interface

// end file