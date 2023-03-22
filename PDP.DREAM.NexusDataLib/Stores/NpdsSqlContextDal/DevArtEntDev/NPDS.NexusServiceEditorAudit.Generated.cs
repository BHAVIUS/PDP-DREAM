﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 3/17/2023 11:00:38 AM
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

namespace PDP.DREAM.NexusDataLib.Stores
{
    [GeneratedCode("Devart Entity Developer", "")]
    [Serializable()]
    public partial class NexusServiceEditorAudit {

        public virtual Guid AccessGuidKey { get; set; }

        public virtual Guid RecordGuidRef { get; set; }

        public virtual Guid InfosetGuidRef { get; set; }

        public virtual Guid? AccessRequestedForAgentGuidRef { get; set; }

        public virtual Guid? AccessApprovedByAgentGuidRef { get; set; }

        public virtual bool RequestIsApproved { get; set; }

        public virtual bool RequestIsDenied { get; set; }

        public virtual bool EditorHasServiceAccess { get; set; }

        public virtual short HasIndex { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? CreatedOn { get; set; }

        public virtual Guid? CreatedByAgentGuidRef { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

        public virtual Guid? UpdatedByAgentGuidRef { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public virtual Guid? DeletedByAgentGuidRef { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
