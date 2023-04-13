// PdpXunitTestBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

// using System.Formats.Asn1;

namespace PDP.DREAM.CoreDataLib.Types;

public abstract class PdpXunitTestBase
{
  // runsettings not supported in xunit
  // https://github.com/xunit/xunit/issues/1439
  // https://stackoverflow.com/questions/38458979/whats-an-xunit-runsettings-equivalent
  // https://stackoverflow.com/questions/40121043/what-is-the-attribute-in-xunit-thats-similar-to-testcontext-in-visual-studio-te
  // https://stackoverflow.com/questions/50065991/how-to-configure-xunit-dotnet-core-project-to-initialize-from-different-configur
  // https://stackoverflow.com/questions/54977531/how-to-read-runsettings-test-parameter-in-xunit-fixture
  // IConfigurationBuilder has .AddEnvironmentVariables() extension
  // https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.environmentvariablesextensions
  public PdpXunitTestBase()
  {
    Environment.SetEnvironmentVariable(MSASPNETENVVAR, XunitEnvirname);
  }

  public void StartNpdsServer(Type tstClass, string tstPath)
  {
    PDPCC = new PdpCodeConfig(tstClass, tstPath);
    PDPSS = new PdpSiteSettings();
    NPDSSD = new NpdsServerDefaults();
  }

} // end class

// end file