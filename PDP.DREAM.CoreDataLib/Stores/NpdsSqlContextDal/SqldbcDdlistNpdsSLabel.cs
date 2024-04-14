// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IList<SelectListItem> GetCccRegistrySupportingLabelList()
  {
    var termItem = (short)NPDSCD.EntityTypeTerminologyItem.ECode;
    var dalItems = this.CoreResrepRoots
      .Where((CoreResrepRoot r) =>
        (r.RecordRegistryGuid == NPDSDC.RegistryGuid && r.EntityTypeCode == termItem))
      .OrderBy((CoreResrepRoot r) => r.EntityName);
    IEnumerable<SelectListItem> uilItems
      = from item in dalItems
        select new SelectListItem
        {
          Text = item.EntityName,
          Value = item.EntityCanonicalLabel
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

  // ATTN: compare GetCccRegistrarDiristriesList
  public IList<SelectListItem> GetCccRegistrarDiristriesTGList()
  {
    // query from CoreServiceDefault
    IEnumerable<CoreServiceDefault> dalItems;
    dalItems = this.CoreServiceDefaults.AsEnumerable()
     .Where(dali => (
       ((dali.DefRegistrarGuid == NPDSCD.RegistrarGuidDefault) &&
        (dali.ServiceTCode == (short)NPDSCD.EntityTypeNpdsDiristry.ECode))
     )).OrderBy(dali => dali.ServicePTag);
    // query to SelectListItem
    IEnumerable<SelectListItem> uilItems
      = from dali in dalItems
        let itemSelected = (dali.ServiceIGuid == NPDSCD.DiristryGuidDefault)
        select new SelectListItem
        {
          Text = dali.ServicePTag,
          Value = $"{dali.ServicePTag}={dali.ServiceIGuid}",
          Selected = itemSelected
        };
    var uilList = uilItems.ToList();
    return uilList;
  }

} // end class

// end file