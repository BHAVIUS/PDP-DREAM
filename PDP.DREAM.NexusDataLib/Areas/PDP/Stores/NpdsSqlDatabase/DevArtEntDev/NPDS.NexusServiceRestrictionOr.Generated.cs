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
    public partial class NexusServiceRestrictionOr {

        public NexusServiceRestrictionOr()
        {
            OnCreated();
        }

        public virtual Guid RestrictionOrGuidKey { get; set; }

        public virtual string? ServiceName { get; set; }

        public virtual Guid InfosetGuidRef { get; set; }

        public virtual Guid RecordGuidRef { get; set; }

        public virtual string ServiceLabel { get; set; }

        public virtual Guid RestrictionAndGuidRef { get; set; }

        public virtual Guid AndAuditGuidRef { get; set; }

        public virtual byte AndHasPriority { get; set; }

        public virtual byte AndHasIndex { get; set; }

        public virtual string RestrictionName { get; set; }

        public virtual Guid OrAuditGuidRef { get; set; }

        public virtual byte OrHasIndex { get; set; }

        public virtual byte OrHasPriority { get; set; }

        public virtual string RestrictionValue { get; set; }

        public virtual bool IsWordPhrase { get; set; }

        public virtual bool IsConceptLabel { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? CreatedOn { get; set; }

        public virtual Guid? CreatedByAgentGuid { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

        public virtual Guid? UpdatedByAgentGuid { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public virtual Guid? DeletedByAgentGuid { get; set; }

        public virtual NexusServiceRestrictionAnd NexusServiceRestrictionAnd { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
