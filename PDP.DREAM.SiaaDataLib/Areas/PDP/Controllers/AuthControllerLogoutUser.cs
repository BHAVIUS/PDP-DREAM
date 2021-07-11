// AuthControllerLogoutUser.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AuthController 
  {

    [HttpGet, HttpPost]
    public IActionResult LogoutUser()
    {
      if (OnlineUserIsAuthenticated)
      {
        QebUserSignoutAsync();
        return Redirect(PdpConst.PdpPathIdentLogin);
      }
      else
      {
        return Redirect(PdpConst.PdpPathSiteError);
      }
    }

  }

}
