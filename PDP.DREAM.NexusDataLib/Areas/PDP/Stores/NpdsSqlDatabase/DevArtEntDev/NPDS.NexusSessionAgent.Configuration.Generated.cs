﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-06-07 21:18:53
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
    /// There are no comments for NexusSessionAgentConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class NexusSessionAgentConfiguration : IEntityTypeConfiguration<NexusSessionAgent>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<NexusSessionAgent> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<NexusSessionAgent> builder)
        {
            builder.ToTable(@"NexusSessionAgent", @"dbo");
            builder.Property(x => x.AgentGuidKey).HasColumnName(@"AgentGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.AgentInfosetGuidRef).HasColumnName(@"AgentInfosetGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.AgentIsAuthor).HasColumnName(@"AgentIsAuthor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.AgentIsEditor).HasColumnName(@"AgentIsEditor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.AgentIsAdmin).HasColumnName(@"AgentIsAdmin").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IdentityUserGuidRef).HasColumnName(@"IdentityUserGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IdentityUserName).HasColumnName(@"IdentityUserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.IdentitySystemGuidRef).HasColumnName(@"IdentitySystemGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.SessionGuidKey).HasColumnName(@"SessionGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.SessionDateCreated).HasColumnName(@"SessionDateCreated").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.SessionDateAccessed).HasColumnName(@"SessionDateAccessed").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.HasKey(@"AgentGuidKey");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<NexusSessionAgent> builder);

        #endregion
    }

}
