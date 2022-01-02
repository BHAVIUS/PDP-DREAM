// AnonControllerResetEmail.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

// Anonymous
public partial class AnonNexusController
{

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
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

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public ActionResult EmailReset() { return View(); }

}
