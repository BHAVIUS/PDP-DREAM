namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // TODO: currently QueryFormat is a bool rather than an enum
    //  variable name QueryFormat is OK if plan to enhance to enum
    //  but if remains just bool true/false then change variable name
    //  currently used only to distinguish exact case-sensitive match from partial case-insensitive match on query
    // alternative name might just be QueryType

    // default values

    public bool QueryFormatDeflt
    { get { return defQuery; } }
    private bool defQuery = NpdsServiceDefaults.GetValues.NpdsDefaultQueryFormat;

    // requested values

    public bool QueryFormatReqst
    {
      set { reqQuery = value; }
      get { return reqQuery; }
    }
    private bool reqQuery = false;

    // validated values

    public bool QueryFormat
    {
      get { return QueryFormatReqst; }
    }

  }

}
