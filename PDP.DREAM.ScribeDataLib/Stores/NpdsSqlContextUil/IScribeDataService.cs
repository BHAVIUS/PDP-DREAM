// IScribeDataService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public interface IScribeDataService
{
  QebUserRestContext PRC { get; set; }

  // User Interface/Interaction Layer (with acronym "uil") for viewable ViewModel objects and editable EditModel objects

  void SetRestContext(ref QebUserRestContext qurc);
  bool CreatePdpAgent();
  bool CreatePdpAgentSession();
  bool IdentifyPdpAgent();
  bool IdentifyPdpAgentSession();

  NexusResrepEditModel ArchiveRecord(NexusResrepEditModel editObj);
  void CreateLocationXhtml(LocationEditModel editObj);

  ResrepAuthorRequestEditModel DeleteAgentResourceRequest(ResrepAuthorRequestEditModel editObj);
  NexusSnapshotEditModel DeleteArchivedRecord(NexusSnapshotEditModel editObj);
  CrossReferenceEditModel DeleteCrossReference(CrossReferenceEditModel editObj);
  LocationEditModel DeleteLocation(LocationEditModel editObj);
  NexusResrepEditModel DeleteRecord(NexusResrepEditModel editObj);
  SupportingLabelEditModel DeleteSupportingLabel(SupportingLabelEditModel editObj);
  SupportingTagEditModel DeleteSupportingTag(SupportingTagEditModel editObj);
  EntityLabelEditModel DeleteTagAndLabel(EntityLabelEditModel editObj);


  ResrepAuthorRequestEditModel EditAgentResourceRequest(ResrepAuthorRequestEditModel editObj);
  NexusSnapshotEditModel EditArchivedRecord(NexusSnapshotEditModel editObj);
  CrossReferenceEditModel EditCrossReference(CrossReferenceEditModel editObj);
  LocationEditModel EditLocation(LocationEditModel editObj);
  NexusResrepEditModel EditRecord(NexusResrepEditModel editObj);
  SupportingLabelEditModel EditSupportingLabel(SupportingLabelEditModel editObj);
  SupportingTagEditModel EditSupportingTag(SupportingTagEditModel editObj);
  EntityLabelEditModel EditTagAndLabel(EntityLabelEditModel editObj);

  IList<SelectListItem> GetEntityTypeSelectListItems();
  IList<SelectListItem> GetInfosetStatusSelectListItems();
  IList<SelectListItem> GetSupportingLabelSelectListItems();
  //IList<SelectListItem> GetRegistrarSelectListItems();
  //IList<SelectListItem> GetRegistrySelectListItems();
  //IList<SelectListItem> GetDirectorySelectListItems();

  IEnumerable<ResrepAuthorRequestEditModel> ListEditableAgentRecordRequests();
  IEnumerable<NexusSnapshotEditModel> ListEditableArchivedRecords(Guid guidKey);
  IEnumerable<CrossReferenceEditModel> ListEditableCrossReferences(Guid guidKey);
  IEnumerable<LocationEditModel> ListEditableLocations(Guid guidKey);
  IEnumerable<NexusResrepEditModel> ListEditableRecords();
  IEnumerable<NexusResrepEditModel> ListEditableResourcesWithAgent(Guid guidKey);
  IEnumerable<SupportingLabelEditModel> ListEditableSupportingLabels(Guid guidKey);
  IEnumerable<SupportingTagEditModel> ListEditableSupportingTags(Guid guidKey);
  IEnumerable<EntityLabelEditModel> ListEditableTagAndLabels(Guid guidKey);

  NexusResrepEditModel GetEditableResourceByIKey(Guid guidKey);
  NexusSnapshotEditModel GetEditableArchivedResourceByKey(Guid guidKey);
  CrossReferenceEditModel GetEditableCrossReferenceByKey(Guid guidKey);
  LocationEditModel GetEditableLocationByKey(Guid guidKey);
  SupportingLabelEditModel GetEditableSupportingLabelByKey(Guid guidKey);
  SupportingTagEditModel GetEditableSupportingTagByKey(Guid guidKey);
  EntityLabelEditModel GetEditableTagAndLabelByKey(Guid guidKey);
  ResrepAuthorRequestEditModel GetEditableAgentResourceRequestByKey(Guid guidKey);

  NexusResrepViewModel GetViewableRecordByKey(Guid guidKey, bool isInfosetKey = false);
  IEnumerable<NexusResrepViewModel> ListViewableRecords();
  IEnumerable<NexusResrepViewModel> ListViewableRecordsWithAgent(Guid guidKey);
  IEnumerable<NexusSnapshotViewModel> ListViewableArchivedRecords(Guid guidKey);
  IEnumerable<CrossReferenceViewModel> ListViewableCrossReferences(Guid guidKey);
  IEnumerable<EntityTypeViewModel> ListViewableEntityTypes();
  IEnumerable<LocationViewModel> ListViewableLocations(Guid guidKey);
  IEnumerable<SupportingLabelViewModel> ListViewableSupportingLabels(Guid guidKey);
  IEnumerable<SupportingTagViewModel> ListViewableSupportingTags(Guid guidKey);
  IEnumerable<EntityLabelViewModel> ListViewableTagAndLabels(Guid guidKey);

  NexusResrepViewModel RequestReleaseRecord(NexusResrepViewModel viewObj);
  NexusResrepEditModel ValidateRecord(NexusResrepEditModel editObj);
  LocationEditModel ValidateLocation(LocationEditModel editObj);

  // Message Exchange Layer (with acronym "mel") for serializable data transfer objects

  NpdsResrepItem MelNrrItem { get; set; } // TODO ?
  NpdsResrepList MelNrrList { get; set; } // TODO ?
  NpdsResrepXmlRoot GetXmlmsgFormtdNexusResourceByKey(Guid guidKey, bool isInfosetKey = false);
  NpdsResrepXmlRoot GetXmlmsgFormtdNexusResourcesWithPredicateFilters();

}

