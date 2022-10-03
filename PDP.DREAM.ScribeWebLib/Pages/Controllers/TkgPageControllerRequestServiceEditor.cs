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

  public virtual JsonResult OnPostReadServiceEditorRequests([DataSourceRequest] DataSourceRequest dsRequest,
    Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    DataSourceResult dsResult = PSDC.ListEditableServiceEditorRequests().ToDataSourceResult(dsRequest);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostWriteServiceEditorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ServiceEditorRequestEditModel rem, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    var recordName = rem.ItemXnam;
    var infosetGuid = PdpGuid.ParseToNonNullable(rem.RRInfosetGuid, Guid.Empty);
    if (infosetGuid == Guid.Empty) // assure presence of valid infosetGuid
    {
      rem.PdpStatusMessage = $"{nameof(rem.RRInfosetGuid)} not valid in {nameof(OnPostWriteServiceEditorRequest)}";
      ModelState.AddModelError(recordName, rem.PdpStatusMessage);
    }
    if (ModelState.IsValid) { rem = PSDC.EditServiceEditorRequest(rem); }
    rem.PdpStatusElement = eidResrepRootStatus;
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

  public virtual JsonResult OnPostDeleteServiceEditorRequest([DataSourceRequest] DataSourceRequest dsRequest,
   ServiceEditorRequestEditModel rem, Guid recordGuid, bool isLimited = false)
  {
    ResetScribeRepository(); // use PSDC
    if (ModelState.IsValid) { rem = PSDC.DeleteServiceEditorRequest(rem); rem.PdpStatusElement = eidResrepRootStatus; }
    DataSourceResult dsResult = (new[] { rem }).ToDataSourceResult(dsRequest, ModelState);
    var jsonData = new JsonResult(dsResult, QebKendoJsonOptions);
    return jsonData;
  }

} // end class

// end file