using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AdminController
  {
    [HttpGet]
    public IActionResult ViewRoles() { return View(); }

    [HttpGet, HttpPost]
    public JsonResult SelectSiaaRoles([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = QUC.ListEditableAppRoles().ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet]
    public IActionResult EditRoles() { return View(); }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditSiaaRole([DataSourceRequest] DataSourceRequest dsr, SiaaRoleUxm editObj)
    {
      if (ModelState.IsValid) { editObj = QUC.EditRole(editObj); }
      DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete/Post for Rest, Post for Ajax
    public JsonResult DeleteSiaaRole([DataSourceRequest] DataSourceRequest dsr, SiaaRoleUxm editObj)
    {
      if (ModelState.IsValid) { editObj = QUC.DeleteRole(editObj); }
      DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
