// ScribeAgentControllerAddAgent.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.ScribeDataLib.Controllers;

public partial class AgentScribeController
{
  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AddRoleAgent() { return View(); }

  [HttpPost, ValidateAntiForgeryToken]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AddRoleAgent(int id)
  {
    // use of string id is merely trick with dummy arg to differentiate overload with distinct signature

    var agentSessionCreated = false;
    var usrRoles = QUC.GetAppUserRolesForUserGuid(QebUserGuid);
    var strAgent = PdpConst.IdentityRoleNames.NpdsAgent.ToString();
    Guid? roleGuid = null;

    if (!usrRoles.Contains(strAgent))
    {
      roleGuid = QUC.GetAppRoleGuidByRoleName(strAgent);
      if (roleGuid != null) { AddRoleUser((Guid)roleGuid); }
    }

    if (agentSessionCreated) { return RedirectToAction("RoleAgentAdded", "Agent", new { area = PdpConst.PdpMvcArea }); }
    return View();
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult RoleAgentAdded() { return View(); }

} // class
