// PrcEchoFormat.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
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
