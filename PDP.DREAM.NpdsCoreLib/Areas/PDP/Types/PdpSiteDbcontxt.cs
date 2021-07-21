// PdpDbcntxtArray.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace PDP.DREAM.NpdsCoreLib.Types
{
  public class PdpSiteDbContext : DbContext
  {
    public PdpSiteDbContext() { }
    public PdpSiteDbContext(int dbidx, string dbnam, string dbcstr)
    {
      DbIndex = dbidx; DbName = dbnam; DbConstr = dbcstr;
    }
    public int DbIndex { get; set; }
    public string DbName { get; set; }
    public string DbConstr { get; set; } // database connection string
  }
  public class PdpSiteDbContext<TDbcntxt> : PdpSiteDbContext where TDbcntxt : DbContext
  {
    public PdpSiteDbContext(int dbidx, string dbnam, string dbcstr)
    {
      DbContxt = (new PdpSiteDbContext(dbidx, dbnam, dbcstr)) as TDbcntxt;
    }
    public TDbcntxt DbContxt { get; set; } // database context
    public Type DbType { get { return DbContxt.GetType(); } }
  }

} // namespace
