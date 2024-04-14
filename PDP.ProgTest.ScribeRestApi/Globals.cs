// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

global using System;
global using System.Diagnostics;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.CookiePolicy;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Diagnostics;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.Razor;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Logging;

global using Kendo.Mvc.Extensions;
global using Kendo.Mvc.UI;

global using PDP.DREAM.CoreDataLib.Models;
global using PDP.DREAM.CoreDataLib.Services;
global using PDP.DREAM.CoreDataLib.Stores;
global using PDP.DREAM.CoreDataLib.Types;
global using PDP.DREAM.CoreDataLib.Utilities;
global using PDP.DREAM.CoreWebLib.Controllers;
global using PDP.DREAM.CoreWebLib.Models;
global using PDP.DREAM.NexusWebLib.Controllers;
global using PDP.DREAM.NexusWebLib.Models;
global using PDP.DREAM.ScribeWebLib.Controllers;
global using PDP.DREAM.ScribeWebLib.Models;


// static libraries

global using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
global using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
global using static PDP.DREAM.CoreDataLib.Models.PdpSiteRoutes;
global using static PDP.DREAM.CoreDataLib.Utilities.QebFile;
global using static PDP.DREAM.CoreDataLib.Utilities.QebString;

