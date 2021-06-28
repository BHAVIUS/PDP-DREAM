using System;
using System.Collections.Generic;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface INexusDataService
  {
    PdpRestContext PRC { get; set; }

    // User Interface/Interaction Layer (with acronym "uil") for viewable ViewModel objects and editable EditModel objects

    void SetRestContext(ref PdpRestContext prc);

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

    NpdsResrepItem MelNrrItem { get; set; } // TODO ?
    NpdsResrepList MelNrrList { get; set; } // TODO ?

    NpdsResrepXmlRoot GetXmlmsgFormtdNexusResourceByKey(Guid guidKey, bool isInfosetKey = false);

    NpdsResrepXmlRoot GetXmlmsgFormtdNexusResourcesWithPredicateFilters();
  }
}