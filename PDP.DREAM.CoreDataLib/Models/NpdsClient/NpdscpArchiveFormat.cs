// PrcArchiveFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values


  // validated values

  private bool archiveFormat = false;
  public bool ArchiveFormat
  {
    get { return archiveFormat; }
    set { archiveFormat = value; }
  }

} // end class

// end file