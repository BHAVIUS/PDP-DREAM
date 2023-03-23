// SqlServer2019Siaa.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Migrations;

public static class SqlServer2022Siaa
{
  public static bool CheckDatabase(QebiDbsqlContext dbCntxt)
  {
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then should also have schema 
    string errorMessage = string.Empty;
    bool dbIsValid = false;
    try
    {
      var dbCnstr = dbCntxt.ContextCnstr();
      dbExists = dbCntxt.Database.CanConnect();
      var dbCnctn = dbCntxt.DbsqlConnect();
      dbHasSchema = dbCntxt.ContextHasSchema();
      dbCntxt.DbsqlDisconnect();
    }
    catch (SqlException exc)
    {
      errorMessage = SqlErrorMessage(exc);
    }
    // final check for valid database
    dbIsValid = (dbExists && dbHasSchema && string.IsNullOrEmpty(errorMessage));
    return dbIsValid;
  }

  public static bool CreateDatabase(string dbCnstr, IFileProvider fileProv)
  {
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then must have schema and initial seed data
    string errorMessage = string.Empty;
    bool dbValid = false;

    QebiDbsqlContext? dbCntxt = null;

    try
    {
      dbCntxt = new QebiDbsqlContext();
    }
    catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }

    // assume database does not exist when cannot connect
    if (!dbCntxt.Database.CanConnect())
    {
      // userCntxt.Database.Migrate(); // create database with migrations ? problems! does not work ?!?
      // userCntxt.Database.EnsureCreated(); // add schema to database if not yet added problems! does not work ?!?
      // TODO: move hardcoded strings to options / appsettings
      var fileInfo = fileProv.GetFileInfo(@"\Migrations\CreatePdpSiaa10Gangkhar.sql");
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      var mstrDbconstr = NPDSSD.QebiDbconstr
        .Replace("PdpSiaa10Gangkhar", "master");
      errorMessage = QebSql.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = new QebiDbsqlContext();
        dbExists = dbCntxt.Database.CanConnect();
        dbHasSchema = QebSql.ContextHasSchema(dbCntxt);
      }
      catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }
    }

    // assume possibly empty database does exist when can connect
    if (dbCntxt.Database.CanConnect())
    {
      dbExists = true;
      Guid? appSiaaGuid, appGuid, rolGuid, usrGuid;
      var appSiaaName = PDPSS.AppSecureUiaaName;
      appSiaaGuid = PDPSS.AppSecureUiaaGuid;
      if (dbCntxt.GetAppByAppName(appSiaaName) == null)
      {
        appSiaaGuid = null;
        appGuid = Guid.NewGuid();
        var appDesc = DescribeApp(appSiaaName);
        dbCntxt.QebIdentityAppEdit(appGuid, appSiaaName, appDesc);
      }
      appGuid = dbCntxt.GetAppByAppName(appSiaaName)?.AppGuidKey;
      if ((appGuid == null) || (appGuid == Guid.Empty)) { throw new NullReferenceException(); }
      else if ((appSiaaGuid == null) || (appSiaaGuid == Guid.Empty)) { PDPSS.AppSecureUiaaGuid = (Guid)appGuid; }
      if (dbCntxt.GetAppRolesByAppGuid().Count == 0)
      {
        var rolDesc = string.Empty;
        foreach (string rolName in Enum.GetNames(typeof(NamesForClientRoles)))
        {
          rolGuid = Guid.NewGuid();
          rolDesc = DescribeRole(rolName);
          dbCntxt.QebIdentityAppRoleEdit(appGuid, rolGuid, rolName, rolDesc);
        }
      }
      if (dbCntxt.GetAppUsersByAppGuid().Count == 0)
      {
        usrGuid = Guid.NewGuid();
        dbCntxt.QebIdentityAppUserEdit(appGuid, usrGuid,
          "PdpAdminFirstName", "PdpAdminLastName", "PdpAdminUserName", PDPSS.AppHostEmail);
        foreach (string rolName in Enum.GetNames(typeof(NamesForClientRoles)))
        {
          rolGuid = dbCntxt.GetAppRoleGuidByRoleName(rolName, appGuid);
          dbCntxt.QebIdentityAppLinkEdit(Guid.NewGuid(), usrGuid, rolGuid, appGuid);
        }
      }
    }

    // TODO: create separate method to check for SeededSchema
    // recheck database for SeededSchema
    try
    {
      dbCntxt = new QebiDbsqlContext();
      if (dbCntxt.QebIdentityApps.Any() && dbCntxt.QebIdentityAppRoles.Any() &&
        dbCntxt.QebIdentityAppUsers.Any()) { dbHasSchema = true; }
    }
    catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }

    // final check for valid database
    dbValid = (dbExists && dbHasSchema && (errorMessage == string.Empty));
    return dbValid;
  }

  private static string DescribeApp(string appName)
  {
    return $"SIAA database seeded with PDP AppSecureUiaaName = '{appName}'";
  }

  private static string DescribeRole(string roleName)
  {
    return $"SIAA database seeded with PDP AppUserRole '{roleName}'";
  }

} // end class

// end file