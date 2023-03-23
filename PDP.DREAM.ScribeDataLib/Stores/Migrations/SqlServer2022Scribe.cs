// SqlServer2019Scribe.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Migrations;

public static class SqlServer2022Scribe
{
  public static bool CheckDatabase(ScribeDbsqlContext? dbCntxt)
  {
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then should also have schema 
    string errorMessage = string.Empty;
    bool dbIsValid = false;
    try
    {
      dbExists = dbCntxt.Database.CanConnect();
      dbHasSchema = QebSql.ContextHasSchema(dbCntxt);
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

    ScribeDbsqlContext? dbCntxt = null;

    try
    {
      dbCntxt = new ScribeDbsqlContext();
    }
    catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }

    // assume database does not exist when cannot connect
    if (!dbCntxt.Database.CanConnect())
    {
      // userCntxt.Database.Migrate(); // create database with migrations ? problems! does not work ?!?
      // userCntxt.Database.EnsureCreated(); // add schema to database if not yet added problems! does not work ?!?
      // TODO: move hardcoded strings to options / appsettings
      var fileInfo = fileProv.GetFileInfo(@"\Migrations\CreatePdpScribe10Gangkhar.sql");
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      var mstrDbconstr = NPDSSD.ScribeDbconstr
        .Replace("PdpScribe10Tahtli", "master");
      errorMessage = QebSql.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = new ScribeDbsqlContext();
        dbExists = dbCntxt.Database.CanConnect();
        dbHasSchema = QebSql.ContextHasSchema(dbCntxt);
      }
      catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }
    }

    // final check for valid database
    dbValid = (dbExists && dbHasSchema && (errorMessage == string.Empty));
    return dbValid;
  }

} // end class

// end file