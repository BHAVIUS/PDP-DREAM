using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public static class HttpJsonRequests
  {
    private const string jsonMediaType = "application/json";
    private static readonly HttpClientHandler hcHandlerEtol; // error tolerant handler
    private static readonly HttpClient httpClientEtol; // error tolerant client
    private static readonly HttpClient httpClient;

    static HttpJsonRequests()
    {
      hcHandlerEtol = new HttpClientHandler()
      { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true };
      httpClientEtol = new HttpClient(hcHandlerEtol);
      httpClient = new HttpClient();
    }

    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/july/async-programming-brownfield-async-development
    // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to
    // https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json?view=net-5.0
    public static string? GetJsonString(string strUrl, bool ignoreSslError = false)
    {
      var jsonString = GetJsonStringAsync(strUrl, ignoreSslError).GetAwaiter().GetResult();
      return jsonString;
    }
    public static async Task<string?> GetJsonStringAsync(string strUrl, bool ignoreSslError = false)
    {
      HttpClient hc;
      if (ignoreSslError) { hc = httpClientEtol; }
      else { hc = httpClient; }
      hc.DefaultRequestHeaders.Accept.Clear();
      hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));

      string? strJson;
      try { strJson = await hc.GetStringAsync(strUrl); }
      catch (HttpRequestException) { strJson = null; }
      return strJson;
    }

  }

}
