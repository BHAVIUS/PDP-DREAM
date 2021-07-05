using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  // anonymous
  public partial class AnonController
  {

    [HttpGet]
    public ActionResult ConfirmEmail(string id, string ct)
    {
      var uxm = new ChangeEmailUxm(id, ct);
      return View(uxm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult ConfirmEmail(ChangeEmailUxm uxm)
    {
      if (ModelState.IsValid)
      {
        uxm = ArgCheckModel(uxm);
        uxm = ChangeEmailWithToken(uxm);
        if (uxm.TokenConfirmed && uxm.EmailChanged)
        {
          ViewBag.PRC = PRC;
          return View("EmailConfirmed");
        }
        else { ModelState.AddModelError("", "The security token was not validated."); }
      }
      else { ModelState.AddModelError("", "The submitted form is not valid."); }
      ViewBag.PRC = PRC;
      return View(uxm);
    }

    [HttpGet]
    public ActionResult EmailConfirmed() { return View(); }

  } // class

} // namespace
