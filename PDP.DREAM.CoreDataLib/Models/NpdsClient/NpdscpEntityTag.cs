// PrcEntityTag.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string? reqEntityTag = string.Empty;
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

  private string? entityTag = string.Empty;
  public string? EntityTag
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RegexPrincipalTag))
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

} // end class

// end file