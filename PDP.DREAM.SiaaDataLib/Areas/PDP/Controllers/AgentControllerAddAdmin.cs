// AgentControllerAddAdmin.cs 
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
    public IActionResult AddRoleAdmin() { return View(); }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult AddRoleAdmin(int id)
    {
      // use of string id is merely trick with dummy arg to differentiate overload with distinct signature

      var agentSessionCreated = false;
      var usrRoles = QUC.GetAppUserRolesForUserGuid(QebUserGuid);
      var strAgent = PdpConst.IdentityRoleNames.NpdsAgent.ToString();
      var strAdmin = PdpConst.IdentityRoleNames.NpdsAdmin.ToString();
      Guid? roleGuid = null;

      if (!usrRoles.Contains(strAgent))
      {
        roleGuid = QUC.GetAppRoleGuidByRoleName(strAgent);
        if (roleGuid != null) { AddRoleUser((Guid)roleGuid); }
      }
      if (!usrRoles.Contains(strAdmin))
      {
        roleGuid = QUC.GetAppRoleGuidByRoleName(strAdmin);
        if (roleGuid != null)
        {
          AddRoleUser((Guid)roleGuid);
          agentSessionCreated = PSDC.EditPdpAgentSession(ref pdpRestCntxt);
          PSDC.PdpAgentSessionAddRole(PdpConst.IdentityRoleNames.NpdsAdmin);
        }
      }

      if (agentSessionCreated) { return RedirectToAction("RoleAdminAdded", "Agent", new { area = PdpConst.PdpMvcArea }); }
      return View();
    }

    [HttpGet]
    public IActionResult RoleAdminAdded() { return View(); }

  }

}
