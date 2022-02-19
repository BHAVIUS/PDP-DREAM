// PdpExceptionFilter.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace PDP.DREAM.CoreDataLib.Controllers;

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
