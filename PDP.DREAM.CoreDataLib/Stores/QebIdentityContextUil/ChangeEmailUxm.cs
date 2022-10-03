// ChangeEmailUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models;

public interface IConfirmEmail
{
  public string? UserName { get; set; }
  public string? SecurityToken { get; set; }
  public string? EmailAddress { get; set; }
  public string? EmailAlternate { get; set; }
  public bool EmailConfirmed { get; set; }
} // interface

public class ChangeEmailUxm : ConfirmTokenUxm, IConfirmEmail, IFormTaskUxm
{
  // required parameterless constructor
  public ChangeEmailUxm() { InitPath(); }
  public ChangeEmailUxm(Guid ug) { InitPath(); UserGuid = ug; }
  public ChangeEmailUxm(string id, string ct) : base(id, ct) {InitPath();  }
  public ChangeEmailUxm(string id, string ct, short ws) : base(id, ct, ws) { InitPath(); }
  public ChangeEmailUxm(string id, string ct, string firstName, string lastName, string email) : base(id, ct)
  {
    InitPath(); 
    PersonName = ConcatNames(firstName, lastName);
    NewEmail = email;
  }

  public void InitPath()
  {
    ReturnUrlPath = "/NPDS/AnonCore/ConfirmEmail";
  }

  public string? OldEmail { get; set; } = string.Empty;

  public string? NewEmail { get; set; } = string.Empty;

  public string? AltEmail { get; set; } = string.Empty;

  public bool EmailChanged { get; set; } = false;
}

// end file