﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-06-07 21:18:53
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

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
    [GeneratedCode("Devart Entity Developer", "")]
    [Serializable()]
    public partial class NexusSessionAgent {

        public NexusSessionAgent()
        {
            OnCreated();
        }

        public virtual Guid AgentGuidKey { get; set; }

        public virtual Guid? AgentInfosetGuidRef { get; set; }

        public virtual bool AgentIsAuthor { get; set; }

        public virtual bool AgentIsEditor { get; set; }

        public virtual bool AgentIsAdmin { get; set; }

        public virtual Guid IdentityUserGuidRef { get; set; }

        public virtual string? IdentityUserName { get; set; }

        public virtual Guid IdentitySystemGuidRef { get; set; }

        public virtual Guid SessionGuidKey { get; set; }

        public virtual DateTime? SessionDateCreated { get; set; }

        public virtual DateTime? SessionDateAccessed { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
