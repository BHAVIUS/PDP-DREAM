// SqldbcUilOtherTextViewGet.cs 
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
  public OtherTextViewModel GetViewableOtherTextByKey(Guid guidKey)
  { return QueryStorableOtherTextByKey(guidKey).ToViewable().SingleOrDefault(); }
  public OtherTextViewModel GetViewableOtherTextByKey(string guidKey)
  { return GetViewableOtherTextByKey(Guid.Parse(guidKey)); }
  
  // GetStorables //
  public NexusOtherText GetStorableOtherTextByKey(Guid guidKey)
  { return QueryStorableOtherTextByKey(guidKey).SingleOrDefault(); }
  public NexusOtherText GetStorableOtherTextByKey(string guidKey)
  { return GetStorableOtherTextByKey(Guid.Parse(guidKey)); }

  // QueryStorables //
  public IQueryable<NexusOtherText> QueryStorableOtherTextByKey(Guid guidKey)
  {
    IQueryable<NexusOtherText> qry = this.NexusOtherTexts;
    qry = qry.Where(r => (r.FgroupGuidKey == guidKey));
    return qry;
  }

} // end class

// end file