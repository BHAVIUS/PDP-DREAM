// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // QueryString Key Values for search parameters
  // TODO: refactor to a dictionary of strings
  public string? LnqLexLabAny { get; set; } = ESS;
  public string? LnqLexLabCan { get; set; } = ESS;
  public string? LnqLexLabAls { get; set; } = ESS;
  public string? LnqLexLabSup { get; set; } = ESS;
  public string? LnqLexTagAny { get; set; } = ESS;
  public string? LnqLexTagSup { get; set; } = ESS;
  public string? LnqLexOText { get; set; } = ESS;

  // TODO: create analogous method for parsing route parameter values into strongly typed values
  public void ParseNpdsQueryString(IQueryCollection? qryStrCol)
  {
    int count = (qryStrCol?.Count ?? 0);
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
          case QskDiristryTag:
            DiristryTagReqst = qsValue;
            break;
          case QskRegistryTag:
            RegistryTagReqst = qsValue;
            break;
          case QskDirectoryTag:
            DirectoryTagReqst = qsValue;
            break;
          case QskRegistrarTag:
            RegistrarGuidReqst = qsValue;
            break;
          case QskDiristryGuid:
            DiristryGuidReqst = qsValue;
            break;
          case QskRegistryGuid:
            RegistryGuidReqst = qsValue;
            break;
          case QskDirectoryGuid:
            DirectoryGuidReqst = qsValue;
            break;
          case QskRegistrarGuid:
            RegistrarTagReqst = qsValue;
            break;
          // >=5 character keys
          case QskLexLabAny:
            LnqLexLabAny = qsValue;
            break;
          case QskLexLabCan:
            LnqLexLabCan = qsValue;
            break;
          case QskLexLabAls:
            LnqLexLabAls = qsValue;
            break;
          case QskLexLabSup:
            LnqLexLabSup = qsValue;
            break;
          case QskLexTagAny:
            LnqLexTagAny = qsValue;
            break;
          case QskLexTagSup:
            LnqLexTagSup = qsValue;
            break;
          case QskLexOText:
            LnqLexOText = qsValue;
            break;
          // 4-character keys
          case QskServiceType:
            ServiceTypeReqst = qsValue;
            break;
          case QskServiceTag:
            ServiceTagReqst = qsValue;
            break;
          case QskEntityType:
            EntityTypeReqst = qsValue;
            break;
          case QskEntityTag:
            EntityTagReqst = qsValue;
            break;
          case QskEntityName:
            EntityNameReqst = qsValue;
            break;
          case QskEntityNature:
            EntityNatureReqst = qsValue;
            break;
          // 3-character keys
          case QskUserKey:
            ClientUserKey = qsValue;
            break;
          case QskAgentKey:
            ClientAgentKey = qsValue;
            break;
          case QskSessionKey:
            ClientSessionKey = qsValue;
            break;
          case QskRecordAccess:
            RecordAccessReqst = qsValue;
            break;
          case QskUserName:
            CiaamUserName = qsValue;
            break;
          case QskPassWord:
            CiaamPassWord = qsValue;
            break;
          // 2-character keys
          case QskListCount:
            ListCountReqst = PdpQueryString.ParsePosInt(qsValue, ListCountMinDeflt);
            break;
          case QskMessageFormat:
            MessageFormatReqst = qsValue;
            break;
          case QskResrepFormat:
            ResrepFormatReqst = qsValue;
            break;
          case QskArchiveFormat:
            ArchiveFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case QskCheckFormat:
            CheckFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case QskEchoFormat:
            EchoFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case QskQueryFormat:
            QueryFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case QskVerboseFormat:
            VerboseFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          default:
            break;
        }
      }
    }
  }

} // end class

// end file