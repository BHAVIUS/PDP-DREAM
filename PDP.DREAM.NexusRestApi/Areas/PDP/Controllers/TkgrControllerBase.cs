// TkgrControllerBase.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NexusDataLib.Controllers;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public abstract partial class TkgrControllerBase : NexusDataControllerBase
  {
    public TkgrControllerBase(NexusDbsqlContext npdsCntxt) : base(npdsCntxt) { }

    [HttpGet]
    public override IActionResult Index()
    {
      PRC.PageTitle = PRC.RecordAccess.ToString() + " Index";
      return View();
    }

    [HttpGet]
    public override IActionResult Help()
    {
      PRC.PageTitle = PRC.RecordAccess.ToString() + " Help";
      return View();
    }

    [HttpGet]
    public virtual IActionResult NpdsView(string serviceType, string serviceTag, string entityType = "")
    {
      PRC.ParseNpdsServTagEntity(serviceType, serviceTag, entityType, "View");
      return View(PRC.ViewName);
    }

  } // end class

} // namespace

