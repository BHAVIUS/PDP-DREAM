// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Models;

public class SwlRoot : PdpProjectRoot
{
  public SwlRoot()
  {
    // convention with filename same as class name
    CallingClass = nameof(SwlRoot);
    CallingFile = CallingClass.AppendCsext();
    CallingPath = CallingFile.PathToSourceFile();
    ProjectRootPath = CallingFile.PathToSourceFolder();
    SolutionRootPath = CallingFile.PathToSourceFolderParent();
  }

} // end class

// end file