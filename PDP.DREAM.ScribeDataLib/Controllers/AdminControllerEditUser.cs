// AdminControllerEditUser.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.ScribeDataLib.Controllers;

public partial class AdminScribeController
{
  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult ViewSiaaUsers() { return View(); }

  [HttpGet, HttpPost]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public JsonResult SelectSiaaUsers([DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult result = QUC.ListEditableAppUsers().ToDataSourceResult(request);
    return Json(result);
  }

  [HttpGet]
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public IActionResult EditSiaaUsers() { return View(); }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public JsonResult EditSiaaUser([DataSourceRequest] DataSourceRequest dsr, QebiUserUxm editObj)
  {
    if (ModelState.IsValid) { editObj = QUC.EditSiaaUser(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete/Post for Rest, Post for Ajax
  [PdpMvcRoute(ranNpds, raoNpds, PdpConst.PdpMvcArea)]
  public JsonResult DeleteSiaaUser([DataSourceRequest] DataSourceRequest dsr, QebiUserUxm editObj)
  {
    if (!editObj.UserGuid.IsEmpty()) { editObj = QUC.DeleteSiaaUser(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

} // class
