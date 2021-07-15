// AuthControllerChangePassword.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // Authorized
  public partial class AuthController
  {
    // Anonymous ResetPassword when forgotten, Authorized ChangePassword when known

    [HttpGet]
    public IActionResult ChangePassword()
    {
      QUC.GetUserByPrincipal(User);
      var uxm = new ChangePasswordUxm(QebUserGuid);
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChangePassword(ChangePasswordUxm uxm)
    {
      QUC.GetUserByPrincipal(User);
      if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
      {
        uxm.UserGuid = QebUserGuid;
        uxm.UserName = QebUserName;
        uxm = ChangePasswordWithOld(uxm);
        if (uxm.PasswordChanged)
        {
          return View("PasswordChanged");
        }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm);
    }

    [HttpGet]
    public IActionResult PasswordChanged() { return View(); }

  }

}
