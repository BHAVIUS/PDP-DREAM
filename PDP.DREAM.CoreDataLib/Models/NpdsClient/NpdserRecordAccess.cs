// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqRecordAccess = ESS;
  public string? RecordAccessReqst
  {
    set {
      reqRecordAccess = value;
      if (!string.IsNullOrEmpty(reqRecordAccess))
      { recordAccess = ValidateRecordAccess(reqRecordAccess); }
    }
    get { return reqRecordAccess; }
  }

  // validated value (non-nullable typed)

  private NpdsRecordAccess recordAccess = NPDSCD.RecordAccessDefault;
  public NpdsRecordAccess RecordAccess
  {
    set { recordAccess = ValidateRecordAccess(value); }
    get {
      if (recordAccess == null)
      { recordAccess = NPDSCD.RecordAccessDefault; }
      return recordAccess;
    }
  }

  // validators

  private NpdsRecordAccess ValidateRecordAccess(string strName)
  {
    NpdsRecordAccess recordAccess = NPDSCD.ParseRecordAccess(strName);
    return recordAccess;
  }
  private NpdsRecordAccess ValidateRecordAccess(NpdsRecordAccess recItem)
  {
    var strName = recItem.EName;
    switch (strName)
    {
      case DdeRecordAccess.Admin: // authenticated NPDS admin
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = true;
        break;
      case DdeRecordAccess.Editor: // authenticated NPDS editor
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = true;
        AdminModeClientRequired = false;
        break;
      case DdeRecordAccess.Author: // authenticated NPDS author
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = true;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        break;
      case DdeRecordAccess.Agent:  // authenticated NPDS agent
        AuthenticatedClientRequired = true;
        UserModeClientRequired = false;
        AgentModeClientRequired = true;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        break;
      case DdeRecordAccess.User: // authenticated NPDS user 
        AuthenticatedClientRequired = true;
        UserModeClientRequired = true;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        break;
      case DdeRecordAccess.Anon: // anonymous NPDS client
      default:
        AuthenticatedClientRequired = false;
        UserModeClientRequired = false;
        AgentModeClientRequired = false;
        AuthorModeClientRequired = false;
        EditorModeClientRequired = false;
        AdminModeClientRequired = false;
        // assure Anon for default cases including .None
        // TODO: address cases for Reviewer and Publisher 
        recItem = NPDSCD.RecordAccessAnon;
        break;
    }
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // RecordAccess determines role-based read/write access privileges to individual records in database
  // Anon is anonymous; User/Agent/Author/Editor/Admin are authenticated
  // User is authenticated but not consented, while Agent is both authenticated and consented
  // Agent can see all transferrable records which may or may not include his own --- TODO: create a transferrable flag
  // Author can only see own records
  // Editor can see all records in registry / directory / diristry if declared Editor for that registry / directory / diristry
  // Admin can see all records in registrar

  public static class DdeRecordAccess
  {
    public const string None = "None"; // 0
    public const string Anon = "Anon"; // 1
    public const string User = "User"; // 2
    public const string Admin = "Admin"; // 3
    public const string Agent = "Agent"; // 11
    public const string Author = "Author"; // 12
    public const string Reviewer = "Reviewer"; // 13
    public const string Editor = "Editor"; // 14
    public const string Publisher = "Publisher"; // 15
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsRecordAccess RecordAccessNone =
     new NpdsRecordAccess(0, DdeRecordAccess.None, DdeRecordAccess.None);
  public NpdsRecordAccess RecordAccessAnon =
     new NpdsRecordAccess(1, DdeRecordAccess.Anon, DdeRecordAccess.Anon);
  public NpdsRecordAccess RecordAccessUser =
     new NpdsRecordAccess(2, DdeRecordAccess.User, DdeRecordAccess.User);
  public NpdsRecordAccess RecordAccessAdmin =
      new NpdsRecordAccess(3, DdeRecordAccess.Admin, DdeRecordAccess.Admin);
  public NpdsRecordAccess RecordAccessAgent =
      new NpdsRecordAccess(11, DdeRecordAccess.Agent, DdeRecordAccess.Agent);
  public NpdsRecordAccess RecordAccessAuthor =
      new NpdsRecordAccess(12, DdeRecordAccess.Author, DdeRecordAccess.Author);
  public NpdsRecordAccess RecordAccessReviewer =
      new NpdsRecordAccess(13, DdeRecordAccess.Reviewer, DdeRecordAccess.Reviewer);
  public NpdsRecordAccess RecordAccessEditor =
      new NpdsRecordAccess(14, DdeRecordAccess.Editor, DdeRecordAccess.Editor);
  public NpdsRecordAccess RecordAccessPublisher =
      new NpdsRecordAccess(15, DdeRecordAccess.Publisher, DdeRecordAccess.Publisher);

  // TODO: migrate from static enums to dynamic db enumerators
  // to dynamic db records initialized on app startup

  protected void InitRecordAccessEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsRecordAccess>
    {
      RecordAccessNone,
      RecordAccessAnon,
      RecordAccessUser,
      RecordAccessAdmin,
      RecordAccessAgent,
      RecordAccessAuthor,
      RecordAccessReviewer,
      RecordAccessEditor,
      RecordAccessPublisher,
    };
    RecordAccessList = recItms;
    RecordAccessNewItem = recItms[0];
    RecordAccessDefault = recItms[1];
  }

  //public void ResetRecordAccessDefault(string enam)
  //{
  //  try
  //  {
  //    RecordAccessDefault = RecordAccessList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch { }
  //  if (RecordAccessDefault == null) { InitRecordAccessEnums(); }
  //}

  // nullable lists
  public List<NpdsRecordAccess>? RecordAccessList { get; set; } = null;
  public List<string>? RecordAccessNames
  {
    get {
      if (RecordAccessList == null) { InitRecordAccessEnums(); }
      return RecordAccessList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsRecordAccess ParseRecordAccess(byte ecod)
  {
    NpdsRecordAccess? recItm = null;
    if (RecordAccessList == null) { InitRecordAccessEnums(); }
    try
    {
      recItm = RecordAccessList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = RecordAccessDefault; }
    return recItm;
  }
  public NpdsRecordAccess ParseRecordAccess(string recNam)
  {
    NpdsRecordAccess? recItm = null;
    if (RecordAccessList == null) { InitRecordAccessEnums(); }
    try
    {
      recItm = RecordAccessList?
         .Where(r => r.EName.ToLower() == recNam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = RecordAccessDefault; }
    return recItm;
  }
  public NpdsRecordAccess RecordAccessDefault { get; set; }
  public NpdsRecordAccess RecordAccessNewItem { get; set; }

} // end class

// end file