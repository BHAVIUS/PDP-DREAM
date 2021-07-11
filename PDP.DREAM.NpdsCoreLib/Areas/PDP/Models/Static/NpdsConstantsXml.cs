// NpdsConstantsXml.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public static partial class NpdsConst
  {
    // this class contains nothing but constants and enums only
    // if enum value not set in code, it defaults to 0

    #region NPDS XML Schema namespace string constants

    // TODO: consider converting to coded convention with list suffix + "Set" always used
    //       then can eliminate different Xnams for both Item and List
    //       but will lose flexibility of changing names for Lists such as done with
    //       lists of PORTAL/DOORS/Nexus resreps
    // TODO: consider recoding to import these from an actual XML Schema that serves as the master file
    //    need better interoperability between a UML model or other model that generates the schema
    // TODO: centralizing Xnames here also facilitates globalization with language resources if/when globalized

    // const for NPDS namespace for use in XML Schema and XML instances
    public const string NpdsNamespace = "http://npds.portaldoors.org/nsvo/npdsystem#";
    // in the copyright string a "&169;" character does not display in output unless in XHTML
    public const string NpdsCopyright = "Nexus-PORTAL-DOORS-Scribe Cyberinfrastructure System (c) 2006-2021 Carl Taswell and the PORTAL-DOORS Project Team";
    public const string NpdsVersion = "2.2.2 (2021-03-19)";
    public const string NpdsVersionXnam = "Version";
    // const for root/branch node element names
    public const string NpdsRootXnam = "NPDS";
    // public const string NpdsMessageXnam = "Message";
    // public const string NpdsClientRequestXnam = "ClientRequest";
    // public const string NpdsServerResponseXnam = "ServerResponse";

    public const string NexusServerXnam = "NexusServer";
    public const string NexusResrepListXnam = "NexusService"; // Nexus Service can have NexusResReps, PortalResReps, DoorsResReps, CoreResReps
    public const string NexusResrepItemXnam = "NexusResRep";
    public const string PortalServerXnam = "PortalServer";
    public const string PortalResrepListXnam = "PortalService"; // PORTAL Service can have PortalResReps, CoreResReps
    public const string PortalResrepItemXnam = "PortalResRep";
    public const string DoorsServerXnam = "DoorsServer";
    public const string DoorsResrepListXnam = "DoorsService";  // DOORS Service can have DoorsResReps, CoreResReps
    public const string DoorsResrepItemXnam = "DoorsResRep";
    public const string CoreServerXnam = "CoreServer";
    public const string CoreResrepListXnam = "CoreService"; // Core Service can have CoreResReps
    public const string CoreResrepItemXnam = "CoreResRep";

    public const string ResrepListXnam = "ResourceRepresentations";
    public const string ResrepItemXnam = "ResourceRepresentation";
    public const string ResrepKeyXnam = "RRRecordGuid";

    public const string EntityListXnam = "EntityMetadataSet";
    public const string EntityItemXnam = "EntityMetadata";
    public const string EntityKeyXnam = "EntityGuid";
    public const string RecordListXnam = "RecordMetadataSet";
    public const string RecordItemXnam = "RecordMetadata";
    public const string RecordKeyXnam = "RecordHandle";
    public const string InfosetListXnam = "InfosetMetadataSet";
    public const string InfosetItemXnam = "InfosetMetadata";
    public const string InfosetKeyXnam = "InfosetGuid";

    public const string DisplayTextXnam = "DisplayText";
    public const string DisplayImageUrlXnam = "DisplayImageUrl";

    public const string ItemIsAgentSharedXnam = "IsAgentShared";
    public const string ItemIsAuthorPrivateXnam = "IsAuthorPrivate";
    public const string ItemIsUpdaterLimitedXnam = "IsUpdaterLimited";
    public const string ItemIsManagerReleasedXnam = "IsManagerReleased";

    public const string ItemIsCachedXnam = "IsCached";
    public const string ItemIsCanonicalXnam = "IsCanonical";
    public const string ItemIsConciseXnam = "IsConcise";
    public const string ItemIsGeneratingXnam = "IsGenerating";
    public const string ItemIsMarkedXnam = "IsMarked";
    public const string ItemIsPrincipalXnam = "IsPrincipal";
    public const string ItemIsPrivateXnam = "IsPrivate";
    public const string ItemIsResolvableXnam = "IsResolvable";
    public const string ItemStatusXnam = "Status";
    public const string ItemTestedOnXnam = "TestedOn";

    public const string ItemForeignKeyXnam = "RRRecordGuid";
    public const string ItemPrimaryKeyXnam = "RRFgroupGuid";
    public const string ItemIndexXnam = "Index";
    public const string ItemPriorityXnam = "Priority";
    public const string ListCountXnam = "Count";

    // TODO: ListCountDefaults ? a part of NPDS or convert to PDP site defaults???
    // TODO: and what about paging within a list???
    public const int ListCountDeflt = 10;
    public const int ListCountMaxDeflt = 100;
    public const int ListCountSuperMaxDeflt = 1000;

    public const string RequestXnam = "ClientRequest";
    public const string RequestUrlXnam = "URL";
    public const string RequestNoteXnam = "Note";
    public const string RequestQuestionXnam = "Question";

    public const string ResponseXnam = "ServerResponse";
    public const string ResponseNoteXnam = "Note";
    public const string ResponseStatusXnam = "Status";
    public const string ResponseAnswerXnam = "Answer";
    public const string ResponseRelatedXnam = "Related";
    public const string ResponseReferredXnam = "Referred";

    // EntityMetadata items

    public const string EntityLabelItemXnam = "EntityLabel";
    public const string EntityLabelListXnam = "EntityLabels";
    public const string TagTokenItemXnam = "TagToken";
    public const string TagTokenListXnam = "TagTokens";
    public const string LabelUriItemXnam = "LabelUri";
    public const string LabelUriListXnam = "LabelUris";


    public const string EntityTypeNameXnam = "EntityType";

    public const string CanonicalLabelItemXnam = "CanonicalLabel";
    public const string CanonicalLabelListXnam = "CanonicalLabels";
    public const string AliasLabelItemXnam = "AliasLabel";
    public const string AliasLabelListXnam = "AliasLabels";

    public const string ContactItemXnam = "Contact";
    public const string ContactListXnam = "Contacts";
    public const string CrossReferenceItemXnam = "CrossReference";
    public const string CrossReferenceListXnam = "CrossReferences";

    public const string DescriptionItemXnam = "Description";
    public const string DescriptionListXnam = "Descriptions";
    public const string DistributionItemXnam = "Distribution";
    public const string DistributionListXnam = "Distributions";

    public const string FairMetricItemXnam = "FairMetric";
    public const string FairMetricListXnam = "FairMetrics";

    public const string LocationItemXnam = "Location";
    public const string LocationListXnam = "Locations";
    public const string LocationUrlXnam = "LocationUrl";
    public const string LocationXmlXnam = "LocationXml";
    public const string LocationXhtmlXnam = "LocationXhtml";
    public const string LocationRdfOwlXnam = "LocationRdfOwl";

    public const string NameItemXnam = "Name";
    public const string NameListXnam = "Names";
    public const string NatureItemXnam = "Nature";
    public const string NatureListXnam = "Natures";

    public const string OtherEntityItemXnam = "OtherEntity";
    public const string OtherEntityListXnam = "OtherEntities";
    public const string OtherTextItemXnam = "OtherText"; // originally OtherMetadataItem
    public const string OtherTextListXnam = "OtherTexts"; // originally OtherMetadataList
    public const string OwnerItemXnam = "Owner";
    public const string OwnerListXnam = "Owners";

    public const string PrincipalTagItemXnam = "PrincipalTag";
    public const string PrincipalTagListXnam = "PrincipalTags";

    public const string ServiceDefaultItemXnam = "ServiceDefault";
    public const string ServiceDefaultListXnam = "ServiceDefaults";
    public const string ServiceRestrictionAndItemXnam = "ServiceRestrictionAnd";
    public const string ServiceRestrictionAndListXnam = "ServiceRestrictionAnds";
    public const string ServiceRestrictionOrItemXnam = "ServiceRestrictionOr";
    public const string ServiceRestrictionOrListXnam = "ServiceRestrictionOrs";

    public const string SnapshotItemXnam = "Snapshot";
    public const string SnapshotListXnam = "Snapshots";
    public const string SupportingTagItemXnam = "SupportingTag";
    public const string SupportingTagListXnam = "SupportingTags";
    public const string SupportingLabelItemXnam = "SupportingLabel";
    public const string SupportingLabelListXnam = "SupportingLabels";

    // RecordMetadata items
    public const string CreatedByItemXnam = "CreatedBy";
    public const string CreatedByListXnam = "CreatedBySet";
    public const string CreatedOnItemXnam = "CreatedOn";
    public const string CreatedOnListXnam = "CreatedOnSet";
    public const string DirectoryItemXnam = "Directory";
    public const string DirectoryListXnam = "DirectorySet";
    public const string ManagedByItemXnam = "ManagedBy";
    public const string ManagedByListXnam = "ManagedBySet";
    public const string ProvenanceItemXnam = "Provenance";
    public const string ProvenanceListXnam = "Provenances";
    public const string RegistrantItemXnam = "Registrant";
    public const string RegistrantListXnam = "Registrants";
    public const string RegistrarItemXnam = "Registrar";
    public const string RegistrarListXnam = "Registrars";
    public const string DiristryItemXnam = "Diristry";
    public const string DiristryListXnam = "Diristries";
    public const string RegistryItemXnam = "Registry";
    public const string RegistryListXnam = "Registries";
    public const string SecondaryDirectoryItemXnam = "SecondaryDirectory";
    public const string SecondaryDirectoryListXnam = "SecondaryDirectories";
    public const string SecondaryRegistryItemXnam = "SecondaryRegistry";
    public const string SecondaryRegistryListXnam = "SecondaryRegistries";
    public const string SignatureItemXnam = "Signature";
    public const string SignatureListXnam = "Signatures";
    public const string UpdatedByItemXnam = "UpdatedBy";
    public const string UpdatedByListXnam = "UpdatedBySet";
    public const string UpdatedOnItemXnam = "UpdatedOn";
    public const string UpdatedOnListXnam = "UpdatedOnSet";
    public const string RecordCachedOnXnam = "CachedOn";
    public const string RecordCreatedOnXnam = "CreatedOn";
    public const string RecordUpdatedOnXname = "UpdatedOn";

    // InfosetMetadata items
    public const string DoorsValidationItemXnam = "DoorsValidation";
    public const string DoorsValidationListXnam = "DoorsValidations";
    public const string NexusEntailmentItemXnam = "NexusEntailment";
    public const string NexusEntailmentListXnam = "NexusEntailments";
    public const string PortalValidationItemXnam = "PortalValidation";
    public const string PortalValidationListXnam = "PortalValidations";

    #endregion

  }

}