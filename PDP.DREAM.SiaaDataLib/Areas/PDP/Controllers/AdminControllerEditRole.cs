// AdminControllerEditRole.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

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
