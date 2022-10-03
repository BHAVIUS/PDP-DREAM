﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/28/2022 11:13:51 PM
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

namespace PDP.DREAM.CoreDataLib.Stores
{
    /// <summary>
    /// There are no comments for CoreFieldFormatItemConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class CoreFieldFormatItemConfiguration : IEntityTypeConfiguration<CoreFieldFormatItem>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<CoreFieldFormatItem> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<CoreFieldFormatItem> builder)
        {
            builder.ToTable(@"CoreFieldFormatItem", @"dbo");
            builder.Property(x => x.CodeKey).HasColumnName(@"CodeKey").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.FormatName).HasColumnName(@"FormatName").HasColumnType(@"nvarchar(32)").ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.FormatDescription).HasColumnName(@"FormatDescription").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.HasKey(@"CodeKey");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<CoreFieldFormatItem> builder);

        #endregion
    }

}
