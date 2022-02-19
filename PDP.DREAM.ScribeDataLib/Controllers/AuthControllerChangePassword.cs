// AuthControllerChangePassword.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.ScribeDataLib.Controllers;

// Authorized
public partial class AuthScribeController
{
  // Anonymous ResetPassword when forgotten, Authorized ChangePassword when known

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult ChangePassword()
  {
    QUC.GetUserByPrincipal(User);
    var uxm = new ChangePasswordUxm(QebUserGuid);
    return View(uxm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
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
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult PasswordChanged() { return View(); }

} // class
