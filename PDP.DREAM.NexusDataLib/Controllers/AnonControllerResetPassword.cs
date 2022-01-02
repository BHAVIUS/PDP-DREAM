// AnonControllerResetPassword.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

// anonymous
public partial class AnonNexusController
{
  //
  // Anonymous ResetPassword when forgotten, Authorized ChangePassword when known
  //

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult ResetPassword(string id, string ct)
  {
    if ((!string.IsNullOrWhiteSpace(id)) && (!string.IsNullOrWhiteSpace(ct)))
    { // initialize to WizardStep3
      var uxm3 = new ChangePasswordUxm3(id, ct);
      return View("ResetPassword3", uxm3);
    }
    else
    { // initialize to WizardStep1
      var uxm1 = new ChangePasswordUxm1();
      return View("ResetPassword1", uxm1);
    }
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult ResetPassword1(ChangePasswordUxm1 uxm1)
  {
    // Wizard Step 1 on post
    if (!string.IsNullOrWhiteSpace(uxm1.UserName))
    {
      var username = uxm1.UserName;
      var uxm2 = new ChangePasswordUxm2(username);
      var usr = QUC.GetUserByUserName(username);
      if (usr != null)
      {
        uxm2.UserGuid = usr.UserGuidKey;
        uxm2.SecurityQuestion = usr.SecurityQuestion;
        return View("ResetPassword2", uxm2);
      }
      else { PdpPrcMvcAddErrors("Username not found. "); }
    }
    else { PdpPrcMvcAddErrors("Username not input or SecurityQuestion not found. "); }
    return View("ResetPassword1", uxm1);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult ResetPassword2(ChangePasswordUxm2 uxm2)
  {
    // Wizard Step 2 on Post
    if ((!string.IsNullOrWhiteSpace(uxm2.UserName)) && (!string.IsNullOrWhiteSpace(uxm2.SecurityAnswer)))
    {
      var uxm = ResetPasswordWithToken(uxm2.UserName, uxm2.SecurityAnswer); // update database
      if (uxm.DbfieldReset)
      {
        uxm = NotifyPasswordWithToken(uxm); // send message
        if (uxm.NoticeSent) { return View("PasswordRequested"); }
      }
      PdpPrcMvcAddErrors(uxm.Message);
    }
    else { PdpPrcMvcAddErrors("Question not answered"); }
    return View("ResetPassword2", uxm2);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult ResetPassword3(ChangePasswordUxm3 uxm3)
  {
    // Wizard Step 3 on Post
    if ((!string.IsNullOrWhiteSpace(uxm3.UserName)) && (!string.IsNullOrWhiteSpace(uxm3.SecurityToken))
      && (!string.IsNullOrWhiteSpace(uxm3.NewPassword)))
    {
      var uxm = ChangePasswordWithToken(uxm3.UserName, uxm3.SecurityToken, uxm3.NewPassword);
      if (uxm.PasswordChanged) { return View("PasswordReset"); }
      PdpPrcMvcAddErrors(uxm.Message);
    }
    return View(uxm3);
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult PasswordReset() { return View(); }

}
