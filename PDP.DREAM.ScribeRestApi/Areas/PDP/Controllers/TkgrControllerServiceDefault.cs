// TkgrControllerServiceDefault.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidServiceDefaultStatus = "span#ServiceDefaultStatus";

    //[HttpGet, HttpPost] // Get for Rest, Post for Ajax
    //public JsonResult SelectScribeServiceDefaultsForView([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    //{
    //  ResetScribeRepository(); // use PSDC
    //  DataSourceResult result = PSDC.ListViewableServiceDefaults(recordGuid, isLimited).ToDataSourceResult(request);
    //  return Json(result);
    //}

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public JsonResult SelectScribeServiceDefaultsForEdit([DataSourceRequest] DataSourceRequest request, Guid recordGuid, bool isLimited = false)
    {
      ResetScribeRepository(); // use PSDC
      DataSourceResult result = PSDC.ListEditableServiceDefaults(recordGuid, isLimited).ToDataSourceResult(request);
      return Json(result);
    }

    [HttpPut, HttpPost] // Put/Post for Rest, Post for Ajax
    public JsonResult EditScribeServiceDefault([DataSourceRequest] DataSourceRequest dsr, ServiceDefaultEditModel nre, Guid recordGuid)
    {
      ResetScribeRepository(); // use PSDC
      var recordName = nre.ItemXnam;
      nre.RRRecordGuid = PdpGuid.ParseToNonNullable(nre.RRRecordGuid, recordGuid);
      if ((nre.RRRecordGuid == null) || (nre.RRRecordGuid == Guid.Empty)) // assure presence of valid recordGuid
      { ModelState.AddModelError(recordName, "Guid is null, not a valid recordGuid."); }
      // TODO: redundant check at controller action level for XML parsing validation
      if (ModelState.IsValid) { nre = PSDC.EditServiceDefault(nre); nre.PdpStatusName = eidServiceDefaultStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

    [HttpDelete, HttpPost] // Delete for Rest, Post for Ajax
    public JsonResult DeleteScribeServiceDefault([DataSourceRequest] DataSourceRequest dsr, ServiceDefaultEditModel nre)
    {
      ResetScribeRepository(); // use PSDC
      if (ModelState.IsValid) { nre = PSDC.DeleteServiceDefault(nre); nre.PdpStatusName = eidServiceDefaultStatus; }
      DataSourceResult result = (new[] { nre }).ToDataSourceResult(dsr, ModelState);
      return Json(result);
    }

  }

}
