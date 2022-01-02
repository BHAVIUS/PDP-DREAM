// AnonControllerConfirmEmail.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

// anonymous
public partial class AnonNexusController
{
  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult ConfirmEmail(string id, string ct)
  {
    var uxm = new ChangeEmailUxm(id, ct);
    return View(uxm);
  }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult ConfirmEmail(ChangeEmailUxm uxm)
  {
    if (ModelState.IsValid)
    {
      uxm = ArgCheckModel(uxm);
      uxm = ChangeEmailWithToken(uxm);
      if (uxm.TokenConfirmed && uxm.EmailChanged)
      {
        ViewData["PRC"] = PRC;
        return View("EmailConfirmed");
      }
      else { ModelState.AddModelError("", "The security token was not validated."); }
    }
    else { ModelState.AddModelError("", "The submitted form is not valid."); }
    ViewData["PRC"] = PRC;
    return View(uxm);
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult EmailConfirmed() { return View(); }

} // class
