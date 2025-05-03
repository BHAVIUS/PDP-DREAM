// INexusDataService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models;

public interface INexusDataService
{
  QebUserRestContext PRC { get; set; }

  // User Interface/Interaction Layer (with acronym "uil") for viewable ViewModel objects and editable EditModel objects

  void SetRestContext(ref QebUserRestContext qurc);

  bool CreatePdpAgent();

  bool CreatePdpAgentSession();

  bool IdentifyPdpAgent();

  bool IdentifyPdpAgentSession();

  //IList<SelectListItem> GetEntityTypeSelectListItems();
  //IList<SelectListItem> GetInfosetStatusSelectListItems();
  //IList<SelectListItem> GetSupportingLabelSelectListItems();
  //IList<SelectListItem> GetRegistrarSelectListItems();
  //IList<SelectListItem> GetRegistrySelectListItems();
  //IList<SelectListItem> GetDirectorySelectListItems();

  NexusResrepViewModel GetViewableRecordByKey(Guid guidKey, bool isInfosetKey = false);

  IEnumerable<NexusResrepViewModel> ListViewableRecords();

  IEnumerable<NexusResrepViewModel> ListViewableRecordsWithAgent(Guid guidKey);

  IEnumerable<NexusSnapshotViewModel> ListViewableArchivedRecords(Guid guidKey);

  IEnumerable<CrossReferenceViewModel> ListViewableCrossReferences(Guid guidKey);

  IEnumerable<EntityTypeViewModel> ListViewableEntityTypes();

  IEnumerable<LocationViewModel> ListViewableLocations(Guid guidKey);

  IEnumerable<SupportingLabelViewModel> ListViewableSupportingLabels(Guid guidKey);

  IEnumerable<SupportingTagViewModel> ListViewableSupportingTags(Guid guidKey);

  IEnumerable<EntityLabelViewModel> ListViewableEntityLabels(Guid guidKey);

  // Message Exchange Layer (with acronym "mel") for serializable data transfer objects

  NpdsResrepItem MelNrrItem { get; set; }
  NpdsResrepList MelNrrList { get; set; }

  NpdsResrepXmlRoot GetXmlmsgFormtdNexusResourceByKey(Guid guidKey, bool isInfosetKey = false);

  NpdsResrepXmlRoot GetXmlmsgFormtdNexusResourcesWithPredicateFilters();
}
