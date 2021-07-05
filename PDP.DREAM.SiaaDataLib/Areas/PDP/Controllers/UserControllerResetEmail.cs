using System;

using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
  {

    // allows anonymous but requires Username and Security Q&A to reset invalid/forgotten
    protected ChangeEmailUxm ResetEmailWithToken(string username, string securityanswer)
    {
      var uxm = new ChangeEmailUxm();
      uxm.UserName = username;
      uxm.SecurityAnswer = securityanswer;
      return ResetEmailWithToken(uxm);
    }

    // requires known current UserName and Security Q&A to reset forgotten Email
    protected ChangeEmailUxm ResetEmailWithToken(ChangeEmailUxm uxm)
    {
      uxm.DbtestPassed = false;
      uxm.DbfieldReset = false;
      try
      {
        var usr = QUC.GetUserByUserName(uxm.UserName);
        uxm.DbtestPassed = (string.Equals(usr.SecurityAnswer, uxm.SecurityAnswer, StringComparison.OrdinalIgnoreCase));
        if (usr == null)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not found. ";
        }
        else if (!usr.UserIsApproved)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "User not approved. ";
        }
        else if (!uxm.DbtestPassed)
        {
          uxm.ErrorOccurred = true;
          uxm.Message += "Security answer not matched to current. ";
        }
        else
        {
          uxm.PersonName = uxm.ConcatNames(usr.FirstName, usr.LastName);
          uxm.EmailAddress = usr.EmailAddress;
          // token confirmation required
          uxm.EmailConfirmed = false;
          uxm.SecurityToken = PdpCryptoService.GenerateToken();
          if (!string.IsNullOrEmpty(uxm.SecurityToken))
          {
            usr.EmailConfirmed = uxm.EmailConfirmed;
            usr.SecurityToken = uxm.SecurityToken;
            usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
            usr.DateLastEdit = DateTime.UtcNow;
            usr.EmailAlternate = uxm.NewEmail;
            usr.DateEmailConfirmed = null;

            uxm = StoreEmail(uxm, usr);
          }
          else
          {
            uxm.ErrorOccurred = true;
            uxm.Message += "Security token not generated. ";
          }
        }
      }
      catch (Exception error)
      {
        uxm.Error = error;
        uxm.ErrorOccurred = true;
        uxm.Message += "Server error occurred resetting email.";
      }
      return uxm;
    }

  } // class

} // namespace
