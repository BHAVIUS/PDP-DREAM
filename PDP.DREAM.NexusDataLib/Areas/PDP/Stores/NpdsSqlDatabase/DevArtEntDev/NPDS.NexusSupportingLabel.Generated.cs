﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-07-11 08:12:07
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
    public partial class NexusSupportingLabel {

        public NexusSupportingLabel()
        {
            OnCreated();
        }

        public virtual Guid FgroupGuidKey { get; set; }

        public virtual Guid RecordGuidRef { get; set; }

        public virtual Guid AuditGuidRef { get; set; }

        public virtual short InfosetTypeCodeRef { get; set; }

        public virtual string? InfosetTypeName { get; set; }

        public virtual byte HasIndex { get; set; }

        public virtual byte HasPriority { get; set; }

        public virtual string SupportingLabel { get; set; }

        public virtual bool IsMarked { get; set; }

        public virtual bool IsPrincipal { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid? ManagedByAgentGuidRef { get; set; }

        public virtual string? ManagedByAgentUserName { get; set; }

        public virtual DateTime? CreatedOn { get; set; }

        public virtual Guid? CreatedByAgentGuidRef { get; set; }

        public virtual string? CreatedByAgentUserName { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

        public virtual Guid? UpdatedByAgentGuidRef { get; set; }

        public virtual string? UpdatedByAgentUserName { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public virtual Guid? DeletedByAgentGuidRef { get; set; }

        public virtual string? DeletedByAgentUserName { get; set; }

        public virtual NexusResrepRoot NexusResrepRoot { get; set; }

        public virtual NexusResrepStem NexusResrepStem { get; set; }

        public virtual NexusResrepLeaf NexusResrepLeaf { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
