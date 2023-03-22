// ConfirmTokenUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ConfirmTokenUxm : IFormTaskUxm, IConfirmEmail
{
  // required parameterless constructor
  public ConfirmTokenUxm() { }
  public ConfirmTokenUxm(string id) { UserName = id; }
  public ConfirmTokenUxm(string id, string ct) { UserName = id; SecurityToken = ct; }
  public ConfirmTokenUxm(string id, string ct, Int16 ws) { UserName = id; SecurityToken = ct; WizardStep = ws; }

  // begin IFormTaskUxm
  public string? FormTitle { get; set; } = string.Empty;
  public string? FormMessage { get; set; } = string.Empty;
  public bool FormCompleted { get; set; }
  public bool ErrorOccurred { get; set; }
  public Exception? Error { get; set; } = null;
  // end IFormTaskUxm

  public string? ReturnUrlPath { get; set; } = string.Empty;

  public string? ConcatNames(string? first, string? last)
  { PersonName = first + " " + last; return PersonName; }
  public string? ConcatNames(string? first, string? middle, string? last)
  { PersonName = first + " " + middle + " " + last; return PersonName; }
  public string? PersonName { get; set; } = string.Empty;
  public string? EmailAddress { get; set; } = string.Empty;
  public string? EmailAlternate { get; set; } = string.Empty;
  public Guid UserGuid { get; set; } = Guid.Empty;


  [Display(Name = "Current username")]
  [StringLength(32, ErrorMessage = "String must be <=32 characters.")]
  public virtual string? UserName { get; set; } = string.Empty;

  [Display(Name = "Current password")]
  public virtual string? PassWord { get; set; } = string.Empty;

  [Display(Name = "Security question")]
  public virtual string? SecurityQuestion { get; set; } = string.Empty;

  [Display(Name = "Security answer")]
  public virtual string? SecurityAnswer { get; set; } = string.Empty;

  [Display(Name = "Security token")]
  public virtual string? SecurityToken { get; set; } = string.Empty;

  public bool RequireSecTok { get; set; } = false;
  public bool RequireASQ { get; set; } = false;
  public short WizardStep { get; set; } = 0;
  public bool DbtestPassed { get; set; } = false;
  public bool DbfieldReset { get; set; } = false;
  public bool NoticeSent { get; set; } = false;
  public bool TokenConfirmed { get; set; } = false;
  public bool UserLoginOk { get; set; } = false;
  public bool EmailConfirmed { get; set; } = false;

}

// end file