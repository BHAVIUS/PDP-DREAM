// QebUserRestContext
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

using static PDP.DREAM.CoreDataLib.Models.PdpAppStatus;
using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Models;

// ATTN: recall different patterns when property is enum vs when property is string
public partial class QebUserRestContext
{
  // default values

  public NpdsRecordAccess RecordAccessDeflt { get; } = NPDSSD.NpdsDefaultRecordAccess;

  // requested values

  public string RecordAccessReqst
  {
    set {
      reqRecordAccess = value;
      if (string.IsNullOrWhiteSpace(reqRecordAccess))
      {
        reqRecordAccess = RecordAccessDeflt.ToString();
      }
      if (!string.IsNullOrEmpty(reqRecordAccess))
      {
        RecordAccess = ValidateRecordAccess(reqRecordAccess);
      }
    }
    get { return reqRecordAccess; }
  }
  private string reqRecordAccess = string.Empty;

  // validated values

  public NpdsRecordAccess RecordAccess
  {
    set { recordAccess = ValidateRecordAccess(value); }
    get {
      if (recordAccess == 0)
      { RecordAccess = RecordAccessDeflt; }
      return recordAccess;
    }
  }
  private NpdsRecordAccess recordAccess = default;

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