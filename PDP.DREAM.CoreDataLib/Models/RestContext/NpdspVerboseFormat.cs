// QurcVerboseFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // default values

  // requested values

  public bool VerboseFormatReqst
  {
    set { reqVerbForm = value; }
    get { return reqVerbForm; }
  }
  private bool reqVerbForm = false;

  // validated values

  public bool VerboseFormat
  {
    get { return VerboseFormatReqst; }
  }

}

