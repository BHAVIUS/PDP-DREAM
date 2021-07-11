// ScribeTestLibController.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.ScribeDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea), AllowAnonymous]
  public class ScribeTestLibController : ScribeDataControllerBase
  {
    private readonly ILogger<ScribeTestLibController>? libLogger = null;

    public ScribeTestLibController(ILogger<ScribeTestLibController> logger)
    {
      libLogger = logger;
    }

  } // class

} // namespace
