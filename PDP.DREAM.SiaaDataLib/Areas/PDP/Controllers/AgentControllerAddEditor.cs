using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AgentController
  {

    [HttpGet]
    public IActionResult AddRoleEditor() { return View(); }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult AddRoleEditor(int id)
    {
      // use of string id is merely trick with dummy arg to differentiate overload with distinct signature

      var agentSessionCreated = false;
      var usrRoles = QUC.GetAppUserRolesForUserGuid(QebUserGuid);
      var strAgent = PdpConst.IdentityRoleNames.NpdsAgent.ToString();
      var strEditor = PdpConst.IdentityRoleNames.NpdsEditor.ToString();
      Guid? roleGuid = null;

      if (!usrRoles.Contains(strAgent))
      {
        roleGuid = QUC.GetAppRoleGuidByRoleName(strAgent);
        if (roleGuid != null) { AddRoleUser((Guid)roleGuid); }
      }
      if (!usrRoles.Contains(strEditor))
      {
        roleGuid = QUC.GetAppRoleGuidByRoleName(strEditor);
        if (roleGuid != null)
        {
          AddRoleUser((Guid)roleGuid);
          agentSessionCreated = PSDC.EditPdpAgentSession(ref pdpRestCntxt);
          PSDC.PdpAgentSessionAddRole(PdpConst.IdentityRoleNames.NpdsEditor);
        }
      }

      if (agentSessionCreated) { return RedirectToAction("RoleEditorAdded", "Agent", new { area = PdpConst.PdpMvcArea }); }
      return View();
    }

    [HttpGet]
    public IActionResult RoleEditorAdded() { return View(); }

  }

}
