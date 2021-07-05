using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // Authorized
  public partial class AuthController
  {
    // Anonymous ResetUsername when forgotten, Authorized ChangeUsername when known

    [HttpGet]
    public IActionResult ChangeUsername()
    {
      QUC.GetUserByPrincipal(User);
      var uxm = new ChangeUsernameUxm(QebUserGuid);
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChangeUsername(ChangeUsernameUxm uxm)
    {
      QUC.GetUserByPrincipal(User);
      if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
      {
        uxm.UserGuid = QebUserGuid;
        ChangeUsernameWithOld(uxm);
        if (uxm.UsernameChanged)
        {
          QebUserSignout();
          return View("UsernameChanged");
        }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm);
    }

    [HttpGet]
    public IActionResult UsernameChanged() { return View(); }

  }

}
