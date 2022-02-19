// TkgrControllerUxmSelectList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  protected void BuildDropDownListsForResrepRoot()
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

} // class
