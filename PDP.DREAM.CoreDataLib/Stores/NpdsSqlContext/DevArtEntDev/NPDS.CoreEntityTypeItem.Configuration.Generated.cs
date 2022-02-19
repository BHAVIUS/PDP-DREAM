﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2/7/2022 4:14:51 PM
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
    /// There are no comments for CoreEntityTypeItemConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class CoreEntityTypeItemConfiguration : IEntityTypeConfiguration<CoreEntityTypeItem>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<CoreEntityTypeItem> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<CoreEntityTypeItem> builder)
        {
            builder.ToTable(@"CoreEntityTypeItem", @"dbo");
            builder.Property(x => x.CodeKey).HasColumnName(@"CodeKey").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.TypeName).HasColumnName(@"TypeName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.TypeDescription).HasColumnName(@"TypeDescription").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.TypeIsComponent).HasColumnName(@"TypeIsComponent").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.TypeIsConstituent).HasColumnName(@"TypeIsConstituent").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.TypeEditedByAgent).HasColumnName(@"TypeEditedByAgent").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.TypeEditedByAuthor).HasColumnName(@"TypeEditedByAuthor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.TypeEditedByEditor).HasColumnName(@"TypeEditedByEditor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.TypeEditedByAdmin).HasColumnName(@"TypeEditedByAdmin").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.HasKey(@"CodeKey");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<CoreEntityTypeItem> builder);

        #endregion
    }

}
