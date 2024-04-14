// NpdsParsersUri.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class NpdsParsers
{
  public static Uri ToUri(this string strUri)
  {
    return ParseUri(strUri);
  }

  public static Uri? ParseUri(string? strUri)
  {
    Uri? objUri = null;
    if ((!(string.IsNullOrEmpty(strUri))) && Uri.IsWellFormedUriString(strUri, UriKind.Absolute))
    {
      objUri = new Uri(strUri, UriKind.Absolute);
    }
    else
    {
      objUri = new Uri("http://invalid.uri", UriKind.Absolute);
    }
    return objUri;
  }
  public static Uri? ParseUrl(string? strUrl)
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

  public static Uri? ParseEntityLabel(string? strEntityLabel)
  {
    return ParseUri(strEntityLabel);
  }
  public static Uri? ParseEntityLabel(string? strNameSpace, string? strPrincipalTag)
  {
    return ParseUri(strNameSpace + strPrincipalTag);
  }
  public static Uri? ParseEntityLabel(Uri? uriNameSpace, string? strPrincipalTag)
  {
    return ParseUri(uriNameSpace.ToString() + strPrincipalTag);
  }

} // class

// end file