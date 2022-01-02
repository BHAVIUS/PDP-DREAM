// NetHttpClientExtensions.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Net.Http; // for HttpClient
using System.Threading.Tasks;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class NetHttpClientExtensions
{
  public static readonly HttpClient PdpHttpClient = new HttpClient();

  public static async Task<bool> UrlIsValidAsync(this string url)
  {
    bool isValid = false;
    try
    {
      HttpResponseMessage response = await PdpHttpClient.GetAsync(url);
      response.EnsureSuccessStatusCode();
      isValid = true;
    }
    catch (HttpRequestException ex) { }
    catch (Exception ex) { }
    return isValid;
  }

  public static bool UrlIsValid(this string url)
  {
    return UrlIsValidAsync(url).GetAwaiter().GetResult();
  }

}
