// UilDropDownListItems.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: maintain these objects as non-nullable

public class EntityTypeListItem
{
  public string EntityTypeName { get; set; } = string.Empty;
  public short EntityTypeCode { get; set; }
  public PdpAppConst.NpdsEntityType EntityTypeEnum { get { return (PdpAppConst.NpdsEntityType)EntityTypeCode; } }
  public bool Selected { get; set; }
}

public class FieldFormatListItem
{
  public string FieldFormatName { get; set; } = string.Empty;
  public short FieldFormatCode { get; set; }
  public PdpAppConst.NpdsFieldFormat FieldFormatEnum { get { return (PdpAppConst.NpdsFieldFormat)FieldFormatCode; } }
  public bool Selected { get; set; }
}

public class InfosetPortalStatusListItem
{
  public string InfosetPortalStatusName { get; set; } = string.Empty;
  public short InfosetPortalStatusCode { get; set; }
  public PdpAppConst.NpdsInfosetStatus InfosetPortalStatusEnum { get { return (PdpAppConst.NpdsInfosetStatus)InfosetPortalStatusCode; } }
  public bool Selected { get; set; }
}

public class InfosetDoorsStatusListItem
{
  public string InfosetDoorsStatusName { get; set; } = string.Empty;
  public short InfosetDoorsStatusCode { get; set; }
  public PdpAppConst.NpdsInfosetStatus InfosetDoorsStatusEnum { get { return (PdpAppConst.NpdsInfosetStatus)InfosetDoorsStatusCode; } }
  public bool Selected { get; set; }
}

public class RecordServiceListItem
{
  public Guid ServiceIGuid { get; set; } = Guid.Empty;
  public string ServicePTag { get; set; } = string.Empty;
  public bool Selected { get; set; }
}
// Nexus
public class RecordDiristryListItem
{
  public Guid RecordDiristryGuid { get; set; } = Guid.Empty;
  public string RecordDiristryName { get; set; } = string.Empty;
  public bool Selected { get; set; }
}
// PORTAL
public class RecordRegistryListItem
{
  public Guid RecordRegistryGuid { get; set; } = Guid.Empty;
  public string RecordRegistryName { get; set; } = string.Empty;
  public bool Selected { get; set; }
}
// DOORS
public class RecordDirectoryListItem
{
  public Guid RecordDirectoryGuid { get; set; } = Guid.Empty;
  public string RecordDirectoryName { get; set; } = string.Empty;
  public bool Selected { get; set; }
}
// Scribe
public class RecordRegistrarListItem
{
  public Guid RecordRegistrarGuid { get; set; } = Guid.Empty;
  public string RecordRegistrarName { get; set; } = string.Empty;
  public bool Selected { get; set; }
}

// end file