// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial interface IQebiUser
{
  // allows anonymous but requires Username and Security Q&A to reset invalid/forgotten
  protected static ChangePasswordUxm ResetPasswordWithToken(string username, string securityanswer)
  {
    var uxm = new ChangePasswordUxm();
    uxm.UserName = username;
    uxm.SecurityAnswer = securityanswer;
    uxm = ResetPasswordWithToken(uxm, new QebiDbsqlContext());
    return uxm;
  }

  // requires known current UserName and Security Q&A to reset forgotten PassWord
  protected static ChangePasswordUxm ResetPasswordWithToken(ChangePasswordUxm uxm, QebiDbsqlContext qudc)
  {
    uxm.DbtestPassed = false;
    uxm.DbfieldReset = false;
    try
    {
      var usr = qudc.GetUserByUserName(uxm.UserName);
      uxm.EmailAddress = usr.EmailAddress;
      uxm.EmailAlternate = usr.EmailAlternate;
      uxm.DbtestPassed = (string.Equals(usr.SecurityAnswer, uxm.SecurityAnswer, StringComparison.OrdinalIgnoreCase));
      if (usr == null)
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "User not found. ";
      }
      else if (!usr.UserIsApproved)
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "User not approved. ";
      }
      else if (!uxm.DbtestPassed)
      {
        uxm.ErrorOccurred = true;
        uxm.FormNote += "Security answer not matched to current. ";
      }
      else
      {
        uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
        // token confirmation required
        uxm.SecurityToken = QebCryptoService.GenerateToken();
        if (!string.IsNullOrEmpty(uxm.SecurityToken))
        {
          usr.PasswordHash = QebCryptoService.HashToken(uxm.SecurityToken);
          usr.SecurityToken = uxm.SecurityToken;
          usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
          usr.DateLastEdit = DateTime.UtcNow;

          uxm = StorePassword(uxm, usr, qudc);
        }
        else
        {
          uxm.ErrorOccurred = true;
          uxm.FormNote += "Security token not generated. ";
        }
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormNote += "Server error occurred resetting password. ";
    }
    return uxm;
  }

} // end interface

// end file