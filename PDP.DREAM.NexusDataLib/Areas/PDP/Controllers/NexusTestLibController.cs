﻿// NexusTestLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NexusDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class NexusTestLibController : NexusDataControllerBase
  {
    private readonly ILogger<NexusTestLibController>? libLogger = null;

    public NexusTestLibController(ILogger<NexusTestLibController> logger)
    {
      libLogger = logger;
    }

  } // class

} // namespace
