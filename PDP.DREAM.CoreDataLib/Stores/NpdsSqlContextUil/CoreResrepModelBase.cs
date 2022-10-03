// CoreResrepModelBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Text;

using PDP.DREAM.CoreDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Types.PdpGuid;
using static PDP.DREAM.CoreDataLib.Utilities.PdpStringFrasFormFile;

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class CoreResrepModelBase : ICoreResrepModelBase
{
  // ItemXnam as string cannot be nullable if/when used as a key in a dictionary
  public string ItemXnam { get { return itemXnam; } }
  protected string itemXnam = string.Empty;

  public Guid? AgentGuid { get; set; } = Guid.Empty;
  //public string? AgentGuidStr { get { return AgentGuid.ToPdpGuidString(); } }
  
  // TODO: rename and refactor as a PdpDataItemStatus class that can be called in the datastore layer
  //  with properties ElemId, Name, Label, Message, Stored 
  public string? PdpStatusElement { get; set; } = string.Empty;
  public string? PdpStatusLabel { get; set; } = string.Empty;
  public string? PdpStatusMessage { get; set; } = string.Empty;
  public string? PdpStatusItemName { get; set; } = string.Empty;
  public bool PdpStatusItemStored { get; set; } = false;

  // TODO: migrate to TkgrNpdsModel
  // TKGR tracking for Admin/Editor/Author/Agent in shared views/pages
  public string? TkgrRecordAccess { get; set; } = string.Empty;
  //public string? TkgrFormAction { get; set; } = string.Empty;
  //public string? TkgrArea { get; set; } = string.Empty;
  //public string? TkgrController { get; set; } = string.Empty;
  //public string? TkgrView { get; set; } = string.Empty;
  //public string? TkgrPage { get; set; } = string.Empty;
  //public string? TkgrPath { get; set; } = string.Empty;

  // TODO: LINQ problems with byte array on .Include()
  //   for property HasVersion in the Core Audit table field collection
  //   public byte[]? HasVersion { get; set; } = null;
  // TODO: LINQ problems with SqlDataReader when enumerating byte fields
  //   must test SqlDataReader enumerating without errors when using LINQ on a byte field


  // independent parent table //

  public Guid? RRRecordGuid { get; set; } = Guid.Empty; // ResRep Record guid
  public string? RRRecordGuidStr { get { return RRRecordGuid.ToPdpGuidString(); } }
  public Guid? RRInfosetGuid { get; set; } = Guid.Empty; // ResRep Infoset guid
  public string? RRInfosetGuidStr { get { return RRInfosetGuid.ToPdpGuidString(); } }
  public string? RecordHandle { get; set; } = string.Empty;
  public bool RecordIsDeleted { get; set; } = false;

  // dependent child tables //

  public Guid? RRFgroupGuid { get; set; } = Guid.Empty; // ResRep Fgroup guid
  public string? RRFgroupGuidStr { get { return RRFgroupGuid.ToPdpGuidString(); } }

  // resrep record attributes
  public short HasIndex { get; set; } = 0;
  public short HasPriority { get; set; } = 0;
  public bool IsConceptLabel { get; set; } = false;
  public bool IsDeleted { get; set; } = false;
  public bool IsExcluding { get; set; } = false;
  public bool IsLimited { get; set; } = false;
  public bool IsMarked { get; set; } = false;
  public bool IsPrincipal { get; set; } = false;
  public bool IsPrivate { get; set; } = false;
  public bool IsReleased { get; set; } = false;
  public bool IsShared { get; set; } = false;
  public bool IsSufficient { get; set; } = false;
  public bool IsWordPhrase { get; set; } = false;

  // enum codes may be byte in UIL and short/smallint in DAL
  public short EntityTypeCode { get; set; } = (short)default(PdpAppConst.NpdsEntityType);
  public string? EntityTypeName { get; set; } = string.Empty;
  
  public string? EntityInitialTag { get; set; } = string.Empty;
  public string? EntityPrincipalTag { get; set; } = string.Empty;
  public string? EntityCanonicalLabel { get; set; } = string.Empty;

  public virtual string? EntityName { get; set; } = string.Empty;
  public string? EntityName64
  {
    get {
      if (string.IsNullOrEmpty(EntityName)) { entNam64 = string.Empty; }
      else { entNam64 = EntityName.ToTruncatedPhrase(64); }
      return entNam64;
    }
  }
  private string? entNam64;

  public virtual string? EntityNature { get; set; } = string.Empty;
  public string? EntityNature128
  {
    get {
      if (string.IsNullOrEmpty(EntityNature)) { entNat128 = string.Empty; }
      else { entNat128 = EntityNature.ToTruncatedPhrase(128); }
      return entNat128;
    }
  }
  private string? entNat128;

  public short InfosetTypeCode { get; set; } = 0;
  public string? InfosetTypeName { get; set; } = string.Empty;
  public short FieldFormatCode { get; set; } = 0;
  public string? FieldFormatName { get; set; } = string.Empty;

  public Guid? ManagedByAgentGuid { get; set; } = Guid.Empty;
  public string? ManagedByAgentName { get; set; } = string.Empty;

  public DateTime? CreatedOn { get; set; } = null;
  public Guid? CreatedByAgentGuid { get; set; } = Guid.Empty;
  public string? CreatedByAgentName { get; set; } = string.Empty;

  public DateTime? UpdatedOn { get; set; } = null;
  public Guid? UpdatedByAgentGuid { get; set; } = Guid.Empty;
  public string? UpdatedByAgentName { get; set; } = string.Empty;

  public DateTime? DeletedOn { get; set; } = null;
  public Guid? DeletedByAgentGuid { get; set; } = Guid.Empty;
  public string? DeletedByAgentName { get; set; } = string.Empty;

} // end class

// end file