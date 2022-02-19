// PdpMvcRouteDescriptor.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PDP.DREAM.CoreDataLib.Types;

public static class PdpMvcRouteDescriptor
{
  public static string ActionName(this ActionExecutingContext oaeCntxt)
  {
    var actDesc = (ControllerActionDescriptor)oaeCntxt.ActionDescriptor;
    // var actRouteValues = actDesc.RouteValues;
    // string actName = actRouteValues["action"]; // area, action, controller, page
    string actName = actDesc.ActionName;
    return actName;
  }

} // end class

// end file
