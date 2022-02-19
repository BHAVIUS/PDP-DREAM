// SiteController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public class SiteController : CoreDataRestApiControllerBase
{
  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Help()
  {
    return View();
  }

  // Index/Help first, then rest alphabetical, except special route last

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Design() { return View(); }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Info()
  {
    string npdsTag = string.Empty;
    if (string.IsNullOrWhiteSpace(PRC.SiteTitle))
    {
      if (PRC.ServerType == NpdsConst.ServerType.Diristry)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultDiristryTag;
        PRC.SiteTitle = string.Format("{0} Nexus Diristry", npdsTag);
      }
      else if (PRC.ServerType == NpdsConst.ServerType.Registry)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultRegistryTag;
        PRC.SiteTitle = string.Format("{0} PORTAL Registry", npdsTag);
      }
      else if (PRC.ServerType == NpdsConst.ServerType.Directory)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultDirectoryTag;
        PRC.SiteTitle = string.Format("{0} DOORS Directory", npdsTag);
      }
      else if (PRC.ServerType == NpdsConst.ServerType.Registrar)
      {
        npdsTag = NpdsServiceDefaults.Values.NpdsDefaultRegistrarTag;
        PRC.SiteTitle = string.Format("{0} Scribe Registrar", npdsTag);
      }
    }
    if (string.IsNullOrWhiteSpace(PRC.PageTitle))
    {
      if (PRC.ServerType != NpdsConst.ServerType.Registrar)
      {
        PRC.PageTitle = string.Format("{0} Resource Metadata Records", npdsTag);
      }
    }
    return View();
  }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Papers() { return View(); }

  [HttpGet]
  [PdpMvcRoute(CoreDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Privacy() { return View(); }

  // special route last to handle empty paths for use with site startup
  [Route("", Name = "PdpSiteEmptyPath", Order = CoreDLC.raordView)]
  public IActionResult EmptyPath() { return Redirect(PdpSiteSettings.Values.AppSiteMvcDefPath); }

} // class
