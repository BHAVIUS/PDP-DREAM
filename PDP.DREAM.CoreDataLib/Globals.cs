// Globals.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.Specialized;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Data;
global using System.Data.SqlTypes;
global using System.Diagnostics;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Globalization;
global using System.IO;
global using System.Net;
global using System.Net.Http;
global using System.Net.Http.Headers;
global using System.Net.Mail;
global using System.Reflection;
global using System.Reflection.Metadata;
global using System.Runtime.CompilerServices;
global using System.Runtime.Serialization;
global using System.Runtime.Serialization.Json;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.Encodings.Web;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Xml;
global using System.Xml.Linq;
global using System.Xml.Schema;
global using System.Xml.Serialization;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.Extensions;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApplicationModels;
global using Microsoft.AspNetCore.Mvc.Controllers;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.Razor;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.AspNetCore.Mvc.Routing;
global using Microsoft.AspNetCore.Mvc.ViewFeatures;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;

global using SendGrid;
global using SendGrid.Helpers.Mail;

global using PDP.DREAM.CoreDataLib.Models;
global using PDP.DREAM.CoreDataLib.Services;
global using PDP.DREAM.CoreDataLib.Stores;
global using PDP.DREAM.CoreDataLib.Types;
global using PDP.DREAM.CoreDataLib.Utilities;

global using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
global using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
global using static PDP.DREAM.CoreDataLib.Models.PdpSiteRoutes;
global using static PDP.DREAM.CoreDataLib.Types.PdpGuid;
global using static PDP.DREAM.CoreDataLib.Utilities.QebFile;
global using static PDP.DREAM.CoreDataLib.Utilities.QebString;
global using static PDP.DREAM.CoreDataLib.Utilities.QebSql;
