﻿// AnonControllerResetUsername.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // anonymous
  public partial class AnonController
  {
    //
    // Anonymous ResetUsername when forgotten, Authorized ChangeUsername when known
    //

    [HttpGet]
    public ActionResult ResetUsername(string id, string ct)
    {
      if ((!string.IsNullOrWhiteSpace(id)) && (!string.IsNullOrWhiteSpace(ct)))
      { // initialize to WizardStep3
        var uxm3 = new ChangeUsernameUxm3(id, ct);
        return View("ResetUsername3", uxm3);
      }
      else
      { // initialize to WizardStep1
        var uxm1 = new ChangeUsernameUxm1();
        return View("ResetUsername1", uxm1);
      }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult ResetUsername1(ChangeUsernameUxm1 uxm1)
    {
      // Wizard Step 1 on post
      if (!string.IsNullOrWhiteSpace(uxm1.PassWord))
      {
        var password = uxm1.PassWord;
        var uxm2 = new ChangeUsernameUxm2(password);
        var usr = QUC.GetUserByUserName(password);
        if (usr != null)
        {
          uxm2.UserGuid = usr.UserGuidKey;
          uxm2.SecurityQuestion = usr.SecurityQuestion;
          return View("ResetUsername2", uxm2);
        }
        else { PdpPrcMvcAddErrors("Username not found. "); }
      }
      else { PdpPrcMvcAddErrors("Username not input or SecurityQuestion not found. "); }
      return View("ResetUsername1", uxm1);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult ResetUsername2(ChangeUsernameUxm2 uxm2)
    {
      // Wizard Step 2 on Post
      if ((!string.IsNullOrWhiteSpace(uxm2.PassWord)) && (!string.IsNullOrWhiteSpace(uxm2.SecurityAnswer)))
      {
        var uxm = ResetUsernameWithToken(uxm2.PassWord, uxm2.SecurityAnswer); // update database
        if (uxm.DbfieldReset)
        {
          uxm = NotifyUsernameWithToken(uxm); // send message
          if (uxm.NoticeSent) { return View("UsernameRequested"); }
        }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      else { PdpPrcMvcAddErrors("Question not answered"); }
      return View("ResetUsername2", uxm2);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult ResetUsername3(ChangeUsernameUxm3 uxm3)
    {
      // Wizard Step 3 on Post
      if ((!string.IsNullOrWhiteSpace(uxm3.PassWord)) && (!string.IsNullOrWhiteSpace(uxm3.SecurityToken))
        && (!string.IsNullOrWhiteSpace(uxm3.NewUsername)))
      {
        var uxm = ChangeUsernameWithToken(uxm3.PassWord, uxm3.SecurityToken, uxm3.NewUsername);
        if (uxm.UsernameChanged) { return View("UsernameReset"); }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm3);
    }

    [HttpGet]
    public ActionResult UsernameReset() { return View(); }

  }

}
