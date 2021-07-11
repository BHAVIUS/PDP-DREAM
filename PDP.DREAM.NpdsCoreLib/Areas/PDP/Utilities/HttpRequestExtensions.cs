// HttpRequestExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Collections.Specialized;
using System.Net.Http; // for HttpRequestMessage
// Microsoft.AspNetCore.Http.HttpContext has both HttpRequest and HttpResponse

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class HttpRequestExtensions
  {
    public static string UrlFull(this HttpRequestMessage req)
    {
      return req.RequestUri.ToString();
    }

    public static string UrlMain(this HttpRequestMessage req)
    {
      var urlMain = string.Empty;
      var reqUri = req.RequestUri;
      if (reqUri != null) { urlMain = RemoveQuery(reqUri); }
      return urlMain;
    }

    public static string UrlBase(this HttpRequestMessage req)
    {
      var urlBase = string.Empty;
      var reqUri = req.RequestUri;
      if (reqUri != null) { urlBase = RemovePathAndQuery(reqUri); }
      return urlBase;
    }

    public static NameValueCollection GetQueryKeys(this HttpRequestMessage req)
    {
      return req.GetQueryKeys();
    }


    // http://stackoverflow.com/questions/2019735/request-rawurl-vs-request-url

    public static string RemoveQuery(Uri url)
    {
      return RemoveQuery(url.ToString());
    }
    public static string RemoveQuery(string url)
    {
      // remove querystring if present
      int idx = url.IndexOf("?");
      if (idx > -1) { url = url.Remove(idx); }
      return url;
    }

    public static string RemovePathAndQuery(Uri url)
    {
      string paq = url.PathAndQuery;
      return url.ToString().Replace(paq, string.Empty);
    }
    public static string AddForwardSlash(string url)
    {
      if (url.EndsWith("/")) { return url; }
      else { return (url + "/"); }
    }

  }

}
