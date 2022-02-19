// PdpMvcActionRouteAttribute.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using Microsoft.AspNetCore.Mvc.Routing;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Types;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PdpMvcRouteAttribute : Attribute, IRouteTemplateProvider
{
  // for use with private fields, prefix acronym pmr for PDP MVC Route 
  public PdpMvcRouteAttribute(string actionName, string cntrlrName)
  {
    CreatePdpMvcRouteTemplate(actionName, cntrlrName, "", "", "", true);
  }
  public PdpMvcRouteAttribute(string actionName, string cntrlrName, bool withArea, string routeTemplateSuffix, string routeNamePrefix, string routeNameSuffix = "")
  {
    CreatePdpMvcRouteTemplate(actionName, cntrlrName, routeTemplateSuffix, routeNamePrefix, routeNameSuffix, withArea);
  }
  public PdpMvcRouteAttribute(string actionName, string cntrlrName, string routeTemplateSuffix, string routeNamePrefix, string routeNameSuffix = "")
  {
    CreatePdpMvcRouteTemplate(actionName, cntrlrName, routeTemplateSuffix, routeNamePrefix, routeNameSuffix, true);
  }
  public PdpMvcRouteAttribute()
  {
    pmrTemplate = PatternTemplateWithArea("", true);
    pmrName = "PdpMvcRouteEmpty";
  }
  public PdpMvcRouteAttribute(string appName, int routeOrder, string areaName = "")
  {
    if (string.IsNullOrEmpty(appName))
    { throw new ArgumentNullException($"{nameof(appName)} is null or empty in {nameof(PdpMvcRouteAttribute)}"); }
    if (string.IsNullOrEmpty(areaName))
    {
      pmrTemplate = "/[controller]/[action]";
      pmrName = $"{appName}:[controller]_[action]";
    }
    else
    {
      pmrTemplate = $"/{areaName}/[controller]/[action]";
      pmrName = $"{appName}:[area]_[controller]_[action]";
    }
    pmrOrder = routeOrder;
  }
  public PdpMvcRouteAttribute(string routeTemplate, bool withArea)
  {
    pmrTemplate = PatternTemplateWithArea(routeTemplate, withArea);
  }
  public PdpMvcRouteAttribute(string routeTemplate, string routeName, bool withArea)
  {
    pmrTemplate = PatternTemplateWithArea(routeTemplate, withArea);
    pmrName = routeName;
  }
  public PdpMvcRouteAttribute(string routeTemplate, string routeName, int routeOrder, bool withArea = false)
  {
    pmrTemplate = PatternTemplateWithArea(routeTemplate, withArea);
    pmrName = routeName;
    pmrOrder = routeOrder;
  }

  private string PatternTemplateWithArea(string routeTemplate, bool withArea)
  {
    var pmrPattern = ((withArea) ? $"{PdpConst.PdpMvcArea}/{routeTemplate}" : routeTemplate);
    if (string.IsNullOrEmpty(pmrPattern)) { pmrPattern = "/"; };
    return pmrPattern;
  }
  private string PatternAreaControllerAction(string areaName, string cntrlrName, string actionName)
  {
    if (string.IsNullOrEmpty(areaName)) { areaName = "{area}"; }
    if (string.IsNullOrEmpty(cntrlrName)) { cntrlrName = "{controller}"; }
    if (string.IsNullOrEmpty(actionName)) { actionName = "{action}"; }
    var pmrPattern = $"{areaName}/{cntrlrName}/{actionName}";
    return pmrPattern;
  }
  private string PatternControllerAction(string cntrlrName, string actionName)
  {
    if (string.IsNullOrEmpty(cntrlrName)) { cntrlrName = "{controller}"; }
    if (string.IsNullOrEmpty(actionName)) { actionName = "{action}"; }
    var pmrPattern = $"{cntrlrName}/{actionName}";
    return pmrPattern;
  }
  private void CreatePdpMvcRouteTemplate(string actionName, string cntrlrName, string routeTemplateSuffix, string routeNamePrefix, string routeNameSuffix, bool withArea)
  {
    // create route template
    var pmrAction = "";
    var pmrCntrlr = "";
    if (!string.IsNullOrEmpty(actionName)) { pmrAction = actionName.Replace(actionSymbol, "", StringComparison.CurrentCultureIgnoreCase); }
    if (!string.IsNullOrEmpty(cntrlrName)) { pmrCntrlr = cntrlrName.Replace(cntrlrSymbol, "", StringComparison.CurrentCultureIgnoreCase); }
    if (withArea) { pmrTemplate = PatternAreaControllerAction(PdpConst.PdpMvcArea, pmrCntrlr, pmrAction); }
    else { pmrTemplate = PatternControllerAction(pmrCntrlr, pmrAction); }
    if (!string.IsNullOrEmpty(routeTemplateSuffix)) { pmrTemplate = $"{pmrTemplate}/{routeTemplateSuffix}"; }
    // create route name
    if (string.IsNullOrWhiteSpace(routeNamePrefix)) { routeNamePrefix = ""; }
    if (string.IsNullOrWhiteSpace(routeNameSuffix)) { routeNameSuffix = ""; }
    if (!string.IsNullOrEmpty(cntrlrName))
    {
      pmrName = routeNamePrefix + pmrTemplate.Replace("/", "") + routeNameSuffix;
    }
    else if (!string.IsNullOrEmpty(actionName))
    {
      pmrName = routeNamePrefix + actionName + routeNameSuffix;
    }
    else { throw new ArgumentNullException($"invalid input args in {nameof(CreatePdpMvcRouteTemplate)}"); }
  }
  private const string actionSymbol = "action";
  private const string cntrlrSymbol = "controller";

  string? IRouteTemplateProvider.Template
  { get { return pmrTemplate; } }
  private string? pmrTemplate = null;

  string? IRouteTemplateProvider.Name
  { get { return pmrName; } }
  private string? pmrName = null;
  int? IRouteTemplateProvider.Order
  { get { return pmrOrder; } }
  private int? pmrOrder = null;

} // class
