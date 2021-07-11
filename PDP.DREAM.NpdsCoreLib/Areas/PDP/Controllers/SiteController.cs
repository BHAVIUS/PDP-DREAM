// SiteController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsCoreLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class SiteController : NpdsCoreControllerBase
  {
    [HttpGet]
    public IActionResult Info()
    {
      string npdsTag = string.Empty;
      if (string.IsNullOrWhiteSpace(PRC.SiteTitle))
      {
        if (PRC.ServerType == NpdsConst.ServerType.Diristry)
        {
          npdsTag = NpdsServiceDefaults.GetValues.NpdsDefaultDiristryTag;
          PRC.SiteTitle = string.Format("{0} Nexus Diristry", npdsTag);
        }
        else if (PRC.ServerType == NpdsConst.ServerType.Registry)
        {
          npdsTag = NpdsServiceDefaults.GetValues.NpdsDefaultRegistryTag;
          PRC.SiteTitle = string.Format("{0} PORTAL Registry", npdsTag);
        }
        else if (PRC.ServerType == NpdsConst.ServerType.Directory)
        {
          npdsTag = NpdsServiceDefaults.GetValues.NpdsDefaultDirectoryTag;
          PRC.SiteTitle = string.Format("{0} DOORS Directory", npdsTag);
        }
        else if (PRC.ServerType == NpdsConst.ServerType.Registrar)
        {
          npdsTag = NpdsServiceDefaults.GetValues.NpdsDefaultRegistrarTag;
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
    public override IActionResult Help()
    {
      // TODO 2018/6/5: move these property definitions to PdpRestContext or to PrcOptionsForRequest
      PRC.HelpUrl = PRC.PdpReqstHost + "?" + PdpConst.PdpHelpRouteQueryKey;
      PRC.DebugUrl = PRC.PdpReqstHost + "?" + PdpConst.PdpDebugRouteQueryKey;
      PRC.HelpDebugUrl = PRC.PdpReqstHost + "?" + PdpConst.PdpHelpRouteQueryKey + "&" + PdpConst.PdpDebugRouteQueryKey;
      return View();
    }

    [HttpGet]
    public IActionResult Design() { return View(); }

    [HttpGet]
    public IActionResult Papers() { return View(); }

  }

}
