// PrcEchoFormat.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {

    public bool EchoFormatReqst
    {
      set { reqEchoFormat = value; }
      get { return reqEchoFormat; }
    }
    private bool reqEchoFormat = false;

    public bool EchoFormat
    {
      get { return EchoFormatReqst; }
    }

  }
}
