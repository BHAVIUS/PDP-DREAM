// PdpMvcRouteDescriptor.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpRazorViewRouteDescriptor
{
  public static string ActionName(this ActionExecutingContext oaeCntxt)
  {
    var actnDesc = (ControllerActionDescriptor)oaeCntxt.ActionDescriptor;
    string actnName = actnDesc.ActionName;
    return actnName;
  }

  public static string ControllerName(this ActionExecutingContext oaeCntxt)
  {
    var actnDesc = (ControllerActionDescriptor)oaeCntxt.ActionDescriptor;
    string cntrlName = actnDesc.ControllerName;
    return cntrlName;
  }

} // end class

// end file
