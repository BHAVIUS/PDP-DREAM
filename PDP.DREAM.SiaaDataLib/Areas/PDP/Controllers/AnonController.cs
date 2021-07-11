// AnonController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public partial class AnonController : UserController
  {
    public AnonController(ScribeDbsqlContext npdsCntxt, QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
      : base(npdsCntxt, userCntxt, emlSndr, smsSndr, lgrFtry) { }

    [HttpGet]
    public IActionResult Index() { return View(); }

    [HttpGet]
    public IActionResult AccessDenied() { return View(); }

  } // class

} // namespace
