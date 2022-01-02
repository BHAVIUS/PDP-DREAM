// PrcVerboseFormat.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
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

}
