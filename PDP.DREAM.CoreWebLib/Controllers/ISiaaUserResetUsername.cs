// ISiaaUserResetUsername.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;

namespace PDP.DREAM.CoreWebLib.Controllers;

public partial interface ISiaaUser
{
  // allows anonymous but requires Password and Security Q&A to reset invalid/forgotten
  protected static ChangeUsernameUxm ResetUsernameWithToken(string password, string securityanswer)
  {
    var uxm = new ChangeUsernameUxm();
    uxm.PassWord = password;
    uxm.SecurityAnswer = securityanswer;
    uxm = ResetUsernameWithToken(uxm, new QebIdentityContext());
    return uxm;
  }

  // requires known current PassWord and Security Q&A to reset forgotten Username
  protected static ChangeUsernameUxm ResetUsernameWithToken(ChangeUsernameUxm uxm, QebIdentityContext qudc)
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
        // token confirmation required
        uxm.SecurityToken = QebCryptoService.GenerateToken();
        if (!string.IsNullOrEmpty(uxm.SecurityToken))
        {
          usr.UserName = uxm.UserName;
          usr.UserNameDisplayed = uxm.PersonName;
          usr.SecurityToken = uxm.SecurityToken;
          usr.DateTokenExpired = DateTime.UtcNow.AddHours(24);
          usr.DateLastEdit = DateTime.UtcNow;

          uxm = StoreUsername(uxm, usr, qudc);
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
      uxm.Error = error;
      uxm.ErrorOccurred = true;
      uxm.FormMessage += "Server error occurred resetting username. ";
    }
    return uxm;
  }

} // end interface

// end file