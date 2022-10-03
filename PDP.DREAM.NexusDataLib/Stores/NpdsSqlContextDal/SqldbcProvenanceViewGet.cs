// SqldbcUilProvenanceViewGet.cs 
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
  public ProvenanceViewModel GetViewableProvenanceByKey(Guid guidKey)
  { return QueryStorableProvenanceByKey(guidKey).ToViewable().SingleOrDefault(); }
  public ProvenanceViewModel GetViewableProvenanceByKey(string guidKey)
  { return GetViewableProvenanceByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusProvenance GetStorableProvenanceByKey(Guid guidKey)
  { return QueryStorableProvenanceByKey(guidKey).SingleOrDefault(); }
  public NexusProvenance GetStorableProvenanceByKey(string guidKey)
  { return GetStorableProvenanceByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusProvenance> QueryStorableProvenanceByKey(Guid guidKey)
  {
    IQueryable<NexusProvenance> qry = this.NexusProvenances;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file