// SqldbcUilSrvcRstrctAndEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  // GetEditables //

  public ServiceRestrictionAndEditModel GetEditableRestrictionAndByKey(Guid guidKey)
  { return QueryScribeRestrictionAndByKey(guidKey).ToEditable().SingleOrDefault(); }
  public ServiceRestrictionAndEditModel GetEditableRestrictionAndByKey(string guidKey)
  { return GetEditableRestrictionAndByKey(Guid.Parse(guidKey)); }

  // GetStorables //

  public NexusServiceRestrictionAnd GetStorableRestrictionAndByKey(Guid guidKey)
  { return QueryScribeRestrictionAndByKey(guidKey).SingleOrDefault(); }
  public NexusServiceRestrictionAnd GetStorableRestrictionAndByKey(string guidKey)
  { return GetStorableRestrictionAndByKey(Guid.Parse(guidKey)); }

  // QueryStorables //

  public IQueryable<NexusServiceRestrictionAnd> QueryScribeRestrictionAndByKey(Guid guidKey)
  {
    IQueryable<NexusServiceRestrictionAnd> qry = this.NexusServiceRestrictionAnds;
    qry = qry.Where(r => (r.RestrictionAndGuidKey == guidKey));
    return qry;
  }
 
} // end class

// end file