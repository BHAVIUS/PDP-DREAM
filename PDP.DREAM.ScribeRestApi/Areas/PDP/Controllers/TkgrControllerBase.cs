// TkgrControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Net;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Controllers;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public abstract partial class TkgrControllerBase : SiaaDataControllerBase
  {
    public TkgrControllerBase() { }
    public TkgrControllerBase(ScribeDbsqlContext npdsCntxt) : base(npdsCntxt) { }
    public TkgrControllerBase(ScribeDbsqlContext npdsCntxt, PdpAgentCmsContext userCntxt) : base(npdsCntxt, userCntxt) { }
    public TkgrControllerBase(PdpAgentCmsContext userCntxt) : base(userCntxt) { }
    public TkgrControllerBase(PdpAgentCmsContext userCntxt, ScribeDbsqlContext npdsCntxt) : base(userCntxt, npdsCntxt) { }

    [HttpGet]
    public override IActionResult Index()
    {
      PRC.PageTitle = PRC.RecordAccess.ToString() + " Index";
      return View();
    }

    [HttpGet]
    public override IActionResult Help()
    {
      PRC.PageTitle = PRC.RecordAccess.ToString() + " Help";
      return View();
    }

    [HttpGet]
    public virtual IActionResult NpdsView(string serviceType, string serviceTag, string entityType = "")
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
      return View(PRC.ViewName);
    }

    [HttpGet]
    public virtual IActionResult NpdsEdit(string serviceType, string serviceTag, string entityType = "")
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "Edit");
      BuildDropDownListsForResrepStem();
      return View(PRC.ViewName);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectEntityTypesForView([DataSourceRequest] DataSourceRequest request)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListViewableEntityTypes().ToDataSourceResult(request);
      return Json(result);
    }

    public string? HtmlCleanByEncodeDecode(string? dirty)
    {
      var clean = WebUtility.HtmlDecode(WebUtility.HtmlEncode(dirty));
      return clean;
    }
    public string? UrlCleanByEncodeDecode(string? dirty)
    {
      var clean = WebUtility.UrlDecode(WebUtility.UrlEncode(dirty));
      return clean;
    }
    public string? HtmlEscapeHashLiteral(string? withHash)
    {
      var withoutHash = withHash.Replace("#", "\\#");
      return withoutHash;
    }

  }

}
