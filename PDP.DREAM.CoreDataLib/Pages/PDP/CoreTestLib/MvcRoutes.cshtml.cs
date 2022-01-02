using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PDP.DREAM.CoreDataLib.Pages;

public class PdpCoreTestLibMvcRoutes : PdpPrcWebPageControllerBase
{
  // [HttpGet]
  // [PdpMvcRoute("CoreTestLib/MvcRoutes", "PdpCoreTestLibMvcRoutes", true)]
  public void OnGet()
  {
    // dev/test/debug
    // var pageDef1 = PdpSiteSettings.Values.AppSiteMvcDefPage;
    // var pageLayout = PRC.RazorMvcLayoutPage;
    // var pageDef2 = PRC.PageMvcRazor;
  }

}
