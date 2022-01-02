// AnonControllerLoginUser.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Controllers;

public partial class AnonNexusController
{
  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult LoginUser(string? returnUrl = null)
  {
    returnUrl = ArgCheckReturnUrl(returnUrl);
    if (OnlineUserIsAuthenticated)
    {
      return Redirect(returnUrl);
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
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public virtual async Task<IActionResult> LoginUser(LoginUserUxm uxm)
  {
    QebUserSignoutAsync(); // clear authentication cookie
    var pdpSignin = new PdpIdentityResult();
    var qebUser = new QebIdentityAppUser();

    ArgCheckModel(uxm);
    if (ModelState.IsValid)
    {
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
        // Signin without user roles or agent session
        pdpSignin = await QebUserSigninAsync(uxm.UserName, uxm.PassWord);
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

  protected string ArgCheckReturnUrl(string theUrl)
  {
    if ((theUrl == null) || !IsUrlLocalToLoginDomain(theUrl))
    { theUrl = Url.Content(NexusDLC.PdpPathIdentProfile); }
    return theUrl;
  }

  protected void ArgCheckModel(LoginUserUxm uxm)
  {
    uxm.Message = string.Empty;
    uxm.ReturnUrl = ArgCheckReturnUrl(uxm.ReturnUrl);
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
