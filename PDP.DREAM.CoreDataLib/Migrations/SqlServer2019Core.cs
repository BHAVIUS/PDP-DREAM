// SqlServer2019Core.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Text;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Migrations;

public static class SqlServer2019Core
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
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then should also have schema 
    string errorMessage = string.Empty;
    bool dbIsValid = false;
    try
    {
      var dbCntxt = new CoreDbsqlContext(dbCnstr);
      dbIsValid = (dbCntxt.Database.CanConnect() && (dbCntxt.PdcSqlconn != null));
      dbCntxt.CloseSqlConnection();
    }
    catch (SqlException exc)
    {
      errorMessage = PdpStatus.SqlErrorMessage(exc);
    }
    // TODO: implement check on existence of DB schema
    dbHasSchema = true; // force reset until schema check coded
    // final check for valid database
    dbIsValid = (dbExists && dbHasSchema && (errorMessage == string.Empty));
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
      dbCntxt = new CoreDbsqlContext(NpdsServiceDefaults.Values.NpdsCoreDbconstr);
    }
    catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }

    // assume database does not exist when cannot connect
    if (!dbCntxt.Database.CanConnect())
    {
      // userCntxt.Database.Migrate(); // create database with migrations ? problems! does not work ?!?
      // userCntxt.Database.EnsureCreated(); // add schema to database if not yet added problems! does not work ?!?
      // TODO: move hardcoded strings to options / appsettings
      var fileInfo = fileProv.GetFileInfo(@"\Migrations\CreatePdpCore10.sql");
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      var mstrDbconstr = NpdsServiceDefaults.Values.NpdsCoreDbconstr
        .Replace("PdpCore10", "master");
      errorMessage = PdpSql.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = new CoreDbsqlContext(NpdsServiceDefaults.Values.NpdsCoreDbconstr);
      }
      catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }
    }

    // final check for valid database
    dbValid = (dbExists && dbHasSeededSchema && (errorMessage == string.Empty));
    return dbValid;
  }

} // class
