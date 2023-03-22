// PrcEntityName.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string? reqEntityName = string.Empty;
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

  private string? entityName = string.Empty;
  public string? EntityName
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RegexSupportingTag))
      {
        entityName = value;
      }
      else
      {
        entityName = string.Empty;
      }

    }
    get { return entityName; }
  }

} // end class

// end file