using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public partial class ScribeDbsqlContext
  {
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

  } // class

} // namespace
