// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.ScribeDataLib.Models;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.ScribeWebLib.Controllers;

public partial class TkgsPageControllerBase
{
  public virtual JsonResult OnPostReadResrepAuthorRequests([DataSourceRequest] DataSourceRequest dsRequest)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableResrepAuthorRequests().ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepAuthorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    var recordName = rem.ItemXnam;
    var recordGuid = PdpGuid.ParseToNonNullable(rem.RRRecordGuid, Guid.Empty);
    if (recordGuid == Guid.Empty) // assure presence of valid resouceGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRRecordGuid)} not valid in {nameof(OnPostWriteResrepAuthorRequest)}";
      ModelState.AddModelError(recordName, rem.PdpStatusMessage);
    }
    if (ModelState.IsValid)
    {
      // TODO: reactivate the archiving before reassigning the author
      // NexusResrepEditModel rem = PDC.GetEditableScribeResrepRecordByKey((Guid)rem.ResrepRGuid);
      // PDC.ArchiveScribeResrepRecord(rem);
      rem = PSDC.EditResrepAuthorRequest(rem);
    }
    rem.PdpStatusElement = eidResrepRootStatus;
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    return new JsonResult(dsResult);
  }

  public virtual JsonResult OnPostDeleteResrepAuthorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ResrepAuthorRequestEditModel rem)
  {
    ResetScribeRepository(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteResrepAuthorRequest(rem); rem.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file