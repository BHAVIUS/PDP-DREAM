// PrcArchiveFormat.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
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
