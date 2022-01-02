// CoreDataLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Pages;

public class PdpCoreDataLibEntities : CoreDataWebPageControllerBase
{
  public void OnGet()
  {
  }

  // [HttpGet, HttpPost] // Get for Rest, Post for Ajax
  // [PdpMvcRoute("{controller}/SelectEntityTypes", "PdpCoreSelectEntityTypes", true)]
  public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
  {
    ResetCoreRepository(); // use PCDC
    DataSourceResult result = PCDC.ListViewableEntityTypes().ToDataSourceResult(request);
    return new JsonResult(result);
  }

}
