﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/28/2022 11:37:15 PM
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
    public partial class QebIdentityAppUserSession {

        public virtual Guid SessionGuidKey { get; set; }

        public virtual DateTime? SessionDateCreated { get; set; }

        public virtual DateTime? SessionDateAccessed { get; set; }

        public virtual DateTime? SessionDateExpired { get; set; }

        public virtual Guid UserGuidKey { get; set; }

        public virtual string UserName { get; set; }

        public virtual string UserNameDisplayed { get; set; }

        public virtual bool UserIsApproved { get; set; }

        public virtual bool UserIsAgent { get; set; }

        public virtual Guid? AgentGuidRef { get; set; }

        public virtual bool AgentIsAuthor { get; set; }

        public virtual bool AgentIsEditor { get; set; }

        public virtual bool AgentIsAdmin { get; set; }

        public virtual Guid AppGuidKey { get; set; }

        public virtual string AppName { get; set; }

        public virtual string AppDescription { get; set; }

        public virtual string ConcurrencyStamp { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
