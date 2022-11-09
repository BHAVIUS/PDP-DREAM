// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: eliminate redundancies in parsing of PRC properties and querystr properties
//   as found in file NxsSqlRepInitPrd method InitializePredicate()
//   and anywhere else that PRC and/or querystr properties are parsed

// content for NPDS request
// *Deflt is *Default on property names
// *Reqst is *Request on property names
public partial class NpdsParameters
{
  public bool ClientIsUser { get; set; } = false;

  public bool ClientHasUserAccess
  { get { return (UserModeClientRequired && ClientIsUser); } }

  public bool ClientIsAgent { get; set; } = false;

  public bool ClientHasAgentAccess
  { get { return (AgentModeClientRequired && ClientIsAgent); } }

  public bool ClientIsAuthor { get; set; } = false;

  public bool ClientHasAuthorAccess
  { get { return (AuthorModeClientRequired && ClientIsAuthor); } }

  public bool ClientIsEditor { get; set; } = false;

  public bool ClientHasEditorAccess
  { get { return (EditorModeClientRequired && ClientIsEditor); } }

  public bool ClientIsAdmin { get; set; } = false;

  public bool ClientHasAdminAccess
  { get { return (AdminModeClientRequired && ClientIsAdmin); } }

  public bool ClientHasEditorOrAdminAccess
  { get { return (ClientHasEditorAccess || ClientHasAdminAccess); } }

  public bool ClientHasAuthorOrEditorAccess
  { get { return (ClientHasAuthorAccess || ClientHasEditorAccess); } }

  public bool ClientHasScribeEditAccess
  { get { return (ClientHasAuthorAccess || ClientHasEditorAccess || ClientHasAdminAccess); } }

  public bool AuthorizedClientIsRequired
  {
    get {
      return (AuthenticatedClientRequired || UserModeClientRequired || AgentModeClientRequired ||
        AuthorModeClientRequired || EditorModeClientRequired || AdminModeClientRequired);
    }
  }

  public bool AuthenticatedClientRequired { get; set; } = false;
  public bool UserModeClientRequired { get; set; } = false;
  public bool AgentModeClientRequired { get; set; } = false;
  public bool AuthorModeClientRequired { get; set; } = false;
  public bool EditorModeClientRequired { get; set; } = false;
  public bool AdminModeClientRequired { get; set; } = false;


  public bool ClientIsAuthenticated { get; set; } = false;
  public bool ClientIsAuthorized
  {
    get {
      switch (RecordAccess)
      {
        case PdpAppConst.NpdsRecordAccess.Admin:
          return ClientHasAdminAccess;
        case PdpAppConst.NpdsRecordAccess.Editor:
          return ClientHasEditorAccess;
        case PdpAppConst.NpdsRecordAccess.Author:
          return ClientHasAuthorAccess;
        case PdpAppConst.NpdsRecordAccess.Agent:
          return ClientHasAgentAccess;
        case PdpAppConst.NpdsRecordAccess.AuthUser:
          return ClientHasUserAccess;
        default:
          return false;
      }
    }
  }

  public bool ClientIsAnonymous
  {
    get { return (!ClientIsAuthorized); }
  }

  public bool ClientIsVerified
  {
    get { return (ClientIsAuthenticated && ClientIsAuthorized); }
  }

  public virtual void DebugClientAccess()
  {
    Debug.WriteLine($"{nameof(UserModeClientRequired)} = {UserModeClientRequired}; {nameof(ClientHasUserAccess)} = {ClientHasUserAccess};");
    Debug.WriteLine($"{nameof(AgentModeClientRequired)} = {AgentModeClientRequired}; {nameof(ClientHasAgentAccess)} = {ClientHasAgentAccess};");
    Debug.WriteLine($"{nameof(AuthorModeClientRequired)} = {AuthorModeClientRequired}; {nameof(ClientHasAuthorAccess)} = {ClientHasAuthorAccess};");
    Debug.WriteLine($"{nameof(EditorModeClientRequired)} = {EditorModeClientRequired}; {nameof(ClientHasEditorAccess)} = {ClientHasEditorAccess};");
    Debug.WriteLine($"{nameof(AdminModeClientRequired)} = {AdminModeClientRequired}; {nameof(ClientHasAdminAccess)} = {ClientHasAdminAccess};");
    Debug.WriteLine($"{nameof(ClientHasScribeEditAccess)} = {ClientHasScribeEditAccess};");
  }

  public virtual void DebugNpdsParams()
  {
    Debug.WriteLine($"NpdsPath = /{ServiceType}/{ServiceTag}/{EntityTag}");
  }

} // end class

// end file