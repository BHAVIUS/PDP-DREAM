﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 3/17/2023 11:00:38 AM
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

namespace PDP.DREAM.NexusDataLib.Stores
{
    /// <summary>
    /// There are no comments for NexusDescriptionConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class NexusDescriptionConfiguration : IEntityTypeConfiguration<NexusDescription>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<NexusDescription> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<NexusDescription> builder)
        {
            builder.ToTable(@"NexusDescription", @"dbo");
            builder.Property(x => x.FgroupGuidKey).HasColumnName(@"FgroupGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordGuidRef).HasColumnName(@"RecordGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InfosetTypeCodeRef).HasColumnName(@"InfosetTypeCodeRef").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.InfosetTypeName).HasColumnName(@"InfosetTypeName").HasColumnType(@"nvarchar(32)").ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.FieldFormatCodeRef).HasColumnName(@"FieldFormatCodeRef").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.FieldFormatName).HasColumnName(@"FieldFormatName").HasColumnType(@"nvarchar(32)").ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(max)").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.HasIndex).HasColumnName(@"HasIndex").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.HasPriority).HasColumnName(@"HasPriority").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(3, 0);
            builder.Property(x => x.IsMarked).HasColumnName(@"IsMarked").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsPrincipal).HasColumnName(@"IsPrincipal").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ManagedByAgentGuidRef).HasColumnName(@"ManagedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.ManagedByAgentUserName).HasColumnName(@"ManagedByAgentUserName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.CreatedByAgentGuidRef).HasColumnName(@"CreatedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.CreatedByAgentUserName).HasColumnName(@"CreatedByAgentUserName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.UpdatedByAgentGuidRef).HasColumnName(@"UpdatedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.UpdatedByAgentUserName).HasColumnName(@"UpdatedByAgentUserName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.DeletedOn).HasColumnName(@"DeletedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DeletedByAgentGuidRef).HasColumnName(@"DeletedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.DeletedByAgentUserName).HasColumnName(@"DeletedByAgentUserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.HasKey(@"FgroupGuidKey");
            builder.HasOne(x => x.NexusResrepRoot).WithMany(op => op.NexusDescriptions).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasOne(x => x.NexusResrepStem).WithMany(op => op.NexusDescriptions).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasOne(x => x.NexusResrepLeaf).WithMany(op => op.NexusDescriptions).HasForeignKey(@"RecordGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<NexusDescription> builder);

        #endregion
    }

}
