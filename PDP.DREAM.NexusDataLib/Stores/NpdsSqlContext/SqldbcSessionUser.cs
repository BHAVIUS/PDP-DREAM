// SqldbcAgentSession.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.NexusDataLib.Stores
{
  public partial class NexusDbsqlContext
  {
    public bool EditCoreUserSession(ref PdpRestContext prc)
    {
      if (!PdpGuid.IsInvalidGuid(pdpSitSets.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(prc.UserGuid))
      {
        OpenSqlConnection();
        OpenSqlCommand("CoreUserSessionEdit");
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityAppGuid", pdpSitSets.AppSecureUiaaGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityUserGuid", prc.UserGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.NVarChar, 64, "@IdentityUserNameDisp", prc.UserNameDisplayed);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, prc.AgentGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, prc.SessionGuid);
        int errorExists = PdpSql.ExecuteCommand(ref pdpDataCmmnd);
        // may have same PdpAgentGuid associated with different AspnetUserGuid
        //   and/or may have same AspnetUserGuid associated with different AspnetSystemIid
        if (errorExists == 0)
        {
          prc.AgentGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentGuid");
          prc.SessionGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
        }
        CloseSqlConnection();
        if (errorExists == 0) { return true; }
      }
      return false;
    }

    public bool CheckCoreUserSession(ref PdpRestContext prc)
    {
      if (!PdpGuid.IsInvalidGuid(pdpSitSets.AppSecureUiaaGuid) && !PdpGuid.IsInvalidGuid(prc.UserGuid))
      {
        OpenSqlConnection();
        OpenSqlCommand("CoreUserSessionCheck");
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@IdentityAppGuid", pdpSitSets.AppSecureUiaaGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@SessionValueIsRequired", prc.SessionValueIsRequired);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@SessionGuid", ParameterDirection.InputOutput, prc.SessionGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@UserGuid", ParameterDirection.InputOutput, prc.UserGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentGuid", ParameterDirection.InputOutput, prc.AgentGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.UniqueIdentifier, 38, "@AgentInfosetGuid", ParameterDirection.Output, prc.AgentInfosetGuid);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.NVarChar, 64, "@AgentUserNameDisp", ParameterDirection.Output, prc.UserNameDisplayed);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAuthor", ParameterDirection.Output);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsEditor", ParameterDirection.Output);
        PdpSql.AddParameter(ref pdpDataCmmnd, SqlDbType.Bit, 1, "@AgentIsAdmin", ParameterDirection.Output);
        int errorExists = PdpSql.ExecuteCommand(ref pdpDataCmmnd);
        if (errorExists == 0)
        {
          prc.SessionGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@SessionGuid");
          prc.UserGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@UserGuid");
          prc.AgentGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentGuid");
          prc.AgentInfosetGuid = PdpSql.GetGuid(ref pdpDataCmmnd, "@AgentInfosetGuid");
          // next 3 properties implicit by existence of row in table
          // TODO: until or unless privilege revocation implemented
          prc.ClientIsAuthenticated = true;
          prc.ClientIsUser = true;
          prc.ClientIsAgent = true;
          // next 4 properties must check column value
          prc.UserNameDisplayed = PdpSql.GetChar(ref pdpDataCmmnd, "@AgentUserNameDisp");
          prc.ClientIsAuthor = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsAuthor");
          prc.ClientIsEditor = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsEditor");
          prc.ClientIsAdmin = PdpSql.GetBit(ref pdpDataCmmnd, "@AgentIsAdmin");
          // TODO: enhance PdsAgent table to enable revocation of agent privileges
        }
        CloseSqlConnection();
        if (errorExists == 0) { return true; }
      }
      return false;
    }

    public bool PdpCoreUserSessionAddRole(PdpConst.IdentityRoleNames role)
    {
      var roleAdded = false;
      //var nsa = this.CoreSessionUsers.Single(s => (s.AgentGuidKey == PRC.AgentGuid));
      //switch (role)
      //{
      //  case PdpConst.IdentityRoleNames.NpdsAuthor:
      //    nsa.AgentIsAuthor = true; // secure default with false
      //    break;
      //  case PdpConst.IdentityRoleNames.NpdsEditor:
      //    nsa.AgentIsEditor = true; // secure default with false
      //    break;
      //  case PdpConst.IdentityRoleNames.NpdsAdmin:
      //    nsa.AgentIsAdmin = true; // secure default with false
      //    break;
      //  default:
      //    throw new InvalidEnumArgumentException();
      //}
      //try
      //{
      //  var errorCode = CoreUserSessionAddRole(nsa.IdentityUserGuidRef,
      //    nsa.AgentGuidKey, nsa.AgentIsAuthor, nsa.AgentIsEditor, nsa.AgentIsAdmin);
      //  roleAdded = true;
      //}
      //catch (Exception ex) { var error = ex.Message; }
      return roleAdded;
    }

  } // class

} // namespace
