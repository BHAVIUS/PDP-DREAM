// SqldbcUilDropdownlist.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Stores
{
  public partial class CoreDbsqlContext
  {
    public IEnumerable<EntityTypeListItem> GetItemsForEntityTypeSelectList()
    {
      IEnumerable<CoreEntityTypeItem> dalItems = this.CoreEntityTypeItems.Where(
        itm => ((itm.TypeEditedByAuthor == true && PRC.ClientHasAuthorAccess == true && itm.TypeIsComponent == false)
        || (itm.TypeEditedByEditor == true && PRC.ClientHasEditorAccess == true && itm.TypeIsComponent == false)
        || (itm.TypeEditedByAdmin == true && PRC.ClientHasAdminAccess == true)));
      IEnumerable<EntityTypeListItem> uilItems
        = from item in dalItems
          orderby item.TypeName
          select new EntityTypeListItem
          {
            EntityTypeName = item.TypeName,
            EntityTypeCode = item.CodeKey,
            Selected = (item.CodeKey == (int)PRC.EntityTypeDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public IEnumerable<InfosetPortalStatusListItem> GetItemsForInfosetPortalStatusSelectList()
    {
      IEnumerable<CoreInfosetStatusItem> dalItems = this.CoreInfosetStatusItems.Where(
       itm => ((itm.StatusEditedByAuthor == true && PRC.ClientHasAuthorAccess == true)
       || (itm.StatusEditedByEditor == true && PRC.ClientHasEditorAccess == true)
       || (itm.StatusEditedByAdmin == true && PRC.ClientHasAdminAccess == true)));
      IEnumerable<InfosetPortalStatusListItem> uilItems
        = from item in dalItems
          orderby item.StatusName
          select new InfosetPortalStatusListItem
          {
            InfosetPortalStatusName = item.StatusName,
            InfosetPortalStatusCode = item.CodeKey,
            Selected = (item.CodeKey == NpdsConst.DefaultInfosetStatusCode)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public IEnumerable<InfosetDoorsStatusListItem> GetItemsForInfosetDoorsStatusSelectList()
    {
      IEnumerable<CoreInfosetStatusItem> dalItems = this.CoreInfosetStatusItems.Where(
       itm => ((itm.StatusEditedByAuthor == true && PRC.ClientHasAuthorAccess == true)
       || (itm.StatusEditedByEditor == true && PRC.ClientHasEditorAccess == true)
       || (itm.StatusEditedByAdmin == true && PRC.ClientHasAdminAccess == true)));
      IEnumerable<InfosetDoorsStatusListItem> uilItems
        = from item in dalItems
          orderby item.StatusName
          select new InfosetDoorsStatusListItem
          {
            InfosetDoorsStatusName = item.StatusName,
            InfosetDoorsStatusCode = item.CodeKey,
            Selected = (item.CodeKey == NpdsConst.DefaultInfosetStatusCode)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    // TODO: simplify scheme to eliminate the other component types
    // should be limited to Diristry, Registry, Directory, Registrar
    // primary, secondary and authoritative, peering, caching should be tracked in other properties

    public IList<SelectListItem> GetItemsForRegistrarDiristriesSelectListMvc()
    {
      IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.Where(
       itm => ((itm.DefRegistrarGuid == PRC.RegistrarGuid) 
     && (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsDiristry)));
      IEnumerable<SelectListItem> uilItems
        = from itm in dalItems
          orderby itm.ServiceName
          select new SelectListItem
          {
            Text = itm.ServicePTag,
            Value = itm.ServiceIGuid.ToString(),
            Selected = (itm.ServiceIGuid == PRC.DiristryGuidDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public IList<RecordDiristryListItem> GetItemsForRegistrarDiristriesSelectList()
    {
      IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.Where(
       itm => ((itm.DefRegistrarGuid == PRC.RegistrarGuid) && (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsDiristry)));
      IEnumerable<RecordDiristryListItem> uilItems
        = from itm in dalItems
          orderby itm.ServiceName
          select new RecordDiristryListItem
          {
            RecordDiristryName = itm.ServicePTag,
            RecordDiristryGuid = itm.ServiceIGuid,
            Selected = (itm.ServiceIGuid == PRC.DiristryGuidDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }


    public IList<RecordDiristryListItem> GetItemsForNpdsDiristrySelectList()
    {
      IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.Where(
        itm => ((itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsRoot) ||
        (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsDiristry)));
      IEnumerable<RecordDiristryListItem> uilItems
        = from itm in dalItems
          orderby itm.ServicePTag
          select new RecordDiristryListItem
          {
            RecordDiristryName = itm.ServicePTag,
            RecordDiristryGuid = itm.ServiceIGuid,
            Selected = (itm.ServiceIGuid == PRC.DiristryGuidDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public IList<RecordRegistryListItem> GetItemsForNpdsRegistrySelectList()
    {
      IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.Where(
        itm => ((itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsRoot) ||
        (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsRegistry) ||
        (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsDiristry)));
      IEnumerable<RecordRegistryListItem> uilItems
        = from itm in dalItems
          orderby itm.ServicePTag
          select new RecordRegistryListItem
          {
            RecordRegistryName = itm.ServicePTag,
            RecordRegistryGuid = itm.ServiceIGuid,
            Selected = (itm.ServiceIGuid == PRC.RegistryGuidDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public IList<RecordDirectoryListItem> GetItemsForNpdsDirectorySelectList()
    {
      IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.Where(
        itm => ((itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsRoot) ||
        (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsDirectory) ||
        (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsDiristry)));
      IEnumerable<RecordDirectoryListItem> uilItems
        = from itm in dalItems
          orderby itm.ServicePTag
          select new RecordDirectoryListItem
          {
            RecordDirectoryName = itm.ServicePTag,
            RecordDirectoryGuid = itm.ServiceIGuid,
            Selected = (itm.ServiceIGuid == PRC.DirectoryGuidDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public IList<RecordRegistrarListItem> GetItemsForNpdsRegistrarSelectList()
    {
      IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.Where(
        itm => ((itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsRoot) ||
        (itm.ServiceTCode == (Int16)NpdsConst.EntityType.NpdsRegistrar)));
      IEnumerable<RecordRegistrarListItem> uilItems
        = from itm in dalItems
          orderby itm.ServicePTag
          select new RecordRegistrarListItem
          {
            RecordRegistrarName = itm.ServicePTag,
            RecordRegistrarGuid = itm.ServiceIGuid,
            Selected = (itm.ServiceIGuid == PRC.RegistrarGuidDeflt)
          };
      var uilList = uilItems.ToList();
      return uilList;
    }


    public IList<RecordServiceListItem> GetItemsForNpdsRootServiceSelectList()
    {
      IEnumerable<RecordServiceListItem> uilItems
        = from item in this.CoreServiceDefaults
          orderby item.ServicePTag
          select new RecordServiceListItem
          {
            ServicePTag = item.ServicePTag,
            ServiceIGuid = item.ServiceIGuid
          };
      var uilList = uilItems.ToList();
      return uilList;
    }

    public void LoadNpdsServiceCache()
    {
      // cache of NPDS Services PrincipalTags and InfosetGuids (not ResourceGuids)
      var npdsCache = NpdsServiceDefaults.Values.NpdsServiceCache;
      var npdsList = GetItemsForNpdsRootServiceSelectList();
      foreach (RecordServiceListItem item in npdsList)
      {
        npdsCache.Add(item.ServicePTag, item.ServiceIGuid);
      }

    }

  }

}
