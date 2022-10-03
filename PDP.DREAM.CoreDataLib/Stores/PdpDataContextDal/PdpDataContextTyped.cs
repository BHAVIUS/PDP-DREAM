// PdpDataContextTyped.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.EntityFrameworkCore;

namespace PDP.DREAM.CoreDataLib.Stores;

//public class PdpDataContext<TDbcntxt> : PdpDataContext where TDbcntxt : DbContext
//{
//  public PdpDataContext(int dbidx, string dbnam, string dbcstr)
//  {
//    DbContxt = (new PdpDataContext(dbidx, dbnam, dbcstr)) as TDbcntxt;
//  }
//  public TDbcntxt DbContxt { get; set; } // database context
//  public Type DbType { get { return DbContxt.GetType(); } }
//}
