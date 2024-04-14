// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqEntityTag = ESS;
  public string? EntityTagReqst
  {
    set
    {
      reqEntityTag = value;
      EntityTag = reqEntityTag;
    }
    get { return reqEntityTag; }
  }

  // validated values

  private string? entityTag = ESS;
  public string? EntityTag
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RgxsPrincipalTag))
      {
        entityTag = value;
      }
      else
      {
        entityTag = ESS;
      }

    }
    get { return entityTag; }
  }

} // end class

// end file