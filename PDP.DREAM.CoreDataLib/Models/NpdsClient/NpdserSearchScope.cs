// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqSearchScope = ESS;
  public string? SearchScopeReqst
  {
    set {
      reqSearchScope = value;
      if (!string.IsNullOrEmpty(reqSearchScope))
      { searchScope = ValidateSearchScope(reqSearchScope); }
    }
    get { return reqSearchScope; }
  }

  // validated values

  private NpdsSearchScope? searchScope = NPDSCD.SearchScopeDefault;
  public NpdsSearchScope? SearchScope
  {
    set { searchScope = ValidateSearchScope(value); }
    get {
      if (searchScope == null)
      { searchScope = NPDSCD.SearchScopeDefault; }
      return searchScope;
    }
  }

  // validators

  private NpdsSearchScope ValidateSearchScope(string recName)
  {
    var recItem = NPDSCD.ParseSearchScope(recName);
    return ValidateSearchScope(recItem);
  }
  private NpdsSearchScope ValidateSearchScope(NpdsSearchScope recItem)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public partial class NpdsClientDefaults
{
  // Network SearchScope determines extent of distributed network nodes
  //    searched throughout NPDS;
  // Consider mapping Local/Regional/Global searches for forwarding/caching server
  //    to Answer/Related/Referred sections of NPDS response message;
  // however recall that an authoritative server should return only an
  //    Answer response for Local scope query directed at it without
  //    Related response for Regional scope query or
  //    Referred response for Global scope query
  // ie, Scope for Search over all network nodes

  public static class DdeSearchScope
  {
    public const string None = "None"; // 0
    public const string Local = "Local"; // 1
    public const string Regional = "Regional"; // 2
    public const string Global = "Global"; // 3
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsSearchScope SearchScopeNone =
    new NpdsSearchScope(0, DdeSearchScope.None, "Unspecified SearchScope");
  public NpdsSearchScope SearchScopeLocal =
    new NpdsSearchScope(1, DdeSearchScope.Local, "Local SearchScope");
  public NpdsSearchScope SearchScopeRegional =
    new NpdsSearchScope(2, DdeSearchScope.Regional, "Regional SearchScope");
  public NpdsSearchScope SearchScopeGlobal =
    new NpdsSearchScope(3, DdeSearchScope.Global, "Global SearchScope");

  // ATTN: for Net8Gangkhar, devtest protected only on InitSearchScopes
  protected void InitSearchScopeEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsSearchScope>
    {
     SearchScopeNone,
     SearchScopeLocal,
     SearchScopeRegional,
     SearchScopeGlobal,
    };
    SearchScopeList = recItms;
    SearchScopeNewItem = recItms[0];
    SearchScopeDefault = recItms[1];
  }

  //public void ResetSearchScopeDefault(string enam)
  //{
  //  try
  //  {
  //    SearchScopeDefault = SearchScopeList?
  //       .Where(itm => itm.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitSearchScopes();
  //  }
  //}

  public List<string>? SearchScopeNames
  {
    get {
      if (SearchScopeList == null) { InitSearchScopeEnums(); }
      return SearchScopeList.Select(itm => itm.EName).ToList<string>();
    }
  }
  public NpdsSearchScope ParseSearchScope(string recNam)
  {
    NpdsSearchScope? recItm = null;
    if (SearchScopeList == null) { InitSearchScopeEnums(); }
    try
    {
      recItm = SearchScopeList?
         .Where(r => r.EName.ToLower() == recNam.ToLower())
         .FirstOrDefault();
    }
    catch { }
    if (recItm == null) { recItm = SearchScopeDefault; }
    return recItm;
  }

  public List<NpdsSearchScope>? SearchScopeList { get; set; } = null;
  public NpdsSearchScope? SearchScopeNewItem { get; set; } = null;
  public NpdsSearchScope? SearchScopeDefault { get; set; } = null;

} // end class

// end file
