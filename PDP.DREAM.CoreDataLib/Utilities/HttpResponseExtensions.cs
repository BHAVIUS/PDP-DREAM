// HttpResponseExtensions.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Net;

using Microsoft.AspNetCore.Http; // for HttpContext
// Microsoft.AspNetCore.Http.HttpContext has both HttpRequest and HttpResponse

namespace PDP.DREAM.CoreDataLib.Utilities
{
  public static class HttpResponseExtensions
  {
    public static string ResponseStatus(HttpContext webCont, HttpStatusCode statCode)
    {
      if (webCont == null)
      {
        return string.Empty;
      }
      HttpResponse webResp = webCont.Response;
      if (webResp == null)
      {
        return string.Empty;
      }
      webResp.StatusCode = (int)statCode;
      // TODO: create method in class PdpEnum to return Enum.GetName of current Enum value
      return string.Format("{0}  {1}", webResp.StatusCode.ToString(), statCode.ToString());
    }

    public static void SetBadRequestResponse(HttpContext webCont)
    {
      HttpResponse webResp = webCont.Response;
      // System.Net.HttpStatusCode
      webResp.StatusCode = (int) HttpStatusCode.BadRequest;
    }
    //public static void SetNotFoundResponse()
    //{
    //  HttpResponse webResp = System.Web.HttpContext.Current.Response;
    //  webResp.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
    //}
    public static void SetInternalErrorResponse(HttpContext webCont)
    {
      HttpResponse webResp = webCont.Response;
      // System.Net.HttpStatusCode
      webResp.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
    //public static void WcfSetInternalErrorResponse()
    //{
    //  WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
    //  WebOperationContext.Current.OutgoingResponse.SuppressEntityBody = true;
    //}

    //public static void SendResponseMessage(string msg)
    //{
    //  System.Web.HttpContext wc = HttpContext.Current;
    //  wc.Response.Write(msg); // appears to reset any response headers set by WebOperationContext !!!!
    //}

    //public static void SetResponseHeader()
    //{
    //  throw new NotImplementedException();

    //  //System.ServiceModel.Web.WebOperationContext woc = WebOperationContext.Current;
    //  //woc.OutgoingResponse.ContentType = "text/xml"; // this works
    //  //woc.OutgoingResponse.Headers.Remove(System.Net.HttpResponseHeader.SetCookie); // this does NOT work
    //  //woc.OutgoingResponse.LastModified = DateTime.Now; // this works
    //}

    //public static void SetCookieHeader(string strCookie = null)
    //{
    //  throw new NotImplementedException();

    //  // TODO: next code line does not work;
    //  // WebOperationContext.Current.OutgoingResponse.Headers.Item(Net.HttpResponseHeader.SetCookie) = strCookie
    //  // debugging confirms strCookie is Nothing
    //  // but Set-Cookie set to empty string anyway, and then ASP.NET WCF service further outputs
    //  // the following header:Set-Cookie: ASP.NET_SessionId=embolkesbbhjm1npgjafr0eo; path=/; HttpOnly

    //  // TODO: next code line does not work
    //  // WebOperationContext.Current.OutgoingResponse.Headers.Remove(System.Net.HttpResponseHeader.SetCookie);
    //  // debugging confirms that SetCookie header is nothing but then ASP.NET WCF service later outputs
    //  // the following header Set-Cookie: ASP.NET_SessionId=cujivrq4y2ljwceweu44zpas; path=/; HttpOnly

    //}

  } // class

} // namespace
