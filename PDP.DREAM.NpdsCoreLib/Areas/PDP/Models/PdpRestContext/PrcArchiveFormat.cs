// PrcArchiveFormat.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {

    public bool ArchiveFormatReqst { get; set; }

    public bool ArchiveFormat
    {
      get { return (ArchiveFormatReqst && ClientIsAuthorized); }
    }

  }

}
