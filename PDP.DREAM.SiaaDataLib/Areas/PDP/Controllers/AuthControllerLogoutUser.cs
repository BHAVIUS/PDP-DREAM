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
