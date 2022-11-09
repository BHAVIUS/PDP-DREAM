// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.CoreDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IList<EntityTypeListItem> GetEntityTypeSelectList()
  {
    // query from CoreEntityTypeItem
    IEnumerable<CoreEntityTypeItem> dalItems = this.CoreEntityTypeItems.AsEnumerable()
      .Where(dali => (
      (dali.TypeEditedByAuthor == true && QURC.ClientHasAuthorAccess == true && dali.TypeIsComponent == false) ||
      (dali.TypeEditedByEditor == true && QURC.ClientHasEditorAccess == true && dali.TypeIsComponent == false) ||
      (dali.TypeEditedByAdmin == true && QURC.ClientHasAdminAccess == true)
      )).OrderBy(dali => dali.TypeName);
    // query to EntityTypeListItem
    IEnumerable<EntityTypeListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.CodeKey == DefaultEntityTypeCode)
        select new EntityTypeListItem
        {
          EntityTypeName = dali.TypeName,
          EntityTypeCode = dali.CodeKey,
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<FieldFormatListItem> GetFieldFormatSelectList()
  {
    // TODO: fix the name discrepancy, ie, FieldFormat, FeatureFormat or InfssetFormat of InfosubsetFormat ???

    // query from CoreEntityTypeItem
    IEnumerable<CoreFieldFormatItem> dalItems = this.CoreFieldFormatItems.AsEnumerable()
      .Where(dali => ((0 < dali.CodeKey) && (dali.CodeKey != 100) && (dali.CodeKey < 255)))
      .OrderBy(dali => dali.FormatName);
    // query to FieldFormatListItem
    IEnumerable<FieldFormatListItem> uilItems =
      from dali in dalItems
      let itemSelected = (dali.CodeKey == DefaultFieldFormatCode)
      select new FieldFormatListItem
      {
        FieldFormatName = dali.FormatName,
        FieldFormatCode = dali.CodeKey,
        Selected = itemSelected
      };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<InfosetPortalStatusListItem> GetInfosetPortalStatusSelectList()
  {
    // query from CoreInfosetStatusItem
    IEnumerable<CoreInfosetStatusItem> dalItems = this.CoreInfosetStatusItems.AsEnumerable()
      .Where(dali => (
      (dali.StatusEditedByAuthor == true && QURC.ClientHasAuthorAccess == true) ||
      (dali.StatusEditedByEditor == true && QURC.ClientHasEditorAccess == true) ||
      (dali.StatusEditedByAdmin == true && QURC.ClientHasAdminAccess == true)
      )).OrderBy(dali => dali.StatusName);
    // query to InfosetPortalStatusListItem
    IEnumerable<InfosetPortalStatusListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.CodeKey == DefaultInfosetStatusCode)
        select new InfosetPortalStatusListItem
        {
          InfosetPortalStatusName = dali.StatusName,
          InfosetPortalStatusCode = dali.CodeKey,
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<InfosetDoorsStatusListItem> GetInfosetDoorsStatusSelectList()
  {
    // query from CoreInfosetStatusItem
    IEnumerable<CoreInfosetStatusItem> dalItems = this.CoreInfosetStatusItems.AsEnumerable()
      .Where(dali => (
      (dali.StatusEditedByAuthor == true && QURC.ClientHasAuthorAccess == true) ||
      (dali.StatusEditedByEditor == true && QURC.ClientHasEditorAccess == true) ||
      (dali.StatusEditedByAdmin == true && QURC.ClientHasAdminAccess == true)
      )).OrderBy(dali => dali.StatusName);
    // query to InfosetDoorsStatusListItem
    IEnumerable<InfosetDoorsStatusListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.CodeKey == DefaultInfosetStatusCode)
        select new InfosetDoorsStatusListItem
        {
          InfosetDoorsStatusName = dali.StatusName,
          InfosetDoorsStatusCode = dali.CodeKey,
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  // TODO: simplify scheme to eliminate the other component types
  // should be limited to Diristry, Registry, Directory, Registrar
  // primary, secondary and authoritative, peering, caching should be tracked in other properties

  public IList<SelectListItem> GetRegistrarDiristriesSelectListMvc()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.DefRegistrarGuid == QURC.RegistrarGuid) &&
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDiristry)
      )).OrderBy(dali => dali.ServiceName);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == QURC.DiristryGuidDeflt)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<RecordDiristryListItem> GetRegistrarDiristriesSelectList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.DefRegistrarGuid == QURC.RegistrarGuid) &&
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDiristry)
      )).OrderBy(dali => dali.ServiceName);
    // query to RecordDiristryListItem
    IEnumerable<RecordDiristryListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == QURC.DiristryGuidDeflt)
        select new RecordDiristryListItem
        {
          RecordDiristryName = dali.ServicePTag,
          RecordDiristryGuid = dali.ServiceIGuid,
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetCoreDiristrySelectListMvc()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRoot) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDiristry)
      )).OrderBy(dali => dali.ServiceName);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == QURC.DiristryGuidDeflt)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<RecordDiristryListItem> GetCoreDiristrySelectList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRoot) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDiristry)
      )).OrderBy(dali => dali.ServicePTag);
    // query to RecordDiristryListItem
    IEnumerable<RecordDiristryListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == QURC.DiristryGuidDeflt)
        select new RecordDiristryListItem
        {
          RecordDiristryName = dali.ServicePTag,
          RecordDiristryGuid = dali.ServiceIGuid,
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<RecordRegistryListItem> GetCoreRegistrySelectList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRoot) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRegistry) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDiristry)
      )).OrderBy(dali => dali.ServicePTag);
    // query to RecordRegistryListItem
    IEnumerable<RecordRegistryListItem> uilItems
      = from dali in dalItems
        select new RecordRegistryListItem
        {
          RecordRegistryName = dali.ServicePTag,
          RecordRegistryGuid = dali.ServiceIGuid,
          Selected = (dali.ServiceIGuid == QURC.RegistryGuidDeflt)
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<RecordDirectoryListItem> GetCoreDirectorySelectList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRoot) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDirectory) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsDiristry)
      )).OrderBy(dali => dali.ServicePTag);
    // query to RecordDirectoryListItem
    IEnumerable<RecordDirectoryListItem> uilItems
      = from dali in dalItems
        orderby dali.ServicePTag
        select new RecordDirectoryListItem
        {
          RecordDirectoryName = dali.ServicePTag,
          RecordDirectoryGuid = dali.ServiceIGuid,
          Selected = (dali.ServiceIGuid == QURC.DirectoryGuidDeflt)
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<RecordRegistrarListItem> GetCoreRegistrarSelectList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRoot) ||
      (dali.ServiceTCode == (short)NpdsEntityType.NpdsRegistrar)
      )).OrderBy(dali => dali.ServicePTag);
    // query to RecordRegistrarListItem
    IEnumerable<RecordRegistrarListItem> uilItems
      = from dali in dalItems
        select new RecordRegistrarListItem
        {
          RecordRegistrarName = dali.ServicePTag,
          RecordRegistrarGuid = dali.ServiceIGuid,
          Selected = (dali.ServiceIGuid == QURC.RegistrarGuidDeflt)
        };
    var uilList = uilItems.ToList();
    return uilList;
  }


  public IList<RecordServiceListItem> GetCoreServiceSelectList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (0 < dali.ServiceTCode && dali.ServiceTCode < 40))
      .OrderBy(dali => dali.ServicePTag);
    // query to RecordServiceListItem
    IEnumerable<RecordServiceListItem> uilItems
      = from dali in this.CoreServiceDefaults
        select new RecordServiceListItem
        {
          ServicePTag = dali.ServicePTag,
          ServiceIGuid = dali.ServiceIGuid,
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public void LoadNpdsServiceCache()
  {
    // cache of NPDS Services PrincipalTags and InfosetGuids (not ResourceGuids)
    var npdsCache = NPDSSD.NpdsServiceCache;
    var npdsList = GetCoreServiceSelectList();
    foreach (RecordServiceListItem uili in npdsList)
    {
      npdsCache.Add(uili.ServicePTag, uili.ServiceIGuid);
    }

  }

} // end class

// end file