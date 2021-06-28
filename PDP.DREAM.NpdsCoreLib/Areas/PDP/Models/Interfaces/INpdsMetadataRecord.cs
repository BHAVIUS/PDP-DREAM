// Interfaces for PDS data records; code uses acronyms PDSDR or Npdsdr in interface names;
// original design for PDS data records in 2008 IEEE TITB vol 12 no 2 pp 194-196;
// see Sections VII A and VII B of paper
//
// code also uses abbreviation ResRep for Resource Representation

using System;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public interface INpdsMetadataRecordCore
  {
    // required elements
    NpdsRecordRegistrarItem RecordRegistrar { get; set; }
    // permitted elements
    NpdsRecordRegistrantItem RecordRegistrant { get; set; }
    NpdsRecordCreatedByItem RecordCreatedBy { get; set; }
    NpdsRecordCreatedOnItem RecordCreatedOn { get; set; }
    NpdsRecordUpdatedByItem RecordUpdatedBy { get; set; }
    NpdsRecordUpdatedOnItem RecordUpdatedOn { get; set; }
    NpdsRecordManagedByItem RecordManagedBy { get; set; }
    NpdsRecordSignatureItem? RecordSignatureItem { get; set; }
    NpdsRecordSignatureList? RecordSignatureList { get; set; }

    // permitted attributes
    Guid? RecordGuid { get; set; }
    string? RecordHandle { get; set; }
  }

  public interface INpdsMetadataRecordPortal : INpdsMetadataRecordCore
  {
    // required elements
    NpdsRecordDirectoryItem RecordDirectory { get; set; }

    // permitted elements

    NpdsRecordCrossReferenceItem? RecordCrossReferenceItem { get; set; }
    NpdsRecordCrossReferenceList? RecordCrossReferenceList { set; get; }
    NpdsRecordOtherTextItem? RecordOtherTextItem { get; set; }
    NpdsRecordOtherTextList? RecordOtherTextList { set; get; }
  }

  public interface INpdsMetadataRecordDoors : INpdsMetadataRecordCore
  {
    // required elements
    NpdsRecordRegistryItem RecordRegistry { get; set; }

    // permitted elements
    NpdsRecordDistributionItem? RecordDistributionItem { get; set; }
    NpdsRecordDistributionList? RecordDistributionList { get; set; }
    NpdsRecordProvenanceItem? RecordProvenanceItem { get; set; }
    NpdsRecordProvenanceList? RecordProvenanceList { get; set; }
  }

  public interface INpdsMetadataRecordNexus : INpdsMetadataRecordPortal, INpdsMetadataRecordDoors
  {
  }

}
