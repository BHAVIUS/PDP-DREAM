// QebUserRestContext
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class NpdsClient
{
  // requested values

  private string reqRecordAccess = string.Empty;
  public string RecordAccessReqst
  {
    set {
      reqRecordAccess = value;
      if (string.IsNullOrWhiteSpace(reqRecordAccess))
      {
        reqRecordAccess = NPDSSD.RecordAccessDefault.ToString();
      }
      if (!string.IsNullOrEmpty(reqRecordAccess))
      {
        RecordAccess = ValidateRecordAccess(reqRecordAccess);
      }
    }
    get { return reqRecordAccess; }
  }

  // validated values

  private NpdsRecordAccess recordAccess = default;
  public NpdsRecordAccess RecordAccess
  {
    set { recordAccess = ValidateRecordAccess(value); }
    get {
      if (recordAccess == 0)
      { RecordAccess = NPDSSD.RecordAccessDefault; }
      return recordAccess;
    }
  }

  // validators

  private NpdsRecordAccess ValidateRecordAccess(string strValue, NpdsRecordAccess defValue = NpdsRecordAccess.AnonUser)
  {
    NpdsRecordAccess enmValue = PdpEnum<NpdsRecordAccess>.ParseString(strValue, defValue);
    recordAccess = ValidateRecordAccess(enmValue);
    return recordAccess;
  }
  private NpdsRecordAccess ValidateRecordAccess(NpdsRecordAccess value)
  {
    switch (value)
    {
      case NpdsRecordAccess.Admin: // authenticated NPDS admin
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = true;
        break;
      case NpdsRecordAccess.Editor: // authenticated NPDS editor
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = true;
        AdminModeClientRequired = false;
        break;
      case NpdsRecordAccess.Author: // authenticated NPDS author
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = true;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        break;
      case NpdsRecordAccess.Agent:  // authenticated NPDS agent
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = true;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        break;
      case NpdsRecordAccess.AuthUser: // authenticated NPDS user 
        AuthenticatedClientRequired = true;
        UserModeClientRequired = true;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        break;
      case NpdsRecordAccess.AnonUser: // anonymous NPDS client
      default:
        AuthenticatedClientRequired = false;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        // assure AnonUser for default cases including .None
        // TODO: address cases for Reviewer and Publisher 
        value = NpdsRecordAccess.AnonUser;
        break;
    }
    return value;
  }

} // end class

// end file