﻿// PrcArchiveFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{

  public bool ArchiveFormatReqst { get; set; }

  public bool ArchiveFormat
  {
    get { return (ArchiveFormatReqst && ClientIsAuthorized); }
  }

}

