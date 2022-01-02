// PrcSearchScope.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.CoreDataLib.Models
{
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum SearchScope { None = 1, Local, Regional, Global }

    // default values

    public NpdsConst.SearchScope SearchScopeDeflt
    { get { return defSearchScope; } }
    private NpdsConst.SearchScope defSearchScope = NpdsServiceDefaults.Values.NpdsDefaultSearchScope;

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

    public NpdsConst.SearchScope SearchScope
    {
      set { searchScope = ValidateSearchScope(value); }
      get
      {
        if (searchScope == 0)
        { SearchScope = SearchScopeDeflt; }
        return searchScope;
      }
    }
    private NpdsConst.SearchScope searchScope = default;

    // validators

    private NpdsConst.SearchScope ValidateSearchScope(string strValue)
    {
      NpdsConst.SearchScope enmValue = PdpEnum<NpdsConst.SearchScope>.Parse(strValue, NpdsConst.SearchScope.None);
      return ValidateSearchScope(enmValue);
    }
    private NpdsConst.SearchScope ValidateSearchScope(NpdsConst.SearchScope value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
