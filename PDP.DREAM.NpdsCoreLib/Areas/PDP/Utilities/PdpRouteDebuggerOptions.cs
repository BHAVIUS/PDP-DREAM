// PdpRouteDebuggerOptions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Routing;

#nullable disable

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
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

}
