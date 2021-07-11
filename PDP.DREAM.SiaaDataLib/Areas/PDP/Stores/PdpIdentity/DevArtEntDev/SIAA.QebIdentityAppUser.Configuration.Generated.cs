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
    /// There are no comments for QebIdentityAppUserConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class QebIdentityAppUserConfiguration : IEntityTypeConfiguration<QebIdentityAppUser>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<QebIdentityAppUser> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<QebIdentityAppUser> builder)
        {
            builder.ToTable(@"QebIdentityAppUser", @"dbo");
            builder.Property(x => x.UserGuidKey).HasColumnName(@"UserGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.SessionGuidRef).HasColumnName(@"SessionGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.AppGuidRef).HasColumnName(@"AppGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.AccessFailedCount).HasColumnName(@"AccessFailedCount").HasColumnType(@"int").IsRequired().ValueGeneratedNever().HasPrecision(10, 0);
            builder.Property(x => x.ConcurrencyStamp).HasColumnName(@"ConcurrencyStamp").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.DateEmailConfirmed).HasColumnName(@"DateEmailConfirmed").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateLastEdit).HasColumnName(@"DateLastEdit").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateLastLockout).HasColumnName(@"DateLastLockout").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateLastLogin).HasColumnName(@"DateLastLogin").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DatePasswordChanged).HasColumnName(@"DatePasswordChanged").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateProfileChanged).HasColumnName(@"DateProfileChanged").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateTokenExpired).HasColumnName(@"DateTokenExpired").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateUserCreated).HasColumnName(@"DateUserCreated").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DateUserNameChanged).HasColumnName(@"DateUserNameChanged").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.EmailAddress).HasColumnName(@"EmailAddress").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.EmailAlternate).HasColumnName(@"EmailAlternate").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.EmailConfirmed).HasColumnName(@"EmailConfirmed").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.LockoutEnabled).HasColumnName(@"LockoutEnabled").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.LockoutEnd).HasColumnName(@"LockoutEnd").HasColumnType(@"datetimeoffset").ValueGeneratedNever();
            builder.Property(x => x.LockoutEndDateUtc).HasColumnName(@"LockoutEndDateUtc").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.Organization).HasColumnName(@"Organization").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.PasswordHash).HasColumnName(@"PasswordHash").HasColumnType(@"nvarchar(1024)").IsRequired().ValueGeneratedNever().HasMaxLength(1024);
            builder.Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.PhoneNumberConfirmed).HasColumnName(@"PhoneNumberConfirmed").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.SecurityAnswer).HasColumnName(@"SecurityAnswer").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.SecurityQuestion).HasColumnName(@"SecurityQuestion").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.SecurityStamp).HasColumnName(@"SecurityStamp").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.SecurityToken).HasColumnName(@"SecurityToken").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.TwoFactorEnabled).HasColumnName(@"TwoFactorEnabled").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.UserIsApproved).HasColumnName(@"UserIsApproved").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.UserNameDisplayed).HasColumnName(@"UserNameDisplayed").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.WebsiteAddress).HasColumnName(@"WebsiteAddress").HasColumnType(@"nvarchar(256)").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            builder.HasKey(@"UserGuidKey");
            builder.HasOne(x => x.QebIdentityApp).WithMany(op => op.QebIdentityAppUsers).HasForeignKey(@"AppGuidRef").IsRequired(true);
            builder.HasMany(x => x.QebIdentityAppUserRoles).WithOne(op => op.QebIdentityAppUser).HasForeignKey(@"UserGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<QebIdentityAppUser> builder);

        #endregion
    }

}
