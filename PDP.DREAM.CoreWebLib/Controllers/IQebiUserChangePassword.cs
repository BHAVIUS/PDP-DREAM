// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial interface IQebiUser
{
  protected static ChangePasswordUxm ChangePasswordWithToken(string username, string securitytoken, string newpassword)
  {
    var uxm = new ChangePasswordUxm();
    uxm.UserName = username;
    uxm.SecurityToken = securitytoken;
    uxm.NewPassword = newpassword;
    uxm = ChangePasswordWithToken(uxm, new QebiDbsqlContext());
    return uxm;
  }

  protected static ChangePasswordUxm ChangePasswordWithToken(ChangePasswordUxm uxm, QebiDbsqlContext qudc)
  {
    try
    {
      var usr = qudc.GetUserByUserNameAndToken(uxm.UserName, uxm.SecurityToken);
      if ((usr == null) || (usr.UserGuid == EGS))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "User not found. ";
      }
      else if (!IsTokenDateValid(usr.DateTokenExpired))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "Security token expired. ";
      }
      else if (!QebCryptoService.TokenEqualsToken(uxm.SecurityToken, usr.SecurityToken))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "Security token invalid. ";
      }
      else if (string.IsNullOrEmpty(uxm.NewPassword))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "New password not submitted. ";
      }
      else
      {
        uxm.DbtestPassed = true;
        uxm.TokenConfirmed = true;

        usr.PasswordHash = QebCryptoService.HashToken(uxm.NewPassword);
        usr.DatePasswordChanged = DateTime.UtcNow;
        usr.DateLastEdit = usr.DatePasswordChanged;
        usr.SecurityToken = ESS;
        usr.DateTokenExpired = null;

        uxm = StorePassword(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred changing password. ";
    }
    return uxm;
  }

  // requires authenticated login to change Password
  protected static ChangePasswordUxm ChangePasswordWithOld(ChangePasswordUxm uxm, QebiDbsqlContext qudc)
  {
    uxm.ErrorOccurred = false;
    uxm.DbfieldReset = false;
    uxm.PasswordChanged = false;
    try
    {
      var usr = qudc.GetUserByUserGuid(uxm.UserGuid);
      if (usr == null)
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "User not found. ";
      }
      else if (!QebCryptoService.VerifyHashedToken(usr.PasswordHash, uxm.PassWord))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "Password not matched to current. ";
      }
      else
      {
        uxm.DbtestPassed = true;
        uxm.UserName = usr.UserName;
        uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);

        usr.PasswordHash = QebCryptoService.HashToken(uxm.NewPassword);
        usr.DatePasswordChanged = DateTime.UtcNow;
        usr.DateLastEdit = usr.DatePasswordChanged;
        usr.SecurityToken = ESS;
        usr.DateTokenExpired = null;

        uxm = StorePassword(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred changing password. ";
    }
    return uxm;
  }

  protected static ChangePasswordUxm StorePassword(ChangePasswordUxm uxm, QebiUser usr, QebiDbsqlContext qudc)
  {
    var errorCode = qudc.QebiUserUpdatePassword(usr.AppGuid,
      usr.UserGuid, usr.PasswordHash, usr.SecurityToken,
      usr.DateTokenExpired, usr.DatePasswordChanged, usr.DateLastEdit);

    if (errorCode < 0)
    {
      uxm.ErrorOccurred = true;
      uxm.FormNote += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
    }
    else
    {
      uxm.DbfieldReset = true;
      uxm.PasswordChanged = true;
    }
    return uxm;
  }

} // end interface

// end file