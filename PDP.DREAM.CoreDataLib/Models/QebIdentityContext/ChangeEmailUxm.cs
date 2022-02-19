// ChangeEmailUxm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models
{
  public interface IConfirmEmail
  {
    public string? UserName { get; set; }
    public string? SecurityToken { get; set; }
    public string? EmailAddress { get; set; }
    public bool EmailConfirmed { get; set; }
  } // interface

  public class ChangeEmailUxm : ConfirmTokenUxm, IConfirmEmail
  {
    // required parameterless constructor
    public ChangeEmailUxm() { }
    public ChangeEmailUxm(Guid ug) { UserGuid = ug; }
    public ChangeEmailUxm(string id) : base(id) { }
    public ChangeEmailUxm(string id, string ct) : base(id, ct) { }
    public ChangeEmailUxm(string id, string ct, short ws) : base(id, ct, ws) { }
    public ChangeEmailUxm(string id, string ct, string firstName, string lastName, string email) : base(id, ct)
    {
      PersonName = ConcatNames(firstName, lastName);
      NewEmail = email;
    }

    public string OldEmail { get; set; } = string.Empty;

    public string NewEmail { get; set; } = string.Empty;

    public string AltEmail { get; set; } = string.Empty;

    public bool EmailChanged { get; set; } = false;
  }

}
