﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-07-11 08:12:07
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
    /// There are no comments for NexusEntityLabelConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class NexusEntityLabelConfiguration : IEntityTypeConfiguration<NexusEntityLabel>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<NexusEntityLabel> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<NexusEntityLabel> builder)
        {
            builder.ToTable(@"NexusEntityLabel", @"dbo");
            builder.Property(x => x.FgroupGuidKey).HasColumnName(@"FgroupGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordGuidRef).HasColumnName(@"RecordGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InfosetTypeCodeRef).HasColumnName(@"InfosetTypeCodeRef").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.InfosetTypeName).HasColumnName(@"InfosetTypeName").HasColumnType(@"nvarchar(32)").ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.InfosetGuidRef).HasColumnName(@"InfosetGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityName).HasColumnName(@"EntityName").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.EntityTypeCodeRef).HasColumnName(@"EntityTypeCodeRef").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.EntityTypeName).HasColumnName(@"EntityTypeName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.EntityLabel).HasColumnName(@"EntityLabel").HasColumnType(@"nvarchar(384)").IsRequired().ValueGeneratedNever().HasMaxLength(384);
            builder.Property(x => x.TagToken).HasColumnName(@"TagToken").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.ServiceTypeCodeRef).HasColumnName(@"ServiceTypeCodeRef").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.LabelUrl).HasColumnName(@"LabelUrl").HasColumnType(@"nvarchar(384)").ValueGeneratedNever().HasMaxLength(384);
            builder.Property(x => x.LabelUri).HasColumnName(@"LabelUri").HasColumnType(@"nvarchar(256)").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            builder.Property(x => x.ServiceTypeName).HasColumnName(@"ServiceTypeName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.IsGenerating).HasColumnName(@"IsGenerating").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsResolvable).HasColumnName(@"IsResolvable").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.HasPriority).HasColumnName(@"HasPriority").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.HasIndex).HasColumnName(@"HasIndex").HasColumnType(@"tinyint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.IsPrivate).HasColumnName(@"IsPrivate").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsMarked).HasColumnName(@"IsMarked").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsPrincipal).HasColumnName(@"IsPrincipal").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ManagedByAgentGuidRef).HasColumnName(@"ManagedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.ManagedByAgentUserName).HasColumnName(@"ManagedByAgentUserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.CreatedByAgentGuidRef).HasColumnName(@"CreatedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.CreatedByAgentUserName).HasColumnName(@"CreatedByAgentUserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.UpdatedByAgentGuidRef).HasColumnName(@"UpdatedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.UpdatedByAgentUserName).HasColumnName(@"UpdatedByAgentUserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.DeletedOn).HasColumnName(@"DeletedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DeletedByAgentGuidRef).HasColumnName(@"DeletedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.DeletedByAgentUserName).HasColumnName(@"DeletedByAgentUserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.HasKey(@"FgroupGuidKey");
            builder.HasOne(x => x.NexusResrepRoot).WithMany(op => op.NexusEntityLabels).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasOne(x => x.NexusResrepStem).WithMany(op => op.NexusEntityLabels).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasOne(x => x.NexusResrepLeaf).WithMany(op => op.NexusEntityLabels).HasForeignKey(@"RecordGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<NexusEntityLabel> builder);

        #endregion
    }

}
