// PrcEntityNature.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClient
{
  // requested values

  private string? reqEntityNature = string.Empty;
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

  private string? entityNature = string.Empty;
  public string? EntityNature
  {
    set
    {
      if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, PdpAppConst.RegexSafeGeneric))
      {
        entityNature = value;
      }
      else
      {
        entityNature = string.Empty;
      }

    }
    get { return entityNature; }
  }

} // end class

// end file