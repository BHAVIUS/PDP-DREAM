// SqldbcUilDescriptionViewGet.cs 
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
  public DescriptionViewModel GetViewableDescriptionByKey(Guid guidKey)
  { return QueryStorableDescriptionByKey(guidKey).ToViewable().SingleOrDefault(); }
  public DescriptionViewModel GetViewableDescriptionByKey(string guidKey)
  { return GetViewableDescriptionByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusDescription GetStorableDescriptionByKey(Guid guidKey)
  { return QueryStorableDescriptionByKey(guidKey).SingleOrDefault(); }
  public NexusDescription GetStorableDescriptionByKey(string guidKey)
  { return GetStorableDescriptionByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusDescription> QueryStorableDescriptionByKey(Guid guidKey)
  {
    IQueryable<NexusDescription> qry = this.NexusDescriptions;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file