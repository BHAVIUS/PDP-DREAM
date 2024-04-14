// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.UnitTest.CoreWebLib;

// maintain consistent use of @"teststring" with '@' for literal strings

public class TestCoreDbsqlc : PdpSiteTestBase
{
  private readonly Type typCodeClass = typeof(TestCoreDbsqlc);
  private readonly string namCodeClass = nameof(TestCoreDbsqlc);
  private string tstDataPath = QebFile.PathToSolutionDataFolder(defDataDirname);

  public TestCoreDbsqlc(ITestOutputHelper toh)
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
  public void CreateAltCoreDbsqlContext()
  {
    bool created = false;
    string? dbcnstr = "";
    try
    {
      var vocabNpdsClient = new NpdsClientWrace(NPDSCD.DatabaseTypeVocab);
      using (var dbc = new CoreDbsqlContext(vocabNpdsClient))
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
    Assert.Equal(NPDSCD.VocabDbconstr, dbcnstr);
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
  public void CreateQebiDbsqlContext()
  {
    bool created = false;
    string? dbcnstr = "";
    bool hasQebiAppGuid = false;
    bool hasPdpssAppGuid = false;
    try
    {
      using (var dbc = new QebiDalContext())
      {
        dbc.DbsqlConnect();
        var dbcnctn = dbc.DbsqlCnctn;
        dbcnstr = dbcnctn.ConnectionString;
        hasQebiAppGuid = dbc.QebiContextHasAppGuid();
        dbc.DbsqlDisconnect();
      }
      hasPdpssAppGuid = !PDPSS.CiaamAppGuid.IsEmpty();
      created = true;
    }
    catch (Exception exc)
    {
      Debug.WriteLine(exc.ToString());
    }
    Assert.True(created);
    Assert.Equal(NPDSCD.CiaamDbconstr, dbcnstr);
    Assert.True(hasQebiAppGuid);
    Assert.True(hasPdpssAppGuid);
    Debug.WriteLine($"{nameof(CreateQebiDbsqlContext)} completed at timestamp {QebString.DateTimeNowSortString()}");
  }

} // end class

// end file