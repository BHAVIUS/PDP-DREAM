// AuthControllerLogoutUser.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Controllers;

public partial class AuthNexusController
{
  [HttpGet, HttpPost]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult LogoutUser()
  {
    if (OnlineUserIsAuthenticated)
    {
      QebUserSignoutAsync();
      return Redirect(NexusDLC.PdpPathIdentLogin);
    }
    else
    {
      return Redirect(CoreDLC.PdpPathSiteErrors);
    }
  }

} // class
