// PrcContentForService.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Http;

namespace PDP.DREAM.CoreDataLib.Models;

// content for NPDS service
public partial class PdpRestContext
{
  // NPDS Service Defaults (NPDSSD)
  private NpdsServiceDefaults npdsSrvcDefs = NpdsServiceDefaults.Values;
  public NpdsServiceDefaults NPDSSD { get { return npdsSrvcDefs; } }

  private HttpContext? npdsCntxt;
  public HttpContext? NpdsContext
  {
    set { npdsCntxt = value; }
    get { return npdsCntxt; }
  }

  public string? ServiceError { set; get; }

  public string? ServiceNote { set; get; }

}
