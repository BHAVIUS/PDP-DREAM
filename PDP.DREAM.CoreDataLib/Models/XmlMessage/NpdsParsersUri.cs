// NpdsParsersUri.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class NpdsParsers
{
  public static Uri ToUri(this string strUri)
  {
    return ParseUri(strUri);
  }

  public static Uri ParseUri(string strUri)
  {
    Uri objUri = null;
    if ((!(string.IsNullOrEmpty(strUri))) && Uri.IsWellFormedUriString(strUri, UriKind.Absolute))
    {
      objUri = new Uri(strUri, UriKind.Absolute);
    }
    else
    {
      objUri = new Uri("invalid.uri");
    }
    return objUri;
  }

  public static Uri ParseUrl(string strUrl)
  {
    Uri objUrl = null;
    if (!(string.IsNullOrEmpty(strUrl)) && (Uri.IsWellFormedUriString(strUrl, UriKind.Absolute)))
    {
      if (strUrl.Substring(0, 3).ToLower() == "www")
      {
        strUrl = "http://" + strUrl;
      }
      objUrl = new Uri(strUrl, UriKind.Absolute);
    }
    else
    {
      objUrl = new Uri("http://invalid.url", UriKind.Absolute);
    }
    return objUrl;
  }

  public static Uri ParseLabel(string strLabel)
  {
    Uri uriLabel;
    if ((!(string.IsNullOrEmpty(strLabel))) && Uri.IsWellFormedUriString(strLabel, UriKind.Absolute))
    {
      uriLabel = new Uri(strLabel, UriKind.Absolute);
    }
    else
    {
      uriLabel = new Uri("http://invalid.uri", UriKind.Absolute);
    }
    return uriLabel;
  }

  public static Uri ParseLabel(string strNameSpace, string strPrincipalTag)
  {
    return ParseLabel(strNameSpace + strPrincipalTag);
  }

  public static Uri ParseLabel(Uri uriNameSpace, string strPrincipalTag)
  {
    return ParseLabel(uriNameSpace.ToString() + strPrincipalTag);
  }

} // class
