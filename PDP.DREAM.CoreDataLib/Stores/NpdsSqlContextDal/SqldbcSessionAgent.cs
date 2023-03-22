// SqldbcSessionAgent.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public bool EditCoreSessionAgent(ref QebiUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.ClientUserGuid))
    {
      dbsqlCnctn = DbsqlConnect();
      var dbsqlCmmnd = OpenSqlCommand("CoreSessionAgentEdit", dbsqlCnctn);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityAppGuid", PDPSS.AppSecureUiaaGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityUserGuid", qurc.ClientUserGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@IdentityUserNameDisp", qurc.ClientUserNameDisplayed);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.ClientAgentGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.ClientSessionGuid);
      int errorExists = QebSql.ExecuteCommand(ref dbsqlCmmnd);
      // may have same PdpAgentGuid associated with different AspnetUserGuid
      //   and/or may have same AspnetUserGuid associated with different AspnetSystemIid
      if (errorExists == 0)
      {
        qurc.ClientAgentGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@AgentGuid");
        qurc.ClientSessionGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@SessionGuid");
      }
      DbsqlDisconnect();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool CheckCoreSessionAgent(ref QebiUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.ClientUserGuid))
    {
      dbsqlCnctn = DbsqlConnect();
      var dbsqlCmmnd = OpenSqlCommand("CoreSessionAgentCheck", dbsqlCnctn);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityAppGuid", PDPSS.AppSecureUiaaGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@SessionValueIsRequired", qurc.SessionClientRequired);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.ClientSessionGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", ParameterDirection.InputOutput, qurc.ClientUserGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.ClientAgentGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentInfosetGuid", ParameterDirection.Output, qurc.ClientAgentInfosetGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@AgentUserNameDisp", ParameterDirection.Output, qurc.ClientUserNameDisplayed);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@AgentIsAuthor", ParameterDirection.Output);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@AgentIsEditor", ParameterDirection.Output);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@AgentIsAdmin", ParameterDirection.Output);
      int errorExists = QebSql.ExecuteCommand(ref dbsqlCmmnd);
      if (errorExists == 0)
      {
        qurc.ClientSessionGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@SessionGuid");
        qurc.ClientUserGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@UserGuid");
        qurc.ClientAgentGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@AgentGuid");
        qurc.ClientAgentInfosetGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@AgentInfosetGuid");
        // next 3 properties implicit by existence of row in table
        // TODO: until or unless privilege revocation implemented
        qurc.ClientIsAuthenticated = true;
        qurc.ClientIsUser = true;
        qurc.ClientIsAgent = true;
        // next 4 properties must check column value
        qurc.ClientUserNameDisplayed = QebSql.GetChar(ref dbsqlCmmnd, "@AgentUserNameDisp");
        qurc.ClientIsAuthor = QebSql.GetBit(ref dbsqlCmmnd, "@AgentIsAuthor");
        qurc.ClientIsEditor = QebSql.GetBit(ref dbsqlCmmnd, "@AgentIsEditor");
        qurc.ClientIsAdmin = QebSql.GetBit(ref dbsqlCmmnd, "@AgentIsAdmin");
        // TODO: enhance PdsAgent table to enable revocation of agent privileges
      }
      DbsqlDisconnect();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool AddRoleCoreSessionAgent(NamesForClientRoles role)
  {
    var roleAdded = false;
    var csa = this.CoreSessionAgents
      .Single(a => (a.AgentGuidKey == NPDSCP.ClientAgentGuid));
    switch (role)
    {
      case NamesForClientRoles.NpdsAuthor:
        csa.AgentIsAuthor = true; // secure default with false
        break;
      case NamesForClientRoles.NpdsEditor:
        csa.AgentIsEditor = true; // secure default with false
        break;
      case NamesForClientRoles.NpdsAdmin:
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