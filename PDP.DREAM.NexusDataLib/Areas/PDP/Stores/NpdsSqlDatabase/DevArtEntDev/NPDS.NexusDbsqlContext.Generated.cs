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
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{

    [GeneratedCode("Devart Entity Developer", "")]
    public partial class NexusDbsqlContext : PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase.CoreDbsqlContext
    {

        public NexusDbsqlContext() :
            base()
        {
            OnCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"Data Source=.\SQLD2019;Initial Catalog=PdpScribe10;Integrated Security=True;Persist Security Info=True");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<NexusCrossReference> NexusCrossReferences
        {
            get;
            set;
        }

        public virtual DbSet<NexusDescription> NexusDescriptions
        {
            get;
            set;
        }

        public virtual DbSet<NexusDistribution> NexusDistributions
        {
            get;
            set;
        }

        public virtual DbSet<NexusLocation> NexusLocations
        {
            get;
            set;
        }

        public virtual DbSet<NexusOtherText> NexusOtherTexts
        {
            get;
            set;
        }

        public virtual DbSet<NexusProvenance> NexusProvenances
        {
            get;
            set;
        }

        public virtual DbSet<NexusResrepStem> NexusResrepStems
        {
            get;
            set;
        }

        public virtual DbSet<NexusSupportingLabel> NexusSupportingLabels
        {
            get;
            set;
        }

        public virtual DbSet<NexusSupportingTag> NexusSupportingTags
        {
            get;
            set;
        }

        public virtual DbSet<NexusEntityAliasLabel> NexusEntityAliasLabels
        {
            get;
            set;
        }

        public virtual DbSet<NexusEntityCanonicalLabel> NexusEntityCanonicalLabels
        {
            get;
            set;
        }

        public virtual DbSet<NexusFairMetric> NexusFairMetrics
        {
            get;
            set;
        }

        public virtual DbSet<NexusEntityLabel> NexusEntityLabels
        {
            get;
            set;
        }

        public virtual DbSet<NexusResrepSnapshot> NexusResrepSnapshots
        {
            get;
            set;
        }

        public virtual DbSet<NexusResrepRoot> NexusResrepRoots
        {
            get;
            set;
        }

        public virtual DbSet<NexusResrepAuthorRequest> NexusResrepAuthorRequests
        {
            get;
            set;
        }

        public virtual DbSet<NexusServiceRestrictionAnd> NexusServiceRestrictionAnds
        {
            get;
            set;
        }

        public virtual DbSet<NexusServiceRestrictionOr> NexusServiceRestrictionOrs
        {
            get;
            set;
        }

        public virtual DbSet<NexusServiceEditorRequest> NexusServiceEditorRequests
        {
            get;
            set;
        }

        public virtual DbSet<NexusServiceNpdsDefault> NexusServiceNpdsDefaults
        {
            get;
            set;
        }

        public virtual DbSet<NexusServiceEditorAudit> NexusServiceEditorAudits
        {
            get;
            set;
        }

        public virtual DbSet<NexusResrepAuthorAudit> NexusResrepAuthorAudits
        {
            get;
            set;
        }

        public virtual DbSet<NexusResrepLeaf> NexusResrepLeafs
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<NexusCrossReference>(new NexusCrossReferenceConfiguration());
            modelBuilder.ApplyConfiguration<NexusDescription>(new NexusDescriptionConfiguration());
            modelBuilder.ApplyConfiguration<NexusDistribution>(new NexusDistributionConfiguration());
            modelBuilder.ApplyConfiguration<NexusLocation>(new NexusLocationConfiguration());
            modelBuilder.ApplyConfiguration<NexusOtherText>(new NexusOtherTextConfiguration());
            modelBuilder.ApplyConfiguration<NexusProvenance>(new NexusProvenanceConfiguration());
            modelBuilder.ApplyConfiguration<NexusResrepStem>(new NexusResrepStemConfiguration());
            modelBuilder.ApplyConfiguration<NexusSupportingLabel>(new NexusSupportingLabelConfiguration());
            modelBuilder.ApplyConfiguration<NexusSupportingTag>(new NexusSupportingTagConfiguration());
            modelBuilder.ApplyConfiguration<NexusEntityAliasLabel>(new NexusEntityAliasLabelConfiguration());
            modelBuilder.ApplyConfiguration<NexusEntityCanonicalLabel>(new NexusEntityCanonicalLabelConfiguration());
            modelBuilder.ApplyConfiguration<NexusFairMetric>(new NexusFairMetricConfiguration());
            modelBuilder.ApplyConfiguration<NexusEntityLabel>(new NexusEntityLabelConfiguration());
            modelBuilder.ApplyConfiguration<NexusResrepSnapshot>(new NexusResrepSnapshotConfiguration());
            modelBuilder.ApplyConfiguration<NexusResrepRoot>(new NexusResrepRootConfiguration());
            modelBuilder.ApplyConfiguration<NexusResrepAuthorRequest>(new NexusResrepAuthorRequestConfiguration());
            modelBuilder.ApplyConfiguration<NexusServiceRestrictionAnd>(new NexusServiceRestrictionAndConfiguration());
            modelBuilder.ApplyConfiguration<NexusServiceRestrictionOr>(new NexusServiceRestrictionOrConfiguration());
            modelBuilder.ApplyConfiguration<NexusServiceEditorRequest>(new NexusServiceEditorRequestConfiguration());
            modelBuilder.ApplyConfiguration<NexusServiceNpdsDefault>(new NexusServiceNpdsDefaultConfiguration());
            modelBuilder.ApplyConfiguration<NexusServiceEditorAudit>(new NexusServiceEditorAuditConfiguration());
            modelBuilder.ApplyConfiguration<NexusResrepAuthorAudit>(new NexusResrepAuthorAuditConfiguration());
            modelBuilder.ApplyConfiguration<NexusResrepLeaf>(new NexusResrepLeafConfiguration());
            CustomizeMapping(ref modelBuilder);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
