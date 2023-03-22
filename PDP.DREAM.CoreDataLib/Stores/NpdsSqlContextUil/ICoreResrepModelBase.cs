// ICoreResrepModelBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public interface ICoreResrepModelBase
{
  // ItemXnam as string cannot be nullable if/when used as a key in a dictionary
  string ItemXnam { get; }


  Guid? AgentGuid { get; set; }
  string? PdpStatusElement { get; set; }
  string? PdpStatusMessage { get; set; }
  bool PdpStatusItemStored { get; set; }


  // independent parent table
  Guid? RRRecordGuid { get; set; } // ResRep Record guid
  Guid? RRInfosetGuid { get; set; } // ResRep Infoset guid
  string? RRRecordAccess { get; set; } // ResRep RecordAccess string

  // dependent child tables
  Guid? RRFgroupGuid { get; set; } // ResRep Fgroup guid
  short HasIndex { get; set; }
  short HasPriority { get; set; }
  bool IsDeleted { get; set; }
  bool IsLimited { get; set; }
  bool IsMarked { get; set; }
  bool IsPrincipal { get; set; }
  bool IsPrivate { get; set; }
  bool IsReleased { get; set; }
  bool IsShared { get; set; }
  short InfosetTypeCode { get; set; }
  string? InfosetTypeName { get; set; }
  short FieldFormatCode { get; set; }
  string? FieldFormatName { get; set; }

} // end interface

// end file