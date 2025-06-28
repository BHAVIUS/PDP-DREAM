// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.BuildDevEnv.Migrations;

public static class SqlServer2022Qebi
{
  // TODO: build a function that converts
  // from NpdsDatabaseType to corresponding derived PdpDbsqlContext
  // analogous to
  // public static bool CheckDatabase(NpdsDatabaseType dbType)

  public static bool CreateDatabase(string mstrDbconstr, string fileName, IFileProvider fileProv)
  {
    // mstrDbconstr should be connection string to master database in SqlServer 2022
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then must have schema and initial seed data
    string errorMessage = ESS;
    bool dbValid = false;

    QebiDbsqlContext? dbCntxt = null;

    try
    {
      dbCntxt = new QebiDbsqlContext();
    }
    catch (SqlException exc)
    {
      errorMessage = SqlErrorMessage(exc);
    }
    DbContext dbCntxtBase = dbCntxt.ToDbContext<QebiDbsqlContext>();
    // assume database does not exist when cannot connect
    if (!dbCntxtBase.ContextCanConnect())
    {
      var fileInfo = fileProv.GetFileInfo(fileName);
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      errorMessage = QebSqlLinq.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxtBase = dbCntxt.ToDbContext<QebiDbsqlContext>();
        dbExists = dbCntxtBase.ContextCanConnect();
        dbHasSchema = dbCntxtBase.ContextHasSchema();
      }
      catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }
    }

    // assume possibly empty database does exist when can connect
    if (dbCntxt.Database.CanConnect())
    {
      dbExists = true;
      Guid? appCiaamGuid, appGuid, rolGuid, usrGuid;
      var appCiaamName = PDPSS.AppSecureCiaamName;
      appCiaamGuid = PDPSS.CiaamAppGuid;
      if (dbCntxt.GetAppByAppName(appCiaamName) == null)
      {
        appCiaamGuid = null;
        appGuid = PdpNewGuid();
        var appDesc = DescribeApp(appCiaamName);
        dbCntxt.QebiAppEdit(appGuid, appCiaamName, appDesc);
      }
      appGuid = dbCntxt.GetAppByAppName(appCiaamName)?.AppGuid;
      if ((appGuid == null) || (appGuid == EGS)) { throw new NullReferenceException(); }
      else if ((appCiaamGuid == null) || (appCiaamGuid == EGS)) { PDPSS.CiaamAppGuid = (Guid)appGuid; }
      if (dbCntxt.GetAppRolesByAppGuid().Count == 0)
      {
        Byte? rolCode = 0;
        // TODO: this code block must be rebuilt for dynamic db enumerators
        foreach (string rolName in Enum.GetNames(typeof(DdeRoleType)))
        {
          rolGuid = PdpNewGuid();
          rolCode = dbCntxt.GetQebiRoleCodeByRoleName(rolName, appGuid);
          dbCntxt.QebiAppRoleEdit(appGuid, rolGuid, rolCode, ESS);
        }
      }
      if (dbCntxt.GetAppUsersByAppGuid().Count == 0)
      {
        usrGuid = PdpNewGuid();
        dbCntxt.QebiUserEdit(appGuid, usrGuid,
          "PdpAdminFirstName", "PdpAdminLastName", "PdpAdminUserName", PDPSS.AppHostEmail);
        foreach (string rolName in Enum.GetNames(typeof(DdeRoleType)))
        {
          rolGuid = dbCntxt.GetQebiRoleGuidByRoleName(rolName, appGuid);
          dbCntxt.QebiLinkEdit(PdpNewGuid(), appGuid, usrGuid, rolGuid);
        }
      }
    }

    // TODO: create separate method to check for SeededSchema
    // recheck database for SeededSchema
    try
    {
      dbCntxt = new QebiDbsqlContext();
      if (dbCntxt.QebiApps.Any() && dbCntxt.QebiAppRoles.Any() &&
        dbCntxt.QebiUsers.Any()) { dbHasSchema = true; }
    }
    catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }

    // final check for valid database
    dbValid = (dbExists && dbHasSchema && (errorMessage == ESS));
    return dbValid;
  }

  private static string DescribeApp(string appName)
  {
    return $"CIAAM database seeded with PDP AppSecureCiaamName = '{appName}'";
  }

  private static string DescribeRole(string roleName)
  {
    return $"CIAAM database seeded with PDP AppUserRole '{roleName}'";
  }

} // end class

// end file