// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Stores;

public partial class CoreDbsqlContext
{
  public IList<EntityTypeUxm> ListViewableEntityTypes()
  {
    // query from CoreEntityTypeItem
    IOrderedQueryable<CoreEntityTypeItem> dalItems = this.CoreEntityTypeItems
      // where clause
      // order clause
      .OrderBy(dali => dali.TypeName);
    // query to EntityTypeUxm
    IQueryable<EntityTypeUxm> uilItems =
      from dali in dalItems
      select new EntityTypeUxm
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

  public IEnumerable<EntityTypeUxm> ListEditableEntityTypes()
  {
    var qry = this.CoreEntityTypeItems
      // where clause
      .Where(itm => (itm.TypeEditedByAdmin == true))
      // order clause
      .OrderBy(itm => itm.CodeKey);
    IEnumerable<EntityTypeUxm> rows
      = from itm in qry
        select new EntityTypeUxm
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