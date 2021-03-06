// SqldbcUilEntityTypes.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores
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
