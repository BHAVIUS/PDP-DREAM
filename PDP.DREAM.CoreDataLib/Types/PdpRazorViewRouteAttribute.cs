// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PdpRazorViewRouteAttribute : Attribute, IRouteTemplateProvider
{
  private string pdpRanpView = "PdpDreamView";
  private string pdpRanpApi = "PdpDreamApi";

  // for use with private fields, prefix acronym prr for PDP Razor Route 

  public PdpRazorViewRouteAttribute(string routeTemplateSuffix = "", string routeNamePrefix = "")
  {
    if (string.IsNullOrWhiteSpace(routeNamePrefix))
    { routeNamePrefix = pdpRanpView; }
    if (string.IsNullOrEmpty(DepNpdsFolder))
    {
      prrTemplate = "/[controller]/[action]";
      prrName = $"{routeNamePrefix}:[controller]_[action]";
    }
    else
    {
      prrTemplate = $"/{DepNpdsFolder}/[controller]/[action]";
      prrName = $"{routeNamePrefix}:{DepNpdsFolder}_[controller]_[action]";
    }
    if (!string.IsNullOrWhiteSpace(routeTemplateSuffix)) 
    { prrTemplate += $"/{routeTemplateSuffix}"; }
    prrOrder = DepRaoRazorView;
  }

  // intended for REST API routes
  public PdpRazorViewRouteAttribute(string routeTemplate, string routeName, bool withFolder)
  {
    var routeNamePrefix = pdpRanpApi;
    prrTemplate = routeTemplate;
    prrName = $"{routeNamePrefix}:{routeName}";
    prrOrder = DepRaoRestApi;
  }
  public PdpRazorViewRouteAttribute(string routeTemplate, string routeName, bool withFolder, int routeOrder)
  {
    prrTemplate = PatternTemplateWithDepFolder(routeTemplate, withFolder);
    prrName = routeName;
    prrOrder = routeOrder;
  }

  // DREAM EndPoint Folder = DepFolder
  private string PatternTemplateWithDepFolder(string routeTemplate, bool withDepFolder)
  {
    if (withDepFolder && string.IsNullOrEmpty(DepNpdsFolder))
    { throw new NullReferenceException("DepNpdsFolder is null or empty"); }
    var prrPattern = ((withDepFolder) ? $"/{DepNpdsFolder}/{routeTemplate}" : routeTemplate);
    if (string.IsNullOrEmpty(prrPattern)) { prrPattern = "/"; };
    return prrPattern;
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

} // end class

// end file