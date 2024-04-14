// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

public abstract class PdpProjectRoot
{
  // ATTN: conventions here assume that projects are subfolders of solution
  // project root path should be subfolder of solution root path
  public string CallingClass { get; set; } = ESS;
  public string CallingFile { get; set; } = ESS;
  // CallingPath should return full folder path and file name
  public string CallingPath { get; set; } = ESS;
  // ProjectRootPath should return path to project root with final pathsep
  public string ProjectRootPath { get; set; } = ESS;
  // SolutionRootPath should return path to solution root with final pathsep
  public string SolutionRootPath { get; set; } = ESS;

  public void DebugCallingRoot()
  {
#if DEBUG
    Debug.WriteLine($"CallingClass = '{CallingClass}'");
    Debug.WriteLine($"CallingFile = '{CallingFile}'");
    Debug.WriteLine($"CallingPath = '{CallingPath}'");
    Debug.WriteLine($"ProjectRootPath = '{ProjectRootPath}'");
    Debug.WriteLine($"SolutionRootPath = '{SolutionRootPath}'");
#endif
  }

} // end class

// end file