// PrcContentForResponse.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Text;

using Microsoft.AspNetCore.Http;

namespace PDP.DREAM.CoreDataLib.Models
{
  // content for NPDS response
  public partial class PdpRestContext
  {
    private HttpResponse? npdsRspns;
    public HttpResponse? NpdsResponse
    {
      set { npdsRspns = value; }
      get {  return npdsRspns; } 
    }

    public string? ResponseHeader { get; set; }

    public string? ResponseStatus { get; set; }

    public string? ResponseNote
    {
      set
      {
        var sb = new StringBuilder(ResponseNote);
        sb.AppendLine(value);
        respNote = sb.ToString();
      }
      get
      {
        if (string.IsNullOrEmpty(respNote))
        {
          if (!string.IsNullOrEmpty(ServiceNote)) { respNote = ServiceNote; };
        }
        return respNote;
      }
    }
    private string? respNote;

    public NpdsResrepList? ResponseAnswer { set; get; }
    public NpdsResrepList? ResponseRelated { set; get; }
    public NpdsResrepList? ResponseReferred { set; get; }

    public NpdsResrepList? CoreRecords { set; get; }
    public NpdsResrepList? PortalRecords { set; get; }
    public NpdsResrepList? DoorsRecords { set; get; }
    public NpdsResrepList? NexusRecords { set; get; }

  } // class

} // namespace
