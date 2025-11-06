// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NpdsWebLib.Controllers;

public partial interface IQebiUser
{
  // allows anonymous but requires Password and Security Q&A to reset invalid/forgotten
  protected static ChangeUsernameUxm ResetUsernameWithToken(string password, string securityanswer)
  {
    var uxm = new ChangeUsernameUxm();
    uxm.PassWord = password;
    uxm.SecurityAnswer = securityanswer;
    uxm = ResetUsernameWithToken(uxm, new QebiDbsqlContext());
    return uxm;
  }

  // requires known current PassWord and Security Q&A to reset forgotten Username
  protected static ChangeUsernameUxm ResetUsernameWithToken(ChangeUsernameUxm uxm, QebiDbsqlContext qudc)
  {
    uxm.DbtestPassed = false;
    uxm.DbfieldReset = false;
    try
    {
      var usr = qudc.GetUserByPassWordAndToken(uxm.PassWord, uxm.SecurityToken);
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
          usr.UserName = uxm.UserName;
          usr.UserAlias = uxm.PersonName;
          usr.SecurityToken = uxm.SecurityToken;
          usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
          usr.DateLastEdit = DateTime.UtcNow;

          uxm = StoreUsername(uxm, usr, qudc);
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
      uxm.FormNote += "Server error occurred resetting username. ";
    }
    return uxm;
  }

} // end interface

// end file