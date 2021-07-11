﻿// INpdsMetadataInfoset.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

// Interfaces for PDS data records; code uses acronyms PDSDR or Npdsdr in interface names;
// original design for PDS data records in 2008 IEEE TITB vol 12 no 2 pp 194-196;
// see Sections VII A and VII B of paper
//
// code also uses abbreviation ResRep for Resource Representation

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public interface INpdsMetadataInfosetCore
  {
    // permitted attributes
    Nullable<Guid> InfosetGuid { get; set; } // TODO: code NpdsInfosetGuidItem class
    bool InfosetIsAuthorPrivate { get; set; }
    bool InfosetIsConcise { get; set; }
  }

  public interface INpdsMetadataInfosetPortal : INpdsMetadataInfosetCore
  {
    // required elements
    NpdsInfosetValidationPortalItem InfosetPortalValidation { get; set; }
  }

  public interface INpdsMetadataInfosetDoors : INpdsMetadataInfosetCore
  {
    // required elements
    NpdsInfosetValidationDoorsItem InfosetDoorsValidation { get; set; }
  }

  public interface INpdsMetadataInfosetNexus : INpdsMetadataInfosetPortal, INpdsMetadataInfosetDoors
  {
    // permitted elements
    NpdsInfosetEntailmentNexusItem InfosetNexusEntailment { get; set; }
  }

}
