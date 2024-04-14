// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebiDalContext
{
  public bool EditSessionQebiUser(ref NpdsClientWrace wrace)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.CiaamAppGuid) && !PdpGuid.IsInvalidGuid(wrace.QebiUserGuid))
    {
      using (var dbsqlCnctn = new SqlConnection(NPDSDC.DbcnstrQebi))
      {
        dbsqlCnctn.Open();
        var dbsqlCmmnd = OpenSqlCommand("QebiUserSessionEdit", dbsqlCnctn);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@AppGuid", ParameterDirection.Input, PDPSS.CiaamAppGuid);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@UserGuid", ParameterDirection.Input, wrace.QebiUserGuid);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@UserAlias", ParameterDirection.Input, wrace.CiaamUserAlias);
        int errorExists = QebSqlLinq.ExecuteCommand(ref dbsqlCmmnd);
        if (errorExists == 0)
        {
          wrace.CiaamUserAlias = QebSqlLinq.GetChar(ref dbsqlCmmnd, "@UserAlias");
        }
        dbsqlCnctn.Close();
        if (errorExists == 0) { return true; }
      }
    }
    return false;
  }

  public bool CheckSessionQebiUser(ref NpdsClientWrace wrace)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.CiaamAppGuid) && !PdpGuid.IsInvalidGuid(wrace.QebiUserGuid))
    {
      using (var dbsqlCnctn = new SqlConnection(NPDSDC.DbcnstrQebi))
      {
        dbsqlCnctn.Open();
        var dbsqlCmmnd = OpenSqlCommand("QebiUserSessionCheck", dbsqlCnctn);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@SessionIsRequired", ParameterDirection.Input, wrace.SessionClientRequired);
        // TODO: on next iteration
        // make sure UserGuid unique across all apps, then can simplify by removing appguid
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@AppGuid", ParameterDirection.Input, PDPSS.CiaamAppGuid);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@UserGuid", ParameterDirection.Input, wrace.QebiUserGuid);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@UserAlias", ParameterDirection.Output);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@UserIsPerson", ParameterDirection.Output);
        QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@UserIsAgent", ParameterDirection.Output);
        int errorExists = QebSqlLinq.ExecuteCommand(ref dbsqlCmmnd);
        if (errorExists == 0)
        {
          // wrace.CiaamSessionGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@SessionGuid");
          // wrace.CiaamUserGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@UserGuid");
          // wrace.NpdsAgentGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@AgentGuid");
          // next 2 properties implicit by existence of row in table
          // TODO: until or unless privilege revocation implemented
          wrace.ClientIsAuthenticated = true;
          wrace.ClientIsUser = true;
          // next 4 properties must check column value
          wrace.CiaamUserAlias = QebSqlLinq.GetChar(ref dbsqlCmmnd, "@UserAlias");
          wrace.ClientIsPerson = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@UserIsPerson");
          wrace.ClientIsAgent = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@UserIsAgent");
          // TODO: enhance CiaaNetUser table to enable revocation of privileges
        }
        dbsqlCnctn.Close();
#if DEBUG
        wrace.DebugClientAccess("CheckSessionQebiUser", "QebiDbsqlContext");
#endif
        if (errorExists == 0) { return true; }
      }
    }
    return false;
  }

} // end class

// end file