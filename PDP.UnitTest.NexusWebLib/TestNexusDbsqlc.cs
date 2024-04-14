// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.UnitTest.NexusWebLib;

// maintain consistent use of @"teststring" with '@' for literal strings

public class TestNexusDbsqlc : PdpSiteTestBase
{
  private readonly Type typCodeClass = typeof(TestNexusDbsqlc);
  private readonly string namCodeClass = nameof(TestNexusDbsqlc);
  private string tstDataPath = QebFile.PathToSolutionDataFolder(defDataDirname);

  // private readonly IDbContextFactory<NexusDbsqlContext> nexusdbcf;
  private NexusDbsqlContext? PNDC;
  private IQueryable<ICoreResrepRoot>? dalQueryCoreResrepRoot;
  private IQueryable<ICoreResrepLeaf>? dalQueryCoreResrepLeaf;
  private IList<ICoreResrepRoot>? dalCoreResrepRootList;
  private IList<ICoreResrepLeaf>? dalCoreResrepLeafList;
  private IList<CoreResrepRootUxm?>? uilResrepRootList;
  private IList<CoreResrepLeafUxm?>? uilResrepLeafList;

  public TestNexusDbsqlc()
  {
    // settings in PdpDevTestdata/XunitDevTestDebug.json
    Debug.WriteLine($"tstCodeClass = '{typCodeClass}'; tstDataPath = '{tstDataPath}';");
    StartNpdsServer(typCodeClass, tstDataPath);
  }

  [Fact]
  public void CreatePdpCodeConfig()
  {
    bool created = false;
    try
    {
      PDPCC = new PdpCodeConfig(typCodeClass, tstDataPath);
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
  }

  [Fact]
  public void CreatePdpSiteSettings()
  {
    bool created = false;
    try
    {
      PDPCC = new PdpCodeConfig(typCodeClass, tstDataPath);
      PDPSS = new PdpSiteSettings();
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
  }

  [Fact]
  public void CreateNpdsServerDefaults()
  {
    bool created = false;
    try
    {
      PDPCC = new PdpCodeConfig(typCodeClass, tstDataPath);
      PDPSS = new PdpSiteSettings();
      NPDSCD = new NpdsClientDefaults();
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
  }

  [Fact]
  public void CreateNpdsClient()
  {
    bool created = false;
    int l2rCount = 0, r2lCount = 0;
    try
    {
      PDPCC = new PdpCodeConfig(typCodeClass, tstDataPath);
      PDPSS = new PdpSiteSettings();
      NPDSCD = new NpdsClientDefaults();
      ConfigPdpSettings(typCodeClass, tstDataPath, null);
      AddPdpNpdsCacheService(true);
      l2rCount = NPDSCD.NpdsServiceCache.CountL2R();
      r2lCount = NPDSCD.NpdsServiceCache.CountR2L();
      NPDSSC = new NpdsClientWrace();
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
    Assert.True(l2rCount > 0);
    Assert.True(l2rCount == r2lCount);
  }

  [Fact]
  public void CreateCoreDbsqlContext()
  {
    bool created = false;
    string? dbcnstr = "";
    try
    {
      // zero-param constructor CoreDbsqlContext() defaults to internal use of 
      // var coreNpdsClient = new NpdsClient(NPDSCD.DatabaseTypeCore);
      using (var dbc = new CoreDbsqlContext())
      {
        dbc.DbsqlConnect();
        var dbcnctn = dbc.DbsqlCnctn;
        dbcnstr = dbcnctn.ConnectionString;
        dbc.DbsqlDisconnect();
      }
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
    Assert.Equal(NPDSCD.CoreDbconstr, dbcnstr);
  }

  [Fact]
  public void CreateNpdsServiceCache()
  {
    int l2rCount = 0, r2lCount = 0;
    bool created = false;
    string? dbcnstr = "";
    try
    {
      using (var dbc = new CoreDbsqlContext())
      {
        dbc.DbsqlConnect();
        var dbcnctn = dbc.DbsqlCnctn;
        dbcnstr = dbcnctn.ConnectionString;
        dbc.LoadNpdsServiceCache();
        dbc.DbsqlDisconnect();
      }
      l2rCount = NPDSCD.NpdsServiceCache.CountL2R();
      r2lCount = NPDSCD.NpdsServiceCache.CountR2L();
      if ((l2rCount > 0) && (r2lCount == l2rCount)) { created = true; }
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
    Assert.Equal(NPDSCD.CoreDbconstr, dbcnstr);
    Assert.Equal(l2rCount, r2lCount);
  }

  [Fact]
  public void CreateNexusDbsqlContext()
  {
    bool created = false;
    string dbstat = "Open";
    string dbname2 = "", dbcs2 = "", dbstat2 = "";
    try
    {
      using (var dbc = new NexusDbsqlContext())
      {
        dbc.DbsqlConnect();
        var dbcnctn = dbc.DbsqlCnctn;
        dbcs2 = dbcnctn.ConnectionString;
        dbstat2 = dbcnctn.State.ToString();
        dbc.DbsqlDisconnect();
      }
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
    Assert.Equal(NPDSCD.NexusDbconstr, dbcs2);
    Assert.Equal(dbstat, dbstat2);
  }


  [Fact]
  public void CreateNexusNlmmesh()
  {
    bool created = false;
    string dbstat = "Open", dbname2 = "", dbcs2 = "", dbstat2 = "";
    try
    {
      var npdscp = new NpdsClientWrace(NPDSCD.DatabaseTypeVocab);
      using (var dbc = new NexusDbsqlContext(npdscp))
      {
        dbc.DbsqlConnect();
        var dbcnctn = dbc.DbsqlCnctn;
        dbcs2 = dbcnctn.ConnectionString;
        dbstat2 = dbcnctn.State.ToString();
        dbc.DbsqlDisconnect();
      }
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
    Assert.Equal(NPDSCD.VocabDbconstr, dbcs2);
    Assert.Equal(dbstat, dbstat2);
  }

  [Fact]
  public void GetCountFromNexusNlmmesh()
  {
    string dbstat = "Open", dbname2 = "", dbcs2 = "", dbstat2 = "";
    var serviceType = "Nexus";
    var serviceTag = "NLMMESH";
    var searchFilter = "Diristry";
    // e expected, o observed
    //int ecount = 30194, ocount = 0;
    int ecount = 88, ocount = 0;
    try
    {
      var npdscp = new NpdsClientWrace(NPDSCD.DatabaseTypeVocab);
      using (var dbc = new NexusDbsqlContext(npdscp))
      {
        dbc.DbsqlConnect();
        var dbcnctn = dbc.DbsqlCnctn;
        dbcs2 = dbcnctn.ConnectionString;
        dbstat2 = dbcnctn.State.ToString();
        dbc.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", "", searchFilter, "");
        dbc.NPDSDC.DebugNpdsSelectFilter(nameof(GetCountFromNexusNlmmesh), namCodeClass);
        dalCoreResrepRootList = dbc.ListStorableResrepRoots(null, 88);
        ocount = dalCoreResrepRootList.Count();
        dbc.DbsqlDisconnect();
      }
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.Equal(NPDSCD.VocabDbconstr, dbcs2);
    Assert.Equal(dbstat, dbstat2);
    Assert.Equal(ecount, ocount);

  } // end method

} // end class

// end file