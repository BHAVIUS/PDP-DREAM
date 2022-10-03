// PdpRazorViewRouteAttribute.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc.Routing;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Types;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PdpRazorViewRouteAttribute : Attribute, IRouteTemplateProvider
{
  // for use with private fields, prefix acronym pmr for PDP MVC Route 

  public PdpRazorViewRouteAttribute(string routeTemplateSuffix = "")
  {
    InitNameTemplateOrder("", routeTemplateSuffix);
  }
  public PdpRazorViewRouteAttribute(string routeNamePrefix, string routeTemplateSuffix)
  {
    InitNameTemplateOrder(routeNamePrefix, routeTemplateSuffix);
  }
  private void InitNameTemplateOrder(string routeNamePrefix, string routeTemplateSuffix)
  {
    if (string.IsNullOrEmpty(routeNamePrefix))
    { routeNamePrefix = "PdpDreamView"; }
    if (string.IsNullOrEmpty(DepNpdsFolder))
    {
      pmrTemplate = "/[controller]/[action]";
      pmrName = $"{routeNamePrefix}:[controller]_[action]";
    }
    else
    {
      pmrTemplate = $"/{DepNpdsFolder}/[controller]/[action]";
      pmrName = $"{routeNamePrefix}:{DepNpdsFolder}_[controller]_[action]";
    }
    if (!string.IsNullOrEmpty(routeTemplateSuffix)) { pmrTemplate += $"/{routeTemplateSuffix}"; }
    pmrOrder = DepRaoRazorView;

  }

  // intended for REST API routes
  public PdpRazorViewRouteAttribute(string routeTemplate, string routeName, bool withFolder)
  {
    var routeNamePrefix = "PdpDreamApi";
    pmrTemplate = routeTemplate;
    pmrName = $"{routeNamePrefix}:{routeName}";
    pmrOrder = DepRaoRestApi;
  }
  public PdpRazorViewRouteAttribute(string routeTemplate, string routeName, bool withFolder, int routeOrder)
  {
    pmrTemplate = PatternTemplateWithDepFolder(routeTemplate, withFolder);
    pmrName = routeName;
    pmrOrder = routeOrder;
  }

  // DREAM EndPoint Folder = DepFolder
  private string PatternTemplateWithDepFolder(string routeTemplate, bool withDepFolder)
  {
    if (withDepFolder && string.IsNullOrEmpty(DepNpdsFolder))
    { throw new NullReferenceException("DepNpdsFolder is null or empty"); }
    var pmrPattern = ((withDepFolder) ? $"/{DepNpdsFolder}/{routeTemplate}" : routeTemplate);
    if (string.IsNullOrEmpty(pmrPattern)) { pmrPattern = "/"; };
    return pmrPattern;
  }

  string? IRouteTemplateProvider.Template
  { get { return pmrTemplate; } }
  private string? pmrTemplate = null;

  string? IRouteTemplateProvider.Name
  { get { return pmrName; } }
  private string? pmrName = null;
  int? IRouteTemplateProvider.Order
  { get { return pmrOrder; } }
  private int? pmrOrder = null;

} // end class

// end file