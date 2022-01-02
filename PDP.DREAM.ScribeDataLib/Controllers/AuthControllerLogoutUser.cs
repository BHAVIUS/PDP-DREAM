// AuthControllerLogoutUser.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Controllers;

public partial class AuthScribeController
{
  [HttpGet, HttpPost]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult LogoutUser()
  {
    if (OnlineUserIsAuthenticated)
    {
      QebUserSignoutAsync();
      return Redirect(ScribeDLC.PdpPathIdentLogin);
    }
    else
    {
      return Redirect(CoreDLC.PdpPathSiteErrors);
    }
  }

} // class
