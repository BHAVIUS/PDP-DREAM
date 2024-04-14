// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static class QebiClaimTypes
{
  // QEBI user/session
  public const string UserAlias = "https://npds.portaldoors.org/schema/identity/claims/useralias";
  public const string UserEmail = "https://npds.portaldoors.org/schema/identity/claims/useremail";
  public const string UserName = "https://npds.portaldoors.org/schema/identity/claims/username";
  public const string UserGuid = "https://npds.portaldoors.org/schema/identity/claims/userguid";
  public const string SessionGuid = "https://npds.portaldoors.org/schema/identity/claims/sessionguid";

  // Bridge person/session
  public const string PersonGuid = "https://npds.portaldoors.org/schema/identity/claims/personguid";

  // NPDS agent
  public const string AgentGuid = "https://npds.portaldoors.org/schema/identity/claims/agentguid";

  // user/person/agent roles
  public const string QebiRole = "https://npds.portaldoors.org/schema/identity/claims/qebirole";
  public const string NpdsRole = "https://npds.portaldoors.org/schema/identity/claims/npdsrole";
  public const string AcmsRole = "https://npds.portaldoors.org/schema/identity/claims/acmsrole";
}
