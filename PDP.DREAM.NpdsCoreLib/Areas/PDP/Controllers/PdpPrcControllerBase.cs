// PdpPrcControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  // convention: name abstract controllers with suffix ControllerBase
  public abstract class PdpPrcControllerBase : Controller
  {
    protected const string QuestionChar = "?";
    protected const string AndChar = "&";
    protected const string EqualChar = "=";

    // CONTEXTS

    protected HttpContext? pdpHttpCntxt = null;
    protected HttpRequest? pdpHttpReqst = null;
    protected PdpRestContext? pdpRestCntxt = null;
    public PdpRestContext PRC
    {
      set { pdpRestCntxt = value; }
      get
      {
        if (pdpRestCntxt == null) 
        { throw new NullReferenceException("PdpRestContext PRC cannot be referenced when null."); }
        return pdpRestCntxt;
      }
    }

    public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
    {
      pdpHttpCntxt = oaeCntxt.HttpContext;
      pdpHttpReqst = pdpHttpCntxt.Request;
      pdpRestCntxt = new PdpRestContext(pdpHttpReqst); // calls ParseQueryCollection on new()
    }
    public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
    {
      base.OnActionExecuted(oaeCntxt);
      if (pdpRestCntxt == null) 
      { throw new NullReferenceException("pdpRestCntxt is null in PdpPrcControllerBase OnActionExecuted()."); }
      ViewBag.PRC = pdpRestCntxt;  // PDP REST Context
    }

    protected void PdpPrcMvcAddErrors(string error)
    {
      ModelState.AddModelError("", error);
    }
    protected void PdpPrcMvcAddErrors(string[] errors)
    {
      foreach (var error in errors)
      {
        ModelState.AddModelError("", error);
      }
    }

  }

}
