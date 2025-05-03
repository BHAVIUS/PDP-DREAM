// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgnPageControllerBase
{
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  public virtual JsonResult OnPostReadResrepRoots([DataSourceRequest] DataSourceRequest request, 
    string serviceType, string serviceTag, string entityType, string recordAccess)
  {
    QURC.ParseNpdsSelectFilterForPage(serviceType, serviceTag, entityType, recordAccess);
    ResetNexusRepository();  // use PNDC
    var resreps = PNDC.ListViewableResrepRoots(request, out int numResreps);
    var result = new DataSourceResult() { Data = resreps, Total = numResreps };
    return new JsonResult(result);
  }

  public virtual ContentResult OnPostCheckResrepRoot(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepRootByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return Content(result);
  }

  public virtual ContentResult OnPostCheckResrepStem(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepStemByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return Content(result);
  }

  public virtual ContentResult OnPostCheckResrepLeaf(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepLeafByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return Content(result);
  }

} // end class

// end file