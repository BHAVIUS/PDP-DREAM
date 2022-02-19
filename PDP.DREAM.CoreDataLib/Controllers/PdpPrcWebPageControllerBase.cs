// PdpPrcControllerBase.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Net;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Controllers;

// convention: name abstract controllers with suffix ControllerBase
public abstract class PdpPrcWebPageControllerBase : PageModel
{
  // CONSTANTS
  
  protected const string ranNpds = CoreDLC.ranpView;
  protected const int raoNpds = CoreDLC.raordView;

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

  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    pdpRestCntxt = new PdpRestContext(exeCntxt.HttpContext.Request); // calls ParseQueryCollection on new()
  }
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
    base.OnPageHandlerExecuted(exeCntxt);
    if (pdpRestCntxt == null)
    { throw new NullReferenceException($"pdpRestCntxt is null in {nameof(PdpPrcWebPageControllerBase)} OnActionExecuted()."); }
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
