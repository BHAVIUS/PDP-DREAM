// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.UnitTest.ScribeWebLib;

// maintain consistent use of @"teststring" with '@' for literal strings

public class TestScribeWebLib : PdpSiteTestBase
{
  protected readonly Type tstClass = typeof(TestScribeWebLib);
  protected string tstPath = defDataDirname.PathToSolutionDataFolder();

  private readonly IDbContextFactory<ScribeDbsqlContext> scribedbcf;
  private ScribeDbsqlContext? PSDC;
  private IQueryable<ICoreResrepRoot> dalQueryResrepRoot;
  private IQueryable<ICoreResrepLeaf> dalQueryResrepLeaf;
  private IList<ICoreResrepRoot> dalResrepRootList;
  private IList<ICoreResrepLeaf> dalResrepLeafList;
  private IList<CoreResrepRootUxm?> uilResrepRootList;
  private IList<CoreResrepLeafUxm?> uilResrepLeafList;

  public TestScribeWebLib()
  {
    StartNpdsServer(tstClass, tstPath);
    // TODO: must test creation of WRACE only after non-null HttpRequest
    NPDSSC = new NpdsClientWrace(new DefaultHttpContext())  // calls ParseQueryCollection on new()
    {
      DatabaseType = NPDSCD.DatabaseTypeScribe,
      DatabaseAccess = NPDSCD.DatabaseAccessAnonReadOnly,
      RecordAccess = NPDSCD.RecordAccessAnon
    };
    PSDC = new ScribeDbsqlContext(NPDSSC);
    Debug.WriteLine($"defDataDirname = {defDataDirname}");
    Debug.WriteLine($"tstPath = {tstPath}");
  }

  [Fact]
  public void GetScribeBioportEllitron1()
  {
    var serviceType = "Scribe";
    var serviceTag = "BioPORT";
    var entityTag = "Ellitron";
    var searchFilter = "Diristry";
    var ecount = 1;
    var ocount = 0;
    PSDC.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, searchFilter,"");
    dalQueryResrepRoot = PSDC.QueryStorableResrepRoot();
    uilResrepRootList = dalQueryResrepRoot.ToEditable().ToList();
    ocount = uilResrepRootList.Count();
    Assert.Equal(ecount, ocount);
  }

  [Fact]
  public void GetScribeBioportEllitron2()
  {
    var serviceType = "Scribe";
    var serviceTag = "BioPORT";
    var entityTag = "Ellitron";
    var searchFilter = "Diristry";
    var ecount = 1;
    var ocount = 0;
    PSDC.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, searchFilter, "");
    dalResrepRootList = PSDC.ListStorableResrepRoots();
    ocount = dalResrepRootList.Count();
    Assert.Equal(ecount, ocount);
  }

  [Fact]
  public void GetScribeBioportEllitron3()
  {
    var serviceType = "Scribe";
    var serviceTag = "BioPORT";
    var entityTag = "Ellitron";
    var searchFilter = "Diristry";
    var ecount = 1;
    var ocount = 0;
    PSDC.NPDSDC.ParseNpdsSelectFilter(serviceType, serviceTag, "", entityTag, searchFilter ,"");
    dalResrepRootList = PSDC.ListStorableResrepRootsWithFacets();
    ocount = dalResrepRootList.Count();
    Assert.Equal(ecount, ocount);
  }

} // end class

// end file