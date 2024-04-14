// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class ResrepRootModelBase // : ICoreResrepModelBase
{
  public ResrepRootModelBase() { }
  public ResrepRootModelBase(NpdsRecordAccess nra)
  {
    RRRecordAccess = nra.ToString();
  }

  // ItemXnam as string cannot be nullable if/when used as a key in a dictionary
  protected string itemXnam = ItemDefaultXnam;
  public string ItemXnam { get { return itemXnam; } }
  public string? ResrepSnapshot { get; set; } = ESS;
  public byte ResrepSnapshotsStatusCode { get; set; } // = NPDSCD.InfosetStatusNewItem.ECode;

  // TODO: rename and refactor as a
  // NpdsDataItemStatus as class type for use
  // with RecordStatus property (compare InfosetStatus)
  //  that can be called in the datastore layer
  //  reconcile with FormMessage in AFormTaskUxm
  // currently use flattened with properties in ResrepRootModelBase as
  // NPDS DataItemStatus = NDIS (Ndis as prefix)
  public bool NdisDataStored { get; set; } = false;
  public bool NdisFileStored { get; set; } = false;
  public string? NdisName { get; set; } = ESS;

  // MessageDal and MessageUil for use in pageforms
  public string? NdisMessageDal { get; set; } = ESS;
  public string? NdisMessageUil { get; set; } = ESS;

  // Element Id and Message for use in toolbars
  public string? NdisElemId { get; set; } = ESS;
  public string? NdisElemMsg { get; set; } = ESS;

  // Link Id and Message for use in grid cells
  public string? NdisLinkId { get; set; } = ESS;
  public string? NdisLinkMsg { get; set; } = ESS;

  // TODO: LINQ problems with byte array on .Include()
  //   for property HasVersion in the Core Audit table field collection
  //   public byte[]? HasVersion { get; set; } = null;
  // TODO: LINQ problems with SqlDataReader when enumerating byte fields
  //   must test SqlDataReader enumerating without errors when using LINQ on a byte field

  public Guid? AgentGuid { get; set; } = EGS;

  // TKGR tracking for Admin/Editor/Reviewer/Author/Agent in shared views/pages/forms
  public string? RRRecordAccess { get; set; } = ESS;

  // independent parent table //
  public Guid? RRRecordGuid { get; set; } = EGS; // ResRep Record guid
  public Guid? RRInfosetGuid { get; set; } = EGS; // ResRep Infoset guid
  public string? RecordHandle { get; set; } = ESS;
  public bool RecordIsDeleted { get; set; } = false;

  // dependent child tables //
  public Guid? RRFgroupGuid { get; set; } = EGS; // ResRep Fgroup guid

  // resrep record attributes
  public byte HasIndex { get; set; } = 0;
  public byte HasPriority { get; set; } = 0;
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

  public byte EntityTypeCode { get; set; } = NPDSCD.EntityTypeNewItem.ECode;
  public string? EntityTypeName { get; set; } = ESS;

  public string? EntityCanonicalLabel { get; set; } = ESS;
  public string? EntityPrincipalTag { get; set; } = ESS;

  public virtual string? EntityInitPrinTag { get; set; } = ESS;
  public virtual string? EntityInitSuppTag { get; set; } = ESS;

  public virtual string? EntityName { get; set; } = ESS;
  public string? EntityName64
  {
    get {
      if (string.IsNullOrEmpty(EntityName)) { entNam64 = ESS; }
      else { entNam64 = EntityName.TruncateForTkgr(64); }
      return entNam64;
    }
  }
  private string? entNam64;

  public virtual string? EntityNature { get; set; } = ESS;
  public string? EntityNature128
  {
    get {
      if (string.IsNullOrEmpty(EntityNature)) { entNat128 = ESS; }
      else { entNat128 = EntityNature.TruncateForTkgr(128); }
      return entNat128;
    }
  }
  private string? entNat128;

  public byte InfosetTypeCode { get; set; } = 0;
  public string? InfosetTypeName { get; set; } = ESS;
  public byte FieldFormatCode { get; set; } = 0;
  public string? FieldFormatName { get; set; } = ESS;

  public Guid? ManagedByAgentGuid { get; set; } = EGS;
  public string? ManagedByAgentAlias { get; set; } = ESS;
  public bool AgentIsManager { get { return (ManagedByAgentGuid == AgentGuid); } }

  public Guid? AuthoredByAgentGuid { get; set; } = EGS;
  public string? AuthoredByAgentAlias { get; set; } = ESS;
  public bool AgentIsAuthor { get { return (AuthoredByAgentGuid == AgentGuid); } }
  public bool AuthorHasResrepAccess { get; set; } = false;
  public bool AuthorHasServiceAccess { get; set; }
  public string? AuthorStatus
  {
    get { return ((AuthorHasResrepAccess) ? "Release" : "Request"); }
  }
  public string? AuthorRequestHtml
  {
    get { return $"<span id='Author-{RecordHandle}'><a href='#' id='Author-{RRRecordGuid}' class='k-button' onclick='OnAuthorResrepAccess(this)'>{AuthorStatus}</a></span>"; }
  }

  public Guid? ReviewedByAgentGuid { get; set; } = EGS;
  public string? ReviewedByAgentAlias { get; set; } = ESS;
  public bool AgentIsReviewer { get { return (ReviewedByAgentGuid == AgentGuid); } }
  public bool ReviewerHasResrepAccess { get; set; } = false;
  public bool ReviewerHasServiceAccess { get; set; }
  public string? ReviewerStatus
  {
    get { return ((ReviewerHasResrepAccess) ? "Release" : "Request"); }
  }
  public string? ReviewerRequestHtml
  {
    get { return $"<span id='Reviewer-{RecordHandle}'><a href='#' id='Reviewer-{RRRecordGuid}' class='k-button' onclick='OnReviewerResrepAccess(this)'>{ReviewerStatus}</a></span>"; }
  }

  public Guid? EditedByAgentGuid { get; set; } = EGS;
  public string? EditedByAgentAlias { get; set; } = ESS;
  public bool AgentIsEditor { get { return (EditedByAgentGuid == AgentGuid); } }
  public bool EditorHasResrepAccess { get; set; } = false;
  public bool EditorHasServiceAccess { get; set; }
  public string? EditorStatus
  {
    get { return ((EditorHasResrepAccess) ? "Release" : "Request"); }
  }
  public string? EditorRequestHtml
  {
    get { return $"<span id='Editor-{RecordHandle}'><a href='#' id='Editor-{RRRecordGuid}' class='k-button' onclick='OnEditorResrepAccess(this)'>{EditorStatus}</a></span>"; }
  }

  public DateTime? CreatedOn { get; set; } = null;
  public Guid? CreatedByAgentGuid { get; set; } = EGS;
  public string? CreatedByAgentAlias { get; set; } = ESS;
  public string? CreatedByAgentEmail { get; set; } = ESS;
  public bool AgentIsCreator { get { return (CreatedByAgentGuid == AgentGuid); } }

  public DateTime? UpdatedOn { get; set; } = null;
  public Guid? UpdatedByAgentGuid { get; set; } = EGS;
  public string? UpdatedByAgentAlias { get; set; } = ESS;
  public bool AgentIsUpdater { get { return (UpdatedByAgentGuid == AgentGuid); } }

  public DateTime? DeletedOn { get; set; } = null;
  public Guid? DeletedByAgentGuid { get; set; } = EGS;
  public string? DeletedByAgentAlias { get; set; } = ESS;
  public bool AgentIsDeleter { get { return (DeletedByAgentGuid == AgentGuid); } }

  public byte InfosetPortalStatusCode { get; set; } = NPDSCD.InfosetStatusNewItem.ECode;
  public string? InfosetPortalStatusName { get; set; } = ESS;

  public byte InfosetDoorsStatusCode { get; set; } = NPDSCD.InfosetStatusNewItem.ECode;
  public string? InfosetDoorsStatusName { get; set; } = ESS;

  public bool InfosetIsAuthorPrivate { get; set; } = false;
  public bool InfosetIsAgentShared { get; set; } = false;
  public bool InfosetIsUpdaterLimited { get; set; } = false;
  public bool InfosetIsManagerReleased { get; set; } = false;

  // TODO: consider use of property groups as records???
  // TODO: reconcile/deconflict with properties in NpdsClient

  // Nexus RecordDiristryGuid & RecordDiristryName
  public Guid? RecordDiristryGuid { get; set; } = EGS;
  public string? RecordDiristryTag { get; set; } = ESS;
  public string? RecordDiristryName { get; set; } = ESS;

  // PORTAL RecordRegistryGuid & RecordRegistryName
  public Guid? RecordRegistryGuid { get; set; } = EGS;
  public string? RecordRegistryTag { get; set; } = ESS;
  public string? RecordRegistryName { get; set; } = ESS;

  // DOORS RecordDirectoryGuid & RecordDirectoryName
  public Guid? RecordDirectoryGuid { get; set; } = EGS;
  public string? RecordDirectoryTag { get; set; } = ESS;
  public string? RecordDirectoryName { get; set; } = ESS;

  // Scribe RecordRegistrarGuid & RecordRegistrarName
  public Guid? RecordRegistrarGuid { get; set; } = EGS;
  public string? RecordRegistrarTag { get; set; } = ESS;
  public string? RecordRegistrarName { get; set; } = ESS;

  // TODO: compare/reconcile/deconflict with usage of analogous properties in NpdsClient
  // can the approach below also be migrated for use in NpdsClient AI system of validated properties
  // TODO: compare/reconcile/deconflict PdpdSearchFilter vs SearchFilter
  // TODO: compare/reconcile/deconflict PdpdServiceType vs ServiceType
  // TODO: compare/reconcile/deconflict PdpdServiceTag vs ServiceTag

  public string? PdpdSearchFilter
  {
    get { return pdpdSrchFilter; }
    set { pdpdSrchFilter = value; }
  }
  public string? PdpdServiceType
  {
    get { return pdpdSrvcType; }
    set { pdpdSrvcType = value; }
  }

  // TODO: refactor tag to guid and guid to tag
  // in core utility extensions for NpdsServiceCache

  public string? PdpdServiceTag
  {
    get { return pdpdSrvcTag; }
    set { pdpdSrvcTag = value; pdpdSrvcGuid = NPDSCD.NpdsServiceCache.GetByNullableTag(pdpdSrvcTag); }
  }
  public Guid? PdpdServiceGuid
  {
    get { return pdpdSrvcGuid; }
    set { pdpdSrvcGuid = value; pdpdSrvcTag = NPDSCD.NpdsServiceCache.GetByNullableGuid(pdpdSrvcGuid); }
  }

  protected string? pdpdSrvcType;
  protected string? pdpdSrchFilter;
  protected Guid? pdpdSrvcGuid;
  protected string? pdpdSrvcTag;

} // end class

// end file