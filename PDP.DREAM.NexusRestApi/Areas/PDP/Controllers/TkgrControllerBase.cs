using System.Net;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.NpdsCoreLib.Utilities;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public abstract partial class TkgrControllerBase : NexusDataControllerBase
  {
    public TkgrControllerBase() { }
    public TkgrControllerBase(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

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
      ArgumentChecker.CatchNullOrWhite(serviceType); ArgumentChecker.CatchNullOrWhite(serviceTag);
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
      return View(PRC.ViewName);
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectEntityTypesForView([DataSourceRequest] DataSourceRequest request)
    {
      ResetNexusRepository(); // use PNDC
      DataSourceResult result = PNDC.ListViewableEntityTypes().ToDataSourceResult(request);
      return Json(result);
    }


  }

}
