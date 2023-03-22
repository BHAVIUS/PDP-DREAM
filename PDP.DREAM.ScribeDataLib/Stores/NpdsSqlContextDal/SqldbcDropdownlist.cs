// SqldbcUilDropdownlist.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public IList<SelectListItem> GetItemsForSupportingLabelSelectList()
  {
    var termItem = (short)PdpAppConst.NpdsEntityType.TerminologyItem;
    var dalItems = this.NexusResrepStems
      .Where((NexusResrepStem r) => 
	  (r.RecordRegistryGuidRef == NPDSCP.RegistryGuid && r.EntityTypeCodeRef == termItem))
      .OrderBy((NexusResrepStem r) => r.EntityName);
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

} // end class

// end file