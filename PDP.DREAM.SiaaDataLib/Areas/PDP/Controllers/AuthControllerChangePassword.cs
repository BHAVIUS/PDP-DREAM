using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // authorized
  public partial class AuthController
  {
    // Anonymous ResetPassword when forgotten, Authorized ChangePassword when known

    [HttpGet]
    public IActionResult ChangePassword()
    {
      QUC.GetUserByPrincipal(User);
      var uxm = new ChangePasswordUxm(QebUserGuid);
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChangePassword(ChangePasswordUxm uxm)
    {
      QUC.GetUserByPrincipal(User);
      if ((ModelState.IsValid) && (OnlineUserIsAuthenticated))
      {
        uxm.UserGuid = QebUserGuid;
        ChangePasswordWithOld(uxm);
        if (uxm.PasswordChanged) { return View("PasswordChanged"); }
        PdpPrcMvcAddErrors(uxm.Message);
      }
      return View(uxm);
    }

    [HttpGet]
    public IActionResult PasswordChanged() { return View(); }

  }

}
