using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public partial class ScribeDbsqlContext
  {
    public IList<SelectListItem> GetItemsForSupportingLabelSelectList()
    {
      var termItem = (short)NpdsConst.EntityType.TerminologyItem;
      var dalItems = this.NexusResrepStems
        .Where((NexusResrepStem r) => (r.RecordRegistryGuidRef == PRC.RegistryGuid && r.EntityTypeCodeRef == termItem))
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

  } // class

} // namespace
