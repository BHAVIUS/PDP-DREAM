// PrcCheckFormat.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
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

}
