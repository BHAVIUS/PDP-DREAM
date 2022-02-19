﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2/8/2022 4:03:34 PM
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
    /// There are no comments for QebIdentityAppConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class QebIdentityAppConfiguration : IEntityTypeConfiguration<QebIdentityApp>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<QebIdentityApp> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<QebIdentityApp> builder)
        {
            builder.ToTable(@"QebIdentityApp", @"dbo");
            builder.Property(x => x.AppGuidKey).HasColumnName(@"AppGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.AppName).HasColumnName(@"AppName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.AppDescription).HasColumnName(@"AppDescription").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.ConcurrencyStamp).HasColumnName(@"ConcurrencyStamp").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.HasKey(@"AppGuidKey");
            builder.HasMany(x => x.QebIdentityAppUsers).WithOne(op => op.QebIdentityApp).HasForeignKey(@"AppGuidRef").IsRequired(true);
            builder.HasMany(x => x.QebIdentityAppRoles).WithOne(op => op.QebIdentityApp).HasForeignKey(@"AppGuidRef").IsRequired(true);
            builder.HasMany(x => x.QebIdentityAppUserRoles).WithOne(op => op.QebIdentityApp).HasForeignKey(@"AppGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<QebIdentityApp> builder);

        #endregion
    }

}
