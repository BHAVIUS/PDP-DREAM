// QurcParseNpdsQuery.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebiUserRestContext
{
  // TODO: create analogous method for parsing route parameter values into strongly typed values
  public void ParseNpdsQueryString(IQueryCollection? qryStrCol)
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
            ClientUserKey = qsValue;
            break;
          case PdpAppConst.QskAgentKey:
            ClientAgentKey = qsValue;
            break;
          case PdpAppConst.QskSessionKey:
            ClientSessionKey = qsValue;
            break;
          case PdpAppConst.QskRecordAccess:
            RecordAccessReqst = qsValue;
            break;
          case PdpAppConst.QskUserName:
            ClientUserName = qsValue;
            break;
          case PdpAppConst.QskPassWord:
            ClientPassWord = qsValue;
            break;
          // 2-character keys
          case PdpAppConst.QskListCount:
            ListCountReqst = PdpQueryString.ParsePosInt(qsValue, PdpAppConst.ListCountMinDeflt);
            break;
          case PdpAppConst.QskMessageFormat:
            MessageFormatReqst = qsValue;
            break;
          case PdpAppConst.QskResrepFormat:
            ResrepFormatReqst = qsValue;
            break;
          case PdpAppConst.QskArchiveFormat:
            ArchiveFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskCheckFormat:
            CheckFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskEchoFormat:
            EchoFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskQueryFormat:
            QueryFormat = PdpQueryString.ParseFlag(qsValue);
            break;
          case PdpAppConst.QskVerboseFormat:
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