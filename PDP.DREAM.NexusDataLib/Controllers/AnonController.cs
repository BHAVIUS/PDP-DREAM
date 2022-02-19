﻿// AnonController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.NexusDataLib.Controllers;

[Area(PdpConst.PdpMvcArea), AllowAnonymous]
public partial class AnonNexusController : UserNexusController
{
  public AnonNexusController(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
    : base(userCntxt, emlSndr, smsSndr, lgrFtry) { }

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult Index() { return View(); }

  // Index/Help first, then rest alphabetical

  [HttpGet]
  [PdpMvcRoute(NexusDLC.ranpView, CoreDLC.raordView, PdpConst.PdpMvcArea)]
  public IActionResult AccessDenied() { return View(); }

} // class
