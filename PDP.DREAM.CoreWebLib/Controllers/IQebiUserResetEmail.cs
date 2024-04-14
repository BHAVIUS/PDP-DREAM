// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial interface IQebiUser
{
  // allows anonymous but requires Username and Security Q&A to reset invalid/forgotten
  protected static ChangeEmailUxm ResetEmailWithToken(string username, string securityanswer)
  {
    var uxm = new ChangeEmailUxm();
    uxm.UserName = username;
    uxm.SecurityAnswer = securityanswer;
    uxm = ResetEmailWithToken(uxm, new QebiDalContext());
    return uxm;
  }

  // requires known current UserName and Security Q&A to reset forgotten Email
  protected static ChangeEmailUxm ResetEmailWithToken(ChangeEmailUxm uxm, QebiDalContext qudc)
  {
    uxm.DbtestPassed = false;
    uxm.DbfieldReset = false;
    try
    {
      var usr = qudc.GetUserByUserName(uxm.UserName);
      uxm.DbtestPassed = (string.Equals(usr.SecurityAnswer, uxm.SecurityAnswer, StringComparison.OrdinalIgnoreCase));
      if (usr == null)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "User not found. ";
      }
      else if (!usr.UserIsApproved)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "User not approved. ";
      }
      else if (!uxm.DbtestPassed)
      {
        uxm.ErrorOccurred = true;
        uxm.FormMessage += "Security answer not matched to current. ";
      }
      else
      {
        uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
        uxm.EmailAddress = usr.EmailAddress;
        // token confirmation required
        uxm.EmailConfirmed = false;
        uxm.SecurityToken = QebCryptoService.GenerateToken();
        if (!string.IsNullOrEmpty(uxm.SecurityToken))
        {
          usr.EmailConfirmed = uxm.EmailConfirmed;
          usr.SecurityToken = uxm.SecurityToken;
          usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
          usr.DateLastEdit = DateTime.UtcNow;
          usr.EmailAlternate = uxm.NewEmail;
          usr.DateEmailConfirmed = null;

          uxm = StoreEmail(uxm, usr, qudc);
        }
        else
        {
          uxm.ErrorOccurred = true;
          uxm.FormMessage += "Security token not generated. ";
        }
      }
    }
    catch (Exception error)
    {
      uxm.FormError = error;
      uxm.ErrorOccurred = true;
      uxm.FormMessage += "Server error occurred resetting email.";
    }
    return uxm;
  }

} // end interface

// end file