// SqldbcUilEntityTypes.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Stores
{
  public partial class CoreDbsqlContext
  {
    public IEnumerable<EntityTypeViewModel> ListViewableEntityTypes()
    {
      var qry = this.CoreEntityTypeItems
        .OrderBy(itm => itm.CodeKey);
      IEnumerable<EntityTypeViewModel> rows
        = from itm in qry orderby itm.TypeName
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

  }

}
