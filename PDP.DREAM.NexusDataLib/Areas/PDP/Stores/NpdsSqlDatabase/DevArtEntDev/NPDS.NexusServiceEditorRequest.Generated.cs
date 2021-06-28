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
    public partial class NexusServiceEditorRequest {

        public NexusServiceEditorRequest()
        {
            OnCreated();
        }

        public virtual Guid FgroupGuidKey { get; set; }

        public virtual Guid ResrepRGuidRef { get; set; }

        public virtual Guid ResrepIGuidRef { get; set; }

        public virtual string? ResrepRecordHandle { get; set; }

        public virtual string? ResrepEntityName { get; set; }

        public virtual Guid? ManagedByAgentGuidRef { get; set; }

        public virtual string? ManagedByAgentUserName { get; set; }

        public virtual Guid? AccessRequestedForAgentGuidRef { get; set; }

        public virtual string? AccessRequestedForAgentUserName { get; set; }

        public virtual Guid? AccessApprovedByAgentGuidRef { get; set; }

        public virtual string? AccessApprovedByAgentUserName { get; set; }

        public virtual bool RequestIsApproved { get; set; }

        public virtual bool RequestIsDenied { get; set; }

        public virtual bool EditorHasServiceAccess { get; set; }

        public virtual byte HasIndex { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? CreatedOn { get; set; }

        public virtual Guid? CreatedByAgentGuidRef { get; set; }

        public virtual string? CreatedByAgentUserName { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

        public virtual Guid? UpdatedByAgentGuidRef { get; set; }

        public virtual string? UpdatedByAgentUserName { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public virtual Guid? DeletedByAgentGuidRef { get; set; }

        public virtual string? DeletedByAgentUserName { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
