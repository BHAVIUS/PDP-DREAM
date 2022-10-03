// INexusResrepStem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

namespace PDP.DREAM.NexusDataLib.Stores;

// TODO: simplify to just Guid/Name instead of GuidRef or GuidKey and Name

public interface INexusResrepStem : INexusResrepRoot
{
  string? EntityCanonicalLabel { get; set; }
  Guid? EntityContactGuidRef { get; set; }
  string? EntityContactLabel { get; set; }
  string? EntityContactName { get; set; }
  string? EntityContactTag { get; set; }
  Guid? EntityOtherGuidRef { get; set; }
  string? EntityOtherLabel { get; set; }
  string? EntityOtherName { get; set; }
  string? EntityOtherTag { get; set; }
  Guid? EntityOwnerGuidRef { get; set; }
  string? EntityOwnerLabel { get; set; }
  string? EntityOwnerName { get; set; }
  string? EntityOwnerTag { get; set; }
  string? EntityPrincipalTag { get; set; }
  string? InfosetDoorsStatusName { get; set; }
  DateTime? InfosetDoorsStatusTestedOn { get; set; }
  string? InfosetEntailment { get; set; }
  string? InfosetPortalStatusName { get; set; }
  DateTime? InfosetPortalStatusTestedOn { get; set; }
  IList<NexusNexusSnapshot>? NexusNexusSnapshots { get; set; }
  IList<NexusServiceCoreDefault>? NexusServiceCoreDefaults { get; set; }
  IList<NexusServiceRestrictionAnd>? NexusServiceRestrictionAnds { get; set; }
  DateTime? RecordArchivedOn { get; set; }
  DateTime? RecordCachedOn { get; set; }
  string? RecordCreatedByUserName { get; set; }
  string? RecordDeletedByUserName { get; set; }
  string? RecordDirectoryLabel { get; set; }
  string? RecordDirectoryName { get; set; }
  string? RecordDiristryLabel { get; set; }
  string? RecordDiristryName { get; set; }
  string? RecordManagedByUserName { get; set; }
  Guid? RecordRegistrantGuidRef { get; set; }
  string? RecordRegistrantLabel { get; set; }
  string? RecordRegistrantName { get; set; }
  string? RecordRegistrantTag { get; set; }
  string? RecordRegistrarLabel { get; set; }
  string? RecordRegistrarName { get; set; }
  string? RecordRegistryLabel { get; set; }
  string? RecordRegistryName { get; set; }
  string? RecordSignature { get; set; }
  string? RecordUpdatedByUserName { get; set; }

  // infosubset format codes, names go in each of the infosubsets
  // not here for the RR root, stem or leaf
  // short InfosetFormatCodeRef { get; set; } // InfssetFormatCodeRef
  // string? InfosetFormatName { get; set; } // InfssetFormatName
  // short InfosetDescriptionFormatCode { get; set; }
  // string? InfosetDescriptionFormatName { get; set; }

}
