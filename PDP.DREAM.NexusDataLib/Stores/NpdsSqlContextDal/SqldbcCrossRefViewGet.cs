// SqldbcUilCrossRefViewGet.cs 
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
  public CrossReferenceViewModel GetViewableCrossReferenceByKey(Guid guidKey)
  { return QueryStorableCrossReferenceByKey(guidKey).ToViewable().SingleOrDefault(); }
  public CrossReferenceViewModel GetViewableCrossReferenceByKey(string guidKey)
  { return GetViewableCrossReferenceByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusCrossReference GetStorableCrossReferenceByKey(Guid guidKey)
  { return QueryStorableCrossReferenceByKey(guidKey).SingleOrDefault(); }
  public NexusCrossReference GetStorableCrossReferenceByKey(string guidKey)
  { return GetStorableCrossReferenceByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusCrossReference> QueryStorableCrossReferenceByKey(Guid guidKey)
  {
    IQueryable<NexusCrossReference> qry = this.NexusCrossReferences;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file