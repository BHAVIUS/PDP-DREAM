// SqldbcSessionUser.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebiDbsqlContext
{
  public bool EditQebiSessionUser(ref QebiUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.ClientUserGuid))
    {
      dbsqlCnctn = DbsqlConnect();
      var dbsqlCmmnd = OpenSqlCommand("QebiAppUserSessionEdit", dbsqlCnctn);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@AppGuid", PDPSS.AppSecureUiaaGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", qurc.ClientUserGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.ClientSessionGuid);
      int errorExists = QebSql.ExecuteCommand(ref dbsqlCmmnd);
      if (errorExists == 0)
      {
        qurc.ClientSessionGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@SessionGuid");
      }
      DbsqlDisconnect();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool CheckQebiSessionUser(ref QebiUserRestContext qurc)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(qurc.ClientUserGuid))
    {
      dbsqlCnctn = DbsqlConnect();
      var dbsqlCmmnd = OpenSqlCommand("QebiAppUserSessionCheck", dbsqlCnctn);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@AppGuid", PDPSS.AppSecureUiaaGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@SessionValueIsRequired", qurc.SessionClientRequired);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.ClientSessionGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", ParameterDirection.InputOutput, qurc.ClientUserGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.ClientAgentGuid);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@UserNameDisp", ParameterDirection.Output, qurc.ClientUserNameDisplayed);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@UserIsAgent", ParameterDirection.Output);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@AgentIsAuthor", ParameterDirection.Output);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@AgentIsEditor", ParameterDirection.Output);
      QebSql.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, 1, "@AgentIsAdmin", ParameterDirection.Output);
      int errorExists = QebSql.ExecuteCommand(ref dbsqlCmmnd);
      if (errorExists == 0)
      {
        qurc.ClientSessionGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@SessionGuid");
        qurc.ClientUserGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@UserGuid");
        qurc.ClientAgentGuid = QebSql.GetGuid(ref dbsqlCmmnd, "@AgentGuid");
        // next 2 properties implicit by existence of row in table
        // TODO: until or unless privilege revocation implemented
        qurc.ClientIsAuthenticated = true;
        qurc.ClientIsUser = true;
        // next 4 properties must check column value
        qurc.ClientUserNameDisplayed = QebSql.GetChar(ref dbsqlCmmnd, "@UserNameDisp");
        qurc.ClientIsAgent = QebSql.GetBit(ref dbsqlCmmnd, "@UserIsAgent");
        qurc.ClientIsAuthor = QebSql.GetBit(ref dbsqlCmmnd, "@AgentIsAuthor");
        qurc.ClientIsEditor = QebSql.GetBit(ref dbsqlCmmnd, "@AgentIsEditor");
        qurc.ClientIsAdmin = QebSql.GetBit(ref dbsqlCmmnd, "@AgentIsAdmin");
        // TODO: enhance SiaaNetUser table to enable revocation of privileges
      }
      DbsqlDisconnect();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  // ATTN: must make double-call from MVC action to both contexts
  // TODO: rebuild separately for CoreDbsqlContext
  // TODO: rebuild separately for QebIdentityContext
  // public bool NpdsUserSessionAddRole(NamesForIdentityRoles role) { }

} // end class

// end file