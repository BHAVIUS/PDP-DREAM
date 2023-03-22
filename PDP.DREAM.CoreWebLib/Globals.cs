// Globals.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Diagnostics;
global using System.Drawing.Imaging;
global using System.IO;
global using System.Linq;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading.Tasks;
global using System.Xml;
global using System.Xml.Linq;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.Razor;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;

global using Newtonsoft.Json;
global using Kendo.Mvc.Extensions;
global using Kendo.Mvc.UI;
global using Telerik.Web.Captcha;

global using PDP.DREAM.CoreDataLib.Models;
global using PDP.DREAM.CoreDataLib.Services;
global using PDP.DREAM.CoreDataLib.Stores;
global using PDP.DREAM.CoreDataLib.Types;
global using PDP.DREAM.CoreDataLib.Utilities;
global using PDP.DREAM.CoreWebLib.Controllers;

global using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
global using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
global using static PDP.DREAM.CoreDataLib.Models.PdpSiteRoutes;
global using static PDP.DREAM.CoreDataLib.Types.PdpGuid;
global using static PDP.DREAM.CoreDataLib.Utilities.QebFile;
global using static PDP.DREAM.CoreDataLib.Utilities.QebString;
global using static PDP.DREAM.CoreDataLib.Utilities.QebSql;
