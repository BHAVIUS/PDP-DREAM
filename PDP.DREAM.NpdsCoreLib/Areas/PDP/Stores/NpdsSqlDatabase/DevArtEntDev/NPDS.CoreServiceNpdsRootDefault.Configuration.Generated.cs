﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2021-06-07 20:38:59
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
    /// There are no comments for CoreServiceNpdsRootDefaultConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class CoreServiceNpdsRootDefaultConfiguration : IEntityTypeConfiguration<CoreServiceNpdsRootDefault>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<CoreServiceNpdsRootDefault> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<CoreServiceNpdsRootDefault> builder)
        {
            builder.ToTable(@"CoreServiceNpdsRootDefault", @"dbo");
            builder.Property(x => x.ServiceIGuid).HasColumnName(@"ServiceIGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ServiceName).HasColumnName(@"ServiceName").HasColumnType(@"nvarchar(128)").ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.ServiceNature).HasColumnName(@"ServiceNature").HasColumnType(@"nvarchar(256)").ValueGeneratedNever().HasMaxLength(256);
            builder.Property(x => x.ServiceRGuid).HasColumnName(@"ServiceRGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ServiceTCode).HasColumnName(@"ServiceTCode").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ServiceTName).HasColumnName(@"ServiceTName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.ServicePTag).HasColumnName(@"ServicePTag").HasColumnType(@"nvarchar(128)").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            builder.Property(x => x.DefDiristryGuid).HasColumnName(@"DefDiristryGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.DefRegistryGuid).HasColumnName(@"DefRegistryGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.DefDirectoryGuid).HasColumnName(@"DefDirectoryGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.DefRegistrarGuid).HasColumnName(@"DefRegistrarGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.HasKey(@"ServiceIGuid");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<CoreServiceNpdsRootDefault> builder);

        #endregion
    }

}