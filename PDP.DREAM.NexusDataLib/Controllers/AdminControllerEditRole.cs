// AdminControllerEditRole.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

public partial class AdminNexusController
{
  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult ViewSiaaRoles() { return View(); }

  [HttpGet, HttpPost]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public JsonResult SelectSiaaRoles([DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult result = QUC.ListEditableAppRoles().ToDataSourceResult(request);
    return Json(result);
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult EditSiaaRoles() { return View(); }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public JsonResult EditSiaaRole([DataSourceRequest] DataSourceRequest dsr, QebiRoleUxm editObj)
  {
    if (ModelState.IsValid) { editObj = QUC.EditSiaaRole(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete/Post for Rest, Post for Ajax
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public JsonResult DeleteSiaaRole([DataSourceRequest] DataSourceRequest dsr, QebiRoleUxm editObj)
  {
    if (!editObj.RoleGuid.IsEmpty()) { editObj = QUC.DeleteSiaaRole(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
