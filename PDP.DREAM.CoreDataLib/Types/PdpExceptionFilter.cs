// PdpExceptionFilter.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Types;

// Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter implements
// Microsoft.AspNetCore.Mvc.Filters.IFilterMetadata

public class PdpExceptionFilter : IExceptionFilter
{
  private readonly IHostEnvironment pdwEnvir;
  public PdpExceptionFilter(IHostEnvironment envir) { pdwEnvir = envir; }

  public void OnException(ExceptionContext cntxt)
  {
    if (!pdwEnvir.IsDevelopment()) { return; }

    cntxt.Result = new ContentResult
    {
      Content = cntxt.Exception.ToString()
    };
  }

}
