﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PDP.DREAM.CoreDataLib.Models;

public partial class SiaaNetRole
{
    public Guid AppGuidRef { get; set; }

    public Guid RoleGuidKey { get; set; }

    public string RoleName { get; set; }

    public string RoleDescription { get; set; }

    public string ConcurrencyStamp { get; set; }

    public virtual SiaaNetApp AppGuidRefNavigation { get; set; }

    public virtual ICollection<SiaaNetRoleClaim> SiaaNetRoleClaims { get; } = new List<SiaaNetRoleClaim>();

    public virtual ICollection<SiaaNetUserRoleLink> SiaaNetUserRoleLinks { get; } = new List<SiaaNetUserRoleLink>();
}