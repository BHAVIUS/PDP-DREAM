﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 11/26/2022 6:00:30 PM
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
    /// There are no comments for CoreInfosetTypeItemConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class CoreInfosetTypeItemConfiguration : IEntityTypeConfiguration<CoreInfosetTypeItem>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<CoreInfosetTypeItem> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<CoreInfosetTypeItem> builder)
        {
            builder.ToTable(@"CoreInfosetTypeItem", @"dbo");
            builder.Property(x => x.CodeKey).HasColumnName(@"CodeKey").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.TypeName).HasColumnName(@"TypeName").HasColumnType(@"nvarchar(32)").ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.TypeDescription).HasColumnName(@"TypeDescription").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.HasKey(@"CodeKey");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<CoreInfosetTypeItem> builder);

        #endregion
    }

}
