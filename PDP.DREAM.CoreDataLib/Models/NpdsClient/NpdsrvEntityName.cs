// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqEntityName = ESS;
  public string? EntityNameReqst
  {
    set
    {
      reqEntityName = value;
      EntityName = reqEntityName;
    }
    get { return reqEntityName; }
  }

  // validated values

  private string? entityName = ESS;
  public string? EntityName
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RgxsSupportingTag))
      {
        entityName = value;
      }
      else
      {
        entityName = ESS;
      }

    }
    get { return entityName; }
  }

} // end class

// end file