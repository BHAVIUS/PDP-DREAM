// PdpRouteDebuggerOptions.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Routing;

namespace PDP.DREAM.CoreDataLib.Utilities;

// IRoutingFeature requires property RouteData of type RouteData
public class PdpRouteDebuggerOptions // : IRoutingFeature
{
  public PdpRouteDebuggerOptions() { }

  public PdpRouteDebuggerOptions(IList<IRouter> theRouters)
  {
    if (theRouters == null) { throw new ArgumentNullException(nameof(theRouters)); }
    pdpRouters = theRouters;
  }

  public RouteData RouteInfo
  {
    set { pdpRouteInfo = value; }
    get { return pdpRouteInfo; }
  }
  private RouteData pdpRouteInfo;

  public IList<IRouter> PdpRouters
  {
    set { pdpRouters = value; }
    get { return pdpRouters; }
  }
  private IList<IRouter> pdpRouters;

}
