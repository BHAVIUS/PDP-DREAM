// AuthControllerChangeEmail.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

// Authorized
public partial class AuthNexusController
{
  // Anonymous ResetEmail when forgotten, Authorized ChangeEmail when known

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult ChangeEmail()
  {
    QUC.GetUserByPrincipal(User);
    var uxm = new ChangeEmailUxm(QebUserGuid);
    return View(uxm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
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
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult EmailRequested() { return View(); }

} // class
