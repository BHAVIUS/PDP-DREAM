using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // in file NpdsConstants.cs
    // public enum SearchScope { None = 1, Local, Regional, Global }

    // default values

    public NpdsConst.SearchScope SearchScopeDeflt
    { get { return defSearchScope; } }
    private NpdsConst.SearchScope defSearchScope = NpdsServiceDefaults.GetValues.NpdsDefaultSearchScope;

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
