// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial interface IQebiUser
{
  protected static ChangeUsernameUxm ChangeUsernameWithToken(string password, string securitytoken, string newusername)
  {
    var uxm = new ChangeUsernameUxm();
    uxm.PassWord = password;
    uxm.SecurityToken = securitytoken;
    uxm.NewUsername = newusername;
    uxm = ChangeUsernameWithToken(uxm, new QebiDbsqlContext());
    return uxm;
  }

  protected static ChangeUsernameUxm ChangeUsernameWithToken(ChangeUsernameUxm uxm, QebiDbsqlContext qudc)
  {
    try
    {
      var usr = qudc.GetUserByPassWordAndToken(uxm.PassWord, uxm.SecurityToken);
      if (usr == null)
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
      else if (string.IsNullOrEmpty(uxm.NewUsername))
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "New username not submitted. ";
      }
      else
      {
        uxm.DbtestPassed = true;
        uxm.TokenConfirmed = true;

        usr.UserName = uxm.NewUsername;
        usr.UserAlias = uxm.PersonName;
        usr.DateUserNameChanged = DateTime.UtcNow;
        usr.DateLastEdit = usr.DatePasswordChanged;
        usr.SecurityToken = ESS;
        usr.DateTokenExpired = null;

        uxm = StoreUsername(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred changing username. ";
    }
    return uxm;
  }

  // requires authenticated login to change Username
  protected static ChangeUsernameUxm ChangeUsernameWithOld(ChangeUsernameUxm uxm, QebiDbsqlContext qudc)
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

        usr.UserName = uxm.NewUsername;
        usr.UserAlias = uxm.PersonName;
        usr.DateUserNameChanged = DateTime.UtcNow;
        usr.DateLastEdit = usr.DatePasswordChanged;
        usr.SecurityToken = ESS;
        usr.DateTokenExpired = null;

        uxm = StoreUsername(uxm, usr, qudc);
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred changing username. ";
    }
    return uxm;
  }

  protected static ChangeUsernameUxm StoreUsername(ChangeUsernameUxm uxm, QebiUser usr, QebiDbsqlContext qudc)
  {
    var errorCode = qudc.QebiUserUpdateUsername(usr.AppGuid,
      usr.UserGuid, usr.UserName, usr.UserAlias, usr.SecurityToken,
      usr.DateTokenExpired, usr.DateUserNameChanged, usr.DateLastEdit);

    if (errorCode < 0)
    {
      uxm.ErrorOccurred = true;
      uxm.FormNote += $"Error code = {errorCode} while writing to user with Username {usr.UserName}";
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