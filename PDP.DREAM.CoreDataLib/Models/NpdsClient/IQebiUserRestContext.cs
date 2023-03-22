// IQebUserRestContext.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// IQebiUserRestContext : INpdsClient : IQebiClient
public interface IQebiUserRestContext : INpdsClient
{
  HttpContext? NpdsContext { get; set; }
  string? NpdsReqstBaseUrl { get; set; }
  string? NpdsReqstDisplayUrl { get; set; }
  string? NpdsReqstEncodedUrl { get; set; }
  HostString? NpdsReqstHost { get; set; }
  string? NpdsReqstPath { get; set; }
  string? NpdsReqstPathBase { get; set; }
  IQueryCollection? NpdsReqstQuery { get; set; }
  QueryString? NpdsReqstQueryString { get; set; }
  string? NpdsReqstScheme { get; set; }
  string? NpdsReqstXmlSchemaUrl { get; }
  HttpRequest? NpdsRequest { get; set; }
  HttpResponse? NpdsResponse { get; set; }

  string ParseNpdsSelectFilterForPage(string serviceType, string searchFilter, string serviceTag, string entityType, string recordAccess = "", string pageTitle = "");

  // TODO: update for views
  string ParseNpdsSelectFilterForView(string serviceType, string serviceTag, string entityType, string action = "", string title = "");
  void ParseNpdsService(string serviceType, string searchFilter, string serviceTag, string entityTag = "", string entityVersion = "", string entityType = "", string infosetStatus = "", string recordAccess = "");
  void ParseNpdsQueryString(IQueryCollection? qryStrCol);

} // end interface

// end file