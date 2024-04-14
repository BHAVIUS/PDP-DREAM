// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class QebiUser : QebiDalEntity
{
  public QebiUser()
  {
    AppGuid = OnEntityCreated(AppGuid);
  }

  public QebiUser(IUserSecurityEdit m)
  {
    RegisterProfileModel(m);
  }

  public void RegisterProfileModel(IUserSecurityEdit m)
  {
    this.AppGuid = PDPSS.CiaamAppGuid;
   // this.AgentGuid = PdpNewGuid();
    this.UserGuid = PdpNewGuid();
    this.UserName = m.UserName;
    if (string.IsNullOrEmpty(m.UserAlias)) { this.UserAlias = m.UserName; }
    else { this.UserAlias = m.UserAlias; }
    this.FirstName = m.FirstName ?? "";
    this.LastName = m.LastName ?? "";
    this.PhoneNumber = m.PhoneNumber ?? "";
    if (string.IsNullOrEmpty(m.PhoneAlternate)) { this.PhoneAlternate = m.PhoneAlternate; }
    else { this.PhoneAlternate = m.PhoneAlternate; }
    this.EmailAddress = m.EmailAddress ?? "";
    if (string.IsNullOrEmpty(m.EmailAlternate)) { this.EmailAlternate = m.EmailAddress; }
    else { this.EmailAlternate = m.EmailAlternate; }
    this.WebsiteAddress = m.WebsiteAddress ?? "";
    this.Organization = m.Organization ?? "";
    this.SecurityQuestion = m.SecurityQuestion ?? "";
    this.SecurityAnswer = m.SecurityAnswer ?? "";
    this.SecurityStamp = PdpNewGuid().ToString();
    this.SecurityToken = QebCryptoService.GenerateToken();
    this.PasswordHash = QebCryptoService.HashToken(m.PassWord);
    this.DateUserCreated = DateTime.UtcNow;
    this.DateTokenExpired = DateTime.UtcNow.AddHours(24);
    // TODO: recode to check options on startup for confirmation requirements
    // else initialize true to simplify and handle case when email confirmation not required
    this.UserIsApproved = false;
  }

  public ChangeProfileUxm GetChangeProfileModel()
  {
    var m = new ChangeProfileUxm();
    m.DateLastEdit = this.DateLastEdit;
    m.DateProfileChanged = this.DateProfileChanged;
    m.UserName = this.UserName ?? "";
    m.UserAlias = this.UserAlias ?? "";
    m.FirstName = this.FirstName ?? "";
    m.LastName = this.LastName ?? "";
    m.PhoneNumber = this.PhoneNumber ?? "";
    m.PhoneAlternate = this.PhoneAlternate ?? "";
    m.WebsiteAddress = this.WebsiteAddress ?? "";
    m.Organization = this.Organization ?? "";
    m.SecurityQuestion = this.SecurityQuestion ?? "";
    m.SecurityAnswer = this.SecurityAnswer ?? "";
    return m;
  }

  public void SetChangeProfileModel(IUserProfileEdit m)
  {
    this.DateLastEdit = DateTime.UtcNow;
    this.DateProfileChanged = DateTime.UtcNow;
    this.UserAlias = m.UserAlias ?? "";
    this.FirstName = m.FirstName ?? "";
    this.LastName = m.LastName ?? "";
    this.PhoneNumber = m.PhoneNumber ?? "";
    this.PhoneAlternate = m.PhoneAlternate ?? "";
    this.WebsiteAddress = m.WebsiteAddress ?? "";
    this.Organization = m.Organization ?? "";
    this.SecurityQuestion = m.SecurityQuestion ?? "";
    this.SecurityAnswer = m.SecurityAnswer ?? "";
  }

} // class

// end file