// PdpPrcControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class PdpPrcRestApiControllerBase : Controller
{
  // CONSTANTS
  
  protected const string ranNpds = CoreDLC.RouteAppNameNpdsView;
  protected const int raoNpds = CoreDLC.RouteAppOrderNpdsView;

  protected const string QuestionChar = "?";
  protected const string AndChar = "&";
  protected const string EqualChar = "=";

  // CONTEXTS

  protected PdpRestContext? pdpRestCntxt = null;
  public PdpRestContext PRC
  {
    set { pdpRestCntxt = value; }
    get {
      if (pdpRestCntxt == null)
      { throw new NullReferenceException("PdpRestContext PRC cannot be referenced when null."); }
      return pdpRestCntxt;
    }
  }

  public override void OnActionExecuting(ActionExecutingContext oaeCntxt)
  {
    pdpRestCntxt = new PdpRestContext(oaeCntxt.HttpContext.Request); // calls ParseQueryCollection on new()
  }
  public override void OnActionExecuted(ActionExecutedContext oaeCntxt)
  {
    base.OnActionExecuted(oaeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(PdpPrcRestApiControllerBase)} OnActionExecuted()."); }
    // PDP REST Context in PDP.DREAM.CoreDataLib.Models.PdpRestContext
    pdpRestCntxt.TkgrArea = PdpConst.PdpMvcArea;
    ViewData["PRC"] = pdpRestCntxt;
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

  protected string? HtmlCleanByEncodeDecode(string? dirty)
  {
    var clean = WebUtility.HtmlDecode(WebUtility.HtmlEncode(dirty));
    return clean;
  }
  protected string? UrlCleanByEncodeDecode(string? dirty)
  {
    var clean = WebUtility.UrlDecode(WebUtility.UrlEncode(dirty));
    return clean;
  }
  protected string? HtmlEscapeHashLiteral(string? withHash)
  {
    var withoutHash = withHash.Replace("#", "\\#");
    return withoutHash;
  }

} // class
