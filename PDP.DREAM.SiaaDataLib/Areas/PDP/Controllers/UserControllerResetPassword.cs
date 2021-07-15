// UserControllerResetPassword.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class UserController
  {
    // allows anonymous but requires Username and Security Q&A to reset invalid/forgotten
    protected ChangePasswordUxm ResetPasswordWithToken(string username, string securityanswer)
    {
      var uxm = new ChangePasswordUxm();
      uxm.UserName = username;
      uxm.SecurityAnswer = securityanswer;
      uxm = ResetPasswordWithToken(uxm);
      return uxm;
    }

    // requires known current UserName and Security Q&A to reset forgotten PassWord
    protected ChangePasswordUxm ResetPasswordWithToken(ChangePasswordUxm uxm)
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
          // token confirmation required
          uxm.SecurityToken = PdpCryptoService.GenerateToken();
          if (!string.IsNullOrEmpty(uxm.SecurityToken))
          {
            usr.PasswordHash = PdpCryptoService.HashToken(uxm.SecurityToken);
            usr.SecurityToken = uxm.SecurityToken;
            usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
            usr.DateLastEdit = DateTime.UtcNow;

            uxm = StorePassword(uxm, usr);
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
        uxm.Message += "Server error occurred resetting password. ";
      }
      return uxm;
    }


  } // class

} // namespace
