namespace PDP.UnitTest.CoreWebLib;

public class TestCoreWebLib : PdpSiteTestBase
{
  [Fact]
  public void TestCwlRoot()
  {
    var cwlRoot = new CwlRoot();
    var srcClassName = cwlRoot.CallingClass;
    var srcFileName = cwlRoot.CallingFile;
    var srcFilePath = cwlRoot.CallingPath;
    var srcProjectRootPath = cwlRoot.ProjectRootPath;
    var srcSolutionRootPath = cwlRoot.SolutionRootPath;

    cwlRoot.DebugCallingRoot();
    Assert.Equal(srcClassName, "CwlRoot");
    Assert.Equal(srcFileName, "CwlRoot.cs");
    Assert.Equal(srcFilePath, "D:\\Code\\PDP\\Net8Tahtali\\PDP.DREAM.CoreWebLib\\CwlRoot.cs");
    // TODO: reset these expected paths to values obtained from Environment Variables
    // or alternatively obtained from PDP.DREAM.CoreDataLib.Models.PdpCodeConfig property values
    Assert.Equal(srcProjectRootPath, "D:\\Code\\PDP\\Net8Tahtali\\PDP.DREAM.CoreWebLib\\");
    Assert.Equal(srcSolutionRootPath, "D:\\Code\\PDP\\Net8Tahtali\\");
  }

} // end class

// end file