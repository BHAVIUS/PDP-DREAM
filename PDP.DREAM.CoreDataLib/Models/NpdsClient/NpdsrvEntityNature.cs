// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqEntityNature = ESS;
  public string? EntityNatureReqst
  {
    set
    {
      reqEntityNature = value;
      EntityNature = reqEntityNature;
    }
    get { return reqEntityNature; }
  }

  // validated values

  private string? entityNature = ESS;
  public string? EntityNature
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RgxsSafeGeneric))
      {
        entityNature = value;
      }
      else
      {
        entityNature = ESS;
      }

    }
    get { return entityNature; }
  }

} // end class

// end file