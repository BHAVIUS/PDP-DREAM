// SqlServer2019Core.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Utilities;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Migrations;

public static class SqlServer2019Core
{
  public static bool CheckDatabase(SqlConnection dbConn)
  {
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then should also have schema 
    string errorMessage = string.Empty;
    bool dbIsValid = false;
    try
    {
      var dbCntxt = new CoreDbsqlContext(dbConn);
      dbExists = dbCntxt.Database.CanConnect();
      var openMsg = dbCntxt.OpenSqlConnection();
      dbHasSchema = dbCntxt.DataContextHasSchema();
      var closeMsg = dbCntxt.CloseSqlConnection();
      if ((openMsg == string.Empty) && (closeMsg == string.Empty))
      { dbExists = true; }
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
    bool dbHasSeededSchema = false; // if true, then must have schema and initial seed data
    string errorMessage = string.Empty;
    bool dbValid = false;

    var dbCntxt = new CoreDbsqlContext(dbCnstr);

    try
    {
      dbCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr);
    }
    catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }

    // assume database does not exist when cannot connect
    if (!dbCntxt.Database.CanConnect())
    {
      // userCntxt.Database.Migrate(); // create database with migrations ? problems! does not work ?!?
      // userCntxt.Database.EnsureCreated(); // add schema to database if not yet added problems! does not work ?!?
      // TODO: move hardcoded strings to options / appsettings
      var fileInfo = fileProv.GetFileInfo(@"\Migrations\CreatePdpCore10.sql");
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      var mstrDbconstr = NPDSSD.NpdsCoreDbconstr
        .Replace("PdpCore10", "master");
      errorMessage = PdpSql.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = new CoreDbsqlContext(NPDSSD.NpdsCoreDbconstr);
      }
      catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }
    }

    // final check for valid database
    dbValid = (dbExists && dbHasSeededSchema && (errorMessage == string.Empty));
    return dbValid;
  }

} // end class

// end file