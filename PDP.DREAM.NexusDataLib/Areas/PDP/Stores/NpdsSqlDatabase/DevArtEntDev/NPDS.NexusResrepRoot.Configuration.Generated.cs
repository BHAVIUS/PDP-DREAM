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
    /// There are no comments for NexusResrepRootConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class NexusResrepRootConfiguration : IEntityTypeConfiguration<NexusResrepRoot>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<NexusResrepRoot> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<NexusResrepRoot> builder)
        {
            builder.ToTable(@"NexusResrepRoot", @"dbo");
            builder.Property(x => x.RecordGuidKey).HasColumnName(@"RecordGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityTypeCodeRef).HasColumnName(@"EntityTypeCodeRef").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.EntityTypeName).HasColumnName(@"EntityTypeName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.EntityTypeIsComponent).HasColumnName(@"EntityTypeIsComponent").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityTypeIsConstituent).HasColumnName(@"EntityTypeIsConstituent").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityTypeEditedByAuthor).HasColumnName(@"EntityTypeEditedByAuthor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityTypeEditedByEditor).HasColumnName(@"EntityTypeEditedByEditor").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityTypeEditedByAdmin).HasColumnName(@"EntityTypeEditedByAdmin").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.EntityInitialTag).HasColumnName(@"EntityInitialTag").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.EntityName).HasColumnName(@"EntityName").HasColumnType(@"nvarchar(256)").ValueGeneratedNever().HasMaxLength(256);
            builder.Property(x => x.EntityNature).HasColumnName(@"EntityNature").HasColumnType(@"nvarchar(1024)").ValueGeneratedNever().HasMaxLength(1024);
            builder.Property(x => x.RecordHandle).HasColumnName(@"RecordHandle").HasColumnType(@"char(9)").ValueGeneratedNever().HasMaxLength(9);
            builder.Property(x => x.RecordIsDeleted).HasColumnName(@"RecordIsDeleted").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordIsCached).HasColumnName(@"RecordIsCached").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordDiristryGuidRef).HasColumnName(@"RecordDiristryGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordDiristryTag).HasColumnName(@"RecordDiristryTag").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RecordRegistryGuidRef).HasColumnName(@"RecordRegistryGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordRegistryTag).HasColumnName(@"RecordRegistryTag").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RecordDirectoryGuidRef).HasColumnName(@"RecordDirectoryGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordDirectoryTag).HasColumnName(@"RecordDirectoryTag").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RecordCreatedOn).HasColumnName(@"RecordCreatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.RecordRegistrarTag).HasColumnName(@"RecordRegistrarTag").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.RecordUpdatedOn).HasColumnName(@"RecordUpdatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.RecordRegistrarGuidRef).HasColumnName(@"RecordRegistrarGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordDeletedOn).HasColumnName(@"RecordDeletedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.RecordCreatedByAgentGuidRef).HasColumnName(@"RecordCreatedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.RecordUpdatedByAgentGuidRef).HasColumnName(@"RecordUpdatedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.RecordDeletedByAgentGuidRef).HasColumnName(@"RecordDeletedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.RecordManagedByAgentGuidRef).HasColumnName(@"RecordManagedByAgentGuidRef").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.InfosetGuidKey).HasColumnName(@"InfosetGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InfosetIsAuthorPrivate).HasColumnName(@"InfosetIsAuthorPrivate").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InfosetIsAgentShared).HasColumnName(@"InfosetIsAgentShared").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InfosetIsUpdaterLimited).HasColumnName(@"InfosetIsUpdaterLimited").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.InfosetIsManagerReleased).HasColumnName(@"InfosetIsManagerReleased").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.HasKey(@"RecordGuidKey");
            builder.HasMany(x => x.NexusEntityLabels).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusSupportingTags).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusSupportingLabels).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusCrossReferences).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusOtherTexts).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusLocations).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusDescriptions).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusProvenances).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusDistributions).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusFairMetrics).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusEntityCanonicalLabels).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasMany(x => x.NexusEntityAliasLabels).WithOne(op => op.NexusResrepRoot).HasForeignKey(@"RecordGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<NexusResrepRoot> builder);

        #endregion
    }

}
