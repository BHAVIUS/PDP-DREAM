// PrcCheckFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values


  // validated values

  private bool checkFormat = false; 
  public bool CheckFormat
  {
    get { return checkFormat; }
    set { checkFormat = value; }
  }

} // end class

// end file