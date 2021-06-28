﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-06-07 20:38:59
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable disable

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
    /// <summary>
    /// There are no comments for CoreServiceTypeItemConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class CoreServiceTypeItemConfiguration : IEntityTypeConfiguration<CoreServiceTypeItem>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<CoreServiceTypeItem> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<CoreServiceTypeItem> builder)
        {
            builder.ToTable(@"CoreServiceTypeItem", @"dbo");
            builder.Property(x => x.CodeKey).HasColumnName(@"CodeKey").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.TypeName).HasColumnName(@"TypeName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.TypeDescription).HasColumnName(@"TypeDescription").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.DefaultGeneratingLabel).HasColumnName(@"DefaultGeneratingLabel").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.HasKey(@"CodeKey");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<CoreServiceTypeItem> builder);

        #endregion
    }

}
