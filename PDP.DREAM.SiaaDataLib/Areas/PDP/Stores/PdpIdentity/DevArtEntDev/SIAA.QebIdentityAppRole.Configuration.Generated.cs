﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-06-27 18:49:00
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
    /// There are no comments for QebIdentityAppRoleConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class QebIdentityAppRoleConfiguration : IEntityTypeConfiguration<QebIdentityAppRole>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<QebIdentityAppRole> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<QebIdentityAppRole> builder)
        {
            builder.ToTable(@"QebIdentityAppRole", @"dbo");
            builder.Property(x => x.RoleGuidKey).HasColumnName(@"RoleGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.AppGuidRef).HasColumnName(@"AppGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RoleName).HasColumnName(@"RoleName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RoleDescription).HasColumnName(@"RoleDescription").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.HasKey(@"RoleGuidKey");
            builder.HasOne(x => x.QebIdentityApp).WithMany(op => op.QebIdentityAppRoles).HasForeignKey(@"AppGuidRef").IsRequired(true);
            builder.HasMany(x => x.QebIdentityAppUserRoles).WithOne(op => op.QebIdentityAppRole).HasForeignKey(@"RoleGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<QebIdentityAppRole> builder);

        #endregion
    }

}
