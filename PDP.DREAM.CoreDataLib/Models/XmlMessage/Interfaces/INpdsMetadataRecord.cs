// INpdsMetadataRecord.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

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

