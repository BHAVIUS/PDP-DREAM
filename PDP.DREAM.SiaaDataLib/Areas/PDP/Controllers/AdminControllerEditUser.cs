// AdminControllerEditUser.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.SiaaDataLib.Models.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  public partial class AdminController
  {
    [HttpGet]
    public IActionResult ViewUsers() { return View(); }

    [HttpGet, HttpPost]
    public JsonResult SelectSiaaUsers([DataSourceRequest] DataSourceRequest request)
    {
      // var appUsers = QUC.ListEditableAppUsers();
      // DataSourceResult result = appUsers.ToDataSourceResult(request);
      DataSourceResult result = QUC.ListEditableAppUsers().ToDataSourceResult(request);
      return Json(result);
    }

    [HttpGet]
    public IActionResult EditUsers() { return View(); }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditSiaaUser([DataSourceRequest] DataSourceRequest dsr, SiaaUserUxm editObj)
    {
      if (ModelState.IsValid) { editObj = QUC.EditSiaaUser(editObj); }
      DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete/Post for Rest, Post for Ajax
    public JsonResult DeleteSiaaUser([DataSourceRequest] DataSourceRequest dsr, SiaaUserUxm editObj)
    {
      if (!editObj.UserGuid.IsEmpty()) { editObj = QUC.DeleteSiaaUser(editObj); }
      DataSourceResult result = (new[] { editObj }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
