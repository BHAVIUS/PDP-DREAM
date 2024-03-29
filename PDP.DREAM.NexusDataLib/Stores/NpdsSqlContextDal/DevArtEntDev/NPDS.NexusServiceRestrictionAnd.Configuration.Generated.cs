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
    /// There are no comments for NexusServiceRestrictionAndConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class NexusServiceRestrictionAndConfiguration : IEntityTypeConfiguration<NexusServiceRestrictionAnd>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<NexusServiceRestrictionAnd> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<NexusServiceRestrictionAnd> builder)
        {
            builder.ToTable(@"NexusServiceRestrictionAnd", @"dbo");
            builder.Property(x => x.RestrictionAndGuidKey).HasColumnName(@"RestrictionAndGuidKey").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ServiceName).HasColumnName(@"ServiceName").HasColumnType(@"nvarchar(256)").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            builder.Property(x => x.InfosetGuidRef).HasColumnName(@"InfosetGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.RecordGuidRef).HasColumnName(@"RecordGuidRef").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ServiceLabel).HasColumnName(@"ServiceLabel").HasColumnType(@"nvarchar(256)").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            builder.Property(x => x.RestrictionName).HasColumnName(@"RestrictionName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.Property(x => x.AndHasIndex).HasColumnName(@"AndHasIndex").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.AndHasPriority).HasColumnName(@"AndHasPriority").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.IsExcluding).HasColumnName(@"IsExcluding").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsSufficient).HasColumnName(@"IsSufficient").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.CreatedByAgentGuid).HasColumnName(@"CreatedByAgentGuid").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.UpdatedByAgentGuid).HasColumnName(@"UpdatedByAgentGuid").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.Property(x => x.DeletedOn).HasColumnName(@"DeletedOn").HasColumnType(@"datetime2").ValueGeneratedNever();
            builder.Property(x => x.DeletedByAgentGuid).HasColumnName(@"DeletedByAgentGuid").HasColumnType(@"uniqueidentifier").ValueGeneratedNever();
            builder.HasKey(@"RestrictionAndGuidKey");
            builder.HasMany(x => x.NexusServiceRestrictionOrs).WithOne(op => op.NexusServiceRestrictionAnd).HasForeignKey(@"RestrictionAndGuidRef").IsRequired(true);
            builder.HasOne(x => x.NexusResrepStem).WithMany(op => op.NexusServiceRestrictionAnds).HasForeignKey(@"RecordGuidRef").IsRequired(true);
            builder.HasOne(x => x.NexusResrepLeaf).WithMany(op => op.NexusServiceRestrictionAnds).HasForeignKey(@"RecordGuidRef").IsRequired(true);

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<NexusServiceRestrictionAnd> builder);

        #endregion
    }

}
