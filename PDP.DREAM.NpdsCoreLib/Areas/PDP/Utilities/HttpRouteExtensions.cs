// HttpRouteExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using Microsoft.AspNetCore.Routing;

// Credits: portions of code and/or ideas for code adapted from
//    Microsoft ASP.NET MVC 3 source,
//    http://haacked.com/archive/2008/07/14/make-routing-ignore-requests-for-a-file-extension.aspx
//    http://haacked.com/archive/2010/11/28/getting-the-route-name-for-a-route.aspx,
//    Chapter 24 by Palermo et al in Aspnet MVC 2 In Action.

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class HttpRouteExtensions
  {
    public const string RouteNameKey = "Name";
    public const string RouteHelpKey = "Help";
    public const string RouteExamplesKey = "Examples";
    public const string RouteNamespacesKey = "Namespaces";

    public static Route SetMvcRouteDataToken(this Route r, string dataTokenKey, object dataTokenValue)
    {
      if (r == null)
      {
        throw new ArgumentNullException("Route r is null");
      }
      if (r.DataTokens == null)
      {
        // DataTokens set cannot be accessed because protected
        // r.DataTokens = new RouteValueDictionary();
        throw new ArgumentNullException("Route Data Tokens are null");
      }
      if (dataTokenValue != null)
      {
        r.DataTokens[dataTokenKey] = dataTokenValue;
      }
      return r;
    }

    public static string GetMvcRouteDataTokenString(this Route r, string dataTokenKey)
    {
      if (r == null) { return string.Empty; }
      return r.DataTokens.GetMvcRouteDataTokenString(dataTokenKey);
    }
    public static string GetMvcRouteDataTokenString(this RouteData rData, string dataTokenKey)
    {
      if (rData == null) { return string.Empty; }
      return rData.DataTokens.GetMvcRouteDataTokenString(dataTokenKey);
    }
    public static string GetMvcRouteDataTokenString(this RouteValueDictionary rValues, string dataTokenKey)
    {
      if (rValues == null) { return string.Empty; }
      object dataTokenValue = null;
      rValues.TryGetValue(dataTokenKey, out dataTokenValue);
      if (dataTokenValue == null) { return string.Empty; }
      return dataTokenValue as string;
    }

    public static string[] GetMvcRouteDataTokenStringArray(this Route r, string dataTokenKey)
    {
      if (r == null) { return new string[0]; }
      return r.DataTokens.GetMvcRouteDataTokenStringArray(dataTokenKey);
    }
    public static string[] GetMvcRouteDataTokenStringArray(this RouteData rData, string dataTokenKey)
    {
      if (rData == null) { return new string[0]; }
      return rData.DataTokens.GetMvcRouteDataTokenStringArray(dataTokenKey);
    }
    public static string[] GetMvcRouteDataTokenStringArray(this RouteValueDictionary rValues, string dataTokenKey)
    {
      if (rValues == null) { return new string[0]; }
      object dataTokenValue = null;
      rValues.TryGetValue(dataTokenKey, out dataTokenValue);
      if (dataTokenValue == null) { return new string[0]; }
      return dataTokenValue as string[];
    }

    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url)
    //{
    //  return MvcMapRoute(routes, name, url, null /* defaults */, null /* constraints */, null /* namespaces */, null /* help */, null /* examples */);
    //}

    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url, object defaults)
    //{
    //  return MvcMapRoute(routes, name, url, defaults, null /* constraints */, null /* namespaces */, null /* help */, null /* examples */);
    //}

    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
    //{
    //  return MvcMapRoute(routes, name, url, defaults, constraints, null /* namespaces */, null /* help */, null /* examples */);
    //}
    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string help, string[] examples)
    //{
    //  return MvcMapRoute(routes, name, url, defaults, constraints, null /* namespaces */, help, examples);
    //}

    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url, string[] namespaces)
    //{
    //  return MvcMapRoute(routes, name, url, null /* defaults */, null /* constraints */, namespaces, null /* help */, null /* examples */);
    //}

    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
    //{
    //  return MvcMapRoute(routes, name, url, defaults, null /* constraints */, namespaces, null /* help */, null /* examples */);
    //}

    //public static Route MvcMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
    //{
    //  return MvcMapRoute(routes, name, url, defaults, constraints, namespaces, null /* help */, null /* examples */);
    //}

    //public static RouteCollection MvcMapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces, string help, string[] examples)
    //{
    //  if (routes == null) { throw new ArgumentNullException("routes"); }
    //  if (url == null) { throw new ArgumentNullException("url"); }

    ////  public Route(IRouter target, string routeName, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver);

    //  Route route = new Route(target,  name,  url,  defaults, constraints,  dataTokens,  inlineConstraintResolver);

    //  //Route route = new Route(url)
    //  //{
    //  //  Defaults = new RouteValueDictionary(defaults),
    //  //  Constraints = new RouteValueDictionary(constraints),
    //  //};

    //  //route.SetMvcRouteDataToken(RouteNameKey, name);
    //  //route.SetMvcRouteDataToken(RouteHelpKey, help);
    //  //route.SetMvcRouteDataToken(RouteExamplesKey, examples);
    //  //route.SetMvcRouteDataToken(RouteNamespacesKey, namespaces);

    //  routes.Add(route);
    //  return routes;
    //}

    // Microsoft.AspNetCore.Routing.Route inherits from abstract RouteBase
    //public Route(IRouter target, string routeTemplate, IInlineConstraintResolver inlineConstraintResolver);
    //public Route(IRouter target, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver);
    //public Route(IRouter target, string routeName, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver);
    // abstract RouteBase
    // public RouteBase(string template, string name, IInlineConstraintResolver constraintResolver, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens);


  }

}