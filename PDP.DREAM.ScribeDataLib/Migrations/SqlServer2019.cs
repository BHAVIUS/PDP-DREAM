// SqlServer2019.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeDataLib.Migrations;

public static class SqlServer2019
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
      var dbCntxt = new ScribeDbsqlContext(dbCnstr);
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
    dbIsValid = (dbHasSchema && (errorMessage == string.Empty));
    return dbIsValid;
  }

  public static bool CreateDatabase(string dbCnstr, IFileProvider fileProv)
  {
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSeededSchema = false; // if true, then must have schema and initial seed data
    string errorMessage = string.Empty;
    bool dbValid = false;

    var dbCntxt = new ScribeDbsqlContext(dbCnstr);

    try
    {
      dbCntxt = new ScribeDbsqlContext(NpdsServiceDefaults.Values.NpdsRegistrarDbconstr);
    }
    catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }

    // assume database does not exist when cannot connect
    if (!dbCntxt.Database.CanConnect())
    {
      // userCntxt.Database.Migrate(); // create database with migrations ? problems! does not work ?!?
      // userCntxt.Database.EnsureCreated(); // add schema to database if not yet added problems! does not work ?!?
      // TODO: move hardcoded strings to options / appsettings
      var fileInfo = fileProv.GetFileInfo(@"\Migrations\CreatePdpScribe10.sql");
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      var mstrDbconstr = NpdsServiceDefaults.Values.NpdsRegistrarDbconstr
        .Replace("PdpScribe10", "master");
      errorMessage = PdpSql.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = new ScribeDbsqlContext(NpdsServiceDefaults.Values.NpdsRegistrarDbconstr);
      }
      catch (SqlException exc) { errorMessage = PdpStatus.SqlErrorMessage(exc); }
    }

    // final check for valid database
    dbValid = (dbExists && dbHasSeededSchema && (errorMessage == string.Empty));
    return dbValid;
  }

} // class
