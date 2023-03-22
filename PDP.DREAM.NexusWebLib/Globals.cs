// Globals.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Diagnostics;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Data.SqlClient;
global using Microsoft.Extensions.Logging;

global using Kendo.Mvc.Extensions;
global using Kendo.Mvc.UI;

global using PDP.DREAM.CoreDataLib.Models;
global using PDP.DREAM.CoreDataLib.Services;
global using PDP.DREAM.CoreDataLib.Stores;
global using PDP.DREAM.CoreDataLib.Types;
global using PDP.DREAM.CoreDataLib.Utilities;
global using PDP.DREAM.CoreWebLib.Controllers;
global using PDP.DREAM.NexusDataLib.Models;
global using PDP.DREAM.NexusDataLib.Stores;
global using PDP.DREAM.NexusWebLib.Controllers;

global using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
global using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
global using static PDP.DREAM.CoreDataLib.Models.PdpSiteRoutes;
global using static PDP.DREAM.CoreDataLib.Utilities.QebSql;