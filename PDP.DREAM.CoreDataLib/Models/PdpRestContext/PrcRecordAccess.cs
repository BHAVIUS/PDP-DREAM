// PrcRecordAccess.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
{
  // ATTN: recall different patterns when property is enum vs when property is string
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum RecordAccess { Client = 1, User, Agent, Author, Editor, Admin };

    // default values

    public NpdsConst.RecordAccess RecordAccessDeflt { get; } = NpdsServiceDefaults.Values.NpdsDefaultRecordAccess;

    // requested values

    public string RecordAccessReqst
    {
      set
      {
        reqRecordAccess = value;
        if (!string.IsNullOrEmpty(reqRecordAccess))
        {
          RecordAccess = ValidateRecordAccess(reqRecordAccess);
        }
      }
      get { return reqRecordAccess; }
    }
    private string reqRecordAccess = string.Empty;

    // validated values

    public NpdsConst.RecordAccess RecordAccess
    {
      set { recordAccess = ValidateRecordAccess(value); }
      get
      {
        if (recordAccess == 0)
        { RecordAccess = RecordAccessDeflt; }
        return recordAccess;
      }
    }
    private NpdsConst.RecordAccess recordAccess = default;

    // validators

    private NpdsConst.RecordAccess ValidateRecordAccess(string strValue)
    {
      NpdsConst.RecordAccess enmValue = PdpEnum<NpdsConst.RecordAccess>.Parse(strValue, NpdsConst.RecordAccess.Client);
      return ValidateRecordAccess(enmValue);
    }
    private NpdsConst.RecordAccess ValidateRecordAccess(NpdsConst.RecordAccess value)
    {
      switch (value)
      {
        case NpdsConst.RecordAccess.Admin:
          ClientInUserModeIsRequired = false;
          ClientInAgentModeIsRequired = false;
          ClientInAuthorModeIsRequired = false;
          ClientInEditorModeIsRequired = false;
          ClientInAdminModeIsRequired = true;
          break;
        case NpdsConst.RecordAccess.Editor:
          ClientInUserModeIsRequired = false;
          ClientInAgentModeIsRequired = false;
          ClientInAuthorModeIsRequired = false;
          ClientInEditorModeIsRequired = true;
          ClientInAdminModeIsRequired = false;
          break;
        case NpdsConst.RecordAccess.Author:
          ClientInUserModeIsRequired = false;
          ClientInAgentModeIsRequired = false;
          ClientInAuthorModeIsRequired = true;
          ClientInEditorModeIsRequired = false;
          ClientInAdminModeIsRequired = false;
          break;
        case NpdsConst.RecordAccess.Agent:  // authenticated NPDS agent
          ClientInUserModeIsRequired = false;
          ClientInAgentModeIsRequired = true;
          ClientInAuthorModeIsRequired = false;
          ClientInEditorModeIsRequired = false;
          ClientInAdminModeIsRequired = false;
          break;
        case NpdsConst.RecordAccess.User: // authenticated NPDS user 
          ClientInUserModeIsRequired = true;
          ClientInAgentModeIsRequired = false;
          ClientInAuthorModeIsRequired = false;
          ClientInEditorModeIsRequired = false;
          ClientInAdminModeIsRequired = false;
          break;
        case NpdsConst.RecordAccess.Client: // anonymous NPDS client
        default:
          ClientInUserModeIsRequired = false;
          ClientInAgentModeIsRequired = false;
          ClientInAuthorModeIsRequired = false;
          ClientInEditorModeIsRequired = false;
          ClientInAdminModeIsRequired = false;
          break;
      }
      return value;
    }

  }

}
