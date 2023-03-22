// PdpSiteHttpCntxtAccess.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public class PdpSiteHttpCntxtAccess
{
  protected readonly IHttpContextAccessor hcAccess;
  PdpSiteHttpCntxtAccess(IHttpContextAccessor hca)
  {
    hcAccess = hca;
  }

  public HttpContext PdpSiteContext
  {
    get { return hcAccess.HttpContext; }
  }

  public HttpRequest PdpSiteRequest
  {
    get { return hcAccess.HttpContext.Request; }
  }

  public HttpResponse PdpSiteResponse
  {
    get { return hcAccess.HttpContext.Response; }
  }

} // end class

// end file