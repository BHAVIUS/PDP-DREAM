// GlobalUsing.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Linq;
global using System.IO;
global using System.Reflection;
global using System.Text;
global using System.Threading.Tasks;
global using System.Xml.Linq;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.FileProviders;

global using Kendo.Mvc;
global using Kendo.Mvc.Extensions;
global using Kendo.Mvc.UI;

global using PDP.DREAM.CoreDataLib.Models;
global using PDP.DREAM.CoreDataLib.Stores;
global using PDP.DREAM.CoreDataLib.Types;
global using PDP.DREAM.CoreDataLib.Utilities;
global using PDP.DREAM.NexusDataLib.Models;
global using PDP.DREAM.NexusDataLib.Stores;

global using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
global using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
global using static PDP.DREAM.CoreDataLib.Utilities.QebString;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
