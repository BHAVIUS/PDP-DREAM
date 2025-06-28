// PORTAL-DOORS Project Copyright (c) 2006-2025 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeWebLib.Models;

// ScribeWebLib Root
public class SwlRoot : PdpProjectRoot
{
  public SwlRoot()
  {
    CallingClass = nameof(SwlRoot);
    CallingFile = CallingClass.AppendCsext();
    CallingPath = CallingFile.PathToSourceFile();
    ProjectRootPath = CallingFile.PathToSourceFolder();
    SolutionRootPath = CallingFile.PathToSourceFolderParent();
  }

} // end class

// end file