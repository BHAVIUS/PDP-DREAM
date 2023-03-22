// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PdpRazorPageRouteAttribute : Attribute, IRouteTemplateProvider
{
  // for use with private fields, prefix acronym prr for Pdp Razor Route 

  public PdpRazorPageRouteAttribute(string? routeNamePrefix, string? routeTemplate, string? routeTemplateSuffix = "", int routeOrder = 0)
  {
    // template for route endpoint
    if (string.IsNullOrWhiteSpace(routeNamePrefix))
    { routeNamePrefix = DepRanRazorPage; }
    if (string.IsNullOrWhiteSpace(routeTemplate))
    { routeTemplate = string.Empty; }
    prrTemplate = routeTemplate;
    var revTemplate = prrTemplate.Replace("/", "_");
    if (!string.IsNullOrWhiteSpace(routeTemplateSuffix))
    { prrTemplate += $"/{routeTemplateSuffix}"; }
    // name for route endpoint
    if (string.IsNullOrWhiteSpace(revTemplate))
    { prrName = routeNamePrefix; }
    else
    { prrName = $"{routeNamePrefix}:{revTemplate}"; }
    // order for route endpoint
    if (routeOrder == 0)
    { routeOrder = DepRaoRazorPage; }
    prrOrder = routeOrder;
  }

  string? IRouteTemplateProvider.Template
  { get { return prrTemplate; } }
  private string? prrTemplate = null;

  string? IRouteTemplateProvider.Name
  { get { return prrName; } }
  private string? prrName = null;
  int? IRouteTemplateProvider.Order
  { get { return prrOrder; } }
  private int? prrOrder = null;

}  // end class

// end file