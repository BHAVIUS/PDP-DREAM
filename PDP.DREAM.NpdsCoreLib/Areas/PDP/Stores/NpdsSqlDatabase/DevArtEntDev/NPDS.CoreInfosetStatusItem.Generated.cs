﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-06-07 20:38:59
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
    public partial class CoreInfosetStatusItem {

        public CoreInfosetStatusItem()
        {
            OnCreated();
        }

        public virtual short CodeKey { get; set; }

        public virtual string StatusName { get; set; }

        public virtual string? StatusDescription { get; set; }

        public virtual bool StatusEditedByAgent { get; set; }

        public virtual bool StatusEditedByAuthor { get; set; }

        public virtual bool StatusEditedByEditor { get; set; }

        public virtual bool StatusEditedByAdmin { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
