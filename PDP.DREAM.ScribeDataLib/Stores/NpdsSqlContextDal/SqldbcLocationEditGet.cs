﻿// SqldbcUilLocationEditGet.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Stores;

public partial class ScribeDbsqlContext
{
  public LocationEditModel GetEditableLocationByKey(Guid guidKey)
  { return QueryStorableLocationByKey(guidKey).ToEditable().SingleOrDefault(); }
  public LocationEditModel GetEditableLocationByKey(string guidKey)
  { return GetEditableLocationByKey(Guid.Parse(guidKey)); }

} // end class

// end file