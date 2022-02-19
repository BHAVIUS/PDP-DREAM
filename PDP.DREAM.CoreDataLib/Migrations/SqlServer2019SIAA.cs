// SqlServer2019SIAA.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Migrations;

public static class SqlServer2019Siaa
{
  public static bool CheckConnection(string dbcString)
  {
    var dbcValid = false;
    var dbcBldr = new SqlConnectionStringBuilder(dbcString);
    var dbcObject = new SqlConnection(dbcBldr.ConnectionString);
    try
    {
      dbcObject.Open();
      dbcValid = true;
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"{ex.Number} = {ex.Message}\n");
      throw;
    }
    dbcObject.Close();
    return dbcValid;
  }

  public static bool CheckDatabase(string dbCnstr)
  {
    bool dbHasSchema = false; // if true, then should also have schema 
    string errorMessage = string.Empty;
    bool dbIsValid = false;
    try
    {
      var dbCntxt = new QebIdentityContext(dbCnstr);
      // TODO: create general purpose PDP Data Context that inherits from DbContext
      //    and handles general purpose DbContext utility service methods
      //   a la PdpSiteDbContext in  PDP.DREAM.CoreDataLib.Types
      //   could possibly use that class type for general use
      //dbIsValid = (dbCntxt.Database.CanConnect() && (dbCntxt.PdcSqlconn != null));
      //dbCntxt.CloseSqlConnection();
      dbIsValid = (dbCntxt.Database.CanConnect());
    }
    catch (SqlException exc)
    {
      errorMessage = PdpStatus.SqlErrorMessage(exc);
    }
    // TODO: implement check on existence of DB schema
    dbHasSchema = true; // force reset until schema check coded
    // final check for valid database
    dbIsValid = (dbHasSchema && (errorMessage == string.Empty));
    return dbIsValid;
  }

  public static bool CreateDatabase(string dbCnstr, IFileProvider fileProv)
  {
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSeededSchema = false; // if true, then must have schema and initial seed data
    string errorMessage = string.Empty;
    bool dbValid = false;

    var dbCntxt = new QebIdentityContext(dbCnstr);

    try
    {
      dbCntxt = new QebIdentityContext(NpdsServiceDefaults.Values.NpdsUserDbconstr);
    }
    catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }

    // assume database does not exist when cannot connect
    if (!dbCntxt.Database.CanConnect())
    {
      // userCntxt.Database.Migrate(); // create database with migrations ? problems! does not work ?!?
      // userCntxt.Database.EnsureCreated(); // add schema to database if not yet added problems! does not work ?!?
      // TODO: move hardcoded strings to options / appsettings
      var fileInfo = fileProv.GetFileInfo(@"\Migrations\CreatePdpSiaaTahtali.sql");
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      var mstrDbconstr = NpdsServiceDefaults.Values.NpdsUserDbconstr
        .Replace("PdpSiaaTahtali", "master");
      errorMessage = PdpSql.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = new QebIdentityContext(NpdsServiceDefaults.Values.NpdsUserDbconstr);
      }
      catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }
    }

    // assume possibly empty database does exist when can connect
    if (dbCntxt.Database.CanConnect())
    {
      dbExists = true;
      Guid? appSiaaGuid, appGuid, rolGuid, usrGuid;
      var appSiaaName = PdpSiteSettings.Values.AppSecureUiaaName;
      appSiaaGuid = PdpSiteSettings.Values.AppSecureUiaaGuid;
      if (dbCntxt.GetAppByAppName(appSiaaName) == null)
      {
        appSiaaGuid = null;
        appGuid = Guid.NewGuid();
        var appDesc = DescribeApp(appSiaaName);
        dbCntxt.QebIdentityAppEdit(appGuid, appSiaaName, appDesc);
      }
      appGuid = dbCntxt.GetAppByAppName(appSiaaName)?.AppGuidKey;
      if ((appGuid == null) || (appGuid == Guid.Empty)) { throw new NullReferenceException(); }
      else if ((appSiaaGuid == null) || (appSiaaGuid == Guid.Empty)) { PdpSiteSettings.Values.AppSecureUiaaGuid = (Guid)appGuid; }
      if (dbCntxt.GetAppRolesByAppGuid(appGuid).Count == 0)
      {
        var rolDesc = string.Empty;
        foreach (string rolName in Enum.GetNames(typeof(PdpConst.IdentityRoleNames)))
        {
          rolGuid = Guid.NewGuid();
          rolDesc = DescribeRole(rolName);
          dbCntxt.QebIdentityAppRoleEdit(appGuid, rolGuid, rolName, rolDesc);
        }
      }
      if (dbCntxt.GetAppUsersByAppGuid(appGuid).Count == 0)
      {
        usrGuid = Guid.NewGuid();
        dbCntxt.QebIdentityAppUserEdit(appGuid, usrGuid,
          "PdpAdminFirstName", "PdpAdminLastName", "PdpAdminUserName",
          PdpSiteSettings.Values.AppHostEmail);
        foreach (string rolName in Enum.GetNames(typeof(PdpConst.IdentityRoleNames)))
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
      dbCntxt = new QebIdentityContext(NpdsServiceDefaults.Values.NpdsUserDbconstr);
      if (dbCntxt.QebIdentityApps.Any() && dbCntxt.QebIdentityAppRoles.Any() &&
        dbCntxt.QebIdentityAppUsers.Any()) { dbHasSeededSchema = true; }
    }
    catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }

    // final check for valid database
    dbValid = (dbExists && dbHasSeededSchema && (errorMessage == string.Empty));
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

} // class
