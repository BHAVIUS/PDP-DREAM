// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.BuildDevEnv.Migrations;

public static class SqlServer2022Npds
{
  // TODO: build a function that converts
  // from NpdsDatabaseType to corresponding derived PdpDbsqlContext
  // analogous to
  // public static bool CheckDatabase(NpdsDatabaseType dbType)

  public static bool CreateDatabase<Tdbc>(string mstrDbconstr, string fileName, IFileProvider fileProv)
  {
    // mstrDbconstr should be connection string to master database in SqlServer 2022
    bool dbExists = false; // if true, then may exist but may not have schema
    bool dbHasSchema = false; // if true, then must have schema and initial seed data
    string errorMessage = ESS;
    bool dbValid = false;
    Tdbc dbCntxt = default(Tdbc);
    try
    {
      dbCntxt = (Tdbc)Activator.CreateInstance(typeof(Tdbc));
    }
    catch (SqlException exc)
    {
      errorMessage = SqlErrorMessage(exc);
    }
    DbContext dbCntxtBase = dbCntxt.ToDbContext<Tdbc>();
    // assume database does not exist when cannot connect
    if (!dbCntxtBase.ContextCanConnect())
    {
      var fileInfo = fileProv.GetFileInfo(fileName);
      var sqlScript = File.ReadAllText(fileInfo.PhysicalPath, Encoding.UTF8);
      errorMessage = QebSqlLinq.ExecuteNonQuerySqlScript(mstrDbconstr, sqlScript, true);
      // renew/reset the context
      try
      {
        dbCntxt = (Tdbc)Activator.CreateInstance(typeof(Tdbc));
        dbCntxtBase = dbCntxt.ToDbContext<Tdbc>();
        dbExists = dbCntxtBase.ContextCanConnect();
        dbHasSchema = dbCntxtBase.ContextHasSchema();
      }
      catch (SqlException exc) { errorMessage = SqlErrorMessage(exc); }
    }

    // final check for valid database
    dbValid = (dbExists && dbHasSchema && (errorMessage == ESS));
    return dbValid;
  }

} // end class

// end file