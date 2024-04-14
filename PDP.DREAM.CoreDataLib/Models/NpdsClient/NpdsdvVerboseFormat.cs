// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private bool reqVerbForm = false;
  public bool VerboseFormatReqst
  {
    set { reqVerbForm = value; }
    get { return reqVerbForm; }
  }

  // validated values

  private bool verbForm = false;
  public bool VerboseFormat
  {
    set { verbForm = value; }
    get { return verbForm; }
  }

} // end class

// end file