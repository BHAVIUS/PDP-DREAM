﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-07-11 07:51:08
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

namespace PDP.DREAM.SiaaDataLib.Stores.PdpIdentity
{
    /// <summary>
    /// There are no comments for QebIdentityAppUserRoleConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class QebIdentityAppUserRoleConfiguration : IEntityTypeConfiguration<QebIdentityAppUserRole>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<QebIdentityAppUserRole> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<QebIdentityAppUserRole> builder)
        {
            builder.ToTable(@"QebIdentityAppUserRole", @"dbo");
            builder.Property(x => x.LinkGuidKey).HasColumnName(@"LinkGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.UserGuidRef).HasColumnName(@"UserGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.UserNameDisplayed).HasColumnName(@"UserNameDisplayed").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RoleGuidRef).HasColumnName(@"RoleGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RoleName).HasColumnName(@"RoleName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RoleDescription).HasColumnName(@"RoleDescription").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.AppGuidRef).HasColumnName(@"AppGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.AppName).HasColumnName(@"AppName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.AppDescription).HasColumnName(@"AppDescription").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.ConcurrencyStamp).HasColumnName(@"ConcurrencyStamp").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.HasKey(@"LinkGuidKey");
            builder.HasOne(x => x.QebIdentityAppUser).WithMany(op => op.QebIdentityAppUserRoles).HasForeignKey(@"UserGuidRef").IsRequired(true);
            builder.HasOne(x => x.QebIdentityAppRole).WithMany(op => op.QebIdentityAppUserRoles).HasForeignKey(@"RoleGuidRef").IsRequired(true);
            builder.HasOne(x => x.QebIdentityApp).WithMany(op => op.QebIdentityAppUserRoles).HasForeignKey(@"AppGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<QebIdentityAppUserRole> builder);

        #endregion
    }

}
