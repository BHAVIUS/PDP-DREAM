// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqEntityVersion = ESS;
  public string? EntityVersionReqst
  {
    set
    {
      reqEntityVersion = value;
      // TODO: consider alternative regex check on version such as alphanumeric only or with . and -
      if (!string.IsNullOrEmpty(reqEntityVersion) && Regex.IsMatch(reqEntityVersion, PdpAppConst.RgxsSafeGeneric))
      {
        entityVersion = reqEntityVersion;
      }
      else
      {
        entityVersion = ESS;
      }
    }
    get { return reqEntityVersion; }
  }

  // validated values

  private string? entityVersion = ESS;
  public string? EntityVersion
  {
    set { entityVersion = value; }
    get { return entityVersion; }
  }

} // end class

// end file