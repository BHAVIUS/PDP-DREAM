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
    public partial class NexusServiceRestrictionOr {

        public virtual Guid RestrictionOrGuidKey { get; set; }

        public virtual string ServiceName { get; set; }

        public virtual Guid InfosetGuidRef { get; set; }

        public virtual Guid RecordGuidRef { get; set; }

        public virtual string ServiceLabel { get; set; }

        public virtual Guid RestrictionAndGuidRef { get; set; }

        public virtual short AndHasPriority { get; set; }

        public virtual bool IsExcluding { get; set; }

        public virtual bool IsSufficient { get; set; }

        public virtual short AndHasIndex { get; set; }

        public virtual string RestrictionName { get; set; }

        public virtual short OrHasIndex { get; set; }

        public virtual short OrHasPriority { get; set; }

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
