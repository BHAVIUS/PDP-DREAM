using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

using Microsoft.AspNetCore.Mvc;

namespace PDP.DREAM.CoreDataLib.Pages;

public class NpdsSiteIndex : PdpPrcWebPageControllerBase
{
  // [HttpGet]
  // [PdpMvcRoute("Site/Index", "PdpSiteIndex", true)]
  public void OnGet()
  {
    // dev/test/debug
    // var pageDef1 = PdpSiteSettings.Values.AppSiteMvcDefPage;
    // var pageLayout = PRC.RazorMvcLayoutPage;
    // var pageDef2 = PRC.PageMvcRazor;

    // return Redirect(PdpSiteSettings.Values.AppSiteMvcDefPage);
  }

}
