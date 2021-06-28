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
    public IActionResult ResrepAuthorRequests()
    {
      PRC.PageTitle = "Agent Requests for Resrep Author Access";
      BuildDropDownListsForResrepStem();
      return View();
    }

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeResrepAuthorRequestsForEdit([DataSourceRequest] DataSourceRequest dsr)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableResrepAuthorRequests().ToDataSourceResult(dsr);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsr, ResrepAuthorRequestEditModel rem)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = rem.ItemXnam;
      var recordGuid = PdpGuid.ParseToNonNullable(rem.RRRecordGuid, Guid.Empty);
      if (recordGuid == Guid.Empty) // assure presence of valid resouceGuid
      {
        rem.PdpStatusMessage = $"recordGuid not valid in {MethodBase.GetCurrentMethod().Name}";
        ModelState.AddModelError(recordName, rem.PdpStatusMessage);
      }
      if (ModelState.IsValid)
      {
        // TODO: reactivate the archiving before reassigning the author
        // NexusResrepEditModel rem = PDC.GetEditableScribeResrepRecordByKey((Guid)rem.ResrepRGuid);
        // PDC.ArchiveScribeResrepRecord(rem);
        rem = PSDC.EditResrepAuthorRequest(rem);
      }
      rem.PdpStatusName = eidResrepRootStatus;
      DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsr, ResrepAuthorRequestEditModel rem)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { rem = PSDC.DeleteResrepAuthorRequest(rem); rem.PdpStatusName = eidResrepRootStatus; }
      DataSourceResult result = (new[] { rem }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpPost]
    public JsonResult RequestReleaseNpdsResrepRecord([DataSourceRequest] DataSourceRequest dsr, Guid id)
    {
      ResetScribeRepository(); // use PSDC
      NexusResrepEditModel? rvm = PSDC.GetEditableResrepStemByKey(id);
      if ((rvm != null) && (rvm.RRRecordGuid == id)) { rvm = PSDC.RequestReleaseResrepRecord(rvm); }
      DataSourceResult result = (new[] { rvm }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
