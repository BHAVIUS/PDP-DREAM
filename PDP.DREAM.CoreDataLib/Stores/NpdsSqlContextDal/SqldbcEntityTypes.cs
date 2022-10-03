// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IList<EntityTypeViewModel> ListViewableEntityTypes()
  {
    // query from CoreEntityTypeItem
    IOrderedQueryable<CoreEntityTypeItem> dalItems = this.CoreEntityTypeItems
         // where clause
         .OrderBy(dali => dali.TypeName);
    // query to EntityTypeViewModel
    IQueryable<EntityTypeViewModel> uilItems =
      from dali in dalItems
      select new EntityTypeViewModel
      {
        CodeKey = dali.CodeKey,
        TypeName = dali.TypeName,
        TypeDescription = dali.TypeDescription,
        TypeEditedByAgent = dali.TypeEditedByAgent,
        TypeEditedByAuthor = dali.TypeEditedByAuthor,
        TypeEditedByEditor = dali.TypeEditedByEditor,
        TypeEditedByAdmin = dali.TypeEditedByAdmin
      };
    var uilList = uilItems.ToList();
    return uilList;
  }

  public IEnumerable<EntityTypeViewModel> ListEditableEntityTypes()
  {
    var qry = this.CoreEntityTypeItems
      .Where(itm => (itm.TypeEditedByAdmin == true))
      .OrderBy(itm => itm.CodeKey);
    IEnumerable<EntityTypeViewModel> rows
      = from itm in qry
        select new EntityTypeViewModel
        {
          CodeKey = itm.CodeKey,
          TypeName = itm.TypeName,
          TypeDescription = itm.TypeDescription,
          TypeEditedByAgent = itm.TypeEditedByAgent,
          TypeEditedByAuthor = itm.TypeEditedByAuthor,
          TypeEditedByEditor = itm.TypeEditedByEditor,
          TypeEditedByAdmin = itm.TypeEditedByAdmin
        };
    return rows;
  }

} // end class

// end file