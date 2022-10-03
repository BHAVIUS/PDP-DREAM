// PrcContentForService.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Http;

namespace PDP.DREAM.CoreDataLib.Models;

// content for NPDS service
public partial class QebUserRestContext
{
  private HttpContext? npdsCntxt;
  public HttpContext? NpdsContext
  {
    set { npdsCntxt = value; }
    get { return npdsCntxt; }
  }

  public string? ServiceError { set; get; }
  public string? ServiceNote { set; get; }

}
