// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusWebApp;

// top-level program can be derived from previous combined use of 
//    1) Program.cs with CreateHostBuilder()
//    2) Startup.cs with ConfigureServices() and Configure()
// but then lose visibility of entrypoint and namespace in Class View
// meanwhile the approach below eliminates use of multiple different methods for startup program
// as if a top-level program but retains visibility in Class View

public class Program
{
  public static void Main(string[] appArgs)
  {
    var appType = typeof(Program);
    var appRoot = Environment.CurrentDirectory;
    ConfigPdpSettings(appType, appRoot, appArgs);
    AddPdpUtilityServices();
    QebSqlLinq.CheckDatabase(NPDSCD.DatabaseTypeQEBI);
    QebSqlLinq.CheckDatabase(NPDSCD.DatabaseTypeCore);
    AddPdpNpdsCacheService();
    QebSqlLinq.CheckDatabase(NPDSCD.DatabaseTypeNexus);
    QebSqlLinq.CheckDatabase(NPDSCD.DatabaseTypeVocab);
    QebSqlLinq.CheckDatabase(NPDSCD.DatabaseTypeCache);
    AddPdpNpdsDataServices();
    RunPdpSite();

  } // end method

} // end class

// end file