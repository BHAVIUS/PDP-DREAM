using System.Net;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Utilities;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    protected void BuildDropDownListsForResrepStem()
    {
      ResetScribeRepository(); // use PSDC
      var rrddl = new UilDropDownLists()
      {
        EntityTypeList = PSDC.GetItemsForEntityTypeSelectList(),
      };
      ViewData["EntityTypeList"] = rrddl.EntityTypeList;

      if (PRC.ClientHasAdminAccess)
      {
        rrddl.InfosetPortalStatusList = PSDC.GetItemsForInfosetPortalStatusSelectList();
        ViewData["InfosetPortalStatusList"] = rrddl.InfosetPortalStatusList;
        rrddl.InfosetDoorsStatusList = PSDC.GetItemsForInfosetDoorsStatusSelectList();
        ViewData["InfosetDoorsStatusList"] = rrddl.InfosetDoorsStatusList;

        rrddl.RegistryList = PSDC.GetItemsForNpdsRegistrySelectList();
        ViewData["RegistryList"] = rrddl.RegistryList;
        rrddl.DirectoryList = PSDC.GetItemsForNpdsDirectorySelectList();
        ViewData["DirectoryList"] = rrddl.DirectoryList;
        rrddl.RegistrarList = PSDC.GetItemsForNpdsRegistrarSelectList();
        ViewData["RegistrarList"] = rrddl.RegistrarList;
      }

      if (PRC.ClientHasAuthorEditorOrAdminAccess)
      {
        rrddl.DiristryListMvc = PSDC.GetItemsForRegistrarDiristriesSelectListMvc();
        ViewData["DiristryListMvc"] = rrddl.DiristryListMvc;
        rrddl.DiristryList = PSDC.GetItemsForNpdsDiristrySelectList();
        ViewData["DiristryList"] = rrddl.DiristryList;
        // TODO: rebuild with default list from the defined problem domain for the specialty diristry
        rrddl.SupportingLabelList = PSDC.GetItemsForSupportingLabelSelectList();
        ViewData["SupportingLabelList"] = rrddl.SupportingLabelList;
      }

    }

  }

}
