﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-07-11 07:55:46
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
    public partial class CoreServiceNpdsRoot {

        public CoreServiceNpdsRoot()
        {
            OnCreated();
        }

        public virtual Guid ServiceIGuid { get; set; }

        public virtual string? ServiceName { get; set; }

        public virtual string? ServiceNature { get; set; }

        public virtual Guid ServiceRGuid { get; set; }

        public virtual short ServiceTCode { get; set; }

        public virtual string ServiceTName { get; set; }

        public virtual string ServicePTag { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
