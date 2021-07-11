// SqldbcUilEntityTypes.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase
{
  public partial class CoreDbsqlContext
  {
    public IEnumerable<EntityTypeViewModel> ListViewableEntityTypes()
    {
      var qry = this.CoreEntityTypeItems
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

  }

}
