// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Models;

public class CwlRoot : PdpProjectRoot
{
  public CwlRoot()
  {
    // convention with filename same as class name
    CallingClass = nameof(CwlRoot);
    CallingFile = CallingClass.AppendCsext();
    CallingPath = CallingFile.PathToSourceFile();
    ProjectRootPath = CallingFile.PathToSourceFolder();
    SolutionRootPath = CallingFile.PathToSourceFolderParent();
  }

} // end class

// end file