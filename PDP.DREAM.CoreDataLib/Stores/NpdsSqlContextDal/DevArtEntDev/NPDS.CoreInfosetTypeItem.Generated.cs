﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 10/7/2022 2:39:27 PM
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
    public partial class CoreInfosetTypeItem {

        public virtual short CodeKey { get; set; }

        public virtual string? TypeName { get; set; }

        public virtual string? TypeDescription { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
