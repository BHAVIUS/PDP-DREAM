﻿using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PDP.DREAM.NpdsCoreLib.Utilities
{
  public class ApiKeyMessageHandler : DelegatingHandler
  {
    public ApiKeyMessageHandler()
    {
      this.ApiKey = Guid.NewGuid().ToString();
    }
    public ApiKeyMessageHandler(string key)
    {
      this.ApiKey = key;
    }

    public string ApiKey { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
      if (!ValidateKey(request))
      {
        var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
        var tsc = new TaskCompletionSource<HttpResponseMessage>();
        tsc.SetResult(response);
        return tsc.Task;
      }
      return base.SendAsync(request, token);
    }

    private bool ValidateKey(HttpRequestMessage message)
    {
      string key = "";
      NameValueCollection qkeys = message.GetQueryKeys();
      if (qkeys.HasKeys())
      {
        try { key = qkeys.Get("apikey").ToUpper(); }
        catch { key = ""; }
      }
      bool keyIsValid = (key == ApiKey);
      return keyIsValid;
    }

  }

}