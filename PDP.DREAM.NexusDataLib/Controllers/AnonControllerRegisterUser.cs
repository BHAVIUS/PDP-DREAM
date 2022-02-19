// AnonControllerRegisterUser.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

public partial class AnonNexusController
{
  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult RegisterUser()
  {
    var uxm = new RegisterUserUxm();
    return View(uxm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult RegisterUser(RegisterUserUxm uxm)
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

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult UserRegistered() { return View(); }

} // class
