﻿// PrcVerboseFormat.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
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
