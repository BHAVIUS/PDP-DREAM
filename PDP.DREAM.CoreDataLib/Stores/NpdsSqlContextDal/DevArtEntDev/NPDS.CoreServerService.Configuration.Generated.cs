﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 11/26/2022 6:00:30 PM
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
    /// There are no comments for CoreServerServiceConfiguration in the schema.
    /// </summary>
    [GeneratedCode("Devart Entity Developer", "")]
    public partial class CoreServerServiceConfiguration : IEntityTypeConfiguration<CoreServerService>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<CoreServerService> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<CoreServerService> builder)
        {
            builder.ToTable(@"CoreServerService", @"dbo");
            builder.Property(x => x.ServiceIGuid).HasColumnName(@"ServiceIGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ServiceName).HasColumnName(@"ServiceName").HasColumnType(@"nvarchar(256)").ValueGeneratedNever().HasMaxLength(256);
            builder.Property(x => x.ServiceNature).HasColumnName(@"ServiceNature").HasColumnType(@"nvarchar(1024)").ValueGeneratedNever().HasMaxLength(1024);
            builder.Property(x => x.ServiceRGuid).HasColumnName(@"ServiceRGuid").HasColumnType(@"uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ServiceTCode).HasColumnName(@"ServiceTCode").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever().HasPrecision(5, 0);
            builder.Property(x => x.ServiceTName).HasColumnName(@"ServiceTName").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            builder.Property(x => x.ServicePTag).HasColumnName(@"ServicePTag").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            builder.HasKey(@"ServiceIGuid");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<CoreServerService> builder);

        #endregion
    }

}
