// TkgrControllerResrepRootItem.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.NexusWebLib.Controllers;

public partial class TkgrControllerBase
{
  private const string eidResrepRootStatus = "span#ResrepRootStatus";
  private const string eidResrepStemStatus = "span#ResrepStemStatus";
  private const string eidResrepLeafStatus = "span#ResrepLeafStatus";

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(NexusSelectResrepRoots), "", CoreDLC.ratsStstet, NPvsp)]
  public JsonResult NexusSelectResrepRoots([DataSourceRequest] DataSourceRequest request, string serviceType, string serviceTag, string entityType)
  {
    PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType);
    ResetNexusRepository();  // use PNDC
    var resreps = PNDC.ListViewableResrepRoots(request, out int numResreps);
    var result = new DataSourceResult() { Data = resreps, Total = numResreps };
    return Json(result);
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(NexusCheckResrepRoot), "", CoreDLC.ratsRg, NPvsp)]
  public ActionResult<string?> NexusCheckResrepRoot(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepRootByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(NexusCheckResrepStem), "", CoreDLC.ratsRg, NPvsp)]
  public ActionResult<string?> NexusCheckResrepStem(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepStemByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

  [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  [PdpMvcRoute(nameof(NexusCheckResrepLeaf), "", CoreDLC.ratsRg, NPvsp)]
  public ActionResult<string?> NexusCheckResrepLeaf(Guid recordGuid)
  {
    ResetNexusRepository();  // use PNDC
    NexusResrepViewModel? nre = PNDC.GetViewableResrepLeafByKey(recordGuid);
    var result = string.Empty;
    if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
    return result;
  }

} // class
