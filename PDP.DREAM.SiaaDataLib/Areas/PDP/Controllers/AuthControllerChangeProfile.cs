// AuthControllerChangeProfile.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // authorized

  public partial class AuthController
  {
    [HttpGet]
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
            PSDC.EditPdpAgentSession(ref pdpRestCntxt);
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
    public IActionResult ProfileChanged() { return View(); }

  }

}
