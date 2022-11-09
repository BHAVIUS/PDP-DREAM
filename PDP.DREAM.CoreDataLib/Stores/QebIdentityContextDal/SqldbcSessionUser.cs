// SqldbcSessionUser.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Data;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

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
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AppGuid", PDPSS.AppSecureUiaaGuid);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", qurc.QebUserGuid);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.QebSessionGuid);
      int errorExists = QebSql.ExecuteCommand(ref pdpDataCmmnd);
      if (errorExists == 0)
      {
        qurc.QebSessionGuid = QebSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
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
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AppGuid", PDPSS.AppSecureUiaaGuid);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@SessionValueIsRequired", qurc.QebSessionValueIsRequired);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, qurc.QebSessionGuid);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", ParameterDirection.InputOutput, qurc.QebUserGuid);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, qurc.QebAgentGuid);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.NVarChar, 64, "@UserNameDisp", ParameterDirection.Output, qurc.QebUserNameDisplayed);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@UserIsAgent", ParameterDirection.Output);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAuthor", ParameterDirection.Output);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsEditor", ParameterDirection.Output);
      QebSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAdmin", ParameterDirection.Output);
      int errorExists = QebSql.ExecuteCommand(ref pdpDataCmmnd);
      if (errorExists == 0)
      {
        qurc.QebSessionGuid = QebSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
        qurc.QebUserGuid = QebSql.GetGuid(ref pdpDataCmmnd, "@UserGuid");
        qurc.QebAgentGuid = QebSql.GetGuid(ref pdpDataCmmnd, "@AgentGuid");
        // next 2 properties implicit by existence of row in table
        // TODO: until or unless privilege revocation implemented
        qurc.ClientIsAuthenticated = true;
        qurc.ClientIsUser = true;
        // next 4 properties must check column value
        qurc.QebUserNameDisplayed = QebSql.GetChar(ref pdpDataCmmnd, "@UserNameDisp");
        qurc.ClientIsAgent = QebSql.GetBit(ref pdpDataCmmnd, "@UserIsAgent");
        qurc.ClientIsAuthor = QebSql.GetBit(ref pdpDataCmmnd, "@AgentIsAuthor");
        qurc.ClientIsEditor = QebSql.GetBit(ref pdpDataCmmnd, "@AgentIsEditor");
        qurc.ClientIsAdmin = QebSql.GetBit(ref pdpDataCmmnd, "@AgentIsAdmin");
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
  // public bool NpdsUserSessionAddRole(NamesForIdentityRoles role) { }

} // end class

// end file