// AnonControllerRegisterUser.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AnonController
  {
    [HttpGet]
    public IActionResult UserRegistered() { return View(); }

    [HttpGet]
    public IActionResult RegisterUser()
    {
      var uxm = new RegisterUserUxm();
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterUser(RegisterUserUxm uxm)
    {
      if (ModelState.IsValid)
      {
        if (QUC.CountUsersByUserName(uxm.UserName) == 0)
        {
          var urm = QUC.RegisterSiaaUser(uxm);
          var qebUser = QUC.GetUserByUserName(uxm.UserName); // updated with current SecurityToken

          if (string.Equals(uxm.UserName, qebUser.UserName, StringComparison.OrdinalIgnoreCase) == true) // consistency check on UserName
          {
            uxm.UserRegistered = true;
            var exm = new ChangeEmailUxm(qebUser.UserName, qebUser.SecurityToken, qebUser.FirstName, qebUser.LastName, qebUser.EmailAddress);
            exm = ArgCheckModel(exm);
            exm = NotifyEmailWithToken(exm);
            if (exm.NoticeSent) { return View("UserRegistered"); }
            else { uxm.Message += exm.Message; }
          }
          else
          {
            uxm.UserRegistered = false;
            uxm.Message += "User not registered for requested Username.";
          }
        }
        else
        {
          uxm.UserRegistered = false;
          uxm.Message += "Username already exists. Please try a different one. ";
        }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm);
    }

  }

}
