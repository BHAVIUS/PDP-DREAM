using System;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AgentController
  {

    [HttpGet]
    public IActionResult AddRoleAuthor() { return View(); }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult AddRoleAuthor(int id)
    {
      // use of string id is merely trick with dummy arg to differentiate overload with distinct signature

      var agentSessionCreated = false;
      var usrRoles = QUC.GetAppUserRolesForUserGuid(QebUserGuid);
      var strAgent = PdpConst.IdentityRoleNames.NpdsAgent.ToString();
      var strAuthor = PdpConst.IdentityRoleNames.NpdsAuthor.ToString();
      Guid? roleGuid = null;

      if (!usrRoles.Contains(strAgent))
      {
        roleGuid = QUC.GetAppRoleGuidByRoleName(strAgent);
        if (roleGuid != null) { AddRoleUser((Guid)roleGuid); }
      }
      if (!usrRoles.Contains(strAuthor))
      {
        roleGuid = QUC.GetAppRoleGuidByRoleName(strAuthor);
        if (roleGuid != null)
        {
          AddRoleUser((Guid)roleGuid);
          agentSessionCreated = PSDC.EditPdpAgentSession(ref pdpRestCntxt);
          PSDC.PdpAgentSessionAddRole(PdpConst.IdentityRoleNames.NpdsAuthor);
        }
      }

      if (agentSessionCreated) { return RedirectToAction("RoleAuthorAdded", "Agent", new { area = PdpConst.PdpMvcArea }); }
      return View();
    }

    [HttpGet]
    public IActionResult RoleAuthorAdded() { return View(); }

  }

}
