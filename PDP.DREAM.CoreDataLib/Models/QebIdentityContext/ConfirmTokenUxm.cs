// ConfirmTokenUxm.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class ConfirmTokenUxm : RestTaskUxm, IConfirmEmail
  {
    // required parameterless constructor
    public ConfirmTokenUxm() { }
    public ConfirmTokenUxm(string id) { UserName = id; }
    public ConfirmTokenUxm(string id, string ct) { UserName = id; SecurityToken = ct; }
    public ConfirmTokenUxm(string id, string ct, Int16 ws) { UserName = id; SecurityToken = ct; WizardStep = ws; }

    public string ConcatNames(string first, string last)
    { PersonName = first + " " + last; return PersonName; }
    public string ConcatNames(string first, string middle, string last)
    { PersonName =  first + " " + middle + " " + last; return PersonName; }

    public string? PersonName { get; set; } = string.Empty;
    public string? EmailAddress { get; set; } = string.Empty;
    public Guid UserGuid { get; set; } = Guid.Empty;

    public virtual string? UserName { get; set; } = string.Empty;

    public virtual string? PassWord { get; set; } = string.Empty;

    public virtual string? SecurityQuestion { get; set; } = string.Empty;

    public virtual string? SecurityAnswer { get; set; } = string.Empty;

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

}
