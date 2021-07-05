using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // Anonymous
  public partial class AnonController
  {
    [HttpGet]
    public ActionResult EmailReset() { return View(); }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult ResetEmail(ChangeEmailUxm uxm)
    {
      if (ModelState.IsValid)
      {
        uxm = ChangeEmailWithToken(uxm);
        if (uxm.TokenConfirmed && uxm.EmailChanged) { return View("EmailConfirmed"); }
        else { ModelState.AddModelError("", "The security token was not validated."); }
      }
      else { ModelState.AddModelError("", "The submitted form is not valid."); }
      return View(uxm);
    }

  }

}
