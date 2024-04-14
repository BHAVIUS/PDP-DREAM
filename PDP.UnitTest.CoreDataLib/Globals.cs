// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

global using System;
global using System.Diagnostics;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc.Diagnostics;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Logging;

global using PDP.DREAM.CoreDataLib.Models;
global using PDP.DREAM.CoreDataLib.Services;
global using PDP.DREAM.CoreDataLib.Stores;
global using PDP.DREAM.CoreDataLib.Types;
global using PDP.DREAM.CoreDataLib.Utilities;

global using Xunit;
global using Xunit.Abstractions;
global using Xunit.Extensions;

global using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
global using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
global using static PDP.DREAM.CoreDataLib.Models.PdpSiteRoutes;
global using static PDP.DREAM.CoreDataLib.Utilities.QebString;
global using static PDP.DREAM.CoreDataLib.Utilities.QebSqlLinq;

global using MST = Microsoft.VisualStudio.TestTools.UnitTesting;
