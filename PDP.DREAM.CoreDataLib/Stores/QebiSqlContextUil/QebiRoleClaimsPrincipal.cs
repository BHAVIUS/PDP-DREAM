// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public interface IQebiRoleClaimsPrincipal : IPrincipal { }

// QEBI RoleClaimsPrincipal (QebiRcp)
public class QebiRcp : ClaimsPrincipal, IQebiRoleClaimsPrincipal
{
  // System.Security.Principal.IIdentity
  // IIdentity has string? AuthenticationType, string? Name, bool IsAuthenticated
  public QebiRcp(IIdentity identity) : base(identity) { }

  // System.Security.Principal.IPrincipal
  // IPrincipal has IIdentity? Identity, bool IsInRole
  public QebiRcp(IPrincipal principal) : base(principal) { }

  public QebiRcp RCP { get { return this; } }

  private bool rcpIdent = false;
  public bool IsIdentified
  {
    get {
      if (RCP == null) { rcpIdent = false; }
      else if (RCP.Identities != null)
      {
        rcpIdent = RCP.Identities
        .Any(i => (i.AuthenticationType == PdpIdentityScheme));
      }
      return rcpIdent;
    }
  }

  private bool rcpAuthent = false;
  public bool IsAuthenticated
  {
    get {
      if (RCP == null) { rcpAuthent = false; }
      else if (RCP.Identity != null)
      {
        rcpAuthent = RCP.Identity.IsAuthenticated;
      }
      return rcpAuthent;
    }
  }

  private string rcpUserName = ESS;
  public string UserName
  {
    get {
      if (string.IsNullOrEmpty(rcpUserName))
      {
        rcpUserName = QebiExtensions.GetUserName(RCP);
      }
      return rcpUserName;
    }
  }

  private string rcpUserAlias = ESS;
  public string UserAlias
  {
    get {
      if (string.IsNullOrEmpty(rcpUserAlias))
      {
        rcpUserAlias = QebiExtensions.GetUserAlias(RCP);
      }
      return rcpUserAlias;
    }
  }

  private string rcpUserEmail = ESS;
  public string UserEmail
  {
    get {
      if (string.IsNullOrEmpty(rcpUserEmail))
      {
        rcpUserEmail = QebiExtensions.GetUserEmail(RCP);
      }
      return rcpUserEmail;
    }
  }

  private Guid rcpUserGuid = EGS;
  public Guid UserGuid
  {
    get {
      if (rcpUserGuid.IsEmpty())
      {
        rcpUserGuid = QebiExtensions.GetUserGuid(RCP);
      }
      return rcpUserGuid;
    }
  }

  // TODO: LINQ mappings between types QebiRcp and QebiUser
  private QebiUser? qebUsr = null;
  public QebiUser QebUser
  {
    get {
      return qebUsr;
    }
    set {
      qebUsr = value;
      rcpUserGuid = qebUsr.UserGuid;
      rcpUserName = qebUsr.UserName;
      rcpUserAlias = qebUsr.UserAlias;
      rcpUserEmail = qebUsr.EmailAddress;
    }
  }

  // TODO: LINQ mappings between types QebiRcp and NpdsClient
  public void UpdateWrace(ref NpdsClientWrace wrace)
  {
    wrace.ClientIsAuthenticated = this.IsAuthenticated;
    wrace.CiaamUserName = this.UserName;
    wrace.CiaamUserAlias = this.UserAlias;
    wrace.CiaamUserEmail = this.UserEmail;
    wrace.QebiUserGuid = this.UserGuid;
  }

} // end class

// end file