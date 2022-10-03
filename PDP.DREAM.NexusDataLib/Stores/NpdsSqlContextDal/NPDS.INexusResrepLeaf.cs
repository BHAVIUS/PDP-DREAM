// NPDS.INexusResrepLeaf.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.NexusDataLib.Stores;

public interface INexusResrepLeaf : INexusResrepStem
{
  int InfosetCrossReferencesCount { get; set; }
  string? InfosetCrossReferencesStatusName { get; set; }
  short InfosetCrossReferencesStatusCode { get; set; }

  int InfosetDescriptionsCount { get; set; }
  string? InfosetDescriptionsStatusName { get; set; }
  short InfosetDescriptionsStatusCode { get; set; }

  int InfosetDistributionsCount { get; set; }
  string? InfosetDistributionsStatusName { get; set; }
  short InfosetDistributionsStatusCode { get; set; }

  int InfosetEntityLabelsCount { get; set; }
  string? InfosetEntityLabelsStatusName { get; set; }
  short InfosetEntityLabelsStatusCode { get; set; }

  int InfosetFairMetricsCount { get; set; }
  string? InfosetFairMetricsStatusName { get; set; }
  short InfosetFairMetricsStatusCode { get; set; }

  int InfosetLocationsCount { get; set; }
  string? InfosetLocationsStatusName { get; set; }
  short InfosetLocationsStatusCode { get; set; }

  int InfosetNexusSnapshotsCount { get; set; }
  string? InfosetNexusSnapshotsStatusName { get; set; }
  short InfosetNexusSnapshotsStatusCode { get; set; }

  int InfosetOtherTextsCount { get; set; }
  string? InfosetOtherTextsStatusName { get; set; }
  short InfosetOtherTextsStatusCode { get; set; }

  int InfosetProvenancesCount { get; set; }
  string? InfosetProvenancesStatusName { get; set; }
  short InfosetProvenancesStatusCode { get; set; }

  int InfosetSupportingLabelsCount { get; set; }
  string? InfosetSupportingLabelsStatusName { get; set; }
  short InfosetSupportingLabelsStatusCode { get; set; }

  int InfosetSupportingTagsCount { get; set; }
  string? InfosetSupportingTagsStatusName { get; set; }
  short InfosetSupportingTagsStatusCode { get; set; }

  // infoset status code, name, when tested

  DateTime? InfosetResrepEntityTestedOn { get; set; }
  string? InfosetResrepEntityStatusName { get; set; }
  short InfosetResrepEntityStatusCode { get; set; }

  DateTime? InfosetResrepInfosetTestedOn { get; set; }
  string? InfosetResrepInfosetStatusName { get; set; }
  short InfosetResrepInfosetStatusCode { get; set; }

  DateTime? InfosetResrepRecordTestedOn { get; set; }
  string? InfosetResrepRecordStatusName { get; set; }
  short InfosetResrepRecordStatusCode { get; set; }

}

// end file