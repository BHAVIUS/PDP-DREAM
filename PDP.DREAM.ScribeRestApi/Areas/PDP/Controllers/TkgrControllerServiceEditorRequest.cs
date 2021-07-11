// TkgrControllerServiceEditorRequest.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Reflection;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public IActionResult ServiceEditorRequests()
    {
      PRC.PageTitle = "Agent Requests for Service Editor Access";
      BuildDropDownListsForResrepStem();
      return View();
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeServiceEditorRequestsForEdit([DataSourceRequest] DataSourceRequest dsr)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableServiceEditorRequests().ToDataSourceResult(dsr);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeServiceEditorRequest([DataSourceRequest] DataSourceRequest dsr, ServiceEditorRequestEditModel rem)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = rem.ItemXnam;
      var infosetGuid = PdpGuid.ParseToNonNullable(rem.RRInfosetGuid, Guid.Empty);
      if (infosetGuid == Guid.Empty) // assure presence of valid infosetGuid
      {
        rem.PdpStatusMessage = $"{nameof(rem.RRInfosetGuid)} not valid in {MethodBase.GetCurrentMethod().Name}";
        ModelState.AddModelError(recordName, rem.PdpStatusMessage);
      }
      if (ModelState.IsValid) { rem = PSDC.EditServiceEditorRequest(rem); }
      rem.PdpStatusName = eidResrepRootStatus;
      DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeServiceEditorRequest([DataSourceRequest] DataSourceRequest dsr, ServiceEditorRequestEditModel rem)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { rem = PSDC.DeleteServiceEditorRequest(rem); rem.PdpStatusName = eidResrepRootStatus; }
      DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
