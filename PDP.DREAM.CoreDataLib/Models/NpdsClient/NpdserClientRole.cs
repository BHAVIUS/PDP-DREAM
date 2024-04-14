// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqClientRole = ESS;
  public string? ClientRoleReqst
  {
    set {
      reqClientRole = value;
      if (!string.IsNullOrEmpty(reqClientRole))
      { clientRole = ValidateClientRole(reqClientRole); }
    }
    get { return reqClientRole; }
  }

  // validated value (non-nullable typed)

  private NpdsClientRole clientRole = NPDSCD.ClientRoleDefault;
  public NpdsClientRole CiaamClientRole
  {
    set { clientRole = ValidateClientRole(value); }
    get {
      if (clientRole == null)
      { clientRole = NPDSCD.ClientRoleDefault; }
      return clientRole;
    }
  }

  // validators

  private NpdsClientRole ValidateClientRole(string recName)
  {
    NpdsClientRole recItem = NPDSCD.ParseClientRole(recName);
    return recItem;
  }
  private NpdsClientRole ValidateClientRole(NpdsClientRole recItem)
  {
    // TODO: code this virtual implementation with AI rules
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // migrated from NpdsClientRoles to DdeClientRole
  public static class DdeClientRole
  {
    public const string None = "None";
    public const string NpdsAnon = "NpdsAnon";
    public const string NpdsUser = "NpdsUser";
    public const string NpdsAgent = "NpdsAgent";
    public const string NpdsAuthor = "NpdsAuthor";
    public const string NpdsReviewer = "NpdsReviewer";
    public const string NpdsEditor = "NpdsEditor";
    public const string NpdsPublisher = "NpdsPublisher";
    public const string NpdsAdmin = "NpdsAdmin";
  }

} // end class

public partial class NpdsClientDefaults
{
  public NpdsClientRole ClientRoleNone =
   new NpdsClientRole(0, DdeClientRole.None, DdeClientRole.None);

  public NpdsClientRole ClientRoleNpdsAnon =
   new NpdsClientRole(20, DdeClientRole.NpdsAnon, DdeClientRole.NpdsAnon);
  public NpdsClientRole ClientRoleNpdsUser =
   new NpdsClientRole(21, DdeClientRole.NpdsUser, DdeClientRole.NpdsUser);
  public NpdsClientRole ClientRoleNpdsAgent =
   new NpdsClientRole(22, DdeClientRole.NpdsAgent, DdeClientRole.NpdsAgent);
  public NpdsClientRole ClientRoleNpdsAuthor =
   new NpdsClientRole(23, DdeClientRole.NpdsAuthor, DdeClientRole.NpdsAuthor);
  public NpdsClientRole ClientRoleNpdsReviewer =
   new NpdsClientRole(24, DdeClientRole.NpdsReviewer, DdeClientRole.NpdsReviewer);
  public NpdsClientRole ClientRoleNpdsEditor =
   new NpdsClientRole(25, DdeClientRole.NpdsEditor, DdeClientRole.NpdsEditor);
  public NpdsClientRole ClientRoleNpdsPublisher =
   new NpdsClientRole(26, DdeClientRole.NpdsPublisher, DdeClientRole.NpdsPublisher);
  public NpdsClientRole ClientRoleNpdsAdmin =
   new NpdsClientRole(27, DdeClientRole.NpdsAdmin, DdeClientRole.NpdsAdmin);

  // TODO: migrate from static enums to dynamic db enumerators
  // to dynamic db records initialized on app startup

  protected void InitClientRoleEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsClientRole>
    {
      ClientRoleNone,
      ClientRoleNpdsAnon,
      ClientRoleNpdsUser,
      ClientRoleNpdsAgent,
      ClientRoleNpdsAuthor,
      ClientRoleNpdsReviewer,
      ClientRoleNpdsEditor,
      ClientRoleNpdsPublisher,
      ClientRoleNpdsAdmin,
    };
    ClientRoleList = recItms;
    ClientRoleNewItem = recItms[0];
    ClientRoleDefault = recItms[1];
  }

  // nullable lists
  public List<NpdsClientRole>? ClientRoleList { get; set; } = null;
  public List<string>? ClientRoleNames
  {
    get {
      if (ClientRoleList == null) { InitClientRoleEnums(); }
      return ClientRoleList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsClientRole ParseClientRole(byte ecod)
  {
    NpdsClientRole? recItm = null;
    if (ClientRoleList == null) { InitClientRoleEnums(); }
    try
    {
      recItm = ClientRoleList
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = ClientRoleDefault; }
    return recItm;
  }
  public NpdsClientRole ParseClientRole(string enam)
  {
    NpdsClientRole? recItm = null;
    if (ClientRoleList == null) { InitClientRoleEnums(); }
    try
    {
      recItm = ClientRoleList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = ClientRoleDefault; }
    return recItm;
  }
  public NpdsClientRole ClientRoleDefault { get; set; }
  public NpdsClientRole ClientRoleNewItem { get; set; }

} // end class

// end file