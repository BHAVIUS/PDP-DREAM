// PrcSearchScope.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string reqSearchScope = string.Empty;
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

  // validated values

  private NpdsSearchScope searchScope = default;
  public NpdsSearchScope SearchScope
  {
    set { searchScope = ValidateSearchScope(value); }
    get
    {
      if (searchScope == 0)
      { SearchScope = NPDSSD.SearchScopeDefault; }
      return searchScope;
    }
  }

  // validators

  private NpdsSearchScope ValidateSearchScope(string strValue)
  {
    NpdsSearchScope enmValue = PdpEnum<NpdsSearchScope>.ParseString(strValue, NPDSSD.SearchScopeDefault);
    return ValidateSearchScope(enmValue);
  }
  private NpdsSearchScope ValidateSearchScope(NpdsSearchScope value)
  {
    // TODO: finish coding this virtual implementation with AI rules
    return value;
  }

}

