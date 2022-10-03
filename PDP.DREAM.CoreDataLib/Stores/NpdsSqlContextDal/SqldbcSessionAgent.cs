// SqldbcSessionAgent.cs 
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

public partial class CoreDbsqlContext
{
  public bool EditCoreSessionAgent(ref QebUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.QebUserGuid))
    {
      OpenSqlConnection();
      OpenSqlCommand("CoreSessionAgentEdit");
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityAppGuid", PDPSS.AppSecureUiaaGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityUserGuid", qurc.QebUserGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.NVarChar, 64, "@IdentityUserNameDisp", qurc.QebUserNameDisplayed);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.QebAgentGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.QebSessionGuid);
      int errorExists = PdpSql.ExecuteCommand(ref pdpDataCmmnd);
      // may have same PdpAgentGuid associated with different AspnetUserGuid
      //   and/or may have same AspnetUserGuid associated with different AspnetSystemIid
      if (errorExists == 0)
      {
        qurc.QebAgentGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentGuid");
        qurc.QebSessionGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
      }
      CloseSqlConnection();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool CheckCoreSessionAgent(ref QebUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.QebUserGuid))
    {
      OpenSqlConnection();
      OpenSqlCommand("CoreSessionAgentCheck");
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityAppGuid", PDPSS.AppSecureUiaaGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@SessionValueIsRequired", qurc.QebSessionValueIsRequired);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.QebSessionGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", ParameterDirection.InputOutput, qurc.QebUserGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.QebAgentGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentInfosetGuid", ParameterDirection.Output, qurc.QebAgentInfosetGuid);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.NVarChar, 64, "@AgentUserNameDisp", ParameterDirection.Output, qurc.QebUserNameDisplayed);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAuthor", ParameterDirection.Output);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsEditor", ParameterDirection.Output);
      PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAdmin", ParameterDirection.Output);
      int errorExists = PdpSql.ExecuteCommand(ref pdpDataCmmnd);
      if (errorExists == 0)
      {
        qurc.QebSessionGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
        qurc.QebUserGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@UserGuid");
        qurc.QebAgentGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentGuid");
        qurc.QebAgentInfosetGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentInfosetGuid");
        // next 3 properties implicit by existence of row in table
        // TODO: until or unless privilege revocation implemented
        qurc.ClientIsAuthenticated = true;
        qurc.ClientIsUser = true;
        qurc.ClientIsAgent = true;
        // next 4 properties must check column value
        qurc.QebUserNameDisplayed = PdpSql.GetChar(ref pdpDataCmmnd, "@AgentUserNameDisp");
        qurc.ClientIsAuthor = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsAuthor");
        qurc.ClientIsEditor = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsEditor");
        qurc.ClientIsAdmin = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsAdmin");
        // TODO: enhance PdsAgent table to enable revocation of agent privileges
      }
      CloseSqlConnection();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool AddRoleCoreSessionAgent(NamesForIdentityRoles role)
  {
    var roleAdded = false;
    var csa = this.CoreSessionAgents.Single(a => (a.AgentGuidKey == QURC.QebAgentGuid));
    switch (role)
    {
      case NamesForIdentityRoles.NpdsAuthor:
        csa.AgentIsAuthor = true; // secure default with false
        break;
      case NamesForIdentityRoles.NpdsEditor:
        csa.AgentIsEditor = true; // secure default with false
        break;
      case NamesForIdentityRoles.NpdsAdmin:
        csa.AgentIsAdmin = true; // secure default with false
        break;
      default:
        throw new InvalidEnumArgumentException();
    }
    try
    {
      var errorCode = CoreSessionAgentAddRole(csa.IdentityUserGuidRef,
        csa.AgentGuidKey, csa.AgentIsAuthor, csa.AgentIsEditor, csa.AgentIsAdmin);
      roleAdded = true;
    }
    catch (Exception ex) { var error = ex.Message; }
    return roleAdded;
  }

} // end class

// end file