// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// NPDS Client Wrace (Npdscw*)
public partial class NpdsClientWrace : INpdscwClient
{
  public Byte? CiaamRoleCode { get; set; } = 0;

  public Guid? CiaamAppGuid { get; set; } = EGS;
  public Guid? CiaamRoleGuid { get; set; } = EGS;
  public Guid? QebiUserGuid { get; set; } = EGS;
  public Guid? CiaamSessionGuid { get; set; } = EGS;
  public Guid? CiaamAuxiliaryGuid { get; set; } = EGS;
  public Guid? BridgePersonGuid { get; set; } = EGS;
  public Guid? NpdsAgentGuid { get; set; } = EGS;
  public Guid? NpdsAgentInfosetGuid { get; set; } = EGS;

  public string? CiaamUserRoles { get; set; } = ESS;
  public string? CiaamUserAlias { get; set; } = ESS;
  public string? CiaamUserEmail { get; set; } = ESS;
  public string? CiaamUserName { get; set; } = ESS;
  public string? CiaamPassWord { get; set; } = ESS;
  public string? CiaamRecordAccess { get; set; } = ESS;

  public bool ClientIsUser { get; set; } = false;
  public bool ClientIsPerson { get; set; } = false;
  public bool ClientIsAgent { get; set; } = false;
  public bool ClientIsAuthor { get; set; } = false;
  public bool ClientIsReviewer { get; set; } = false;
  public bool ClientIsEditor { get; set; } = false;
  public bool ClientIsAdmin { get; set; } = false;

  public bool ClientHasUserMode
  { get { return (UserModeClientRequired && ClientIsUser); } }
  public bool ClientHasAgentMode
  { get { return (AgentModeClientRequired && ClientIsAgent); } }
  public bool ClientHasAuthorMode
  { get { return (AuthorModeClientRequired && ClientIsAuthor); } }
  public bool ClientHasReviewerMode
  { get { return (ReviewerModeClientRequired && ClientIsReviewer); } }
  public bool ClientHasEditorMode
  { get { return (EditorModeClientRequired && ClientIsEditor); } }
  public bool ClientHasAdminMode
  { get { return (AdminModeClientRequired && ClientIsAdmin); } }

  public bool ClientCanUserWrite
  { get { return ClientHasUserMode; } }
  public bool ClientCanAgentWrite
  { get { return (ClientHasAdminMode || ClientHasEditorMode || ClientHasReviewerMode || ClientHasAuthorMode || ClientHasAgentMode); } }
  public bool ClientCanScribeWrite
  { get { return (ClientHasAdminMode || ClientHasEditorMode || ClientHasReviewerMode || ClientHasAuthorMode); } }
  public bool ClientCanAuthorWrite
  { get { return (ClientHasAdminMode || ClientHasEditorMode || ClientHasAuthorMode); } }
  public bool ClientCanReviewerWrite
  { get { return (ClientHasAdminMode || ClientHasEditorMode || ClientHasReviewerMode); } }
  public bool ClientCanEditorWrite
  { get { return (ClientHasAdminMode || ClientHasEditorMode); } }
  public bool ClientCanAdminWrite
  { get { return (ClientHasAdminMode); } }

  public bool SessionClientRequired { get; set; } = false;
  public bool AuthenticatedClientRequired { get; set; } = false;
  public bool UserModeClientRequired { get; set; } = false;
  public bool AgentModeClientRequired { get; set; } = false;
  public bool AuthorModeClientRequired { get; set; } = false;
  public bool ReviewerModeClientRequired { get; set; } = false;
  public bool EditorModeClientRequired { get; set; } = false;
  public bool AdminModeClientRequired { get; set; } = false;

  public bool AuthorizedClientIsAllowed
  {
    get {
      return (AuthenticatedClientRequired ||
        UserModeClientRequired || AgentModeClientRequired ||
        AuthorModeClientRequired || ReviewerModeClientRequired ||
        EditorModeClientRequired || AdminModeClientRequired);
    }
  }

  public bool ClientIsAuthenticated { get; set; } = false;
  public bool ClientIsVerified
  {
    get { return (ClientIsAuthenticated && ClientIsAuthorized); }
  }

  private bool clientIsAuthorized = false;
  public bool ClientIsAuthorized
  {
    get {
      var strName = RecordAccess.EName;
      switch (strName)
      {
        case DdeRecordAccess.User:
          clientIsAuthorized = ClientHasUserMode;
          break;
        case DdeRecordAccess.Agent:
          clientIsAuthorized = ClientHasAgentMode;
          break;
        case DdeRecordAccess.Author:
          clientIsAuthorized = ClientHasAuthorMode;
          break;
        case DdeRecordAccess.Reviewer:
          clientIsAuthorized = ClientHasReviewerMode;
          break;
        case DdeRecordAccess.Editor:
          clientIsAuthorized = ClientHasEditorMode;
          break;
        case DdeRecordAccess.Admin:
          clientIsAuthorized = ClientHasAdminMode;
          break;
        default:
          break;
      }
      return clientIsAuthorized;
    }
  }
  public bool ClientIsAnonymous
  {
    get { return (!ClientIsAuthorized); }
  }

  public bool DbcontextIsConfigured(DbContextOptionsBuilder dbcob)
  {
    var isDbconfigured = false;
    if (!dbcob.IsConfigured) { dbcob.UseSqlServer(DatabaseConstr); }
    if (dbcob.IsConfigured)
    {
      if (CiaamAppGuid.IsNullOrEmpty() && !PDPSS.CiaamAppGuid.IsEmpty())
      { CiaamAppGuid = PDPSS.CiaamAppGuid; }
      isDbconfigured = true;
    }
#if DEBUG
    else
    {
      throw new Exception("DbContext builder has not been configured");
    }
#endif
    return isDbconfigured;
  }

  // boolean filters migrated from QueryStorableResrepRootForAnon()
  public bool LnqFltrAuthoritativeOnly { get; set; } = false;
  public bool LnqFltrPublicOnly { get; set; } = false;
  public bool LnqFltrRecordAccess { get; set; } = true;
  public bool LnqFltrNpdsService { get; set; } = true;
  public bool LnqFltrQryStrValues { get; set; } = false;

  // list item properties
  public bool ItemIsPrivate { get; set; } = false;
  public bool ItemIsConcise { get; set; } = false;
  public bool ItemCanBeAccessed
  { get { return (!ItemIsPrivate || ClientIsAuthorized); } }
  public bool ItemCanBeVerbosed // "verbosed" is coined term meaning "displayed verbosely" (CT 2011/10/15)
  { get { return (!ItemIsConcise || ClientIsAuthorized); } }
  public bool ItemDoesArchive
  { get { return (ItemCanBeAccessed && ArchiveFormat); } }
  public bool ItemDoesVerbose
  { get { return (ItemCanBeVerbosed && VerboseFormat); } }

  // IDENTIFICATION/AUTHENTICATION

  // string-ids are "Key"s while guid-ids are "Guid"s
  // string-ids in WRACE, guid-ids in NpdsClient

  private string? qebUsrKey = ESS;
  public string? ClientUserKey
  {
    get { return qebUsrKey; }
    set { qebUsrKey = value; QebiUserGuid = PdpGuid.ParseToNonNullable(qebUsrKey); }
  }

  private string? qebAgtKey = ESS;
  public string? ClientAgentKey
  {
    get { return qebAgtKey; }
    set { qebAgtKey = value; NpdsAgentGuid = PdpGuid.ParseToNonNullable(qebAgtKey); }
  }

  private string? qebAifsKey = ESS;
  public string? ClientAgentInfosetKey
  {
    get { return qebAifsKey; }
    set { qebAifsKey = value; NpdsAgentInfosetGuid = PdpGuid.ParseToNonNullable(qebAifsKey); }
  }

  private string? qebSssnKey = ESS;
  public string? ClientSessionKey
  {
    get { return qebSssnKey; }
    set { qebSssnKey = value; CiaamSessionGuid = PdpGuid.ParseToNonNullable(qebSssnKey); }
  }

} // end class

// end file