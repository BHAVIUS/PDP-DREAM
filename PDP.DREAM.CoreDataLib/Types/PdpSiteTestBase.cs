// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace PDP.DREAM.CoreDataLib.Types;

public abstract class PdpSiteTestBase : PdpProjectRoot
{
  // runsettings not supported in xunit
  // https://github.com/xunit/xunit/issues/1439
  // https://stackoverflow.com/questions/38458979/whats-an-xunit-runsettings-equivalent
  // https://stackoverflow.com/questions/40121043/what-is-the-attribute-in-xunit-thats-similar-to-testcontext-in-visual-studio-te
  // https://stackoverflow.com/questions/50065991/how-to-configure-xunit-dotnet-core-project-to-initialize-from-different-configur
  // https://stackoverflow.com/questions/54977531/how-to-read-runsettings-test-parameter-in-xunit-fixture

  public PdpSiteTestBase()
  {
    // TODO: build PDP wrapper utilities
    //   with IConfigurationBuilder.AddEnvironmentVariables() extension
    // https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.environmentvariablesextensions
    //   to get/set EnvVars for PDP-DREAM for use in program startup
    //   may also be get/set in PdpConfigManager.Configure method ??
    Environment.SetEnvironmentVariable(MSASPNETENVVAR, XunitEnvirname);
  }

  protected const string defDataDirname = "PdpDevTestdata";
  protected INpdscwClient? NPDSSC;

  public void StartNpdsServer(Type tstClass, string tstPath)
  {
    ConfigPdpSettings(tstClass, tstPath, null);
    AddPdpNpdsCacheService(true);
    NPDSSC = new NpdsClientWrace();
  }

} // end class

// end file