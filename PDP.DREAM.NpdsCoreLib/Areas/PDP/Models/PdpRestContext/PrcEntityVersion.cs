using System.Text.RegularExpressions;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // default values

    // requested values

    public string EntityVersionReqst
    {
      set
      {
        reqEntityVersion = value;
        // TODO: consider alternative regex check on version such as alphanumeric only or with . and -
        if (!string.IsNullOrEmpty(reqEntityVersion) && Regex.IsMatch(reqEntityVersion, NpdsConst.RegexSafeGeneric))
        {
          entityVersion = reqEntityVersion;
        }
        else
        {
          entityVersion = string.Empty;
        }
      }
      get { return reqEntityVersion; }
    }
    private string reqEntityVersion = string.Empty;

    // validated values

    public string EntityVersion
    {
      set { entityVersion = value; }
      get { return entityVersion; }
    }
    private string entityVersion = string.Empty;

  }

}
