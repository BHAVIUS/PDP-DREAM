// PdpRouteDebuggerMiddleware.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

// 2019 info on AspNetCore Routing
// https://www.tektutorialshub.com/asp-net-core/asp-net-core-routing/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Utilities;

public class PdpRouteDebuggerMiddleware
{
  private RequestDelegate nextDelegate;
  private IHttpContextAccessor contextAccessor;
  private EndpointDataSource endpointSource;
  private HttpContext prdContext;
  private RouteEndpoint prdRoute;
  private RouteData prdRouteData;
  private IList<IRouter> prdRouters;
  private List<RouteEndpoint> prdEndpoints;

  public PdpRouteDebuggerMiddleware(RequestDelegate theDelegate, IHttpContextAccessor theAccessor, EndpointDataSource theSource)
  {
    if (theDelegate == null) { throw new ArgumentNullException(nameof(theDelegate)); }
    nextDelegate = theDelegate;
    if (theAccessor == null) { throw new ArgumentNullException(nameof(theAccessor)); }
    contextAccessor = theAccessor;
    if (theSource == null) { throw new ArgumentNullException(nameof(theSource)); }
    endpointSource = theSource;

  }

  public async Task Invoke(HttpContext theContext)
  {
    // PRD HttpContext
    if (theContext == null) { theContext = contextAccessor.HttpContext; }
    prdContext = theContext;
    if (prdContext == null) { throw new NullReferenceException(nameof(prdContext)); }

    // PRD Route
    prdRoute = (RouteEndpoint)prdContext.GetEndpoint();
    if (prdRoute == null) { throw new NullReferenceException(nameof(prdRoute)); }

    // PRD RouteData
    prdRouteData = prdContext.GetRouteData();
    if (prdRouteData == null) { prdRouteData = contextAccessor.HttpContext.GetRouteData(); }
    if (prdRouteData == null) { throw new NullReferenceException(nameof(prdRouteData)); }

    // PRD List<RouteEndpoint>
    // prdEndpoints = endpointSource.Endpoints.Select(p => (p as RouteEndpoint)).OrderBy(p => p.Order).ToList();
    prdEndpoints = endpointSource.Endpoints.Cast<RouteEndpoint>().OrderBy(p => p.Order).ToList();

    // PRD IList<IRouter>
    prdRouters = prdRouteData.Routers;
    //if (prdRouters.Count == 0)
    //{
    //  prdRouters = prdContext.Features.Get<IRoutingFeature>().RouteData.Routers;
    //}

    var pageHtml = GetKqriHtmlBodyText();
    theContext.Response.ContentType = "text/html";
    await theContext.Response.WriteAsync(pageHtml);

    // only if continue to next middleware
    // await nextDelegate.Invoke(theContext);
  }

  public string GetKqriHtmlBodyText()
  {
    // kqri is Keyed Query Route Information
    string kqriQuery = prdContext.Request.QueryString.ToString();
    bool doHelp = IsKeyedQueryRoute(kqriQuery, PdpConst.PdpHelpRouteQueryKey);
    bool doDebug = IsKeyedQueryRoute(kqriQuery, PdpConst.PdpDebugRouteQueryKey);
    var kqriHtmlBodyText = new StringBuilder();

    if ((prdEndpoints.Count == 0) && (prdRouters.Count == 0))
    {
      kqriHtmlBodyText.AppendLine("<html><body><h1>PDP RouteDebugger Report</h1>");
      kqriHtmlBodyText.AppendLine("HTTP Requst Path: " + WebUtility.HtmlEncode(prdContext.Request.Path.ToString().Substring(1)) + "<br>");
      var referrer = prdContext.Request.Headers["referer"];
      if (!string.IsNullOrEmpty(referrer))
      {
        kqriHtmlBodyText.AppendLine("<a href=\"" + WebUtility.HtmlEncode(referrer) + "\">Retry " + WebUtility.HtmlEncode(referrer) + "</a><br>");
      }
      kqriHtmlBodyText.AppendLine("</body></html>");
    }
    else
    {
      // Header text
      kqriHtmlBodyText.Append(KqriHtmlHeader());
      // Debug text
      if (doDebug)
      {
        if (prdRouters.Count > 0) { kqriHtmlBodyText.Append(RouteDebugDiagnostics(prdRouters, prdRouteData)); }
        if (prdEndpoints.Count > 0) { kqriHtmlBodyText.Append(RouteDebugDiagnostics(prdEndpoints, prdRouteData)); }
      }
      // Help text
      if (doHelp)
      {
        if (prdRouters.Count > 0) { kqriHtmlBodyText.Append(RouteHelpInformation(prdRouters, doDebug)); }
      }
      // Footer text
      kqriHtmlBodyText.Append(KqriHtmlFooter());
    }
    return kqriHtmlBodyText.ToString();
  }

  private bool IsKeyedQueryRoute(string query, string key)
  {
    string rgxPattern = @"(.*)(\?|&)" + key + "(.*)";
    return Regex.IsMatch(query, rgxPattern, RegexOptions.IgnoreCase);
  }

  private const string linbrksep = " </br> "; // line break separator to demarcate values
  private static string FormatRouteValueDictionary(IDictionary<string, string> values, bool formatKey = true)
  {
    if (values == null || values.Count == 0) { return string.Empty; }
    var sb = new StringBuilder();
    foreach (string key in values.Keys)
    {
      var value = values[key];
      if (value.GetType() == typeof(string[]))
      {
        if (formatKey) { sb.AppendFormat("{0} = ", key); }
        sb.AppendFormat(FormatDataTokenValue(value, linbrksep));
      }
      else
      {
        if (formatKey) { sb.AppendFormat("{0} = {1}{2}", key, value, linbrksep); }
        else { sb.AppendFormat("{0}{1}", value, linbrksep); }
      }
    }
    if (sb.ToString().EndsWith(linbrksep)) { sb.Remove(sb.Length - linbrksep.Length, linbrksep.Length); }
    return sb.ToString();
  }
  private static string FormatRouteValueDictionary(IReadOnlyDictionary<string, object?> values, bool formatKey = true)
  {
    if (values == null || values.Count == 0) { return string.Empty; }
    var sb = new StringBuilder();
    foreach (string key in values.Keys)
    {
      var value = values[key];
      if (value?.GetType() == typeof(string[]))
      {
        if (formatKey) { sb.AppendFormat("{0} = ", key); }
        sb.AppendFormat(FormatDataTokenValue(value, linbrksep));
      }
      else
      {
        if (formatKey) { sb.AppendFormat("{0} = {1}{2}", key, value, linbrksep); }
        else { sb.AppendFormat("{0}{1}", value, linbrksep); }
      }
    }
    if (sb.ToString().EndsWith(linbrksep)) { sb.Remove(sb.Length - linbrksep.Length, linbrksep.Length); }
    return sb.ToString();
  }
  private static string FormatDataTokenValue(object dataTokenValue, string separator = "", string siteBaseUrl = "")
  {
    string dataTokenString = string.Empty;
    if (dataTokenValue != null)
    {
      if (dataTokenValue.GetType() == typeof(string[]))
      {
        var strValArray = (string[])dataTokenValue;
        if (string.IsNullOrEmpty(separator)) { separator = " "; };
        var sb = new StringBuilder();
        if (string.IsNullOrEmpty(siteBaseUrl))
        {
          foreach (string val in strValArray)
          {
            sb.Append(val + separator);
          }
        }
        else
        {
          foreach (string val in strValArray)
          {
            string urlVal = siteBaseUrl + val;
            sb.Append($"<a href='{urlVal}' target='_blank'>{urlVal}</a>{separator}");
          }
        }
        dataTokenString = sb.ToString();
      }
      else
      {
        dataTokenString = dataTokenValue.ToString();
        if (dataTokenString.EndsWith(PdpConst.PdpHelpRouteHackKey))
        {
          dataTokenString = dataTokenString.Remove(dataTokenString.Length - PdpConst.PdpHelpRouteHackKey.Length);
        }
      }
    }
    return dataTokenString;
  }

  // KQRI is Keyed Query Route Information

  private string KqriHtmlHeader()
  {
    return @"<html><head><style>
               body {font-family: Arial;}
               table {empty-cells: show; border: medium solid; margin: 11;}
               table th {background-color: #359; color: #fff; padding: 7;}
               table td {padding: 7; border: thin dotted;}
               </style></head><body><h1>PDP Route Help Debug Utility</h1>";
  }
  private string KqriHtmlFooter()
  {
    return @"<br/><br/><address>ASP.Net MVC URL Route Help Debug Utility &copy; 2011-2021 Carl Taswell
              for the <a href='https://www.portaldoors.org/'>PORTAL-DOORS Project</a>.
              </address></body></html>";
  }

  // TODO: write better docs for route help
  // use of HelpRouteHackKey suppresses display of route help except when debugging
  private string RouteHelpInformation(IList<IRouter> theRouters, bool allHelp = false)
  {
    string baseUrl = prdContext.Request.Host.ToString();
    var sb = new StringBuilder(string.Empty);
    sb.Append(@"<h1>Help Information:</h1><table>");
    sb.Append(@"<tr><th>Name</th><th>Route</th><th>Help</th><th>Examples</th></tr>");
    foreach (Route r in theRouters)
    {
      bool onlyHelpWhenDebug = false;
      string rHelp = r.GetMvcRouteDataTokenString(HttpRouteExtensions.RouteHelpKey);
      if (string.IsNullOrWhiteSpace(rHelp) || (rHelp.EndsWith(PdpConst.PdpHelpRouteHackKey)))
      {
        onlyHelpWhenDebug = true;
      }
      if (allHelp || !onlyHelpWhenDebug)
      {
        rHelp = FormatDataTokenValue(rHelp);
        string rUrl = r.ToString();
        // Microsoft's MVC should prevent nulls on Url so next line is just a failsafe alert
        if (string.IsNullOrEmpty(rUrl)) { rUrl = "<span style='color: red;'>NULL</span>"; };
        string rName = r.Name;
        string rExamples = FormatDataTokenValue(r.GetMvcRouteDataTokenStringArray(HttpRouteExtensions.RouteExamplesKey), linbrksep, baseUrl);
        sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", rName, rUrl, rHelp, rExamples);
      }
    }
    sb.Append(@"</table><div style='clear: both;'></div>");
    return sb.ToString();
  }

  private string RouteDebugDiagnostics(IList<IRouter> theRouters, RouteData theRouteData)
  {
    HttpRequest theRequest = prdContext.Request;
    var sb = new StringBuilder(string.Empty);

    string titFrm = @"<h1>Web Request:</h1><table><caption>{0}</caption>";
    sb.AppendFormat(titFrm, typeof(HttpRequest).FullName);
    string rowFrm = @"<tr><td>{0}</td><td>{1}</td></tr>";
    sb.AppendFormat(rowFrm, "Method", theRequest.Method);
    sb.AppendFormat(rowFrm, "Host", theRequest.Host);
    if (theRequest.PathBase != null) { sb.AppendFormat(rowFrm, "PathBase", theRequest.PathBase); }
    sb.AppendFormat(rowFrm, "Path", theRequest.Path);
    sb.AppendFormat(rowFrm, "QueryString", theRequest.QueryString);

    sb.Append(@"</table><h1>Debug Diagnostics:</h1><table>");
    string matchedColor = "Yellow";
    string unmatchedColor = "OrangeRed";
    string foundColor = "Lime";
    sb.AppendFormat(@"<caption>Unmatched routes displayed in <span style='background-color: {0};'>Orange Red</span>;
                        matched routes in <span style='background-color: {1};'>Lemon Yellow</span>;
                        found route in <span style='background-color: {2};'>Lime Green</span>.</caption>",
                     unmatchedColor, matchedColor, foundColor);
    sb.Append(@"<tr><th>Name</th><th>Route</th><th>Defaults</th><th>Constraints</th></tr>");

    bool routeFound = false;
    Route foundRoute = null;
    string foundRouteUrl = string.Empty;
    string foundRouteName = string.Empty;
    foreach (Route r in theRouters)
    {
      string rUrl = r.ToString(); // URL template for the route
      string rName = r.Name.ToString(); // name for the route
      IDictionary<string, string> defs = r.Defaults.ToDictionary();
      string rDefaults = FormatRouteValueDictionary(defs);
      IDictionary<string, string> cons = r.Constraints.ToDictionary();
      string rConstraints = FormatRouteValueDictionary(cons);
      bool rMatches = (r.DataTokens != null);
      string rColor = rMatches ? matchedColor : unmatchedColor;
      string rNameCell = string.Format("<td>&nbsp; {0} &nbsp;</td>", rName);

      if (rMatches && !routeFound)
      {
        routeFound = true;
        foundRoute = r;
        foundRouteUrl = rUrl;
        foundRouteName = rName;
        rColor = foundColor;
        rNameCell = string.Format("<td style='background-color: {0};'>&nbsp; &raquo;&raquo;&raquo; {1} &nbsp;</td>", foundColor, rName);
      }
      sb.AppendFormat(@"<tr>{0}<td style='background-color: {1};'>{2}</td><td>{3}</td><td>{4}</td></tr>",
          rNameCell, rColor, rUrl, rDefaults, rConstraints);
    }
    sb.AppendFormat(@"</table><h1>Found Route: {0} = {1}</h1>", foundRouteName, foundRouteUrl);

    sb.Append(@"<div style='float: left;'><table><caption>Route Parameters</caption><tr><th>Key</th><th>Value</th></tr>");
    foreach (var pair in theRouteData.Values)
    {
      sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td></tr>", pair.Key, pair.Value);
    }

    sb.Append(@"</table></div><div style='float: left;'><table><caption>Route DataTokens</caption><tr><th>Key</th><th>Value</th></tr>");
    foreach (var pair in theRouteData.DataTokens)
    {
      sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td></tr>", pair.Key, FormatDataTokenValue(pair.Value, linbrksep));
    }

    sb.Append(@"</table></div><div style='clear: both;'></div>");

    return sb.ToString();
  }

  private string RouteDebugDiagnostics(List<RouteEndpoint?>? theEndpoints, RouteData theRouteData)
  {
    HttpRequest theRequest = prdContext.Request;
    var sb = new StringBuilder(string.Empty);

    string titFrm = @"<h1>Web Request:</h1><table><caption>{0}</caption>";
    sb.AppendFormat(titFrm, typeof(HttpRequest).FullName);
    string rowFrm = @"<tr><td>{0}</td><td>{1}</td></tr>";
    sb.AppendFormat(rowFrm, "Method", theRequest.Method);
    sb.AppendFormat(rowFrm, "Host", theRequest.Host);
    if (theRequest.PathBase != null) { sb.AppendFormat(rowFrm, "PathBase", theRequest.PathBase); }
    sb.AppendFormat(rowFrm, "Path", theRequest.Path);
    sb.AppendFormat(rowFrm, "QueryString", theRequest.QueryString);

    sb.Append(@"</table><h1>Debug Diagnostics:</h1><table>");
    string matchedColor = "Yellow";
    string unmatchedColor = "OrangeRed";
    string foundColor = "Lime";
    sb.Append($"<caption>Unmatched routes displayed in <span style='background-color: {unmatchedColor};'>Orange Red</span>; matched routes in <span style= 'background-color: {matchedColor};'>Lemon Yellow</span>; found route in <span style= 'background-color: {foundColor};'>Lime Green</span>.</caption>");
    sb.Append(@"<tr><th>Order</th><th>Name</th><th>Route</th><th>Defaults</th><th>Constraints</th></tr>");

    bool routeFound = false;
    RouteEndpoint foundRoute = null;
    string foundRouteUrl = string.Empty;
    string foundRouteName = string.Empty;
    foreach (RouteEndpoint r in theEndpoints)
    {
      string rUrl = r.ToString(); // URL template for the route
      string rName = r.Metadata.GetMetadata<RouteNameMetadata>().RouteName;
      int rOrder = r.Order; // order for the route
      string rPattern = r.RoutePattern.RawText;
      // IDictionary<string, string> defs = r.Defaults.ToDictionary();
      IReadOnlyDictionary<string, object?> defs = r.RoutePattern.Defaults;
      string rDefaults = FormatRouteValueDictionary(defs);
      // IDictionary<string, string> cons = r.Constraints.ToDictionary();
      IReadOnlyDictionary<string, object?> cons = r.RoutePattern.RequiredValues;
      string rConstraints = FormatRouteValueDictionary(cons);
      // bool rMatches = false;
      bool rMatches = (rPattern == prdRoute.RoutePattern.RawText);
      string rColor = rMatches ? matchedColor : unmatchedColor;
      string rNameCell = $"<td>&nbsp; {rName} &nbsp;</td>";

      if (rMatches && !routeFound)
      {
        routeFound = true;
        foundRoute = r;
        foundRouteUrl = rUrl;
        foundRouteName = rName;
        rColor = foundColor;
        rNameCell = $"<td style='background-color: {foundColor};'>&nbsp; &raquo;&raquo;&raquo; {rName} &nbsp;</td>";
      }
      sb.Append($"<tr><td>{rOrder}</td>{rNameCell}<td style='background-color: {rColor};'>{rPattern}</td><td>{rDefaults}</td><td>{rConstraints}</td></tr>");
    }
    sb.Append($"</table><h1>Found Route: {foundRouteName} = {foundRouteUrl}</h1>");

    sb.Append(@"<div style='float: left;'><table><caption>Route Parameters</caption><tr><th>Key</th><th>Value</th></tr>");
    foreach (var pair in theRouteData.Values)
    {
      sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td></tr>", pair.Key, pair.Value);
    }

    sb.Append(@"</table></div><div style='float: left;'><table><caption>Route DataTokens</caption><tr><th>Key</th><th>Value</th></tr>");
    foreach (var pair in theRouteData.DataTokens)
    {
      sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td></tr>", pair.Key, FormatDataTokenValue(pair.Value, linbrksep));
    }

    sb.Append(@"</table></div><div style='clear: both;'></div>");

    return sb.ToString();
  }

}
