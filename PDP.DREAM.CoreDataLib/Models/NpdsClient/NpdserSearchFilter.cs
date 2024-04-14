// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqSearchFilter = ESS;
  public string? SearchFilterReqst
  {
    set {
      reqSearchFilter = value;
      if (!string.IsNullOrEmpty(reqSearchFilter))
      { searchFilter = ValidateSearchFilter(reqSearchFilter); }
    }
    get { return reqSearchFilter; }
  }

  // validated values

  private NpdsSearchFilter searchFilter = NPDSCD.SearchFilterDefault;
  public NpdsSearchFilter SearchFilter
  {
    set { searchFilter = ValidateSearchFilter(value); }
    get {
      if (searchFilter == null)
      { searchFilter = NPDSCD.SearchFilterDefault; }
      return searchFilter;
    }
  }

  // validators

  private NpdsSearchFilter ValidateSearchFilter(string recName)
  {
    var recItem = NPDSCD.ParseSearchFilter(recName);
    return ValidateSearchFilter(recItem);
  }
  private NpdsSearchFilter ValidateSearchFilter(NpdsSearchFilter recItem)
  {
    // validates search filter and resets default for ResrepFormat
    switch (recItem.EName)
    {
      case DdeSearchFilter.Diristry:
        ResrepFormat = NPDSCD.ResrepFormatNexus;
        DiristryTag = ServiceTagReqst;
        RegistryTag = ESS;
        DirectoryTag = ESS;
        RegistrarTag = ESS;
        break;
      case DdeSearchFilter.Registry:
        ResrepFormat = NPDSCD.ResrepFormatPORTAL;
        DiristryTag = ESS;
        RegistryTag = ServiceTagReqst;
        DirectoryTag = ESS;
        RegistrarTag = ESS;
        break;
      case DdeSearchFilter.Directory:
        ResrepFormat = NPDSCD.ResrepFormatDOORS;
        DiristryTag = ESS;
        RegistryTag = ESS;
        DirectoryTag = ServiceTagReqst;
        RegistrarTag = ESS;
        break;
      case DdeSearchFilter.Registrar:
        ResrepFormat = NPDSCD.ResrepFormatScribe;
        DiristryTagReqst = ESS;
        RegistryTagReqst = ESS;
        DirectoryTagReqst = ESS;
        RegistrarTag = ServiceTagReqst;
        break;
      case DdeSearchFilter.Services:
        ResrepFormat = NPDSCD.ResrepFormatCore;
        DiristryTagReqst = ESS;
        RegistryTagReqst = ESS;
        DirectoryTagReqst = ESS;
        RegistrarTag = ServiceTagReqst;
        break;
      case DdeSearchFilter.AllTags:
        ResrepFormat = NPDSCD.ResrepFormatCore;
        DiristryTag = DiristryTagReqst;
        RegistryTag = RegistryTagReqst;
        DirectoryTag = DirectoryTagReqst;
        RegistrarTag = RegistrarTagReqst;
        break;
      case DdeSearchFilter.AllGuids:
        ResrepFormat = NPDSCD.ResrepFormatCore;
        DiristryGuidResetFromReqstAndDeflt();
        RegistryGuidResetFromReqstAndDeflt();
        DirectoryGuidResetFromReqstAndDeflt();
        RegistrarGuidResetFromReqstAndDeflt();
        break;
      case DdeSearchFilter.None:
        ResrepFormat = NPDSCD.ResrepFormatNone;
        DiristryTag = ESS;
        RegistryTag = ESS;
        DirectoryTag = ESS;
        RegistrarTag = ESS;
        break;
      default:
        throw new Exception($"method {nameof(ValidateSearchFilter)} case not implemented for SearchFilter value {recItem}");
    }
    return recItem;
  }

} // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // Network SearchFilter determines extent of distributed network nodes
  //    searched throughout NPDS;
  // Consider mapping Local/Regional/Global searches for forwarding/caching server
  //    to Answer/Related/Referred sections of NPDS response message;
  // however recall that an authoritative server should return only an
  //    Answer response for Local scope query directed at it without
  //    Related response for Regional scope query or
  //    Referred response for Global scope query
  // NpdsSearchFilter determines filter constraints on search
  //   at a given node where search through records filtered
  //   by constraints on the infoset guidrefs  and/or tags

  public static class DdeSearchFilter
  {
    public const string None = "None"; // 0
    public const string Diristry = "Diristry"; // 1
    public const string Registry = "Registry"; // 2
    public const string Directory = "Directory"; // 3
    public const string Registrar = "Registrar"; // 4
    public const string AllTags = "AllTags"; // 10
    public const string AllGuids = "AllGuids"; // 11
    public const string Organizations = "Organizations"; // 40 (aka "Constituents")
    public const string Persons = "Persons"; // 50 (aka "Constituents")
    public const string Services = "Services"; // 100 (aka "Components")
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsSearchFilter SearchFilterNone =
     new NpdsSearchFilter(0, DdeSearchFilter.None, DdeSearchFilter.None);
  public NpdsSearchFilter SearchFilterDiristry =
     new NpdsSearchFilter(1, DdeSearchFilter.Diristry, DdeSearchFilter.Diristry);
  public NpdsSearchFilter SearchFilterRegistry =
     new NpdsSearchFilter(2, DdeSearchFilter.Registry, DdeSearchFilter.Registry);
  public NpdsSearchFilter SearchFilterDirectory =
      new NpdsSearchFilter(3, DdeSearchFilter.Directory, DdeSearchFilter.Directory);
  public NpdsSearchFilter SearchFilterRegistrar =
      new NpdsSearchFilter(4, DdeSearchFilter.Registrar, DdeSearchFilter.Registrar);
  public NpdsSearchFilter SearchFilterAllTags =
      new NpdsSearchFilter(10, DdeSearchFilter.AllTags, DdeSearchFilter.AllTags);
  public NpdsSearchFilter SearchFilterAllGuids =
      new NpdsSearchFilter(11, DdeSearchFilter.AllGuids, DdeSearchFilter.AllGuids);
  public NpdsSearchFilter SearchFilterOrganizations =
      new NpdsSearchFilter(40, DdeSearchFilter.Organizations, DdeSearchFilter.Organizations);
  public NpdsSearchFilter SearchFilterPersons =
      new NpdsSearchFilter(50, DdeSearchFilter.Persons, DdeSearchFilter.Persons);
  public NpdsSearchFilter SearchFilterServices =
      new NpdsSearchFilter(100, DdeSearchFilter.Services, DdeSearchFilter.Services);

  protected void InitSearchFilterEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsSearchFilter>
    {
      SearchFilterNone,
      SearchFilterDiristry,
      SearchFilterRegistry,
      SearchFilterDirectory,
      SearchFilterRegistrar,
      SearchFilterAllTags,
      SearchFilterAllGuids,
      SearchFilterOrganizations,
      SearchFilterPersons,
      SearchFilterServices,
    };
    SearchFilterList = recItms;
    SearchFilterNewItem = recItms[0];
    SearchFilterDefault = recItms[1];
  }

  //public void ResetSearchFilterDefault(string enam)
  //{
  //  try
  //  {
  //    SearchFilterDefault = SearchFilterList?
  //       .Where(itm => itm.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitSearchFilters();
  //  }
  //}

  public List<string>? SearchFilterNames
  {
    get {
      if (SearchFilterList == null) { InitSearchFilterEnums(); }
      return SearchFilterList.Select(itm => itm.EName).ToList<string>();
    }
  }
  public NpdsSearchFilter ParseSearchFilter(string recNam)
  {
    NpdsSearchFilter? recItm = null;
    if (SearchFilterList == null) { InitSearchFilterEnums(); }
    try
    {
      recItm = SearchFilterList?
         .Where(r => r.EName.ToLower() == recNam.ToLower())
         .FirstOrDefault();
    }
    catch { }
    if (recItm == null) { recItm = SearchFilterDefault; }
    return recItm;
  }

  public List<NpdsSearchFilter>? SearchFilterList { get; set; } = null;
  public NpdsSearchFilter? SearchFilterNewItem { get; set; } = null;
  public NpdsSearchFilter? SearchFilterDefault { get; set; } = null;

} // end class

// end file
