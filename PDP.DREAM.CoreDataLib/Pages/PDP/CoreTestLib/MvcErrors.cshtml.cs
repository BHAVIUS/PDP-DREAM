using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PDP.DREAM.CoreDataLib.Pages
{
  public class PdpCoreTestLibMvcErrors : PdpPrcWebPageControllerBase
  {
    public MvcErrorUxm UXM {  get; set; }

    // [HttpGet, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // [PdpMvcRoute("CoreTestLib/MvcErrors", "PdpCoreTestLibMvcErrors", true)]
    public void OnGet()
    {
      // dev/test/debug
      // var pageDef1 = PdpSiteSettings.Values.AppSiteMvcDefPage;
      // var pageLayout = PRC.RazorMvcLayoutPage;
      // var pageDef2 = PRC.PageMvcRazor;

      UXM = new MvcErrorUxm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
    }

  }

}
