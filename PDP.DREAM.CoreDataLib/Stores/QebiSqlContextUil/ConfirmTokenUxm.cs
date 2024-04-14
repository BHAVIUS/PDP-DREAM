// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ConfirmTokenUxm : FormTaskUxmBase, IConfirmEmail
{
  // required parameterless constructor
  public ConfirmTokenUxm() { }
  public ConfirmTokenUxm(string id) { UserName = id; }
  public ConfirmTokenUxm(string id, string ct) { UserName = id; SecurityToken = ct; }
  public ConfirmTokenUxm(string id, string ct, Int16 ws) { UserName = id; SecurityToken = ct; WizardStep = ws; }

  public string? ReturnUrlPath { get; set; } = ESS;

  //public string? ConcatNames(string? first, string? last)
  //{ PersonName = first + " " + last; return PersonName; }
  //public string? ConcatNames(string? first, string? middle, string? last)
  //{ PersonName = first + " " + middle + " " + last; return PersonName; }
  //public string? PersonName { get; set; } = ESS;
  public string? EmailAddress { get; set; } = ESS;
  public string? EmailAlternate { get; set; } = ESS;
  public Guid UserGuid { get; set; } = EGS;


  [Display(Name = "Current username")]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public virtual string? UserName { get; set; } = ESS;

  [Display(Name = "Current password")]
  public virtual string? PassWord { get; set; } = ESS;

  [Display(Name = "Security question")]
  public virtual string? SecurityQuestion { get; set; } = ESS;

  [Display(Name = "Security answer")]
  public virtual string? SecurityAnswer { get; set; } = ESS;

  [Display(Name = "Security token")]
  public virtual string? SecurityToken { get; set; } = ESS;

  public bool RequireSecTok { get; set; } = false;
  public bool RequireASQ { get; set; } = false;
  public short WizardStep { get; set; } = 0;
  public bool DbtestPassed { get; set; } = false;
  public bool DbfieldReset { get; set; } = false;
  public bool TokenConfirmed { get; set; } = false;
  public bool UserLoginOk { get; set; } = false;
  public bool EmailConfirmed { get; set; } = false;

}

// end file