// PrcSearchScope.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsParameters
{
  // in file NpdsConstants.cs
  // public enum SearchScope { None = 1, Local, Regional, Global }

  // default values

  public PdpAppConst.NpdsSearchScope SearchScopeDeflt
  { get { return defSearchScope; } }
  private PdpAppConst.NpdsSearchScope defSearchScope = PdpAppStatus.NPDSSD.NpdsDefaultSearchScope;

  // requested values

  public string SearchScopeReqst
  {
    set
    {
      reqSearchScope = value;
      if (!string.IsNullOrEmpty(reqSearchScope))
      {
        SearchScope = ValidateSearchScope(reqSearchScope);
      }
    }
    get { return reqSearchScope; }
  }
  private string reqSearchScope = string.Empty;

  // validated values

  public PdpAppConst.NpdsSearchScope SearchScope
  {
    set { searchScope = ValidateSearchScope(value); }
    get
    {
      if (searchScope == 0)
      { SearchScope = SearchScopeDeflt; }
      return searchScope;
    }
  }
  private PdpAppConst.NpdsSearchScope searchScope = default;

  // validators

  private PdpAppConst.NpdsSearchScope ValidateSearchScope(string strValue)
  {
    PdpAppConst.NpdsSearchScope enmValue = PdpEnum<PdpAppConst.NpdsSearchScope>.ParseString(strValue, PdpAppConst.NpdsSearchScope.None);
    return ValidateSearchScope(enmValue);
  }
  private PdpAppConst.NpdsSearchScope ValidateSearchScope(PdpAppConst.NpdsSearchScope value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

