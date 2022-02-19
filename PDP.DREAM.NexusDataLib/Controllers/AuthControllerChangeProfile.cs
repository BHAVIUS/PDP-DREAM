// AuthControllerChangeProfile.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

// authorized

public partial class AuthNexusController
{
  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult DisplayProfile()
  {
    var uxm = new ChangeProfileUxm();
    if (OnlineUserIsAuthenticated)
    {
      var usr = QUC.GetUserByPrincipal(User);
      uxm = usr.GetChangeProfileModel();
    }
    return View(uxm);
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult ChangeProfile()
  {
    QUC.GetUserByPrincipal(User);
    var uxm = new ChangeProfileUxm();
    if (OnlineUserIsAuthenticated)
    {
      var usr = QUC.GetUserByUserNameAndUserGuid(QebUserName, QebUserGuid);
      uxm = usr.GetChangeProfileModel();
    }
    return View(uxm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult ChangeProfile(ChangeProfileUxm uxm)
  {
    if (ModelState.IsValid)
    {
      if (OnlineUserIsAuthenticated)
      {
        var usr = QUC.GetUserByPrincipal(User);
        var tokenVerified = PdpCryptoService.VerifyHashedToken(usr.PasswordHash, uxm.PassWord);
        if (tokenVerified)
        {
          usr.SetChangeProfileModel(uxm);
          var errorCode = QUC.QebIdentityAppUserUpdateProfile(usr.AppGuidRef, usr.UserGuidKey,
            usr.UserNameDisplayed, usr.FirstName, usr.LastName, usr.Organization, usr.PhoneNumber,
            usr.SecurityAnswer, usr.SecurityQuestion, usr.WebsiteAddress, usr.DateProfileChanged, usr.DateLastEdit);

          if (errorCode < 0) { uxm.Message += $"Error code = {errorCode} while writing to user with Username {usr.UserName}"; }
          else { uxm.ProfileChanged = true; }

          PRC.UserGuid = usr.UserGuidKey;
          PRC.UserNameDisplayed = uxm.UserNameDisplayed;
          return Redirect("ProfileChanged");
        }
        else { ModelState.AddModelError("", "Invalid password."); }
      }
      else { ModelState.AddModelError("", "Invalid user."); }
    }
    else { ModelState.AddModelError("", "Invalid model."); }
    return View(uxm);
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult ProfileChanged() { return View(); }

} // class
