﻿// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Services;
using PDP.DREAM.CoreDataLib.Stores;
using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreWebLib.Controllers;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;
using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;

namespace PDP.DREAM.CoreWebLib.Pages;

[RequireHttps, Authorize(Roles = NpdsAdmin)]
public class AdminCoreEditSiaaRoles : CoreDataRazorPageControllerBase
{
  private const string rzrCntrllr = nameof(AdminCoreEditSiaaRoles);
  public AdminCoreEditSiaaRoles(QebIdentityContext? userCntxt = null, CoreDbsqlContext? npdsCntxt = null,
    IEmailSender? emlSndr = null, ISmsSender? smsSndr = null, ILoggerFactory? lgrFtry = null)
    : base(userCntxt, npdsCntxt, emlSndr, smsSndr, lgrFtry) { }

  // OnPageHandlerExecuting before OnGet
  public override void OnPageHandlerExecuting(PageHandlerExecutingContext exeCntxt)
  {
    QURC = new QebUserRestContext(exeCntxt.HttpContext.Request)
    {
      DatabaseAccess = NpdsDatabaseAccess.AuthReadWrite,
      RecordAccess = NpdsRecordAccess.Admin,
      AdminModeClientRequired = true,
      QebSessionValueIsRequired = true
    };
    PSR = new PdpSiteRazorModel("/NPDS/AdminCore/EditSiaaRoles",
      $"{PDPSS.AppOwnerShortName}: Edit SIAA Roles");
    PSR.InitRazorPageMenus("_AdminCoreSpanPageMenu");
    ResetCoreRepository();
    var isVerified = CheckCoreAgentSession();
    if (!isVerified) { RedirectToPage(DepQebIdentRequired); }
  }

  // OnGet before OnPageHandlerExecuted
  public IActionResult OnGet()
  {
#if DEBUG
    CatchNullQurc(nameof(OnGet), rzrCntrllr);
    PSR.DebugRazorPageStrings();
#endif
    return Page();
  }

  // OnPageHandlerExecuted before the [RazorPage].cshtml
  public override void OnPageHandlerExecuted(PageHandlerExecutedContext exeCntxt)
  {
#if DEBUG
    CatchNullQurc(nameof(OnPageHandlerExecuted), rzrCntrllr);
    DebugQurcData(exeCntxt.Result);
#endif
  }

  public JsonResult OnPostReadSiaaRoles([DataSourceRequest] DataSourceRequest request)
  {
    DataSourceResult result = QUDC.ListEditableAppRoles().ToDataSourceResult(request);
    return new JsonResult(result);
  }

  public JsonResult OnPostWriteSiaaRole([DataSourceRequest] DataSourceRequest request, QebiRoleUxm editObj)
  {
    if (!editObj.RoleGuid.IsEmpty()) { editObj = QUDC.EditSiaaRole(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    return new JsonResult(result);
  }

  public JsonResult OnPostDeleteSiaaRole([DataSourceRequest] DataSourceRequest request, QebiRoleUxm editObj)
  {
    if (!editObj.RoleGuid.IsEmpty()) { editObj = QUDC.DeleteSiaaRole(editObj); }
    DataSourceResult result = (new[] { editObj }).ToDataSourceResult(request, ModelState);
    return new JsonResult(result);
  }

} // end class

// end file