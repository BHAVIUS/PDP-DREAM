// UserController.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Controllers;
using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;

namespace PDP.DREAM.NexusDataLib.Controllers;

[Area(PdpConst.PdpMvcArea)]
public partial class UserNexusController : QebIdentityControllerBase
{
  public UserNexusController(QebIdentityContext userCntxt,
    IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
    : base(userCntxt, emlSndr, smsSndr, lgrFtry) { }

}
