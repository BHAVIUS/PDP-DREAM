using System.Text.RegularExpressions;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // default values

    // requested values

    public string EntityTagReqst
    {
      set
      {
        reqEntityTag = value;
        EntityTag = reqEntityTag;
      }
      get { return reqEntityTag; }
    }
    private string reqEntityTag = string.Empty;

    // validated values

    public string EntityTag
    {
      set
      {
        if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, NpdsConst.RegexPrincipalTag))
        {
          entityTag = value;
        }
        else
        {
          entityTag = string.Empty;
        }

      }
      get { return entityTag; }
    }
    private string entityTag = string.Empty;

  }

}
