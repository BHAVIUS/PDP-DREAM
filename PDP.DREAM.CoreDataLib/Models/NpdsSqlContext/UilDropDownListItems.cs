// UilDropDownListItems.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models
{
  // ATTN: maintain these objects as non-nullable

  public class EntityTypeListItem
  {
    public string EntityTypeName { get; set; } = string.Empty;
    public Int16 EntityTypeCode { get; set; }
    public NpdsConst.EntityType EntityTypeEnum { get { return (NpdsConst.EntityType)EntityTypeCode; } }
    public bool Selected { get; set; }
  }

  public class InfosetPortalStatusListItem
  {
    public string InfosetPortalStatusName { get; set; } = string.Empty;
    public Int16 InfosetPortalStatusCode { get; set; }
    public NpdsConst.InfosetStatus InfosetPortalStatusEnum { get { return (NpdsConst.InfosetStatus)InfosetPortalStatusCode; } }
    public bool Selected { get; set; }
  }

  public class InfosetDoorsStatusListItem
  {
    public string InfosetDoorsStatusName { get; set; } = string.Empty;
    public Int16 InfosetDoorsStatusCode { get; set; }
    public NpdsConst.InfosetStatus InfosetDoorsStatusEnum { get { return (NpdsConst.InfosetStatus)InfosetDoorsStatusCode; } }
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

}
