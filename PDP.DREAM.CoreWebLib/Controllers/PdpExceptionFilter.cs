// PdpExceptionFilter.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreWebLib.Controllers;

// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters
// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.filters.iexceptionfilter

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
