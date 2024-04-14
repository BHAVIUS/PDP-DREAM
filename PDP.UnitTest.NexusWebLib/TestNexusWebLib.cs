// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.UnitTest.NexusWebLib;

// maintain consistent use of @"teststring" with '@' for literal strings

public class TestNexusWebLib : PdpSiteTestBase
{
  protected readonly Type tstClass = typeof(TestNexusWebLib);
  protected string tstPath = defDataDirname.PathToSolutionDataFolder();

  private readonly IDbContextFactory<NexusDbsqlContext> nexusdbcf;
  private NexusDbsqlContext? PNDC = null;
  private IQueryable<ICoreResrepRoot> dalQueryResrepRoot;
  private IQueryable<ICoreResrepLeaf> dalQueryResrepLeaf;
  private IList<ICoreResrepRoot> dalResrepRootList;
  private IList<ICoreResrepLeaf> dalResrepLeafList;
  private IList<CoreResrepRootUxm?> uilResrepRootList;
  private IList<CoreResrepLeafUxm?> uilResrepLeafList;

  public TestNexusWebLib()
  {
    StartNpdsServer(tstClass, tstPath);
    // TODO: must test creation of WRACE only after non-null HttpRequest
    NPDSSC = new NpdsClientWrace(new DefaultHttpContext())  // calls ParseQueryCollection on new()
    {
      DatabaseType = NPDSCD.DatabaseTypeNexus,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon
    };
    PNDC = new NexusDbsqlContext(NPDSSC);
    Debug.WriteLine($"defDataDirname = {defDataDirname}");
    Debug.WriteLine($"tstPath = {tstPath}");
  }

  [Fact]
  public void GetNexusBioportEllitron1()
  {
    var serviceType = "Nexus";
    var serviceTag = "BioPORT";
    var entityTag = "Ellitron";
    var searchFilter = "Diristry";
    var ecount = 1;
    var ocount = 0;
    PNDC.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, searchFilter,"");
    dalQueryResrepRoot = PNDC.QueryStorableResrepRoot();
    uilResrepRootList = dalQueryResrepRoot.ToEditable().ToList();
    ocount = uilResrepRootList.Count();
    Assert.Equal(ecount, ocount);
  }

  [Fact]
  public void GetNexusBioportEllitron2()
  {
    var serviceType = "Nexus";
    var serviceTag = "BioPORT";
    var entityTag = "Ellitron";
    var searchFilter = "Diristry";
    var ecount = 1;
    var ocount = 0;
    PNDC.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, searchFilter, "");
    dalResrepRootList = PNDC.ListStorableResrepRoots();
    ocount = dalResrepRootList.Count();
    Assert.Equal(ecount, ocount);
  }

  [Fact]
  public void GetNexusBioportEllitron3()
  {
    var serviceType = "Nexus";
    var serviceTag = "BioPORT";
    var entityTag = "Ellitron";
    var searchFilter = "Diristry";
    var ecount = 1;
    var ocount = 0;
    PNDC.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, searchFilter ,"");
    dalResrepRootList = PNDC.ListStorableResrepRootsWithFacets();
    ocount = dalResrepRootList.Count();
    Assert.Equal(ecount, ocount);
  }

} // end class

// end file