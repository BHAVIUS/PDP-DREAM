// AuthControllerChangeEmail.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // Authorized
  public partial class AuthController
  {
    // Anonymous ResetEmail when forgotten, Authorized ChangeEmail when known

    [HttpGet]
    public IActionResult ChangeEmail()
    {
      QUC.GetUserByPrincipal(User);
      var uxm = new ChangeEmailUxm(QebUserGuid);
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChangeEmail(ChangeEmailUxm uxm)
    {
      QUC.GetUserByPrincipal(User);
      if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
      {
        uxm.UserGuid = QebUserGuid;
        uxm.UserName = QebUserName;
        uxm = ChangeEmailWithOld(uxm);
        if (uxm.DbfieldReset)
        {
          uxm = NotifyEmailWithToken(uxm);
          if (uxm.NoticeSent) { return View("EmailRequested"); }
        }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm);
    }

    [HttpGet]
    public IActionResult EmailRequested() { return View(); }

  }

}
