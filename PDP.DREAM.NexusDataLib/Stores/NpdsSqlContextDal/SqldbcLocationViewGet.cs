// SqldbcUilLocationViewGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  // GetViewables //
  public LocationViewModel GetViewableLocationByKey(Guid guidKey)
  { return QueryStorableLocationByKey(guidKey).ToViewable().SingleOrDefault(); }
  public LocationViewModel GetViewableLocationByKey(string guidKey)
  { return GetViewableLocationByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusLocation GetStorableLocationByKey(Guid guidKey)
  { return QueryStorableLocationByKey(guidKey).SingleOrDefault(); }
  public NexusLocation GetStorableLocationByKey(string guidKey)
  { return GetStorableLocationByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusLocation> QueryStorableLocationByKey(Guid guidKey)
  {
    IQueryable<NexusLocation> qry = this.NexusLocations;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file