// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{

  public IList<SelectListItem> GetCccDiristryList(bool isNpdsAdmin = false)
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems;
    if (isNpdsAdmin)
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
          (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode) ||
          (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode)
       )).OrderBy(dali => dali.ServicePTag);
    }
    else
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
          (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode)
       )).OrderBy(dali => dali.ServicePTag);
    }
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.DiristryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  // ATTN: compare GetCccRegistrarDiristriesTGList
  public IList<SelectListItem> GetCccRegistrarDiristriesList(bool isNpdsAdmin = false)
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems;
    if (isNpdsAdmin)
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
         ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
          (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode)) ||
          (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode)
       )).OrderBy(dali => dali.ServicePTag);
    }
    else
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
         ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
          (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode))
       )).OrderBy(dali => dali.ServicePTag);
    }
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.DiristryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetCccRegistryList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode) ||
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode) ||
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRegistry.ECode)
      )).OrderBy(dali => dali.ServicePTag);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.RegistryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  // include diristries because a diristry is also a registry
  public IList<SelectListItem> GetCccRegistrarRegistriesList(bool isNpdsAdmin = false)
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems;
    if (isNpdsAdmin)
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
       ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRegistry.ECode)) ||
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode)
       )).OrderBy(dali => dali.ServicePTag);
    }
    else
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
       ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRegistry.ECode))
       )).OrderBy(dali => dali.ServicePTag);
    }
    // query to RecordDiristryListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.RegistryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetCccDirectoryList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode) ||
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode) ||
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDirectory.ECode)
      )).OrderBy(dali => dali.ServicePTag);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.DirectoryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  // include diristries because a diristry is also a directory
  public IList<SelectListItem> GetCccRegistrarDirectoriesList(bool isNpdsAdmin = false)
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems;
    if (isNpdsAdmin)
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
       ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDirectory.ECode)) ||
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode)
       )).OrderBy(dali => dali.ServicePTag);
    }
    else
    {
      dalItems = this.CoreServiceDefaults.AsEnumerable()
       .Where(dali => (
       ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDirectory.ECode))
       )).OrderBy(dali => dali.ServicePTag);
    }
    // query to RecordDiristryListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.DirectoryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetCccRegistrarList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRoot.ECode) ||
      (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsRegistrar.ECode)
      )).OrderBy(dali => dali.ServicePTag);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.RegistrarGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IList<SelectListItem> GetCccNpdsServiceList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems = this.CoreServiceDefaults.AsEnumerable()
      .Where(dali => (0 < dali.ServiceTCode && dali.ServiceTCode < 40))
      .OrderBy(dali => dali.ServicePTag);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in this.CoreServiceDefaults
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = dali.ServiceIGuid.ToString(),
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

} // end class

// end file