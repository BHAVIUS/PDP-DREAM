// AgentControllerAddAgent.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AgentController
  {

    [HttpGet]
    public IActionResult AddRoleAgent() { return View(); }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult AddRoleAgent(int id)
    {
      // use of string id is merely trick with dummy arg to differentiate overload with distinct signature

      var agentSessionCreated = false;
      var usrRoles = QUC.GetAppUserRolesForUserGuid(QebUserGuid);
      var strAgent = PdpConst.IdentityRoleNames.NpdsAgent.ToString();

      if (!usrRoles.Contains(strAgent))
      {
        var roleGuid = QUC.GetAppRoleGuidByRoleName(strAgent);
        if (roleGuid != null) { AddRoleUser((Guid)roleGuid); }
      }

      if (agentSessionCreated) { return RedirectToAction("RoleAgentAdded", "Agent", new { area = PdpConst.PdpMvcArea }); }
      return View();
    }

    [HttpGet]
    public IActionResult RoleAgentAdded() { return View(); }

  }

}
