// IQebiClient.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// IQebiUserRestContext : INpdsClient : IQebiClient
public interface  IQebiClient
{
  NamesForClientRoles ClientRole { get; set; }

  Guid? ClientAgentGuid { get; set; }
  Guid? ClientAppSiaaGuid { get; set; }
  Guid? ClientRoleGuid { get; set; }
  Guid? ClientSessionGuid { get; set; }
  Guid? ClientUserGuid { get; set; }

  string? ClientPassWord { get; set; }
  string? ClientUserName { get; set; }
  string? ClientUserNameDisplayed { get; set; }

  bool AdminModeClientRequired { get; set; }
  bool AgentModeClientRequired { get; set; }
  bool AuthenticatedClientRequired { get; set; }
  bool AuthorModeClientRequired { get; set; }
  bool AuthorizedClientIsRequired { get; }
  bool EditorModeClientRequired { get; set; }
  bool SessionClientRequired { get; set; }
  bool UserModeClientRequired { get; set; }

  bool ClientHasUserAccess { get; }
  bool ClientHasAgentAccess { get; }
  bool ClientHasAuthorAccess { get; }
  bool ClientHasEditorAccess { get; }
  bool ClientHasAdminAccess { get; }
  bool ClientHasEditorOrAdminAccess { get; }
  bool ClientHasAuthorOrEditorAccess { get; }
  bool ClientHasScribeEditAccess { get; }

  bool ClientIsAnonymous { get; }
  bool ClientIsAuthenticated { get; set; }
  bool ClientIsAuthorized { get; }
  bool ClientIsVerified { get; }

  bool ClientIsUser { get; set; }
  bool ClientIsAgent { get; set; }
  bool ClientIsAuthor { get; set; }
  bool ClientIsEditor { get; set; }
  bool ClientIsAdmin { get; set; }

} // end interface

// end file