using System;
using System.Net.Http; // for HttpClient
using System.Threading.Tasks;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class HttpClientExtensions
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

}
