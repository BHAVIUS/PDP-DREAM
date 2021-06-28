using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // Authorized
  public partial class AuthController
  {
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
      if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
      {
        QUC.GetUserByPrincipal(User);
        uxm.UserGuid = QebUserGuid;
        ChangeUsernameWithOld(uxm);
        if (uxm.UsernameChanged) { return View("UsernameChanged"); }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm);
    }

    [HttpGet]
    public IActionResult UsernameChanged() { return View(); }

  }

}
