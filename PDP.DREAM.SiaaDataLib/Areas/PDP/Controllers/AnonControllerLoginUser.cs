using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AnonController
  {
    [HttpGet]
    public IActionResult LoginUser(string? returnUrl = null)
    {
      if ((returnUrl == null) || !IsUrlLocalToLoginDomain(returnUrl))
      { returnUrl = Url.Content("~/"); }
      if (OnlineUserIsAuthenticated) // in class SiaaUserAgentControllerBase
      {
        return Redirect(PdpConst.PdpPathIdentProfile);
      }
      else
      {
        QebUserSignoutAsync(); // clear authentication cookie
        var uxm = new LoginUserUxm()
        {
          ReturnUrl = returnUrl
        };
        return View(uxm);
      }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginUser(LoginUserUxm uxm)
    {
      QebUserSignoutAsync(); // clear authentication cookie
      var pdpSignin = new PdpIdentityResult();
      var qebUser = new QebIdentityAppUser();

      if (ModelState.IsValid)
      {
        if (string.IsNullOrEmpty(uxm.ReturnUrl))
        {
          // uxm.ReturnUrl = PdpConst.PdpPathSiteInfo;
          uxm.ReturnUrl = PdpConst.PdpPathIdentProfile;
        }

        if (!string.IsNullOrWhiteSpace(uxm.UserName))
        {
          qebUser = QUC.GetUserByUserName(uxm.UserName);
          if ((string.IsNullOrWhiteSpace(qebUser?.UserName) || (qebUser?.ConcurrencyStamp == PdpConst.PdpInvalidToken)))
          {
            ModelState.AddModelError(string.Empty, $"UserName '{uxm.UserName}' invalid. User not found.");
          }
        }
        else
        {
          ModelState.AddModelError(string.Empty, $"UserName is null or whitespace. User not found.");
        }

        if ((qebUser != null) && (qebUser.ConcurrencyStamp != PdpConst.PdpInvalidToken))
        {
          // Signin without user roles and agent session
          pdpSignin = await QebUserSigninAsync(uxm.UserName, uxm.PassWord);
        }
        List<string>? usrRoles = null;

        if (pdpSignin.Succeeded)
        {
          // create/update PDP Agent Session
          usrRoles = QUC.GetUserRoleNamesByUserGuid(qebUser.UserGuidKey);
          if (usrRoles.Contains(PdpConst.IdentityRoleNames.NpdsAgent.ToString()))
          {
            var agentGuid = (Guid?)PRC.AgentGuid;
            var sessionGuid = (Guid?)PRC.SessionGuid;
            var errorCode = PSDC.ScribeAgentSessionEdit(pdpSiteSets.AppSecureUiaaGuid, qebUser.UserGuidKey,
              qebUser.UserNameDisplayed, ref agentGuid, ref sessionGuid);
            if (errorCode == 0)
            {
              PRC.AgentGuid = agentGuid ?? Guid.Empty;
              PRC.SessionGuid = sessionGuid ?? Guid.Empty;
              errorCode = QUC.QebIdentityAppUserStamp(pdpSiteSets.AppSecureUiaaGuid, qebUser.UserGuidKey, PRC.SessionGuid);
            }
            if (errorCode == 0)
            {
              // Signin with user roles and agent session
              pdpSignin = await QebUserSigninAsync(qebUser.UserName, qebUser.UserGuidKey, PRC.AgentGuid, PRC.SessionGuid, usrRoles);
              // uxm.ReturnUrl = AppendKeys(uxm.ReturnUrl, agt.SessionGuidKey, agt.AgentGuidKey, agt.IdentityUserGuidRef);
            }
          }
        }
        if (!pdpSignin.Succeeded)
        {
          ModelState.AddModelError(string.Empty, "User login invalid");
        }
      }
      else
      {
        ModelState.AddModelError(string.Empty, "User model invalid");
      }

      if (pdpSignin.Succeeded)
      {
        qebLogger.LogInformation(1, "User login succeeded");
        return Redirect(uxm.ReturnUrl);
      }
      else
      {
        qebLogger.LogInformation(2, "User login failed");
        return View(uxm);
      }

    }

    protected void ArgCheckModel(LoginUserUxm uxm)
    {
      uxm.Message = string.Empty;
      if (string.IsNullOrEmpty(uxm.UserName))
      {
        uxm.ErrorOccurred = true;
        uxm.Message += "Username not submitted. ";
      }
      else if (string.IsNullOrEmpty(uxm.PassWord))
      {
        uxm.ErrorOccurred = true;
        uxm.Message += "Password not submitted. ";
      }
      else
      {
        uxm.ErrorOccurred = false;
      }
    }

  } // class

} // namespace
