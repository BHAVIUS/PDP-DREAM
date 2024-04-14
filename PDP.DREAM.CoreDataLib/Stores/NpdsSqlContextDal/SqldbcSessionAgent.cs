// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public bool EditSessionNpdsAgent(ref NpdsClientWrace wrace)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.CiaamAppGuid) && !PdpGuid.IsInvalidGuid(wrace.QebiUserGuid))
    {
      dbsqlCnctn = DbsqlConnect();
      var dbsqlCmmnd = OpenSqlCommand("CoreAgentSessionEdit", dbsqlCnctn);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@CiaamUserGuid", ParameterDirection.Input, wrace.QebiUserGuid);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@CiaamUserAlias", ParameterDirection.Input, wrace.CiaamUserAlias);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@AgentGuid", ParameterDirection.InputOutput, wrace.NpdsAgentGuid);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsAuthor", ParameterDirection.InputOutput, wrace.ClientIsAuthor);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsReviewer", ParameterDirection.InputOutput, wrace.ClientIsReviewer);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsEditor", ParameterDirection.InputOutput, wrace.ClientIsEditor);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsAdmin", ParameterDirection.InputOutput, wrace.ClientIsAdmin);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.DateTime2, "@DateAccessed", ParameterDirection.Output);
      int errorExists = QebSqlLinq.ExecuteCommand(ref dbsqlCmmnd);
      // may have same PdpAgentGuid associated with different AspnetUserGuid
      //   and/or may have same AspnetUserGuid associated with different AspnetSystemIid
      if (errorExists == 0)
      {
        wrace.NpdsAgentGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@AgentGuid");
        wrace.ClientIsAuthor = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsAuthor");
        wrace.ClientIsReviewer = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsReviewer");
        wrace.ClientIsEditor = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsEditor");
        wrace.ClientIsAdmin = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsAdmin");
#if DEBUG
        var dateAccessed = QebSqlLinq.GetDateTime(ref dbsqlCmmnd, "@DateAccessed");
        Debug.WriteLine($"NpdsCoreSessionAgent for {wrace.CiaamUserAlias} edited on {dateAccessed}");
        Debug.WriteLine($"NpdsAgentGuid {wrace.NpdsAgentGuid} with isAuthor = {wrace.ClientIsAuthor},");
        Debug.WriteLine($"isReviewer = {wrace.ClientIsReviewer}, isAdmin = {wrace.ClientIsAdmin}");
#endif
      }
      DbsqlDisconnect();
      if (errorExists == 0) { return true; }
    }
    return false;
  }

  public bool CheckSessionNpdsAgent(ref NpdsClientWrace wrace)
  {
    if (!PdpGuid.IsInvalidGuid(PDPSS.CiaamAppGuid) && !PdpGuid.IsInvalidGuid(wrace.QebiUserGuid))
    {
      dbsqlCnctn = DbsqlConnect();
      var dbsqlCmmnd = OpenSqlCommand("CoreAgentSessionCheck", dbsqlCnctn);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@SessionIsRequired", ParameterDirection.Input, wrace.SessionClientRequired);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@CiaamUserGuid", ParameterDirection.Input, wrace.QebiUserGuid);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.NVarChar, 64, "@CiaamUserAlias", ParameterDirection.Output);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@AgentGuid", ParameterDirection.Output);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.UniqueIdentifier, "@AgentInfosetGuid", ParameterDirection.Output);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsAuthor", ParameterDirection.Output);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsReviewer", ParameterDirection.Output);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsEditor", ParameterDirection.Output);
      QebSqlLinq.AddParameter(ref dbsqlCmmnd, SqlDbType.Bit, "@AgentIsAdmin", ParameterDirection.Output);
      int errorExists = QebSqlLinq.ExecuteCommand(ref dbsqlCmmnd);
      if (errorExists == 0)
      {
       // wrace.CiaamSessionGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@SessionGuid");
       // wrace.CiaamUserGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@UserGuid");
        wrace.NpdsAgentGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@AgentGuid");
        wrace.NpdsAgentInfosetGuid = QebSqlLinq.GetGuid(ref dbsqlCmmnd, "@AgentInfosetGuid");
        // next 3 properties implicit by existence of row in table
        // TODO: until or unless privilege revocation implemented
        wrace.ClientIsAuthenticated = true;
        wrace.ClientIsUser = true;
        wrace.ClientIsAgent = true;
        // next 4 properties must check column value
        wrace.CiaamUserAlias = QebSqlLinq.GetChar(ref dbsqlCmmnd, "@CiaamUserAlias");
        wrace.ClientIsAuthor = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsAuthor");
        wrace.ClientIsReviewer = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsReviewer");
        wrace.ClientIsEditor = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsEditor");
        wrace.ClientIsAdmin = QebSqlLinq.GetBit(ref dbsqlCmmnd, "@AgentIsAdmin");
        // TODO: enhance CiaaNetUser table to enable revocation of privileges
      }
      DbsqlDisconnect();
#if DEBUG
      wrace.DebugClientAccess("CheckSessionNpdsAgent", "CoreDbsqlContext");
#endif
      if (errorExists == 0) { return true; }
    }
    return false;
  }

} // end class

// end file