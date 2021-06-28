﻿using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsCoreLib.Utilities;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.ScribeRestApi.Controllers;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.ScribeWebApp.Controllers
{
  [Area(PdpConst.PdpMvcArea), RequireHttps, Authorize(Roles = PdpConst.NPDSADMIN)]
  public class AdminResrepsController : TkgrControllerBase
  {
    public AdminResrepsController(PdpAgentCmsContext userCntxt, ScribeDbsqlContext dataCntxt) : base(userCntxt, dataCntxt) { }

    public override void OnActionExecuting(ActionExecutingContext oaeCtxt)
    {
      pdpRestCntxt = new PdpRestContext(oaeCtxt.HttpContext.Request)
      {
        DatabaseType = NpdsConst.DatabaseType.Nexus,
        DatabaseAccess = NpdsConst.DatabaseAccess.AuthReadWrite,
        RecordAccess = NpdsConst.RecordAccess.Admin
      };
      CheckClientAgentSession();
    }

    public IActionResult NpdsServiceDefaults()
    {
      PRC.ParseNpdsServTagEntity("Scribe", "NPDS-Root", "", "Edit");
      PRC.PageTitle = $"Admin Edit NPDS-Root Service Defaults";
      BuildDropDownListsForResrepStem();
      return View();
    }

    public IActionResult NpdsServiceRestrictions()
    {
      PRC.ParseNpdsServTagEntity("Scribe", "NPDS-Root", "", "Edit");
      PRC.PageTitle = $"Admin Edit NPDS-Root Service Restrictions";
      BuildDropDownListsForResrepStem();
      return View();
    }

    public IActionResult CheckLocationsForResreps(string serviceType, string serviceTag, string entityType)
    {
      ArgumentChecker.CatchNullOrWhite(serviceType); ArgumentChecker.CatchNullOrWhite(serviceTag);
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
      foreach (var rrr in PSDC.ListEditableResrepStems())
      {
        var recordGuid = (Guid)rrr.RRRecordGuid;
        if (!PdpGuid.IsInvalidGuid(recordGuid))
        {
          foreach (var loc in PSDC.ListEditableLocations(recordGuid))
          {
            if (!string.IsNullOrEmpty(loc.CityLocality)) { PSDC.CheckLocation(loc); }
          }
        }
      }
      return View(PRC.ViewName);
    }

  }

}