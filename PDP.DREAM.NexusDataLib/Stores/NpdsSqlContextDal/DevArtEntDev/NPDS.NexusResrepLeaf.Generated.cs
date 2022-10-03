﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 8/12/2022 2:20:03 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable annotations
#nullable disable warnings

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace PDP.DREAM.NexusDataLib.Stores
{
    [GeneratedCode("Devart Entity Developer", "")]
    [Serializable()]
    public partial class NexusResrepLeaf {

        public virtual Guid RecordGuidKey { get; set; }

        public virtual short EntityTypeCodeRef { get; set; }

        public virtual string EntityTypeName { get; set; }

        public virtual bool EntityTypeIsComponent { get; set; }

        public virtual bool EntityTypeIsConstituent { get; set; }

        public virtual bool EntityTypeEditedByAgent { get; set; }

        public virtual bool EntityTypeEditedByAuthor { get; set; }

        public virtual bool EntityTypeEditedByEditor { get; set; }

        public virtual bool EntityTypeEditedByAdmin { get; set; }

        public virtual string EntityInitialTag { get; set; }

        public virtual string EntityName { get; set; }

        public virtual string EntityNature { get; set; }

        public virtual string EntityPrincipalTag { get; set; }

        public virtual Guid? EntityOtherGuidRef { get; set; }

        public virtual string? EntityOtherName { get; set; }

        public virtual string? EntityOtherTag { get; set; }

        public virtual string? EntityOtherLabel { get; set; }

        public virtual Guid? EntityOwnerGuidRef { get; set; }

        public virtual string? EntityOwnerName { get; set; }

        public virtual string? EntityOwnerTag { get; set; }

        public virtual string? EntityOwnerLabel { get; set; }

        public virtual Guid? EntityContactGuidRef { get; set; }

        public virtual string? EntityContactName { get; set; }

        public virtual string? EntityContactTag { get; set; }

        public virtual string? EntityContactLabel { get; set; }

        public virtual Guid? RecordRegistrantGuidRef { get; set; }

        public virtual string? RecordRegistrantName { get; set; }

        public virtual string? RecordRegistrantTag { get; set; }

        public virtual string? RecordRegistrantLabel { get; set; }

        public virtual string EntityCanonicalLabel { get; set; }

        public virtual string RecordHandle { get; set; }

        public virtual string? RecordSignature { get; set; }

        public virtual bool RecordIsCached { get; set; }

        public virtual DateTime? RecordCachedOn { get; set; }

        public virtual string RecordDiristryName { get; set; }

        public virtual bool RecordIsDeleted { get; set; }

        public virtual string RecordDiristryLabel { get; set; }

        public virtual DateTime? RecordArchivedOn { get; set; }

        public virtual string RecordRegistryName { get; set; }

        public virtual Guid RecordDiristryGuidRef { get; set; }

        public virtual string RecordRegistryLabel { get; set; }

        public virtual string RecordDiristryTag { get; set; }

        public virtual string RecordDirectoryName { get; set; }

        public virtual Guid RecordRegistryGuidRef { get; set; }

        public virtual string RecordDirectoryLabel { get; set; }

        public virtual string RecordRegistryTag { get; set; }

        public virtual string RecordRegistrarName { get; set; }

        public virtual Guid RecordDirectoryGuidRef { get; set; }

        public virtual string RecordRegistrarLabel { get; set; }

        public virtual string RecordDirectoryTag { get; set; }

        public virtual Guid RecordRegistrarGuidRef { get; set; }

        public virtual string RecordRegistrarTag { get; set; }

        public virtual Guid? RecordCreatedByAgentGuidRef { get; set; }

        public virtual DateTime? RecordCreatedOn { get; set; }

        public virtual string? RecordCreatedByUserName { get; set; }

        public virtual Guid? RecordUpdatedByAgentGuidRef { get; set; }

        public virtual DateTime? RecordUpdatedOn { get; set; }

        public virtual string? RecordUpdatedByUserName { get; set; }

        public virtual Guid? RecordDeletedByAgentGuidRef { get; set; }

        public virtual DateTime? RecordDeletedOn { get; set; }

        public virtual string? RecordDeletedByUserName { get; set; }

        public virtual Guid? RecordManagedByAgentGuidRef { get; set; }

        public virtual string? RecordManagedByUserName { get; set; }

        public virtual Guid InfosetGuidKey { get; set; }

        public virtual string? InfosetEntailment { get; set; }

        public virtual bool InfosetIsAuthorPrivate { get; set; }

        public virtual bool InfosetIsAgentShared { get; set; }

        public virtual bool InfosetIsUpdaterLimited { get; set; }

        public virtual bool InfosetIsManagerReleased { get; set; }

        public virtual DateTime? InfosetPortalStatusTestedOn { get; set; }

        public virtual short InfosetPortalStatusCode { get; set; }

        public virtual string? InfosetPortalStatusName { get; set; }

        public virtual DateTime? InfosetDoorsStatusTestedOn { get; set; }

        public virtual short InfosetDoorsStatusCode { get; set; }

        public virtual string? InfosetDoorsStatusName { get; set; }

        public virtual DateTime? InfosetResrepEntityTestedOn { get; set; }

        public virtual short InfosetResrepEntityStatusCode { get; set; }

        public virtual string? InfosetResrepEntityStatusName { get; set; }

        public virtual DateTime? InfosetResrepRecordTestedOn { get; set; }

        public virtual short InfosetResrepRecordStatusCode { get; set; }

        public virtual string? InfosetResrepRecordStatusName { get; set; }

        public virtual DateTime? InfosetResrepInfosetTestedOn { get; set; }

        public virtual short InfosetResrepInfosetStatusCode { get; set; }

        public virtual string? InfosetResrepInfosetStatusName { get; set; }

        public virtual int InfosetEntityLabelsCount { get; set; }

        public virtual short InfosetEntityLabelsStatusCode { get; set; }

        public virtual string? InfosetEntityLabelsStatusName { get; set; }

        public virtual int InfosetSupportingTagsCount { get; set; }

        public virtual short InfosetSupportingTagsStatusCode { get; set; }

        public virtual string? InfosetSupportingTagsStatusName { get; set; }

        public virtual int InfosetSupportingLabelsCount { get; set; }

        public virtual short InfosetSupportingLabelsStatusCode { get; set; }

        public virtual string? InfosetSupportingLabelsStatusName { get; set; }

        public virtual int InfosetCrossReferencesCount { get; set; }

        public virtual short InfosetCrossReferencesStatusCode { get; set; }

        public virtual string? InfosetCrossReferencesStatusName { get; set; }

        public virtual int InfosetOtherTextsCount { get; set; }

        public virtual short InfosetOtherTextsStatusCode { get; set; }

        public virtual string? InfosetOtherTextsStatusName { get; set; }

        public virtual int InfosetLocationsCount { get; set; }

        public virtual short InfosetLocationsStatusCode { get; set; }

        public virtual string? InfosetLocationsStatusName { get; set; }

        public virtual int InfosetDescriptionsCount { get; set; }

        public virtual short InfosetDescriptionsStatusCode { get; set; }

        public virtual string? InfosetDescriptionsStatusName { get; set; }

        public virtual int InfosetProvenancesCount { get; set; }

        public virtual short InfosetProvenancesStatusCode { get; set; }

        public virtual string? InfosetProvenancesStatusName { get; set; }

        public virtual int InfosetDistributionsCount { get; set; }

        public virtual short InfosetDistributionsStatusCode { get; set; }

        public virtual string? InfosetDistributionsStatusName { get; set; }

        public virtual int InfosetFairMetricsCount { get; set; }

        public virtual short InfosetFairMetricsStatusCode { get; set; }

        public virtual string? InfosetFairMetricsStatusName { get; set; }

        public virtual int InfosetNexusSnapshotsCount { get; set; }

        public virtual short InfosetNexusSnapshotsStatusCode { get; set; }

        public virtual string? InfosetNexusSnapshotsStatusName { get; set; }

        public virtual IList<NexusEntityLabel> NexusEntityLabels { get; set; }

        public virtual IList<NexusSupportingTag> NexusSupportingTags { get; set; }

        public virtual IList<NexusSupportingLabel> NexusSupportingLabels { get; set; }

        public virtual IList<NexusCrossReference> NexusCrossReferences { get; set; }

        public virtual IList<NexusOtherText> NexusOtherTexts { get; set; }

        public virtual IList<NexusLocation> NexusLocations { get; set; }

        public virtual IList<NexusDescription> NexusDescriptions { get; set; }

        public virtual IList<NexusDistribution> NexusDistributions { get; set; }

        public virtual IList<NexusProvenance> NexusProvenances { get; set; }

        public virtual IList<NexusFairMetric> NexusFairMetrics { get; set; }

        public virtual IList<NexusEntityAliasLabel> NexusEntityAliasLabels { get; set; }

        public virtual IList<NexusEntityCanonicalLabel> NexusEntityCanonicalLabels { get; set; }

        public virtual IList<NexusServiceRestrictionAnd> NexusServiceRestrictionAnds { get; set; }

        public virtual IList<NexusNexusSnapshot> NexusNexusSnapshots { get; set; }

        public virtual IList<NexusServiceCoreDefault> NexusServiceCoreDefaults { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}