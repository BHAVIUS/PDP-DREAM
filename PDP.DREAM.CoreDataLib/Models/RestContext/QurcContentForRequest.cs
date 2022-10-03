// PrcContentForRequest.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: eliminate redundancies in parsing of PRC properties and querystr properties
//   as found in file NxsSqlRepInitPrd method InitializePredicate()
//   and anywhere else that PRC and/or querystr properties are parsed

// content for NPDS request
// *Deflt is *Default on property names
// *Reqst is *Request on property names
public partial class QebUserRestContext
{
  private HttpRequest? npdsReqst;
  public HttpRequest? NpdsRequest
  {
    set {
      npdsReqst = value;
      if (npdsReqst != null)
      {
        NpdsReqstBaseUrl = $"{npdsReqst.Scheme}://{npdsReqst.Host}{npdsReqst.PathBase}";
        NpdsReqstDisplayUrl = npdsReqst.GetDisplayUrl();
        NpdsReqstEncodedUrl = npdsReqst.GetEncodedUrl();
        NpdsReqstHost = npdsReqst.Host;
        NpdsReqstPath = npdsReqst.Path;
        NpdsReqstPathBase = npdsReqst.PathBase;
        NpdsReqstQuery = npdsReqst.Query;
        NpdsReqstQueryString = npdsReqst.QueryString;
        NpdsReqstScheme = npdsReqst.Scheme;
      }
    }
    get { return npdsReqst; }
  }

  // TODO: re-eval (and refactor/recode?) properties for Net 6
  public string? NpdsReqstBaseUrl { get; set; }
  public string? NpdsReqstDisplayUrl { get; set; }
  public string? NpdsReqstEncodedUrl { get; set; }
  public HostString? NpdsReqstHost { get; set; }
  public string? NpdsReqstPath { get; set; }
  public string? NpdsReqstPathBase { get; set; }
  public IQueryCollection? NpdsReqstQuery { get; set; }
  public QueryString? NpdsReqstQueryString { get; set; }
  public string? NpdsReqstScheme { get; set; }
  // TODO: relocate in a core folder such as /nsvo/ or /npds/
  public string? NpdsReqstXmlSchemaUrl { get { return NpdsReqstBaseUrl + "/pub/xsd/"; } }

  // consider use for main body on POST/PUT and/or for complicated GETs with SPARQL queries
  // but for latter then need to re-implement as XElement instead of String
  public string? RequestNote { get; set; }
  public string? RequestQuestion { get; set; }


  // TODO: move to PrcEntityName in Models/RestContext
  public string? EntityNameReqst { get; set; }
  // TODO: move to PrcEntityNature in Models/RestContext
  public string? EntityNatureReqst { get; set; }
  // QueryString Key Values for search parameters
  public string? QskLexLabAny { get; set; }
  public string? QskLexLabCan { get; set; }
  public string? QskLexLabAls { get; set; }
  public string? QskLexLabSup { get; set; }
  public string? QskLexTagAny { get; set; }
  public string? QskLexTagSup { get; set; }
  public string? QskLexOText { get; set; }

  // TODO: create analogous method for parsing route parameter values into strongly typed values
  public void ParseQueryCollection(IQueryCollection? qryStrCol)
  {
    int count = (qryStrCol?.Count ?? 0);
    if (count == 0) // default to PdpReqstQuery
    {
      qryStrCol = NpdsReqstQuery;
      count = (qryStrCol?.Count ?? 0);
    }
    if (count > 0)
    {
      var qsKeys = qryStrCol.Keys;
      string qsName; bool qsValid; StringValues qsValue;
      foreach (string key in qsKeys)
      {
        qsName = key.ToLower();
        qsValid = qryStrCol.TryGetValue(key, out qsValue);
        switch (qsName)
        {
          // NPDS keys
          case PdpAppConst.QskDiristryTag:
            DiristryTagReqst = qsValue;
            break;
          case PdpAppConst.QskRegistryTag:
            RegistryTagReqst = qsValue;
            break;
          case PdpAppConst.QskDirectoryTag:
            DirectoryTagReqst = qsValue;
            break;
          case PdpAppConst.QskRegistrarTag:
            RegistrarGuidReqst = qsValue;
            break;
          case PdpAppConst.QskDiristryGuid:
            DiristryGuidReqst = qsValue;
            break;
          case PdpAppConst.QskRegistryGuid:
            RegistryGuidReqst = qsValue;
            break;
          case PdpAppConst.QskDirectoryGuid:
            DirectoryGuidReqst = qsValue;
            break;
          case PdpAppConst.QskRegistrarGuid:
            RegistrarTagReqst = qsValue;
            break;
          // >=5 character keys
          case PdpAppConst.QskLexLabAny:
            QskLexLabAny = qsValue;
            break;
          case PdpAppConst.QskLexLabCan:
            QskLexLabCan = qsValue;
            break;
          case PdpAppConst.QskLexLabAls:
            QskLexLabAls = qsValue;
            break;
          case PdpAppConst.QskLexLabSup:
            QskLexLabSup = qsValue;
            break;
          case PdpAppConst.QskLexTagAny:
            QskLexTagAny = qsValue;
            break;
          case PdpAppConst.QskLexTagSup:
            QskLexTagSup = qsValue;
            break;
          case PdpAppConst.QskLexOText:
            QskLexOText = qsValue;
            break;
          // 4-character keys
          case PdpAppConst.QskServiceType:
            ServiceTypeReqst = qsValue;
            break;
          case PdpAppConst.QskServiceTag:
            ServiceTagReqst = qsValue;
            break;
          case PdpAppConst.QskEntityType:
            EntityTypeReqst = qsValue;
            break;
          case PdpAppConst.QskEntityTag:
            EntityTagReqst = qsValue;
            break;
          case PdpAppConst.QskEntityName:
            EntityNameReqst = qsValue;
            break;
          case PdpAppConst.QskEntityNature:
            EntityNatureReqst = qsValue;
            break;
          // 3-character keys
          case PdpAppConst.QskUserKey:
            QebUserKey = qsValue;
            break;
          case PdpAppConst.QskAgentKey:
            QebAgentKey = qsValue;
            break;
          case PdpAppConst.QskSessionKey:
            QebSessionKey = qsValue;
            break;
          case PdpAppConst.QskRecordAccess:
            RecordAccessReqst = qsValue;
            break;
          case PdpAppConst.QskUserName:
            QebUserName = qsValue;
            break;
          case PdpAppConst.QskPassWord:
            QebPassWord = qsValue;
            break;
          // 2-character keys
          case PdpAppConst.QskArchiveFormat:
            ArchiveFormatReqst = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskCheckFormat:
            CheckFormatReqst = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskEchoFormat:
            EchoFormatReqst = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskListCount:
            ListCountReqst = PdpQueryString.ParsePosInt(qsValue, PdpAppConst.ListCountDeflt);
            break;
          case PdpAppConst.QskMessageFormat:
            MessageFormatReqst = qsValue;
            break;
          case PdpAppConst.QskQueryFormat:
            QueryFormatReqst = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskResrepFormat:
            ResrepFormatReqst = qsValue;
            break;
          case PdpAppConst.QskVerboseFormat:
            VerboseFormatReqst = PdpQueryString.ParseFlag(qsValue);
            break;
          default:
            break;
        }
      }
    }
  }

} // end class

// end file