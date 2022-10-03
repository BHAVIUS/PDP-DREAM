// SqldbcAgentSession.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebIdentityContext
{
  public bool EditNpdsUserSession(ref QebUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.QebUserGuid))
    {
      OpenSqlConnection();
      OpenSqlCommand("QebiAppUserSessionEdit");
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AppGuid", PDPSS.AppSecureUiaaGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", qurc.QebUserGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.QebSessionGuid);
      int errorExists = PdpSql.ExecuteCommand(ref pdpDataCmmnd);
      if (errorExists == 0)
      {
        qurc.QebSessionGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
      }
      CloseSqlConnection();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool CheckNpdsUserSession(ref QebUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.QebUserGuid))
    {
      OpenSqlConnection();
      OpenSqlCommand("QebiAppUserSessionCheck");
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AppGuid", PDPSS.AppSecureUiaaGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@SessionValueIsRequired", qurc.QebSessionValueIsRequired);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.QebSessionGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", ParameterDirection.InputOutput, qurc.QebUserGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.QebAgentGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.NVarChar, 64, "@UserNameDisp", ParameterDirection.Output, qurc.QebUserNameDisplayed);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@UserIsAgent", ParameterDirection.Output);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAuthor", ParameterDirection.Output);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsEditor", ParameterDirection.Output);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAdmin", ParameterDirection.Output);
      int errorExists = PdpSql.ExecuteCommand(ref pdpDataCmmnd);
      if (errorExists == 0)
      {
        qurc.QebSessionGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
        qurc.QebUserGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@UserGuid");
        qurc.QebAgentGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentGuid");
        // next 2 properties implicit by existence of row in table
        // TODO: until or unless privilege revocation implemented
        qurc.ClientIsAuthenticated = true;
        qurc.ClientIsUser = true;
        // next 4 properties must check column value
        qurc.QebUserNameDisplayed = PdpSql.GetChar(ref pdpDataCmmnd, "@UserNameDisp");
        qurc.ClientIsAgent = PdpSql.GetBit(ref pdpDataCmmnd, "@UserIsAgent");
        qurc.ClientIsAuthor = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsAuthor");
        qurc.ClientIsEditor = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsEditor");
        qurc.ClientIsAdmin = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsAdmin");
        // TODO: enhance SiaaNetUser table to enable revocation of privileges
      }
      CloseSqlConnection();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  // ATTN: must make double-call from MVC action to both contexts
  // TODO: rebuild separately for CoreDbsqlContext
  // TODO: rebuild separately for QebIdentityContext
  public bool NpdsUserSessionAddRole(NamesForIdentityRoles role)
  {
    var roleAdded = false;
    //var nsa = this.CoreSessionAgents.Single(s => (s.AgentGuidKey == PRC.AgentGuid));
    //switch (role)
    //{
    //  case IdentityRoleNames.NpdsAuthor:
    //    nsa.AgentIsAuthor = true; // secure default with false
    //    break;
    //  case IdentityRoleNames.NpdsEditor:
    //    nsa.AgentIsEditor = true; // secure default with false
    //    break;
    //  case IdentityRoleNames.NpdsAdmin:
    //    nsa.AgentIsAdmin = true; // secure default with false
    //    break;
    //  default:
    //    throw new InvalidEnumArgumentException();
    //}
    //try
    //{
    //  var errorCode = QebiAppUserSessionAddRole(nsa.IdentityUserGuidRef,
    //    nsa.AgentGuidKey, nsa.AgentIsAuthor, nsa.AgentIsEditor, nsa.AgentIsAdmin);
    //  roleAdded = true;
    //}
    //catch (Exception ex) { var error = ex.Message; }
    return roleAdded;
  }

} // end class

// end file