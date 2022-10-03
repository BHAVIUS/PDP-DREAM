// SqldbcUilFairMetricEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using PDP.DREAM.NexusDataLib.Stores;
using PDP.DREAM.ScribeDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public FairMetricEditModel GetEditableFairMetricByKey(Guid guidKey)
  { return QueryStorableFairMetricByKey(guidKey).ToEditable().SingleOrDefault(); }
  public FairMetricEditModel GetEditableFairMetricByKey(string guidKey)
  { return GetEditableFairMetricByKey(Guid.Parse(guidKey)); }

} // end class

// end file