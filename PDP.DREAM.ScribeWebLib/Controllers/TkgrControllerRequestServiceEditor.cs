// TkgrControllerServiceEditorRequest.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;
using PDP.DREAM.ScribeDataLib.Controllers;
using PDP.DREAM.ScribeDataLib.Models;
using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgrControllerBase
{
  [HttpGet]
  [PdpMvcRoute(nameof(NpdsRequestServiceEditor), "", "", ScribeWLC.ranpView)]
  public IActionResult NpdsRequestServiceEditor()
  {
    BuildDropDownListsForResrepRoot();
    return View();
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeSelectServiceEditorRequests), "", "", ScribeWLC.ranpView)]
  public JsonResult ScribeSelectServiceEditorRequests([DataSourceRequest] DataSourceRequest dsr)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult result = PSDC.ListEditableServiceEditorRequests().ToDataSourceResult(dsr);
    return Json(result);
  }

  [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeUpsertServiceEditorRequest), "", "", ScribeWLC.ranpView)]
  public JsonResult ScribeUpsertServiceEditorRequest([DataSourceRequest] DataSourceRequest dsr, ServiceEditorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    var recordName = rem.ItemXnam;
    var infosetGuid = PdpGuid.ParseToNonNullable(rem.RRInfosetGuid, Guid.Empty);
    if (infosetGuid == Guid.Empty) // assure presence of valid infosetGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRInfosetGuid)} not valid in {nameof(ScribeUpsertServiceEditorRequest)}";
      ModelState.AddModelError(recordName, rem.PdpStatusMessage);
    }
    if (ModelState.IsValid) { rem = PSDC.EditServiceEditorRequest(rem); }
    rem.PdpStatusName = eidResrepRootStatus;
    DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

  [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
  [PdpMvcRoute(nameof(ScribeDeleteServiceEditorRequest), "", "", ScribeWLC.ranpView)]
  public JsonResult ScribeDeleteServiceEditorRequest([DataSourceRequest] DataSourceRequest dsr, ServiceEditorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteServiceEditorRequest(rem); rem.PdpStatusName = eidResrepRootStatus; }
    DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
    return Json(result);
  }

}
