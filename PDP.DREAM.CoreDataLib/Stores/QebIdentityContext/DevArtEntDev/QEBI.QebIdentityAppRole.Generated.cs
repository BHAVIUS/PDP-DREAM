﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2/8/2022 4:03:34 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable annotations
#nullable disable warnings

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace PDP.DREAM.CoreDataLib.Stores
{
    [GeneratedCode("Devart Entity Developer", "")]
    [Serializable()]
    public partial class QebIdentityAppRole {

        public virtual Guid RoleGuidKey { get; set; }

        public virtual Guid AppGuidRef { get; set; }

        public virtual string RoleName { get; set; }

        public virtual string RoleDescription { get; set; }

        public virtual string ConcurrencyStamp { get; set; }

        public virtual QebIdentityApp QebIdentityApp { get; set; }

        public virtual IList<QebIdentityAppUserRole> QebIdentityAppUserRoles { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
