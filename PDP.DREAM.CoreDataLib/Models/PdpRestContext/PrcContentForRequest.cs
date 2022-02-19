// PrcContentForRequest.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
{
  // TODO: eliminate redundancies in parsing of PRC properties and querystr properties
  //   as found in file NxsSqlRepInitPrd method InitializePredicate()
  //   and anywhere else that PRC and/or querystr properties are parsed

  // content for NPDS request
  // *Deflt is *Default on property names
  // *Reqst is *Request on property names
  public partial class PdpRestContext
  {
    private HttpRequest? npdsReqst;
    public HttpRequest? NpdsRequest
    {
      set
      {
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


    public bool ClientIsUser { get; set; } = false;

    public bool ClientHasUserAccess
    { get { return (ClientInUserModeIsRequired && ClientIsUser); } }

    public bool ClientIsAgent { get; set; } = false;

    public bool ClientHasAgentAccess
    { get { return (ClientInAgentModeIsRequired && ClientIsAgent); } }

    public bool ClientIsAuthor { get; set; } = false;

    public bool ClientHasAuthorAccess
    { get { return (ClientInAuthorModeIsRequired && ClientIsAuthor); } }

    public bool ClientIsEditor { get; set; } = false;

    public bool ClientHasEditorAccess
    { get { return (ClientInEditorModeIsRequired && ClientIsEditor); } }

    public bool ClientIsAdmin { get; set; } = false;

    public bool ClientHasAdminAccess
    { get { return (ClientInAdminModeIsRequired && ClientIsAdmin); } }

    public bool ClientHasEditorOrAdminAccess
    { get { return (ClientHasEditorAccess || ClientHasAdminAccess); } }

    public bool ClientHasAuthorOrEditorAccess
    { get { return (ClientHasAuthorAccess || ClientHasEditorAccess); } }

    public bool ClientHasAuthorEditorOrAdminAccess
    { get { return (ClientHasAuthorAccess || ClientHasEditorAccess || ClientHasAdminAccess); } }

    public bool AuthorizedClientIsRequired
    {
      get
      {
        return (ClientInUserModeIsRequired || ClientInAgentModeIsRequired ||
          ClientInAuthorModeIsRequired || ClientInEditorModeIsRequired || ClientInAdminModeIsRequired);
      }
    }

    public bool ClientInUserModeIsRequired { get; set; } = false;
    public bool ClientInAgentModeIsRequired { get; set; } = false;
    public bool ClientInAuthorModeIsRequired { get; set; } = false;
    public bool ClientInEditorModeIsRequired { get; set; } = false;
    public bool ClientInAdminModeIsRequired { get; set; } = false;
    public bool ClientIsAuthenticated { get; set; } = false;

    public bool ClientIsAuthorized
    {
      get
      {
        switch (RecordAccess)
        {
          case NpdsConst.RecordAccess.Admin:
            return ClientHasAdminAccess;
          case NpdsConst.RecordAccess.Editor:
            return ClientHasEditorAccess;
          case NpdsConst.RecordAccess.Author:
            return ClientHasAuthorAccess;
          case NpdsConst.RecordAccess.Agent:
            return ClientHasAgentAccess;
          case NpdsConst.RecordAccess.User:
            return ClientHasUserAccess;
          default:
            return false;
        }
      }
    }

    public bool ClientIsVerified
    {
      get { return (ClientIsAuthenticated && ClientIsAuthorized); }
    }

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
            case NpdsConst.QskDiristryTag:
              DiristryTagReqst = qsValue;
              break;
            case NpdsConst.QskRegistryTag:
              RegistryTagReqst = qsValue;
              break;
            case NpdsConst.QskDirectoryTag:
              DirectoryTagReqst = qsValue;
              break;
            case NpdsConst.QskRegistrarTag:
              RegistrarGuidReqst = qsValue;
              break;
            case NpdsConst.QskDiristryGuid:
              DiristryGuidReqst = qsValue;
              break;
            case NpdsConst.QskRegistryGuid:
              RegistryGuidReqst = qsValue;
              break;
            case NpdsConst.QskDirectoryGuid:
              DirectoryGuidReqst = qsValue;
              break;
            case NpdsConst.QskRegistrarGuid:
              RegistrarTagReqst = qsValue;
              break;
            // >=5 character keys
            case NpdsConst.QskLexLabAny:
              QskLexLabAny = qsValue;
              break;
            case NpdsConst.QskLexLabCan:
              QskLexLabCan = qsValue;
              break;
            case NpdsConst.QskLexLabAls:
              QskLexLabAls = qsValue;
              break;
            case NpdsConst.QskLexLabSup:
              QskLexLabSup = qsValue;
              break;
            case NpdsConst.QskLexTagAny:
              QskLexTagAny = qsValue;
              break;
            case NpdsConst.QskLexTagSup:
              QskLexTagSup = qsValue;
              break;
            case NpdsConst.QskLexOText:
              QskLexOText = qsValue;
              break;
            // 4-character keys
            case NpdsConst.QskServiceType:
              ServiceTypeReqst = qsValue;
              break;
            case NpdsConst.QskServiceTag:
              ServiceTagReqst = qsValue;
              break;
            case NpdsConst.QskEntityType:
              EntityTypeReqst = qsValue;
              break;
            case NpdsConst.QskEntityTag:
              EntityTagReqst = qsValue;
              break;
            case NpdsConst.QskEntityName:
              EntityNameReqst = qsValue;
              break;
            case NpdsConst.QskEntityNature:
              EntityNatureReqst = qsValue;
              break;
            // 3-character keys
            case NpdsConst.QskUserKey:
              UserKey = qsValue;
              break;
            case NpdsConst.QskAgentKey:
              AgentKey = qsValue;
              break;
            case NpdsConst.QskSessionKey:
              SessionKey = qsValue;
              break;
            case NpdsConst.QskRecordAccess:
              RecordAccessReqst = qsValue;
              break;
            case NpdsConst.QskUserName:
              UserName = qsValue;
              break;
            case NpdsConst.QskPassWord:
              PassWord = qsValue;
              break;
            // 2-character keys
            case NpdsConst.QskArchiveFormat:
              ArchiveFormatReqst = PdpQueryString.ParseFlag(qsValue);
              break;
            case NpdsConst.QskCheckFormat:
              CheckFormatReqst = PdpQueryString.ParseFlag(qsValue);
              break;
            case NpdsConst.QskEchoFormat:
              EchoFormatReqst = PdpQueryString.ParseFlag(qsValue);
              break;
            case NpdsConst.QskListCount:
              ListCountReqst = PdpQueryString.ParsePosInt(qsValue, NpdsConst.ListCountDeflt);
              break;
            case NpdsConst.QskMessageFormat:
              MessageFormatReqst = qsValue;
              break;
            case NpdsConst.QskQueryFormat:
              QueryFormatReqst = PdpQueryString.ParseFlag(qsValue);
              break;
            case NpdsConst.QskResrepFormat:
              ResrepFormatReqst = qsValue;
              break;
            case NpdsConst.QskVerboseFormat:
              VerboseFormatReqst = PdpQueryString.ParseFlag(qsValue);
              break;
            default:
              break;
          }
        }
      }
    }

  }

}
