using System;
using PDP.DREAM.NpdsCoreLib.Types;
using System.ComponentModel.DataAnnotations;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext : IPdpNetUser
  {
    public bool RememberMe { get; set; } = false;
    public bool RequireASQ { get; set; } = false;
    public bool RequireSecTok { get; set; } = false;
    public bool UserIsApproved { get; set; } = false;

    public short WizardStep { get; set; } = 0;

    public DateTime? DateEmailConfirmed { get; set; } = null;
    public DateTime? DateLastEdit { get; set; } = null;
    public DateTime? DateLastLogin { get; set; } = null;
    public DateTime? DatePasswordChanged { get; set; } = null;
    public DateTime? DateProfileChanged { get; set; } = null;
    public DateTime? DateTokenExpired { get; set; } = null;
    public DateTime? DateUserCreated { get; set; } = null;

    public string? PassWord { get; set; } = string.Empty;
    public string? PasswordHash { get; set; } = string.Empty;
    public string? AltPassword { get; set; } = string.Empty;
    public string? NewPassword { get; set; } = string.Empty;
    public string? SecurityQuestion { get; set; } = string.Empty;
    public string? SecurityAnswer { get; set; } = string.Empty;
    public string? SecurityToken { get; set; } = string.Empty;

    public string? EmailSubject { get; set; } = string.Empty;
    public string? EmailBody { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? OldUsername { get; set; } = string.Empty;
    public string? NewUsername { get; set; } = string.Empty;
    public string? AltUsername { get; set; } = string.Empty;
    public string? OldEmail { get; set; } = string.Empty;
    public string? NewEmail { get; set; } = string.Empty;
    public string? AltEmail { get; set; } = string.Empty;
    public string? EmailAddress { get; set; } = string.Empty;
    public string? EmailAlternate { get; set; } = string.Empty;
    public string? WebsiteAddress { get; set; } = string.Empty;
    public string? Organization { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
    public string? UserNameDisplayed { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; } = string.Empty;

    // IDENTIFICATION/AUTHENTICATION

    // string-ids are "Key"s while guid-ids are "Guid"s
    public string? UserKey
    {
      get { return usrKey; }
      set { usrKey = value; UserGuid = PdpGuid.Parse(usrKey); }
    }
    private string? usrKey = string.Empty;

    public string? AgentKey
    {
      get { return agtKey; }
      set { agtKey = value; AgentGuid = PdpGuid.Parse(agtKey); }
    }
    private string? agtKey = string.Empty;

    public string? AgentInfosetKey
    {
      get { return ifsKey; }
      set { ifsKey = value; AgentInfosetGuid = PdpGuid.Parse(ifsKey); }
    }
    private string? ifsKey = string.Empty;

    public string? SessionKey
    {
      get { return ssnKey; }
      set { ssnKey = value; SessionGuid = PdpGuid.Parse(ssnKey); }
    }
    private string? ssnKey = string.Empty;

    public Guid ApplicationGuid { get; } = PdpSiteSettings.GetValues.AppSecureUiaaGuid;

    // user agent session guids for service APIs

    [Display(Name = "PDP User Key"), StringLength(24, ErrorMessage = "String must be <=24 characters.")]
    public Guid UserGuid { get; set; }

    [Display(Name = "PDP Agent Key"), StringLength(24, ErrorMessage = "String must be <=24 characters.")]
    public Guid AgentGuid { get; set; }

    [Display(Name = "PDP AgentInfoset Key"), StringLength(24, ErrorMessage = "String must be <=24 characters.")]
    public Guid AgentInfosetGuid { get; set; }

    [Display(Name = "PDP Session Key"), StringLength(24, ErrorMessage = "String must be <=24 characters.")]
    public Guid SessionGuid { get; set; }

    [Display(Name = "PDP Session Value IsRequired")]
    public bool SessionValueIsRequired { get; set; } = false;

  }

}