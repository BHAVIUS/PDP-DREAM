// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class QebHttpClientExtensions
{
  public static readonly HttpClient QebHttpClient = new HttpClient();

  public static async Task<bool> UrlIsValidAsync(this string url)
  {
    bool isValid = false;
    try
    {
      HttpResponseMessage response = await QebHttpClient.GetAsync(url);
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
