// PrcCheckFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // default values

  // requested values

  public bool CheckFormatReqst
  {
    set { reqChckForm = value; }
    get { return reqChckForm; }
  }
  private bool reqChckForm = false;

  // validated values

  public bool CheckFormat
  {
    get { return CheckFormatReqst; }
  }

}

